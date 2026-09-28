-- Repoint brand logo + favicon to the new emblem files under /assets/img/
-- (these files are already deployed live on the server).
-- Run against the PRODUCTION database.
UPDATE dbo.SiteSettings
SET LogoDark  = '/assets/img/logo-dark.png',
    LogoLight = '/assets/img/logo-light.png',
    Favicon   = '/assets/img/favicon.png',
    UpdatedAt = SYSUTCDATETIME()
WHERE Id = 1;

SELECT Id, LogoDark, LogoLight, Favicon FROM dbo.SiteSettings;
