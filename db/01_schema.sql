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

