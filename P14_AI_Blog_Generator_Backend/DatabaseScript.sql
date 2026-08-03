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
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [Plans] (
        [PlanId] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NULL,
        [Price] decimal(18,2) NOT NULL,
        [Credits] int NOT NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Plans] PRIMARY KEY ([PlanId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [Users] (
        [UserId] int NOT NULL IDENTITY,
        [UserName] nvarchar(100) NOT NULL,
        [Email] nvarchar(255) NOT NULL,
        [PasswordHash] nvarchar(max) NOT NULL,
        [Role] int NOT NULL,
        [ProfilePictureUrl] nvarchar(500) NULL,
        [AvailableCredits] int NOT NULL DEFAULT 100,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [Blogs] (
        [BlogId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Title] nvarchar(255) NOT NULL,
        [Prompt] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Tone] nvarchar(100) NOT NULL,
        [Audience] nvarchar(100) NOT NULL,
        [Category] nvarchar(500) NOT NULL,
        [WordCount] int NOT NULL,
        [CreditsUsed] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Blogs] PRIMARY KEY ([BlogId]),
        CONSTRAINT [FK_Blogs_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [DeletedAccounts] (
        [DeletedId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Email] nvarchar(255) NOT NULL,
        [Reason] nvarchar(500) NULL,
        [DeletedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_DeletedAccounts] PRIMARY KEY ([DeletedId]),
        CONSTRAINT [FK_DeletedAccounts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [Feedbacks] (
        [FeedbackId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Subject] nvarchar(255) NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        [Rating] int NOT NULL,
        [IsPublic] bit NOT NULL DEFAULT CAST(0 AS bit),
        [Status] int NOT NULL,
        [AdminResponse] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Feedbacks] PRIMARY KEY ([FeedbackId]),
        CONSTRAINT [FK_Feedbacks_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [Issues] (
        [IssueId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Subject] nvarchar(255) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        [AdminResponse] nvarchar(max) NULL,
        [ResolvedAt] datetime2 NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Issues] PRIMARY KEY ([IssueId]),
        CONSTRAINT [FK_Issues_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [Payments] (
        [PaymentId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [PlanId] int NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [CreditsPurchased] int NOT NULL,
        [StripePaymentIntentId] nvarchar(255) NOT NULL,
        [PaymentStatus] int NOT NULL,
        [PurchasedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Payments] PRIMARY KEY ([PaymentId]),
        CONSTRAINT [FK_Payments_Plans_PlanId] FOREIGN KEY ([PlanId]) REFERENCES [Plans] ([PlanId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Payments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [RefreshTokens] (
        [TokenId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Token] nvarchar(max) NOT NULL,
        [ExpiryDate] datetime2 NOT NULL,
        [IsRevoked] bit NOT NULL DEFAULT CAST(0 AS bit),
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_RefreshTokens] PRIMARY KEY ([TokenId]),
        CONSTRAINT [FK_RefreshTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [BlogImages] (
        [ImageId] int NOT NULL IDENTITY,
        [BlogId] int NOT NULL,
        [Prompt] nvarchar(max) NOT NULL,
        [ImageUrl] nvarchar(500) NOT NULL,
        [CreditsUsed] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_BlogImages] PRIMARY KEY ([ImageId]),
        CONSTRAINT [FK_BlogImages_Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [Blogs] ([BlogId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE TABLE [BlogVersions] (
        [VersionId] int NOT NULL IDENTITY,
        [BlogId] int NOT NULL,
        [Title] nvarchar(255) NOT NULL,
        [VersionType] int NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [WordCount] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_BlogVersions] PRIMARY KEY ([VersionId]),
        CONSTRAINT [FK_BlogVersions_Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [Blogs] ([BlogId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE INDEX [IX_BlogImages_BlogId] ON [BlogImages] ([BlogId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE INDEX [IX_Blogs_UserId] ON [Blogs] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE INDEX [IX_BlogVersions_BlogId] ON [BlogVersions] ([BlogId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE UNIQUE INDEX [IX_DeletedAccounts_UserId] ON [DeletedAccounts] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE INDEX [IX_Feedbacks_UserId] ON [Feedbacks] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE INDEX [IX_Issues_UserId] ON [Issues] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE INDEX [IX_Payments_PlanId] ON [Payments] ([PlanId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE INDEX [IX_Payments_UserId] ON [Payments] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE INDEX [IX_RefreshTokens_UserId] ON [RefreshTokens] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Users_UserName] ON [Users] ([UserName]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260727074357_Mig_1'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260727074357_Mig_1', N'8.0.20');
END;
GO

COMMIT;
GO

