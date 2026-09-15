using BardeesCms.Web.Areas.Admin.Models;
using BardeesCms.Web.Authorization;
using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Areas.Admin.Controllers;

[Authorize(Policy = Policies.ManageUsers)]
public class UsersController : AdminControllerBase
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly RoleManager<IdentityRole> _roles;
    private readonly IActivityLogger _log;
    public UsersController(UserManager<ApplicationUser> users, RoleManager<IdentityRole> roles, IActivityLogger log)
    { _users = users; _roles = roles; _log = log; }

    public async Task<IActionResult> Index()
    {
        var users = await _users.Users.OrderBy(u => u.Email).ToListAsync();
        var items = new List<UserListItem>(users.Count);
        foreach (var u in users)
        {
            items.Add(new UserListItem
            {
                Id = u.Id, Email = u.Email, FullName = u.FullName, IsActive = u.IsActive,
                CreatedAt = u.CreatedAt, LastLoginAt = u.LastLoginAt,
                Roles = await _users.GetRolesAsync(u)
            });
        }
        return View(items);
    }

    public IActionResult Create()
    {
        PopulateRoles();
        return View(new UserCreateVm { Role = Roles.Editor });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateVm vm)
    {
        if (!Roles.All.Contains(vm.Role)) ModelState.AddModelError(nameof(vm.Role), "Choose a valid role.");
        if (!ModelState.IsValid) { PopulateRoles(); return View(vm); }

        var user = new ApplicationUser
        {
            UserName = vm.Email, Email = vm.Email, FullName = vm.FullName,
            EmailConfirmed = true, IsActive = true, CreatedAt = DateTime.UtcNow
        };
        var result = await _users.CreateAsync(user, vm.Password);
        if (!result.Succeeded)
        {
            foreach (var err in result.Errors) ModelState.AddModelError("", err.Description);
            PopulateRoles();
            return View(vm);
        }
        await _users.AddToRoleAsync(user, vm.Role);
        await _log.LogAsync("Created", nameof(ApplicationUser), user.Id, $"{user.Email} ({vm.Role})");
        Flash($"User “{user.Email}” created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string id)
    {
        var user = await _users.FindByIdAsync(id);
        if (user is null) return NotFound();
        var roles = await _users.GetRolesAsync(user);
        PopulateRoles();
        return View(new UserEditVm
        {
            Id = user.Id, Email = user.Email, FullName = user.FullName,
            IsActive = user.IsActive, Role = roles.FirstOrDefault() ?? ""
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserEditVm vm)
    {
        if (!Roles.All.Contains(vm.Role)) ModelState.AddModelError(nameof(vm.Role), "Choose a valid role.");
        if (!ModelState.IsValid) { PopulateRoles(); return View(vm); }

        var user = await _users.FindByIdAsync(vm.Id);
        if (user is null) return NotFound();

        // Guard: don't let an admin deactivate their own account (would lock themselves out).
        if (user.Id == _users.GetUserId(User) && !vm.IsActive)
        {
            Flash("You cannot deactivate your own account.", "danger");
            PopulateRoles();
            return View(vm);
        }

        // Guard: don't strip the last SuperAdmin of its role.
        var currentRoles = await _users.GetRolesAsync(user);
        if (currentRoles.Contains(Roles.SuperAdmin) && vm.Role != Roles.SuperAdmin && await IsLastSuperAdmin(user.Id))
        {
            Flash("You cannot remove the SuperAdmin role from the last SuperAdmin.", "danger");
            PopulateRoles();
            return View(vm);
        }

        user.FullName = vm.FullName;
        user.IsActive = vm.IsActive;
        await _users.UpdateAsync(user);

        if (!currentRoles.Contains(vm.Role) || currentRoles.Count != 1)
        {
            if (currentRoles.Count > 0) await _users.RemoveFromRolesAsync(user, currentRoles);
            await _users.AddToRoleAsync(user, vm.Role);
        }

        await _log.LogAsync("Updated", nameof(ApplicationUser), user.Id, $"{user.Email} ({vm.Role})");
        Flash($"User “{user.Email}” saved.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _users.FindByIdAsync(id);
        if (user is null) return NotFound();

        // Guard: don't let an admin delete their own account.
        if (user.Id == _users.GetUserId(User))
        {
            Flash("You cannot delete your own account.", "danger");
            return RedirectToAction(nameof(Index));
        }

        if (await _users.IsInRoleAsync(user, Roles.SuperAdmin) && await IsLastSuperAdmin(user.Id))
        {
            Flash("You cannot delete the last SuperAdmin.", "danger");
            return RedirectToAction(nameof(Index));
        }

        var email = user.Email;
        var result = await _users.DeleteAsync(user);
        if (!result.Succeeded)
        {
            Flash("Could not delete user: " + string.Join("; ", result.Errors.Select(e => e.Description)), "danger");
            return RedirectToAction(nameof(Index));
        }
        await _log.LogAsync("Deleted", nameof(ApplicationUser), id, email);
        Flash($"User “{email}” deleted.");
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> IsLastSuperAdmin(string excludeUserId)
    {
        var admins = await _users.GetUsersInRoleAsync(Roles.SuperAdmin);
        return admins.Count(u => u.Id != excludeUserId) == 0;
    }

    private void PopulateRoles() => ViewBag.Roles = Roles.All;
}
