-- BARDEES CMS — full database setup (schema + seed data)
-- Run once against the db68260 database (e.g. MonsterASP.NET online SQL console).
-- The app also self-creates + seeds on first run, so this is optional.

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [AboutSections] (
        [Id] int NOT NULL IDENTITY,
        [Eyebrow] nvarchar(max) NULL,
        [EyebrowAr] nvarchar(max) NULL,
        [Title] nvarchar(max) NOT NULL,
        [TitleAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [LongDescription] nvarchar(max) NULL,
        [LongDescriptionAr] nvarchar(max) NULL,
        [Image] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [LocationAr] nvarchar(max) NULL,
        [Quote] nvarchar(max) NULL,
        [QuoteAr] nvarchar(max) NULL,
        [ButtonText] nvarchar(max) NULL,
        [ButtonTextAr] nvarchar(max) NULL,
        [ButtonUrl] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_AboutSections] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [ActivityLogs] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [Action] nvarchar(max) NOT NULL,
        [EntityName] nvarchar(max) NULL,
        [EntityId] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [IpAddress] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ActivityLogs] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FullName] nvarchar(max) NULL,
        [AvatarPath] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastLoginAt] datetime2 NULL,
        [IsActive] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [Brands] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [NameAr] nvarchar(max) NULL,
        [Logo] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Role] nvarchar(max) NULL,
        [RoleAr] nvarchar(max) NULL,
        [Sector] nvarchar(max) NULL,
        [SectorAr] nvarchar(max) NULL,
        [WebsiteUrl] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsFeatured] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Brands] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [CollaborationSteps] (
        [Id] int NOT NULL IDENTITY,
        [StepNumber] nvarchar(max) NULL,
        [Title] nvarchar(max) NOT NULL,
        [TitleAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Icon] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_CollaborationSteps] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [ContactMessages] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Phone] nvarchar(max) NULL,
        [Company] nvarchar(max) NULL,
        [Subject] nvarchar(max) NULL,
        [Message] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [IsRead] bit NOT NULL,
        [Status] int NOT NULL,
        [IpAddress] nvarchar(max) NULL,
        CONSTRAINT [PK_ContactMessages] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [ContactSettings] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [TitleAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [WhatsApp] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [LocationAr] nvarchar(max) NULL,
        [Instagram] nvarchar(max) NULL,
        [BookingText] nvarchar(max) NULL,
        [BookingTextAr] nvarchar(max) NULL,
        [BookingButtonText] nvarchar(max) NULL,
        [BookingButtonTextAr] nvarchar(max) NULL,
        [BookingUrl] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_ContactSettings] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [ContentStyles] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [NameAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_ContentStyles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [Disciplines] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [NameAr] nvarchar(max) NULL,
        [Slug] nvarchar(200) NOT NULL,
        [Number] nvarchar(max) NULL,
        [Tagline] nvarchar(max) NULL,
        [TaglineAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Icon] nvarchar(max) NULL,
        [CoverImage] nvarchar(max) NULL,
        [Status] int NOT NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Disciplines] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [Faqs] (
        [Id] int NOT NULL IDENTITY,
        [Question] nvarchar(max) NOT NULL,
        [QuestionAr] nvarchar(max) NULL,
        [Answer] nvarchar(max) NOT NULL,
        [AnswerAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Faqs] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [HeroSections] (
        [Id] int NOT NULL IDENTITY,
        [Eyebrow] nvarchar(max) NULL,
        [EyebrowAr] nvarchar(max) NULL,
        [Title] nvarchar(max) NOT NULL,
        [TitleAr] nvarchar(max) NULL,
        [Subtitle] nvarchar(max) NULL,
        [SubtitleAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Sectors] nvarchar(max) NULL,
        [SectorsAr] nvarchar(max) NULL,
        [PrimaryButtonText] nvarchar(max) NULL,
        [PrimaryButtonTextAr] nvarchar(max) NULL,
        [PrimaryButtonUrl] nvarchar(max) NULL,
        [SecondaryButtonText] nvarchar(max) NULL,
        [SecondaryButtonTextAr] nvarchar(max) NULL,
        [SecondaryButtonUrl] nvarchar(max) NULL,
        [BackgroundImage] nvarchar(max) NULL,
        [BackgroundVideo] nvarchar(max) NULL,
        [FeaturedLabel] nvarchar(max) NULL,
        [FeaturedLabelAr] nvarchar(max) NULL,
        [IssueNumber] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_HeroSections] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [MarketPositions] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [TitleAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Icon] nvarchar(max) NULL,
        [Image] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [LocationAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_MarketPositions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [MediaAssets] (
        [Id] int NOT NULL IDENTITY,
        [FileName] nvarchar(max) NOT NULL,
        [OriginalFileName] nvarchar(max) NULL,
        [FilePath] nvarchar(max) NOT NULL,
        [ThumbnailPath] nvarchar(max) NULL,
        [MediaType] int NOT NULL,
        [MimeType] nvarchar(max) NULL,
        [FileSize] bigint NOT NULL,
        [Width] int NULL,
        [Height] int NULL,
        [AltText] nvarchar(max) NULL,
        [AltTextAr] nvarchar(max) NULL,
        [Caption] nvarchar(max) NULL,
        [CaptionAr] nvarchar(max) NULL,
        [Folder] nvarchar(max) NULL,
        [ContentHash] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_MediaAssets] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [NavigationItems] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [TitleAr] nvarchar(max) NULL,
        [Url] nvarchar(max) NOT NULL,
        [Target] nvarchar(max) NULL,
        [Location] int NOT NULL,
        [Group] nvarchar(max) NULL,
        [GroupAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_NavigationItems] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [PageSections] (
        [Id] int NOT NULL IDENTITY,
        [Key] nvarchar(120) NOT NULL,
        [Page] nvarchar(max) NOT NULL,
        [Eyebrow] nvarchar(max) NULL,
        [EyebrowAr] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        [TitleAr] nvarchar(max) NULL,
        [Note] nvarchar(max) NULL,
        [NoteAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_PageSections] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [Platforms] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [NameAr] nvarchar(max) NULL,
        [Username] nvarchar(max) NULL,
        [Url] nvarchar(max) NULL,
        [Icon] nvarchar(max) NULL,
        [Followers] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Platforms] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [PresenceStats] (
        [Id] int NOT NULL IDENTITY,
        [Value] nvarchar(max) NOT NULL,
        [Label] nvarchar(max) NOT NULL,
        [LabelAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_PresenceStats] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [SeoPages] (
        [Id] int NOT NULL IDENTITY,
        [PageName] nvarchar(max) NOT NULL,
        [Route] nvarchar(300) NOT NULL,
        [MetaTitle] nvarchar(max) NULL,
        [MetaTitleAr] nvarchar(max) NULL,
        [MetaDescription] nvarchar(max) NULL,
        [MetaDescriptionAr] nvarchar(max) NULL,
        [Keywords] nvarchar(max) NULL,
        [CanonicalUrl] nvarchar(max) NULL,
        [OgTitle] nvarchar(max) NULL,
        [OgTitleAr] nvarchar(max) NULL,
        [OgDescription] nvarchar(max) NULL,
        [OgDescriptionAr] nvarchar(max) NULL,
        [OgImage] nvarchar(max) NULL,
        [Robots] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_SeoPages] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [Services] (
        [Id] int NOT NULL IDENTITY,
        [Number] nvarchar(max) NULL,
        [Title] nvarchar(max) NOT NULL,
        [TitleAr] nvarchar(max) NULL,
        [Slug] nvarchar(200) NOT NULL,
        [ShortDescription] nvarchar(max) NULL,
        [ShortDescriptionAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Icon] nvarchar(max) NULL,
        [Image] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsFeatured] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Services] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [SiteSettings] (
        [Id] int NOT NULL IDENTITY,
        [SiteName] nvarchar(max) NOT NULL,
        [SiteNameAr] nvarchar(max) NULL,
        [BrandName] nvarchar(max) NOT NULL,
        [BrandNameAr] nvarchar(max) NULL,
        [BrandNameShort] nvarchar(max) NULL,
        [BrandNameShortAr] nvarchar(max) NULL,
        [Role] nvarchar(max) NULL,
        [RoleAr] nvarchar(max) NULL,
        [Tagline] nvarchar(max) NULL,
        [TaglineAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Logo] nvarchar(max) NULL,
        [Favicon] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [WhatsApp] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [LocationAr] nvarchar(max) NULL,
        [CopyrightText] nvarchar(max) NULL,
        [CopyrightTextAr] nvarchar(max) NULL,
        [DefaultMetaTitle] nvarchar(max) NULL,
        [DefaultMetaDescription] nvarchar(max) NULL,
        [DefaultOgImage] nvarchar(max) NULL,
        [InstagramUrl] nvarchar(max) NULL,
        [TikTokUrl] nvarchar(max) NULL,
        [FacebookUrl] nvarchar(max) NULL,
        [SnapchatUrl] nvarchar(max) NULL,
        [LinkedInUrl] nvarchar(max) NULL,
        [YouTubeUrl] nvarchar(max) NULL,
        [BookingEmail] nvarchar(max) NULL,
        [BookingWhatsApp] nvarchar(max) NULL,
        [GoogleAnalyticsId] nvarchar(max) NULL,
        [GoogleTagManagerId] nvarchar(max) NULL,
        [CustomCss] nvarchar(max) NULL,
        [CustomJs] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_SiteSettings] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [TrustedBrands] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [NameAr] nvarchar(max) NULL,
        [Emphasis] nvarchar(max) NULL,
        [EmphasisAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_TrustedBrands] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [VideoCategories] (
        [Id] int NOT NULL IDENTITY,
        [Key] nvarchar(120) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [NameAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_VideoCategories] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [AboutFacts] (
        [Id] int NOT NULL IDENTITY,
        [AboutSectionId] int NOT NULL,
        [Label] nvarchar(max) NOT NULL,
        [LabelAr] nvarchar(max) NULL,
        [Value] nvarchar(max) NOT NULL,
        [ValueAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_AboutFacts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AboutFacts_AboutSections_AboutSectionId] FOREIGN KEY ([AboutSectionId]) REFERENCES [AboutSections] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [DisciplineSubItems] (
        [Id] int NOT NULL IDENTITY,
        [DisciplineId] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [NameAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        CONSTRAINT [PK_DisciplineSubItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DisciplineSubItems_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [Disciplines] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [PortfolioProjects] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [TitleAr] nvarchar(max) NULL,
        [Slug] nvarchar(200) NOT NULL,
        [ShortDescription] nvarchar(max) NULL,
        [ShortDescriptionAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [ClientName] nvarchar(max) NULL,
        [ClientNameAr] nvarchar(max) NULL,
        [Tag] nvarchar(max) NULL,
        [TagAr] nvarchar(max) NULL,
        [DisciplineId] int NULL,
        [CoverImage] nvarchar(max) NULL,
        [ThumbnailImage] nvarchar(max) NULL,
        [Year] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [LocationAr] nvarchar(max) NULL,
        [ProjectUrl] nvarchar(max) NULL,
        [InstagramUrl] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsFeatured] bit NOT NULL,
        [IsPublished] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_PortfolioProjects] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PortfolioProjects_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [Disciplines] ([Id]) ON DELETE SET NULL
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [HeroSlides] (
        [Id] int NOT NULL IDENTITY,
        [HeroSectionId] int NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [Label] nvarchar(max) NULL,
        [LabelAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_HeroSlides] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HeroSlides_HeroSections_HeroSectionId] FOREIGN KEY ([HeroSectionId]) REFERENCES [HeroSections] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [Videos] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [TitleAr] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Client] nvarchar(max) NULL,
        [ClientAr] nvarchar(max) NULL,
        [Provider] int NOT NULL,
        [VideoUrl] nvarchar(max) NULL,
        [ProviderVideoId] nvarchar(max) NULL,
        [VideoFile] nvarchar(max) NULL,
        [Thumbnail] nvarchar(max) NULL,
        [Duration] nvarchar(max) NULL,
        [IsPortrait] bit NOT NULL,
        [VideoCategoryId] int NULL,
        [DisplayOrder] int NOT NULL,
        [IsFeatured] bit NOT NULL,
        [IsPublished] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Videos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Videos_VideoCategories_VideoCategoryId] FOREIGN KEY ([VideoCategoryId]) REFERENCES [VideoCategories] ([Id]) ON DELETE SET NULL
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE TABLE [PortfolioMedia] (
        [Id] int NOT NULL IDENTITY,
        [PortfolioProjectId] int NOT NULL,
        [MediaType] int NOT NULL,
        [FilePath] nvarchar(max) NOT NULL,
        [ThumbnailPath] nvarchar(max) NULL,
        [Caption] nvarchar(max) NULL,
        [CaptionAr] nvarchar(max) NULL,
        [AltText] nvarchar(max) NULL,
        [AltTextAr] nvarchar(max) NULL,
        [DisplayOrder] int NOT NULL,
        CONSTRAINT [PK_PortfolioMedia] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PortfolioMedia_PortfolioProjects_PortfolioProjectId] FOREIGN KEY ([PortfolioProjectId]) REFERENCES [PortfolioProjects] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AboutFacts_AboutSectionId] ON [AboutFacts] ([AboutSectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ActivityLogs_CreatedAt] ON [ActivityLogs] ([CreatedAt]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ContactMessages_CreatedAt] ON [ContactMessages] ([CreatedAt]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Disciplines_Slug] ON [Disciplines] ([Slug]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_DisciplineSubItems_DisciplineId] ON [DisciplineSubItems] ([DisciplineId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_HeroSlides_HeroSectionId] ON [HeroSlides] ([HeroSectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_PageSections_Key] ON [PageSections] ([Key]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_PortfolioMedia_PortfolioProjectId] ON [PortfolioMedia] ([PortfolioProjectId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_PortfolioProjects_DisciplineId] ON [PortfolioProjects] ([DisciplineId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_PortfolioProjects_Slug] ON [PortfolioProjects] ([Slug]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_SeoPages_Route] ON [SeoPages] ([Route]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Services_Slug] ON [Services] ([Slug]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_VideoCategories_Key] ON [VideoCategories] ([Key]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Videos_VideoCategoryId] ON [Videos] ([VideoCategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260912120713_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260912120713_InitialCreate', N'10.0.0');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260914102442_AddAbayasAndContactTikTok'
)
BEGIN
    ALTER TABLE [ContactSettings] ADD [TikTok] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260914102442_AddAbayasAndContactTikTok'
)
BEGIN
    CREATE TABLE [Abayas] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [NameAr] nvarchar(max) NULL,
        [Slug] nvarchar(200) NOT NULL,
        [Description] nvarchar(max) NULL,
        [DescriptionAr] nvarchar(max) NULL,
        [Fabric] nvarchar(max) NULL,
        [FabricAr] nvarchar(max) NULL,
        [CoverImage] nvarchar(max) NULL,
        [ThumbnailImage] nvarchar(max) NULL,
        [Type] int NOT NULL,
        [DisplayOrder] int NOT NULL,
        [IsFeatured] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Abayas] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260914102442_AddAbayasAndContactTikTok'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Abayas_Slug] ON [Abayas] ([Slug]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260914102442_AddAbayasAndContactTikTok'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260914102442_AddAbayasAndContactTikTok', N'10.0.0');
END;

COMMIT;
GO


GO

SET NOCOUNT ON;
-- Bardees CMS seed data (content only; Identity roles/users are created by the app on startup).

-- ===== SiteSettings =====
SET IDENTITY_INSERT [dbo].[SiteSettings] ON 

INSERT [dbo].[SiteSettings] ([Id], [SiteName], [SiteNameAr], [BrandName], [BrandNameAr], [BrandNameShort], [BrandNameShortAr], [Role], [RoleAr], [Tagline], [TaglineAr], [Description], [DescriptionAr], [Logo], [Favicon], [Email], [Phone], [WhatsApp], [Location], [LocationAr], [CopyrightText], [CopyrightTextAr], [DefaultMetaTitle], [DefaultMetaDescription], [DefaultOgImage], [InstagramUrl], [TikTokUrl], [FacebookUrl], [SnapchatUrl], [LinkedInUrl], [YouTubeUrl], [BookingEmail], [BookingWhatsApp], [GoogleAnalyticsId], [GoogleTagManagerId], [CustomCss], [CustomJs], [CreatedAt], [UpdatedAt]) VALUES (1, N'BARDEES REFAAT', N'برديس رفعت', N'BARDEES REFAAT', N'برديس رفعت', N'Bardees', N'برديس', N'Model · Actress · Brand Ambassador', N'عارضة أزياء · ممثلة · وجه إعلاني', N'Based in Saudi Arabia — Available across the GCC.', N'مقيمة في السعودية — متاحة عبر الخليج.', N'BARDEES REFAAT — model, actress and brand ambassador. Commercial campaigns, fashion, beauty, lifestyle and acting for brands across Saudi Arabia and the GCC.', N'برديس رفعت — عارضة أزياء وممثلة ووجه إعلاني. حملات تجارية وأزياء وجمال ولايف ستايل وتمثيل للعلامات في السعودية والخليج.', NULL, N'assets/brand/favicon.svg', N'hello@bardeesrefaat.com', N'+966 56 576 8902', N'966565768902', N'Saudi Arabia', N'السعودية', N'BARDEES REFAAT', N'برديس رفعت', N'BARDEES REFAAT — Model · Actress · Brand Ambassador', N'Model, actress and brand ambassador based in Saudi Arabia, available across the GCC. Commercial campaigns, fashion, beauty, lifestyle, events, content and acting.', NULL, N'#', N'#', N'#', N'#', NULL, N'https://www.youtube.com/@BardeesRefaat', N'hello@bardeesrefaat.com', N'966565768902', NULL, NULL, NULL, NULL, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[SiteSettings] OFF

-- ===== SeoPages =====
SET IDENTITY_INSERT [dbo].[SeoPages] ON 

INSERT [dbo].[SeoPages] ([Id], [PageName], [Route], [MetaTitle], [MetaTitleAr], [MetaDescription], [MetaDescriptionAr], [Keywords], [CanonicalUrl], [OgTitle], [OgTitleAr], [OgDescription], [OgDescriptionAr], [OgImage], [Robots], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Home', N'/', N'BARDEES REFAAT — Model · Actress · Brand Ambassador', NULL, N'Model, actress and brand ambassador based in Saudi Arabia, available across the GCC. Commercial campaigns, fashion, beauty, lifestyle, events, content and acting.', NULL, NULL, NULL, N'BARDEES REFAAT — Model · Actress · Brand Ambassador', NULL, N'Model, actress and brand ambassador based in Saudi Arabia, available across the GCC. Commercial campaigns, fashion, beauty, lifestyle, events, content and acting.', NULL, NULL, N'index,follow', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[SeoPages] ([Id], [PageName], [Route], [MetaTitle], [MetaTitleAr], [MetaDescription], [MetaDescriptionAr], [Keywords], [CanonicalUrl], [OgTitle], [OgTitleAr], [OgDescription], [OgDescriptionAr], [OgImage], [Robots], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'About', N'/about', N'About — BARDEES REFAAT', NULL, N'A Saudi-based model, actress and brand ambassador working with brands across Saudi Arabia and the GCC.', NULL, NULL, NULL, N'About — BARDEES REFAAT', NULL, N'A Saudi-based model, actress and brand ambassador working with brands across Saudi Arabia and the GCC.', NULL, NULL, N'index,follow', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[SeoPages] ([Id], [PageName], [Route], [MetaTitle], [MetaTitleAr], [MetaDescription], [MetaDescriptionAr], [Keywords], [CanonicalUrl], [OgTitle], [OgTitleAr], [OgDescription], [OgDescriptionAr], [OgImage], [Robots], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'Work', N'/work', N'Work — BARDEES REFAAT', NULL, N'Selected work across commercial, fashion & beauty, food & lifestyle, medical, brand ambassador and acting.', NULL, NULL, NULL, N'Work — BARDEES REFAAT', NULL, N'Selected work across commercial, fashion & beauty, food & lifestyle, medical, brand ambassador and acting.', NULL, NULL, N'index,follow', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[SeoPages] ([Id], [PageName], [Route], [MetaTitle], [MetaTitleAr], [MetaDescription], [MetaDescriptionAr], [Keywords], [CanonicalUrl], [OgTitle], [OgTitleAr], [OgDescription], [OgDescriptionAr], [OgImage], [Robots], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'Services', N'/services', N'Services — BARDEES REFAAT', NULL, N'Modelling, acting, brand ambassadorship, wardrobe styling, brand & event coverage and content creation — tailored to the brief.', NULL, NULL, NULL, N'Services — BARDEES REFAAT', NULL, N'Modelling, acting, brand ambassadorship, wardrobe styling, brand & event coverage and content creation — tailored to the brief.', NULL, NULL, N'index,follow', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[SeoPages] ([Id], [PageName], [Route], [MetaTitle], [MetaTitleAr], [MetaDescription], [MetaDescriptionAr], [Keywords], [CanonicalUrl], [OgTitle], [OgTitleAr], [OgDescription], [OgDescriptionAr], [OgImage], [Robots], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'Abayas', N'/abayas', N'Abayas — Bardees Abaya Collection', NULL, N'Bardees Abaya Collection — ready-to-wear and made-to-measure abayas designed and tailored by Bardees. Order over WhatsApp.', NULL, NULL, NULL, N'Abayas — Bardees Abaya Collection', NULL, N'Bardees Abaya Collection — ready-to-wear and made-to-measure abayas designed and tailored by Bardees. Order over WhatsApp.', NULL, NULL, N'index,follow', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[SeoPages] ([Id], [PageName], [Route], [MetaTitle], [MetaTitleAr], [MetaDescription], [MetaDescriptionAr], [Keywords], [CanonicalUrl], [OgTitle], [OgTitleAr], [OgDescription], [OgDescriptionAr], [OgImage], [Robots], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'Videos', N'/videos', N'Videos — BARDEES REFAAT', NULL, N'Commercial campaigns, modelling, acting and behind-the-scenes films — filter by category and watch.', NULL, NULL, NULL, N'Videos — BARDEES REFAAT', NULL, N'Commercial campaigns, modelling, acting and behind-the-scenes films — filter by category and watch.', NULL, NULL, N'index,follow', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[SeoPages] ([Id], [PageName], [Route], [MetaTitle], [MetaTitleAr], [MetaDescription], [MetaDescriptionAr], [Keywords], [CanonicalUrl], [OgTitle], [OgTitleAr], [OgDescription], [OgDescriptionAr], [OgImage], [Robots], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, N'Contact', N'/contact', N'Contact — BARDEES REFAAT', NULL, N'Looking for a model, actress or advertising face? Book with Bardees over WhatsApp or email.', NULL, NULL, NULL, N'Contact — BARDEES REFAAT', NULL, N'Looking for a model, actress or advertising face? Book with Bardees over WhatsApp or email.', NULL, NULL, N'index,follow', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[SeoPages] OFF

-- ===== PageSections =====
SET IDENTITY_INSERT [dbo].[PageSections] ON 

INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'trusted', N'home', N'Trusted by brands across the Gulf', N'علامات تثق بها عبر الخليج', N'', N'', NULL, NULL, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'clients', N'home', N'Clients & ambassadorships', N'عملاء وسفارات', N'Brands I''ve been the <em>face</em> of.', N'علامات كنت <em>وجهها</em>.', N'A short register of the houses I''ve represented — as ambassador and advertising face — across the Kingdom and the Gulf.', N'سجل موجز للعلامات التي مثّلتها — كسفيرة ووجه إعلاني — في المملكة والخليج.', 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'why', N'home', N'Why Bardees for the Saudi market', N'لماذا برديس للسوق السعودي', N'Built for the <em>Saudi audience.</em>', N'مصمّمة <em>للجمهور السعودي.</em>', N'Helping brands enter, connect with, and grow within the Saudi market.', N'أساعد العلامات على الدخول إلى السوق السعودي والتواصل معه والنمو فيه.', 2, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'disciplines', N'home', N'Contents — select a discipline', N'المحتوى — اختر تخصّصاً', N'Choose the kind of<br><em>collaboration</em> you need.', N'اختر نوع <em>التعاون</em> الذي تحتاجه.', N'Every brand enters differently. Pick a category to see the work that speaks to your campaign.', N'كل علامة تدخل بطريقة مختلفة. اختر فئة لترى الأعمال التي تناسب حملتك.', 3, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'work', N'home', N'Selected — Issue 01', N'مختارات — العدد 01', N'Selected <em>work</em>', N'أعمال <em>مختارة</em>', NULL, NULL, 4, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'showreel', N'home', N'Showreel', N'شوريل', N'See me <em>in motion</em>.', N'شاهدني <em>في حركة</em>.', N'A short cut across campaigns, content and events.', N'لقطة قصيرة من الحملات والمحتوى والفعاليات.', 5, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, N'presence', N'home', N'Digital presence', N'الحضور الرقمي', N'Audience & <em>platforms</em>.', N'الجمهور <em>والمنصات</em>.', NULL, NULL, 6, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (8, N'services', N'home', N'What I offer', N'ما أقدّمه', N'Services, <em>tailored</em>.', N'خدمات <em>مصمّمة</em>.', NULL, NULL, 7, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, N'process', N'home', N'How a collaboration works', N'كيف يتم التعاون', N'Four steps, <em>zero friction</em>.', N'أربع خطوات، <em>بلا تعقيد</em>.', NULL, NULL, 8, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, N'faq', N'home', N'Good to know', N'معلومات مفيدة', N'Questions, <em>answered</em>.', N'أسئلة، <em>وأجوبتها</em>.', NULL, NULL, 9, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (11, N'abaya', N'home', N'Bardees Abaya Collection', N'مجموعة عبايات برديس', N'Designed &amp; tailored by <em>Bardees</em>.', N'تصميم وتفصيل <em>برديس</em>.', N'A personal line of abayas — ready-to-wear and made-to-measure. Ordered directly over WhatsApp.', N'خط عبايات خاص — جاهزة وتفصيل حسب الطلب. تُطلب مباشرةً عبر واتساب.', 10, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (12, N'abaya-page', N'abayas', N'Bardees Abaya Collection', N'مجموعة عبايات برديس', N'Designed &amp; tailored by <em>Bardees</em>.', N'تصميم وتفصيل <em>برديس</em>.', N'A personal line of abayas — from ready-to-wear pieces to made-to-measure tailoring. Order directly over WhatsApp.', N'خط عبايات خاص — من القطع الجاهزة إلى التفصيل حسب الطلب. اطلبي مباشرةً عبر واتساب.', 11, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PageSections] ([Id], [Key], [Page], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Note], [NoteAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (13, N'cta', N'home', N'Let''s work together', N'لنعمل معاً', N'Let''s create content that reflects<br>your <em>brand''s identity</em>.', N'لنصنع محتوى يعكس <em>هوية علامتك</em>.', N'Book your collaboration with Bardees — campaigns, commercial shoots, ambassadorships, events, content and acting across Saudi Arabia and the GCC.', N'احجز تعاونك مع برديس — حملات وتصوير تجاري وسفارات علامات وفعاليات ومحتوى وتمثيل في السعودية والخليج.', 12, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[PageSections] OFF

-- ===== NavigationItems =====
SET IDENTITY_INSERT [dbo].[NavigationItems] ON 

INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (1, N'Home', N'الرئيسية', N'/', NULL, 0, NULL, NULL, 0, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (2, N'Work', N'الأعمال', N'/#work', NULL, 0, NULL, NULL, 1, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (3, N'About', N'عن برديس', N'/about', NULL, 0, NULL, NULL, 2, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (4, N'Services', N'الخدمات', N'/services', NULL, 0, NULL, NULL, 3, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (5, N'Abayas', N'العبايات', N'/abayas', NULL, 0, NULL, NULL, 4, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (6, N'Videos', N'الفيديوهات', N'/videos', NULL, 0, NULL, NULL, 5, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (7, N'Contact', N'تواصل', N'/contact', NULL, 0, NULL, NULL, 6, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (8, N'Home', N'الرئيسية', N'/', NULL, 1, N'Explore', N'استكشف', 0, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (9, N'Work', N'الأعمال', N'/work', NULL, 1, N'Explore', N'استكشف', 1, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (10, N'About', N'عن برديس', N'/about', NULL, 1, N'Explore', N'استكشف', 2, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (11, N'Services', N'الخدمات', N'/services', NULL, 1, N'Explore', N'استكشف', 3, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (12, N'Abayas', N'العبايات', N'/abayas', NULL, 1, N'Explore', N'استكشف', 4, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (13, N'Videos', N'الفيديوهات', N'/videos', NULL, 1, N'Explore', N'استكشف', 5, 1)
INSERT [dbo].[NavigationItems] ([Id], [Title], [TitleAr], [Url], [Target], [Location], [Group], [GroupAr], [DisplayOrder], [IsActive]) VALUES (14, N'Book with Bardees', N'احجز مع برديس', N'/contact', NULL, 1, N'Contact', N'تواصل', 6, 1)
SET IDENTITY_INSERT [dbo].[NavigationItems] OFF

-- ===== HeroSections =====
SET IDENTITY_INSERT [dbo].[HeroSections] ON 

INSERT [dbo].[HeroSections] ([Id], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Subtitle], [SubtitleAr], [Description], [DescriptionAr], [Sectors], [SectorsAr], [PrimaryButtonText], [PrimaryButtonTextAr], [PrimaryButtonUrl], [SecondaryButtonText], [SecondaryButtonTextAr], [SecondaryButtonUrl], [BackgroundImage], [BackgroundVideo], [FeaturedLabel], [FeaturedLabelAr], [IssueNumber], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Model • Actress • Brand Ambassador', N'عارضة أزياء • ممثلة • وجه إعلاني', N'BARDEES', N'برديس', N'REFAAT', N'رفعت', N'Based in Saudi Arabia — <em class="serif-em" style="font-style:normal">available for brands & campaigns across the GCC.</em> Commercial, fashion, beauty, lifestyle, content and acting.', N'مقيمة في السعودية — <em class="serif-em" style="font-style:normal">متاحة للعلامات والحملات عبر الخليج.</em> إعلانات وأزياء وجمال ولايف ستايل ومحتوى وتمثيل.', N'Commercial · Fashion · Beauty · Lifestyle · Acting', N'إعلانات · أزياء · جمال · لايف ستايل · تمثيل', N'View my work', N'شاهد أعمالي', N'#work', N'Work with me', N'لنعمل معاً', N'/contact', NULL, NULL, N'The Index — Issue 01', N'المحتوى — العدد 01', N'01', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[HeroSections] OFF

-- ===== HeroSlides =====
SET IDENTITY_INSERT [dbo].[HeroSlides] ON 

INSERT [dbo].[HeroSlides] ([Id], [HeroSectionId], [Image], [Label], [LabelAr], [DisplayOrder], [IsActive]) VALUES (1, 1, N'couture-01', N'Modest Couture', N'أزياء محتشمة', 0, 1)
INSERT [dbo].[HeroSlides] ([Id], [HeroSectionId], [Image], [Label], [LabelAr], [DisplayOrder], [IsActive]) VALUES (2, 1, N'fashion-olive', N'Editorial Fashion', N'أزياء تحريرية', 1, 1)
INSERT [dbo].[HeroSlides] ([Id], [HeroSectionId], [Image], [Label], [LabelAr], [DisplayOrder], [IsActive]) VALUES (3, 1, N'fashion-blazer-stand', N'Signature Style', N'أسلوب مميّز', 2, 1)
INSERT [dbo].[HeroSlides] ([Id], [HeroSectionId], [Image], [Label], [LabelAr], [DisplayOrder], [IsActive]) VALUES (4, 1, N'fashion-floral', N'Resort Print', N'نقوش صيفية', 3, 1)
INSERT [dbo].[HeroSlides] ([Id], [HeroSectionId], [Image], [Label], [LabelAr], [DisplayOrder], [IsActive]) VALUES (5, 1, N'couture-03', N'Couture Campaign', N'حملة كوتور', 4, 1)
SET IDENTITY_INSERT [dbo].[HeroSlides] OFF

-- ===== AboutSections =====
SET IDENTITY_INSERT [dbo].[AboutSections] ON 

INSERT [dbo].[AboutSections] ([Id], [Eyebrow], [EyebrowAr], [Title], [TitleAr], [Description], [DescriptionAr], [LongDescription], [LongDescriptionAr], [Image], [Location], [LocationAr], [Quote], [QuoteAr], [ButtonText], [ButtonTextAr], [ButtonUrl], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'About Bardees', N'عن برديس', N'Model, actress &amp; <em>brand ambassador</em>.', N'عارضة، ممثلة <em>ووجه إعلاني</em>.', N'A Saudi-based model, actress and brand ambassador working with brands across <em>Saudi Arabia and the GCC.</em>', N'عارضة أزياء وممثلة ووجه إعلاني مقيمة في السعودية، تعمل مع العلامات في <em>السعودية والخليج.</em>', N'With experience in commercial campaigns, fashion, beauty, lifestyle, food &amp; beverage and medical brands, Bardees brings a professional on-camera presence and a strong understanding of the Gulf market. Available for advertising campaigns, commercial shoots, brand partnerships, events, social-media content and acting projects.', N'بخبرة في الحملات التجارية والأزياء والجمال واللايف ستايل والأطعمة والمشروبات والعلامات الطبية، تقدّم برديس حضوراً احترافياً أمام الكاميرا وفهماً قوياً للسوق الخليجي. متاحة للحملات الإعلانية والتصوير التجاري وشراكات العلامات والفعاليات ومحتوى السوشيال ميديا وأعمال التمثيل.', N'couture-01', N'Riyadh', N'الرياض', NULL, NULL, N'Read the full profile', N'اقرأ الملف الكامل', N'/about', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[AboutSections] OFF

-- ===== AboutFacts =====
SET IDENTITY_INSERT [dbo].[AboutFacts] ON 

INSERT [dbo].[AboutFacts] ([Id], [AboutSectionId], [Label], [LabelAr], [Value], [ValueAr], [DisplayOrder], [IsActive]) VALUES (1, 1, N'Based in', N'الإقامة', N'Saudi Arabia', N'السعودية', 0, 1)
INSERT [dbo].[AboutFacts] ([Id], [AboutSectionId], [Label], [LabelAr], [Value], [ValueAr], [DisplayOrder], [IsActive]) VALUES (2, 1, N'Languages', N'اللغات', N'Arabic · English', N'العربية · الإنجليزية', 1, 1)
INSERT [dbo].[AboutFacts] ([Id], [AboutSectionId], [Label], [LabelAr], [Value], [ValueAr], [DisplayOrder], [IsActive]) VALUES (3, 1, N'Dialect', N'اللهجة', N'Saudi Arabic', N'اللهجة السعودية', 2, 1)
INSERT [dbo].[AboutFacts] ([Id], [AboutSectionId], [Label], [LabelAr], [Value], [ValueAr], [DisplayOrder], [IsActive]) VALUES (4, 1, N'Availability', N'التغطية', N'GCC & International', N'الخليج ودولياً', 3, 1)
SET IDENTITY_INSERT [dbo].[AboutFacts] OFF

-- ===== TrustedBrands =====
SET IDENTITY_INSERT [dbo].[TrustedBrands] ON 

INSERT [dbo].[TrustedBrands] ([Id], [Name], [NameAr], [Emphasis], [EmphasisAr], [DisplayOrder], [IsActive]) VALUES (1, N'Calvin Klein', NULL, NULL, NULL, 0, 1)
INSERT [dbo].[TrustedBrands] ([Id], [Name], [NameAr], [Emphasis], [EmphasisAr], [DisplayOrder], [IsActive]) VALUES (2, N'Coach', NULL, N'New York', NULL, 1, 1)
INSERT [dbo].[TrustedBrands] ([Id], [Name], [NameAr], [Emphasis], [EmphasisAr], [DisplayOrder], [IsActive]) VALUES (3, N'Roberto Cavalli', NULL, N'by Franck Muller', NULL, 2, 1)
INSERT [dbo].[TrustedBrands] ([Id], [Name], [NameAr], [Emphasis], [EmphasisAr], [DisplayOrder], [IsActive]) VALUES (4, N'Ferragamo', NULL, NULL, NULL, 3, 1)
INSERT [dbo].[TrustedBrands] ([Id], [Name], [NameAr], [Emphasis], [EmphasisAr], [DisplayOrder], [IsActive]) VALUES (5, N'Alhomaidhi', NULL, N'Watches', NULL, 4, 1)
INSERT [dbo].[TrustedBrands] ([Id], [Name], [NameAr], [Emphasis], [EmphasisAr], [DisplayOrder], [IsActive]) VALUES (6, N'MecroLine', NULL, NULL, NULL, 5, 1)
SET IDENTITY_INSERT [dbo].[TrustedBrands] OFF

-- ===== Brands =====
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([Id], [Name], [NameAr], [Logo], [Description], [DescriptionAr], [Role], [RoleAr], [Sector], [SectorAr], [WebsiteUrl], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Alhomaidhi Watches', N'الحميضي للساعات', N'brand-alhomaidhi', NULL, NULL, N'Brand Ambassador', N'سفيرة العلامة', N'Watches', N'ساعات', NULL, 0, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Brands] ([Id], [Name], [NameAr], [Logo], [Description], [DescriptionAr], [Role], [RoleAr], [Sector], [SectorAr], [WebsiteUrl], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Alhomaidhi Group', N'مجموعة الحميضي', N'brand-alhomaidhi-group', NULL, NULL, N'Brand Ambassador', N'سفيرة العلامة', N'Group', N'مجموعة', NULL, 1, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Brands] ([Id], [Name], [NameAr], [Logo], [Description], [DescriptionAr], [Role], [RoleAr], [Sector], [SectorAr], [WebsiteUrl], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'MecroLine', N'ميكرولاين', N'brand-mecroline', NULL, NULL, N'General Supplies', N'توريدات عامة', N'General Supplies', N'توريدات عامة', NULL, 2, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Brands] OFF

-- ===== MarketPositions =====
SET IDENTITY_INSERT [dbo].[MarketPositions] ON 

INSERT [dbo].[MarketPositions] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [Image], [Location], [LocationAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Saudi Market Understanding', N'فهم السوق السعودي', N'I understand the Saudi audience and the right tone to reach them.', N'أفهم طبيعة الجمهور السعودي وأسلوب التواصل المناسب معه.', NULL, NULL, NULL, NULL, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[MarketPositions] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [Image], [Location], [LocationAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Saudi Dialect', N'اللهجة السعودية', N'I deliver advertising content in the Saudi dialect — naturally and professionally.', N'أقدّم المحتوى الإعلاني باللهجة السعودية بطريقة طبيعية واحترافية.', NULL, NULL, NULL, NULL, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[MarketPositions] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [Image], [Location], [LocationAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'Professional Brand Representation', N'تمثيل احترافي للعلامة', N'I represent brands with an elegant presence suited to companies, hotels, destinations and premium labels.', N'أمثّل العلامة بصورة راقية تناسب الشركات والفنادق والوجهات والعلامات المميزة.', NULL, NULL, NULL, NULL, 2, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[MarketPositions] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [Image], [Location], [LocationAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'Cross-Market Communication', N'التواصل بين الأسواق', N'I help international and Gulf brands introduce themselves to the Saudi audience with impact.', N'أساعد الشركات الدولية والخليجية على تقديم نفسها للجمهور السعودي بتأثير أكبر.', NULL, NULL, NULL, NULL, 3, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[MarketPositions] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [Image], [Location], [LocationAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'On-Camera Presence', N'حضور أمام الكاميرا', N'A confident on-camera presence built for advertising and commercial content.', N'حضور واثق أمام الكاميرا مناسب للإعلانات والمحتوى التجاري.', NULL, NULL, NULL, NULL, 4, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[MarketPositions] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [Image], [Location], [LocationAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'Flexible Content Creation', N'إنتاج محتوى مرن', N'Content tailored for Instagram, TikTok, Snapchat and beyond.', N'محتوى يناسب إنستغرام وتيك توك وسناب شات وغيرها.', NULL, NULL, NULL, NULL, 5, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[MarketPositions] OFF

-- ===== Disciplines =====
SET IDENTITY_INSERT [dbo].[Disciplines] ON 

INSERT [dbo].[Disciplines] ([Id], [Name], [NameAr], [Slug], [Number], [Tagline], [TaglineAr], [Description], [DescriptionAr], [Icon], [CoverImage], [Status], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Commercial', N'إعلانات تجارية', N'commercial', N'01', N'Advertising that lands with the audience.', N'إعلانات تصل إلى الجمهور.', N'Commercial advertising and brand campaigns — including national-day and seasonal spots — delivered with a natural, persuasive on-camera presence.', N'إعلانات تجارية وحملات للعلامات — من بينها إعلانات اليوم الوطني والمواسم — بحضور طبيعي ومؤثّر أمام الكاميرا.', NULL, N'fashion-olive', 0, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Disciplines] ([Id], [Name], [NameAr], [Slug], [Number], [Tagline], [TaglineAr], [Description], [DescriptionAr], [Icon], [CoverImage], [Status], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Fashion & Beauty', N'أزياء وجمال', N'beauty-fashion', N'02', N'The face, the hand, the look.', N'الوجه، واليد، والإطلالة.', N'Fashion and beauty modelling — hand and watch campaigns, makeup and hair, editorial fashion, abayas and couture shoots. One versatile face across the looks a brand builds around.', N'عرض أزياء وجمال — حملات اليد والساعات، ومكياج وشعر، وأزياء تحريرية، وعبايات وتصوير كوتور. وجه متعدد الإطلالات تبني عليه العلامات.', NULL, N'beauty-01', 0, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Disciplines] ([Id], [Name], [NameAr], [Slug], [Number], [Tagline], [TaglineAr], [Description], [DescriptionAr], [Icon], [CoverImage], [Status], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'Food & Lifestyle', N'أطعمة ولايف ستايل', N'food', N'03', N'Appetite & lifestyle, styled.', N'شهية ولايف ستايل، بأسلوب.', N'Food, beverage and lifestyle content for restaurants, cafés, dessert brands and F&B campaigns — appetizing, lifestyle-led content.', N'محتوى للأطعمة والمشروبات واللايف ستايل للمطاعم والكافيهات وعلامات الحلويات وحملات الأطعمة — محتوى شهيّ بروح اللايف ستايل.', NULL, N'beauty-02', 1, 2, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Disciplines] ([Id], [Name], [NameAr], [Slug], [Number], [Tagline], [TaglineAr], [Description], [DescriptionAr], [Icon], [CoverImage], [Status], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'Medical & Healthcare', N'طبي وصحي', N'medical', N'04', N'Trust, on camera.', N'الثقة، أمام الكاميرا.', N'Content and representation for medical brands, dental clinics, medical centers and healthcare campaigns — a clean, reassuring, professional presence.', N'محتوى وتمثيل للعلامات الطبية وعيادات الأسنان والمراكز الطبية والحملات الصحية — حضور نظيف ومطمئن واحترافي.', NULL, N'beauty-03', 1, 3, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Disciplines] ([Id], [Name], [NameAr], [Slug], [Number], [Tagline], [TaglineAr], [Description], [DescriptionAr], [Icon], [CoverImage], [Status], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'Brand Ambassador', N'سفيرة علامات تجارية', N'ambassador', N'05', N'Representing brands across Saudi Arabia & the GCC.', N'أمثّل العلامات في السعودية والخليج.', N'The recognizable, credible face of a brand — in campaigns, at retail and across social. Ambassador for a Gulf marble company and for Alhomaidhi Watches, representing brands across Saudi Arabia and the GCC.', N'الوجه المميّز والموثوق للعلامة — في الحملات وفي نقاط البيع وعلى السوشيال. سفيرة لشركة رخام خليجية ولساعات الحميضي، أمثّل العلامات في السعودية والخليج.', NULL, N'ambassador-instore', 0, 4, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Disciplines] ([Id], [Name], [NameAr], [Slug], [Number], [Tagline], [TaglineAr], [Description], [DescriptionAr], [Icon], [CoverImage], [Status], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'Acting', N'تمثيل', N'acting', N'06', N'Presence that carries a scene.', N'حضور يحمل المشهد.', N'Acting and on-screen work across TV series and theatre, plus commercial acting. TV: Dialect Challenge, Love Again. Theatre: Hamdy & Hamdeya, The Will.', N'تمثيل وأعمال أمام الشاشة في المسلسلات والمسرح، إضافة إلى التمثيل الإعلاني. تلفزيون: تحدي اللهجات، العشق مجددًا. مسرح: حمدي وحمدية، مسرحية الوصية.', NULL, N'fashion-blazer-seated', 0, 5, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Disciplines] OFF

-- ===== DisciplineSubItems =====
SET IDENTITY_INSERT [dbo].[DisciplineSubItems] ON 

INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (1, 1, N'Commercial Advertising', N'إعلانات تجارية', 0)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (2, 1, N'Brand Campaigns', N'حملات العلامات', 1)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (3, 1, N'National-Day Spots', N'إعلانات اليوم الوطني', 2)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (4, 1, N'Product Films', N'أفلام المنتجات', 3)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (5, 2, N'Hand Model — Watches', N'عارضة يد — ساعات', 0)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (6, 2, N'Makeup', N'مكياج', 1)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (7, 2, N'Hairstyle & Hair', N'تسريحات وشعر', 2)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (8, 2, N'Fashion', N'أزياء', 3)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (9, 2, N'Abaya & Couture', N'عبايات وكوتور', 4)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (10, 3, N'Restaurants', N'المطاعم', 0)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (11, 3, N'Cafés', N'الكافيهات', 1)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (12, 3, N'Dessert Brands', N'الحلويات', 2)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (13, 3, N'Lifestyle Content', N'محتوى لايف ستايل', 3)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (14, 4, N'Medical Brands', N'علامات طبية', 0)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (15, 4, N'Dental Clinics', N'عيادات أسنان', 1)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (16, 4, N'Medical Centers', N'مراكز طبية', 2)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (17, 4, N'Healthcare', N'حملات صحية', 3)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (18, 5, N'Brand Ambassador', N'سفيرة علامة', 0)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (19, 5, N'Commercial Modeling', N'مودلينج تجاري', 1)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (20, 5, N'Campaigns', N'الحملات', 2)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (21, 5, N'Social Media Content', N'محتوى السوشيال ميديا', 3)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (22, 5, N'Events & Activations', N'الفعاليات والتفعيلات', 4)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (23, 5, N'Product Launches', N'إطلاق المنتجات', 5)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (24, 6, N'TV — Dialect Challenge', N'تلفزيون — تحدي اللهجات', 0)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (25, 6, N'TV — Love Again', N'تلفزيون — العشق مجددًا', 1)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (26, 6, N'Theatre — Hamdy & Hamdeya', N'مسرح — حمدي وحمدية', 2)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (27, 6, N'Theatre — The Will', N'مسرح — مسرحية الوصية', 3)
INSERT [dbo].[DisciplineSubItems] ([Id], [DisciplineId], [Name], [NameAr], [DisplayOrder]) VALUES (28, 6, N'Commercial Acting', N'تمثيل إعلاني', 4)
SET IDENTITY_INSERT [dbo].[DisciplineSubItems] OFF

-- ===== PortfolioProjects =====
SET IDENTITY_INSERT [dbo].[PortfolioProjects] ON 

INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (1, N'Olive Tailoring', NULL, N'commercial-olive-tailoring', NULL, NULL, NULL, NULL, N'Campaign Look', NULL, N'Commercial', NULL, 1, N'fashion-olive', N'fashion-olive', NULL, NULL, NULL, NULL, NULL, 0, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (2, N'The Film', NULL, N'commercial-the-film', NULL, NULL, NULL, NULL, N'Brand Showreel', NULL, N'Commercial · Video', NULL, 1, N'fashion-pink', N'fashion-pink', NULL, NULL, NULL, NULL, NULL, 1, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (3, N'In-Store', NULL, N'commercial-in-store', NULL, NULL, NULL, NULL, N'Retail Activation', NULL, N'Brand Campaign', NULL, 1, N'ambassador-instore', N'ambassador-instore', NULL, NULL, NULL, NULL, NULL, 2, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (4, N'Gold Hour', NULL, N'beauty-fashion-gold-hour', NULL, NULL, NULL, NULL, N'Beauty Editorial', NULL, N'Makeup · Glam', NULL, 2, N'beauty-01', N'beauty-01', NULL, NULL, NULL, NULL, NULL, 0, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (5, N'Soft Focus', NULL, N'beauty-fashion-soft-focus', NULL, NULL, NULL, NULL, N'Beauty Portrait', NULL, N'Beauty', NULL, 2, N'beauty-03', N'beauty-03', NULL, NULL, NULL, NULL, NULL, 1, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (6, N'Poise', NULL, N'beauty-fashion-poise', NULL, NULL, NULL, NULL, N'Beauty Story', NULL, N'Skin · Glow', NULL, 2, N'beauty-02', N'beauty-02', NULL, NULL, NULL, NULL, NULL, 2, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (7, N'Pinstripe', NULL, N'beauty-fashion-pinstripe', NULL, NULL, NULL, NULL, N'Editorial', NULL, N'Fashion · Suiting', NULL, 2, N'fashion-blazer-stand', N'fashion-blazer-stand', NULL, NULL, NULL, NULL, NULL, 3, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (8, N'Off Duty', NULL, N'beauty-fashion-off-duty', NULL, NULL, NULL, NULL, N'Editorial', NULL, N'Fashion · Attitude', NULL, 2, N'fashion-blazer-seated', N'fashion-blazer-seated', NULL, NULL, NULL, NULL, NULL, 4, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (9, N'Bloom', NULL, N'beauty-fashion-bloom', NULL, NULL, NULL, NULL, N'Resort Story', NULL, N'Fashion · Print', NULL, 2, N'fashion-floral', N'fashion-floral', NULL, NULL, NULL, NULL, NULL, 5, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (10, N'Rosette', NULL, N'beauty-fashion-rosette', NULL, NULL, NULL, NULL, N'Occasion', NULL, N'Fashion', NULL, 2, N'fashion-pink', N'fashion-pink', NULL, NULL, NULL, NULL, NULL, 6, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (11, N'Azure I', NULL, N'beauty-fashion-azure-i', NULL, NULL, NULL, NULL, N'Couture Kaftan', NULL, N'Abaya · Couture', NULL, 2, N'couture-01', N'couture-01', NULL, NULL, NULL, NULL, NULL, 7, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (12, N'Azure II', NULL, N'beauty-fashion-azure-ii', NULL, NULL, NULL, NULL, N'Couture Kaftan', NULL, N'Abaya · Couture', NULL, 2, N'couture-03', N'couture-03', NULL, NULL, NULL, NULL, NULL, 8, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (13, N'Azure III', NULL, N'beauty-fashion-azure-iii', NULL, NULL, NULL, NULL, N'Couture Kaftan', NULL, N'Abaya · Couture', NULL, 2, N'couture-04', N'couture-04', NULL, NULL, NULL, NULL, NULL, 9, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (14, N'Calvin Klein', NULL, N'beauty-fashion-calvin-klein', NULL, NULL, NULL, NULL, N'Watch Campaign', NULL, N'Hand · Watches', NULL, 2, N'hand-calvinklein', N'hand-calvinklein', NULL, NULL, NULL, NULL, NULL, 10, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (15, N'Coach', NULL, N'beauty-fashion-coach', NULL, NULL, NULL, NULL, N'New York — Watch', NULL, N'Hand · Watches', NULL, 2, N'hand-coach', N'hand-coach', NULL, NULL, NULL, NULL, NULL, 11, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (16, N'Roberto Cavalli', NULL, N'beauty-fashion-roberto-cavalli', NULL, NULL, NULL, NULL, N'by Franck Muller', NULL, N'Hand · Watches', NULL, 2, N'hand-cavalli', N'hand-cavalli', NULL, NULL, NULL, NULL, NULL, 12, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (17, N'Ferragamo', NULL, N'beauty-fashion-ferragamo', NULL, NULL, NULL, NULL, N'Watch Campaign', NULL, N'Hand · Watches', NULL, 2, N'hand-ferragamo', N'hand-ferragamo', NULL, NULL, NULL, NULL, NULL, 13, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (18, N'In-Store', NULL, N'ambassador-in-store', NULL, NULL, NULL, NULL, N'Alhomaidhi × Roberto Cavalli', NULL, N'Brand Ambassador', NULL, 5, N'ambassador-instore', N'ambassador-instore', NULL, NULL, NULL, NULL, NULL, 0, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[PortfolioProjects] ([Id], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [ClientName], [ClientNameAr], [Tag], [TagAr], [DisciplineId], [CoverImage], [ThumbnailImage], [Year], [Location], [LocationAr], [ProjectUrl], [InstagramUrl], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (19, N'Cavalli', NULL, N'ambassador-cavalli', NULL, NULL, NULL, NULL, N'Watch House — Ambassador', NULL, N'Ambassador', NULL, 5, N'hand-cavalli', N'hand-cavalli', NULL, NULL, NULL, NULL, NULL, 1, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[PortfolioProjects] OFF

-- ===== PortfolioMedia =====
SET IDENTITY_INSERT [dbo].[PortfolioMedia] ON 

INSERT [dbo].[PortfolioMedia] ([Id], [PortfolioProjectId], [MediaType], [FilePath], [ThumbnailPath], [Caption], [CaptionAr], [AltText], [AltTextAr], [DisplayOrder]) VALUES (1, 2, 1, N'assets/video/reel-b.mp4', N'fashion-pink', NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[PortfolioMedia] OFF

-- ===== VideoCategories =====
SET IDENTITY_INSERT [dbo].[VideoCategories] ON 

INSERT [dbo].[VideoCategories] ([Id], [Key], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (1, N'commercial-campaigns', N'Commercial Campaigns', N'الحملات الإعلانية', 0, 1)
INSERT [dbo].[VideoCategories] ([Id], [Key], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (2, N'modeling', N'Modeling', N'المودلينج', 1, 1)
INSERT [dbo].[VideoCategories] ([Id], [Key], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (3, N'acting', N'Acting', N'التمثيل', 2, 1)
INSERT [dbo].[VideoCategories] ([Id], [Key], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (4, N'bts', N'Behind the Scenes', N'كواليس التصوير', 3, 1)
SET IDENTITY_INSERT [dbo].[VideoCategories] OFF

-- ===== Videos =====
SET IDENTITY_INSERT [dbo].[Videos] ON 

INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (1, N'منشار', N'Manshar', NULL, NULL, N'شركة الرخام', N'Marble Co.', 0, N'https://youtu.be/ZJl6__WQnSs', N'ZJl6__WQnSs', NULL, NULL, NULL, 0, 1, 0, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (2, N'دانو 2', N'Danho 2', NULL, NULL, N'شركة الرخام', N'Marble Co.', 0, N'https://youtu.be/xrtEC_iYvek', N'xrtEC_iYvek', NULL, NULL, NULL, 0, 1, 1, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (3, N'فولاكس', N'Volax', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/0xwe9BzBvEI', N'0xwe9BzBvEI', NULL, NULL, NULL, 0, 1, 2, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (4, N'Campaign Short 01', N'إعلان قصير 01', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/uxk1KrYPk_s', N'uxk1KrYPk_s', NULL, NULL, NULL, 1, 1, 3, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (5, N'Campaign Short 02', N'إعلان قصير 02', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/RERaUPPEjmw', N'RERaUPPEjmw', NULL, NULL, NULL, 1, 1, 4, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (6, N'Campaign Short 03', N'إعلان قصير 03', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/aorNAN5xBLQ', N'aorNAN5xBLQ', NULL, NULL, NULL, 1, 1, 5, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (7, N'Modeling Short 01', N'مودلينج 01', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/DAkRbMczxc0', N'DAkRbMczxc0', NULL, NULL, NULL, 1, 2, 6, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (8, N'Modeling Short 02', N'مودلينج 02', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/G20Clz2Azks', N'G20Clz2Azks', NULL, NULL, NULL, 1, 2, 7, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (9, N'Modeling Short 03', N'مودلينج 03', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/CpINHrP_AZM', N'CpINHrP_AZM', NULL, NULL, NULL, 1, 2, 8, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (10, N'Acting Short 01', N'تمثيل 01', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/T3bY4sxZo8k', N'T3bY4sxZo8k', NULL, NULL, NULL, 1, 3, 9, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (11, N'Acting Short 02', N'تمثيل 02', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/UgLz09iwgBg', N'UgLz09iwgBg', NULL, NULL, NULL, 1, 3, 10, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (12, N'Behind the Scenes 01', N'كواليس 01', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/X4uGZAA0r4U', N'X4uGZAA0r4U', NULL, NULL, NULL, 1, 4, 11, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (13, N'Behind the Scenes 02', N'كواليس 02', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/rX-YX2P5Reo', N'rX-YX2P5Reo', NULL, NULL, NULL, 1, 4, 12, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Videos] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Client], [ClientAr], [Provider], [VideoUrl], [ProviderVideoId], [VideoFile], [Thumbnail], [Duration], [IsPortrait], [VideoCategoryId], [DisplayOrder], [IsFeatured], [IsPublished], [CreatedAt], [UpdatedAt]) VALUES (14, N'Behind the Scenes 03', N'كواليس 03', NULL, NULL, NULL, NULL, 0, N'https://youtu.be/O6BBgc6UdQA', N'O6BBgc6UdQA', NULL, NULL, NULL, 1, 4, 13, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Videos] OFF

-- ===== Platforms =====
SET IDENTITY_INSERT [dbo].[Platforms] ON 

INSERT [dbo].[Platforms] ([Id], [Name], [NameAr], [Username], [Url], [Icon], [Followers], [Description], [DescriptionAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Instagram', NULL, NULL, N'#', N'instagram', NULL, NULL, NULL, 0, 1, CAST(N'2026-09-14T11:30:06.9909731' AS DateTime2), NULL)
INSERT [dbo].[Platforms] ([Id], [Name], [NameAr], [Username], [Url], [Icon], [Followers], [Description], [DescriptionAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'TikTok', NULL, NULL, N'#', N'tiktok', NULL, NULL, NULL, 1, 1, CAST(N'2026-09-14T11:30:07.0553075' AS DateTime2), NULL)
INSERT [dbo].[Platforms] ([Id], [Name], [NameAr], [Username], [Url], [Icon], [Followers], [Description], [DescriptionAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'Snapchat', NULL, NULL, N'#', N'snapchat', NULL, NULL, NULL, 2, 1, CAST(N'2026-09-14T11:30:07.0555223' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Platforms] OFF

-- ===== PresenceStats =====
SET IDENTITY_INSERT [dbo].[PresenceStats] ON 

INSERT [dbo].[PresenceStats] ([Id], [Value], [Label], [LabelAr], [DisplayOrder], [IsActive]) VALUES (1, N'XXK+', N'Followers', N'متابع', 0, 1)
INSERT [dbo].[PresenceStats] ([Id], [Value], [Label], [LabelAr], [DisplayOrder], [IsActive]) VALUES (2, N'XXK+', N'Monthly Reach', N'وصول شهري', 1, 1)
INSERT [dbo].[PresenceStats] ([Id], [Value], [Label], [LabelAr], [DisplayOrder], [IsActive]) VALUES (3, N'XX%', N'Saudi Audience', N'جمهور سعودي', 2, 1)
INSERT [dbo].[PresenceStats] ([Id], [Value], [Label], [LabelAr], [DisplayOrder], [IsActive]) VALUES (4, N'GCC', N'Available', N'متاحة', 3, 1)
SET IDENTITY_INSERT [dbo].[PresenceStats] OFF

-- ===== ContentStyles =====
SET IDENTITY_INSERT [dbo].[ContentStyles] ON 

INSERT [dbo].[ContentStyles] ([Id], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (1, N'Short-form video', N'فيديو قصير', 0, 1)
INSERT [dbo].[ContentStyles] ([Id], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (2, N'Reels', N'ريلز', 1, 1)
INSERT [dbo].[ContentStyles] ([Id], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (3, N'Stories', N'ستوريز', 2, 1)
INSERT [dbo].[ContentStyles] ([Id], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (4, N'Brand campaigns', N'حملات إعلانية', 3, 1)
INSERT [dbo].[ContentStyles] ([Id], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (5, N'Event coverage', N'تغطية فعاليات', 4, 1)
INSERT [dbo].[ContentStyles] ([Id], [Name], [NameAr], [DisplayOrder], [IsActive]) VALUES (6, N'Lifestyle content', N'محتوى لايف ستايل', 5, 1)
SET IDENTITY_INSERT [dbo].[ContentStyles] OFF

-- ===== Services =====
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [Number], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [Icon], [Image], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'01', N'Modeling & Commercial Photography', N'المودلينج والتصوير الإعلاني', N'modeling-commercial-photography', N'Modelling for commercial ads, fashion, beauty, lifestyle, products and brand campaigns across Saudi Arabia and the Gulf.', N'موديل للإعلانات التجارية والأزياء والجمال وأسلوب الحياة والمنتجات والحملات الإعلانية للبراندات في السعودية والخليج.', NULL, NULL, NULL, NULL, 0, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Services] ([Id], [Number], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [Icon], [Image], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'02', N'Acting', N'التمثيل', N'acting', N'Professional on-camera performance for commercials, digital campaigns, TV series, theatre and branded content.', N'أداء احترافي أمام الكاميرا للإعلانات التجارية والحملات الرقمية والمسلسلات والمسرح والمحتوى الخاص بالبراندات.', NULL, NULL, NULL, NULL, 1, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Services] ([Id], [Number], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [Icon], [Image], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'03', N'Brand Ambassador', N'الوجه الإعلاني للبراندات', N'brand-ambassador', N'Representing brands through advertising campaigns, media appearances, digital content, events and long-term partnerships.', N'تمثيل البراندات من خلال الحملات الإعلانية والظهور الإعلامي والمحتوى الرقمي والفعاليات والشراكات طويلة المدى.', NULL, NULL, NULL, NULL, 2, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Services] ([Id], [Number], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [Icon], [Image], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'04', N'Wardrobe Design & Styling', N'تفصيل وتنسيق الأزياء حسب هوية الإعلان', N'wardrobe-design-styling', N'Custom wardrobe designed and tailored to match the ad''s identity, campaign concept and the brand''s visual direction — colours, fabrics and details chosen for a cohesive look.', N'تصميم وتفصيل أزياء مخصّصة تتناسب مع هوية الإعلان وفكرة الحملة والتوجه البصري للبراند — تُختار الألوان والخامات والتفاصيل لظهور متكامل ومتناسق.', NULL, NULL, NULL, NULL, 3, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Services] ([Id], [Number], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [Icon], [Image], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'05', N'Brand & Event Coverage', N'تغطية البراندات والفعاليات', N'brand-event-coverage', N'Coverage for brands, events, exhibitions, openings, product launches and activations — photography, video, on-camera presence and social-ready content.', N'تغطية البراندات والفعاليات والمعارض والافتتاحات وإطلاق المنتجات والـActivations — تصوير فوتوغرافي وفيديو وظهور أمام الكاميرا ومحتوى جاهز للنشر.', NULL, NULL, NULL, NULL, 4, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Services] ([Id], [Number], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [Icon], [Image], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'06', N'Content Creation', N'صناعة المحتوى', N'content-creation', N'Creative content for TikTok, Instagram, Snapchat and beyond — ad videos, product showcases, lifestyle and UGC content.', N'صناعة محتوى إبداعي لمنصات TikTok وInstagram وSnapchat وغيرها — فيديوهات إعلانية وعرض منتجات ومحتوى لايف ستايل ومحتوى UGC.', NULL, NULL, NULL, NULL, 5, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Services] ([Id], [Number], [Title], [TitleAr], [Slug], [ShortDescription], [ShortDescriptionAr], [Description], [DescriptionAr], [Icon], [Image], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, N'07', N'Gulf Market Presence', N'التمثيل والتواجد في السوق الخليجي', N'gulf-market-presence', N'Representing brands in the Saudi and Gulf market through local presence, advertising campaigns, content and commercial collaborations.', N'تمثيل البراندات في السوق السعودي والخليجي من خلال الحضور المحلي والحملات الإعلانية وصناعة المحتوى والتعاونات التجارية.', NULL, NULL, NULL, NULL, 6, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Services] OFF

-- ===== Abayas =====
SET IDENTITY_INSERT [dbo].[Abayas] ON 

INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Noir Botanical', N'نوار بوتانيكال', N'noir-botanical', NULL, NULL, N'Crêpe · Leaf embroidery', N'كريب · تطريز أوراق', N'abaya-01', N'abaya-01', 0, 0, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Taupe Palm', N'بيج بالم', N'taupe-palm', NULL, NULL, N'Crêpe · Palm embroidery', N'كريب · تطريز نخيل', N'abaya-02', N'abaya-02', 0, 1, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'Gold Line', N'الخط الذهبي', N'gold-line', NULL, NULL, N'Crêpe · Gold piping', N'كريب · حواف ذهبية', N'abaya-03', N'abaya-03', 0, 2, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'Bronze Paisley', N'برونز بيزلي', N'bronze-paisley', NULL, NULL, N'Crêpe · Beaded paisley', N'كريب · تطريز بالخرز', N'abaya-04', N'abaya-04', 1, 3, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'Cocoa Pleat', N'كاكاو بليت', N'cocoa-pleat', NULL, NULL, N'Crêpe · Pleated cuffs', N'كريب · أكمام مطويّة', N'abaya-05', N'abaya-05', 0, 4, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'Gilded Trim', N'التطريز الذهبي', N'gilded-trim', NULL, NULL, N'Crêpe · Gold beadwork', N'كريب · خرز ذهبي', N'abaya-06', N'abaya-06', 0, 5, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, N'Sage Embroidery', N'سيج المطرزة', N'sage-embroidery', NULL, NULL, N'Crêpe · Silver embroidery', N'كريب · تطريز فضي', N'abaya-07', N'abaya-07', 1, 6, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (8, N'Onyx Geometric', N'أونيكس الهندسية', N'onyx-geometric', NULL, NULL, N'Crêpe · Geometric embroidery', N'كريب · تطريز هندسي', N'abaya-08', N'abaya-08', 1, 7, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, N'Ivory Rose', N'آيفوري روز', N'ivory-rose', NULL, NULL, N'Crêpe · Floral embroidery', N'كريب · تطريز زهري', N'abaya-09', N'abaya-09', 0, 8, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Abayas] ([Id], [Name], [NameAr], [Slug], [Description], [DescriptionAr], [Fabric], [FabricAr], [CoverImage], [ThumbnailImage], [Type], [DisplayOrder], [IsFeatured], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, N'Merlot Contrast', N'ميرلو', N'merlot-contrast', NULL, NULL, N'Crêpe · Contrast stitch', N'كريب · حياكة متباينة', N'abaya-10', N'abaya-10', 1, 9, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Abayas] OFF

-- ===== CollaborationSteps =====
SET IDENTITY_INSERT [dbo].[CollaborationSteps] ON 

INSERT [dbo].[CollaborationSteps] ([Id], [StepNumber], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'01', N'Choose a discipline', N'اختر التخصّص', N'Pick the category that fits your campaign — from hand model to corporate representation.', N'اختر الفئة التي تناسب حملتك — من عارضة يد إلى تمثيل مؤسسي.', NULL, 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[CollaborationSteps] ([Id], [StepNumber], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'02', N'Share the brief', N'شارك الملخّص', N'Dates, deliverables, usage rights and budget. Note: wardrobe is styled to suit each ad.', N'التواريخ والمخرجات وحقوق الاستخدام والميزانية. ملاحظة: يُنسّق الزي ليلائم كل إعلان.', NULL, 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[CollaborationSteps] ([Id], [StepNumber], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'03', N'Receive a proposal', N'استلم العرض', N'Availability and a tailored quote — priced per assignment for corporate work.', N'التوفّر وعرض سعر مخصّص — بالتسعير لكل مهمة في الأعمال المؤسسية.', NULL, 2, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[CollaborationSteps] ([Id], [StepNumber], [Title], [TitleAr], [Description], [DescriptionAr], [Icon], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'04', N'Create', N'ننفّذ', N'On set, on stage, or in the room — one consistent, professional presence.', N'على موقع التصوير أو المسرح أو في القاعة — حضور واحد ثابت واحترافي.', NULL, 3, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[CollaborationSteps] OFF

-- ===== Faqs =====
SET IDENTITY_INSERT [dbo].[Faqs] ON 

INSERT [dbo].[Faqs] ([Id], [Question], [QuestionAr], [Answer], [AnswerAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'How do rates work?', N'كيف يتم التسعير؟', N'Advertising shoots are quoted by scope and usage. Corporate representation is priced per assignment — by the nature of the event and the duration of attendance.', N'تُسعّر جلسات التصوير الإعلاني حسب النطاق والاستخدام. أما التمثيل المؤسسي فيُسعّر لكل مهمة — حسب طبيعة الحدث ومدة الحضور.', 0, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Faqs] ([Id], [Question], [QuestionAr], [Answer], [AnswerAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Do you work in the Saudi dialect?', N'هل تعملين باللهجة السعودية؟', N'Yes — I deliver advertising content in the Saudi dialect, naturally and professionally, to reach the Saudi audience.', N'نعم — أقدّم المحتوى الإعلاني باللهجة السعودية بطريقة طبيعية واحترافية للوصول إلى الجمهور السعودي.', 1, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Faqs] ([Id], [Question], [QuestionAr], [Answer], [AnswerAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'How is wardrobe handled?', N'كيف يُدار الزي؟', N'Wardrobe is styled to suit each ad — plain and refined for a product like marble, or more expressive and fashion-led where the brand calls for it.', N'يُنسّق الزي ليلائم كل إعلان — بسيط وراقٍ لمنتج مثل الرخام، أو أكثر تعبيراً وأزياءً حين تتطلّب العلامة ذلك.', 2, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Faqs] ([Id], [Question], [QuestionAr], [Answer], [AnswerAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'Do you travel across the GCC?', N'هل تسافرين عبر الخليج؟', N'Yes — based in Saudi Arabia and available across the GCC. Travel is arranged per booking.', N'نعم — مقيمة في السعودية ومتاحة عبر الخليج. يُرتّب السفر لكل حجز.', 3, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
INSERT [dbo].[Faqs] ([Id], [Question], [QuestionAr], [Answer], [AnswerAr], [DisplayOrder], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'How far ahead should we book?', N'كم من الوقت مسبقاً يجب الحجز؟', N'Two to three weeks is ideal, though rush bookings are considered subject to availability.', N'من أسبوعين إلى ثلاثة مثالية، مع إمكانية النظر في الحجوزات العاجلة حسب التوفّر.', 4, 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Faqs] OFF

-- ===== ContactSettings =====
SET IDENTITY_INSERT [dbo].[ContactSettings] ON 

INSERT [dbo].[ContactSettings] ([Id], [Title], [TitleAr], [Description], [DescriptionAr], [Email], [Phone], [WhatsApp], [Location], [LocationAr], [Instagram], [BookingText], [BookingTextAr], [BookingButtonText], [BookingButtonTextAr], [BookingUrl], [IsActive], [CreatedAt], [UpdatedAt], [TikTok]) VALUES (1, N'Looking for a model, actress or advertising face for your next campaign?', N'هل تبحث عن عارضة أزياء أو ممثلة أو وجه إعلاني لحملتك القادمة؟', N'Book with Bardees. Send your brief here and I''ll reply on WhatsApp or by email — the fastest way to reach me is the WhatsApp button.', N'احجز مع برديس. أرسل ملخّصك من هنا وسأرد عبر واتساب أو البريد — وأسرع طريقة للوصول إليّ هي زر واتساب.', N'hello@bardeesrefaat.com', N'+966 56 576 8902', N'966565768902', N'Saudi Arabia · GCC', N'السعودية · الخليج', N'#', N'Book with Bardees', N'احجز مع برديس', N'Book', N'احجز', N'/contact', 1, CAST(N'2026-09-14T11:30:06.2060003' AS DateTime2), NULL, N'#')
SET IDENTITY_INSERT [dbo].[ContactSettings] OFF

