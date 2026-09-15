/* ============================================================================
   BARDEES CMS — admin.js
   Sidebar drawer, flash auto-hide, AJAX toggle + drag-reorder, SweetAlert delete,
   slug auto-fill, image-preview + dropzone helpers. Vanilla JS + SortableJS + SweetAlert2.
   ============================================================================ */
(function () {
  "use strict";

  const $ = (sel, root = document) => root.querySelector(sel);
  const $$ = (sel, root = document) => Array.from(root.querySelectorAll(sel));

  function token() {
    const el = $('input[name="__RequestVerificationToken"]');
    return el ? el.value : "";
  }

  // ---- Mobile drawer -------------------------------------------------------
  const sidebar = $("#adminSidebar"), backdrop = $("#adminBackdrop"), burger = $("#adminBurger");
  function closeDrawer() { sidebar?.classList.remove("open"); backdrop?.classList.remove("show"); }
  burger?.addEventListener("click", () => { sidebar?.classList.toggle("open"); backdrop?.classList.toggle("show"); });
  backdrop?.addEventListener("click", closeDrawer);

  // ---- Flash auto-hide -----------------------------------------------------
  $$("[data-autohide]").forEach(el => setTimeout(() => {
    el.style.transition = "opacity .5s"; el.style.opacity = "0";
    setTimeout(() => el.remove(), 500);
  }, 4000));

  // ---- Toast helper --------------------------------------------------------
  function toast(icon, title) {
    if (window.Swal) {
      Swal.fire({ toast: true, position: "top-end", icon, title, showConfirmButton: false, timer: 2200,
        background: "#251d17", color: "#f0e9dc" });
    }
  }
  window.cmsToast = toast;

  // ---- AJAX active toggle --------------------------------------------------
  document.addEventListener("change", async (e) => {
    const sw = e.target.closest("[data-toggle-url]");
    if (!sw) return;
    try {
      const res = await fetch(sw.dataset.toggleUrl, {
        method: "POST",
        headers: { "RequestVerificationToken": token() },
      });
      if (!res.ok) throw new Error();
      toast("success", "Updated");
    } catch {
      sw.checked = !sw.checked;
      toast("error", "Could not update");
    }
  });

  // ---- SweetAlert delete confirmation --------------------------------------
  document.addEventListener("click", (e) => {
    const btn = e.target.closest("[data-delete-url]");
    if (!btn) return;
    e.preventDefault();
    const name = btn.dataset.deleteName || "this item";
    Swal.fire({
      title: "Delete?",
      html: `You are about to delete <strong>${name}</strong>. This cannot be undone.`,
      icon: "warning", showCancelButton: true, confirmButtonText: "Delete",
      confirmButtonColor: "#d9705f", cancelButtonColor: "#3a2e23",
      background: "#1e1813", color: "#f0e9dc",
    }).then((r) => {
      if (!r.isConfirmed) return;
      const form = document.createElement("form");
      form.method = "post"; form.action = btn.dataset.deleteUrl;
      form.innerHTML = `<input type="hidden" name="__RequestVerificationToken" value="${token()}">`;
      document.body.appendChild(form); form.submit();
    });
  });

  // ---- Drag-and-drop reorder (AJAX) ----------------------------------------
  $$("[data-sortable]").forEach(list => {
    if (!window.Sortable) return;
    Sortable.create(list, {
      handle: ".drag-handle", animation: 160, ghostClass: "dragging",
      onEnd: async () => {
        const ids = $$("[data-id]", list).map(r => r.dataset.id);
        try {
          const res = await fetch(list.dataset.sortable, {
            method: "POST",
            headers: { "Content-Type": "application/json", "RequestVerificationToken": token() },
            body: JSON.stringify(ids),
          });
          if (!res.ok) throw new Error();
          toast("success", "Order saved");
        } catch { toast("error", "Could not save order"); }
      },
    });
  });

  // ---- Slug auto-fill from a title -----------------------------------------
  function slugify(v) {
    return (v || "").toLowerCase().normalize("NFD").replace(/[̀-ͯ]/g, "")
      .replace(/[^a-z0-9\s-]/g, "").replace(/\s+/g, "-").replace(/-+/g, "-").replace(/^-|-$/g, "");
  }
  $$("[data-slug-source]").forEach(src => {
    const target = $(src.dataset.slugSource);
    if (!target) return;
    src.addEventListener("input", () => {
      if (target.dataset.touched === "1") return;
      target.value = slugify(src.value);
    });
    target.addEventListener("input", () => { target.dataset.touched = "1"; });
  });

  // ---- Image preview + loader on file pick (shared across the dashboard) ----
  // Every uploader wired with data-preview="#id" gets the same behaviour:
  // pick a file → spinner shows → decoded image replaces the preview before save.
  function ensurePreviewWrap(img) {
    let wrap = img.parentElement;
    if (!wrap || !wrap.classList.contains("img-preview-wrap")) {
      wrap = document.createElement("span");
      wrap.className = "img-preview-wrap";
      img.parentNode.insertBefore(wrap, img);
      wrap.appendChild(img);
      const spin = document.createElement("span");
      spin.className = "img-preview-spin";
      spin.setAttribute("aria-hidden", "true");
      wrap.appendChild(spin);
    }
    return wrap;
  }
  $$('input[type="file"][data-preview]').forEach(input => {
    const img = $(input.dataset.preview);
    if (!img) return;
    const wrap = ensurePreviewWrap(img);
    input.addEventListener("change", () => {
      const file = input.files && input.files[0];
      if (!file || !/^image\//.test(file.type)) return;
      wrap.classList.add("is-loading");
      wrap.classList.remove("is-ready");
      const url = URL.createObjectURL(file);
      const started = performance.now();
      const probe = new Image();
      const finish = (ok) => {
        wrap.classList.remove("is-loading");
        if (!ok) { URL.revokeObjectURL(url); return; }
        if (img.dataset.objurl) URL.revokeObjectURL(img.dataset.objurl);
        img.src = url; img.dataset.objurl = url;
        img.style.display = "";            // reveal previews that start hidden/empty
        wrap.classList.add("is-ready");
      };
      probe.onload = () => {
        // guarantee the loader is perceivable even for tiny images
        const wait = Math.max(0, 400 - (performance.now() - started));
        setTimeout(() => finish(true), wait);
      };
      probe.onerror = () => finish(false);
      probe.src = url;
    });
  });

  // ---- Simple dropzone → click-through to file input -----------------------
  $$("[data-dropzone-for]").forEach(zone => {
    const input = $(zone.dataset.dropzoneFor);
    if (!input) return;
    zone.addEventListener("click", () => input.click());
    zone.addEventListener("dragover", e => { e.preventDefault(); zone.classList.add("drag"); });
    zone.addEventListener("dragleave", () => zone.classList.remove("drag"));
    zone.addEventListener("drop", e => {
      e.preventDefault(); zone.classList.remove("drag");
      if (e.dataTransfer.files.length) { input.files = e.dataTransfer.files; input.dispatchEvent(new Event("change")); }
    });
  });
})();
