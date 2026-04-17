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
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Type] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE TABLE [Donors] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Phone] nvarchar(max) NOT NULL,
        [RegisterDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Donors] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE TABLE [Employees] (
        [Id] int NOT NULL IDENTITY,
        [Phone] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Username] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE TABLE [Cases] (
        [Id] int NOT NULL IDENTITY,
        [Amount] decimal(18,2) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [Date] datetime2 NOT NULL,
        [SupervisorId] int NULL,
        [DonorId] int NOT NULL,
        [CategoryId] int NOT NULL,
        CONSTRAINT [PK_Cases] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Cases_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Cases_Donors_DonorId] FOREIGN KEY ([DonorId]) REFERENCES [Donors] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Cases_Employees_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [Employees] ([Id]) ON DELETE SET NULL
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE TABLE [Distributions] (
        [Id] int NOT NULL IDENTITY,
        [Amount] decimal(18,2) NOT NULL,
        [DistributionDate] datetime2 NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [Recipient] nvarchar(max) NOT NULL,
        [CaseId] int NOT NULL,
        [HandledByEmployeeId] int NULL,
        CONSTRAINT [PK_Distributions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Distributions_Cases_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [Cases] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Distributions_Employees_HandledByEmployeeId] FOREIGN KEY ([HandledByEmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE SET NULL
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Cases_CategoryId] ON [Cases] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Cases_DonorId] ON [Cases] ([DonorId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Cases_SupervisorId] ON [Cases] ([SupervisorId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Distributions_CaseId] ON [Distributions] ([CaseId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Distributions_HandledByEmployeeId] ON [Distributions] ([HandledByEmployeeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326220530_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260326220530_InitialCreate', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260329030701_AddPasswordToDonor'
)
BEGIN
    ALTER TABLE [Donors] ADD [Password] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260329030701_AddPasswordToDonor'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260329030701_AddPasswordToDonor', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411080532_SeedAdminUser'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Email', N'Name', N'Password', N'Phone', N'Role', N'Username') AND [object_id] = OBJECT_ID(N'[Employees]'))
        SET IDENTITY_INSERT [Employees] ON;
    EXEC(N'INSERT INTO [Employees] ([Id], [Address], [Email], [Name], [Password], [Phone], [Role], [Username])
    VALUES (1, N''Unknown'', N''12baraka34@gmail.com'', N''Ibrahim Admin'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01278988474'', N''Admin'', N''Ibrahim'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Email', N'Name', N'Password', N'Phone', N'Role', N'Username') AND [object_id] = OBJECT_ID(N'[Employees]'))
        SET IDENTITY_INSERT [Employees] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411080532_SeedAdminUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260411080532_SeedAdminUser', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411081355_SeedRealisticData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Type') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] ON;
    EXEC(N'INSERT INTO [Categories] ([Id], [Description], [Type])
    VALUES (101, N''????? ???? ????????? ??????'', N''?????''),
    (102, N''????? ??????? ??????'', N''???''),
    (103, N''???? ?????? ?????? ?????'', N''????''),
    (104, N''???? ???? ?????? ????'', N''?????''),
    (105, N''?????? ?????? ??? ????????'', N''????? ?????'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Type') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411081355_SeedRealisticData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name', N'Password', N'Phone', N'RegisterDate') AND [object_id] = OBJECT_ID(N'[Donors]'))
        SET IDENTITY_INSERT [Donors] ON;
    EXEC(N'INSERT INTO [Donors] ([Id], [Email], [Name], [Password], [Phone], [RegisterDate])
    VALUES (101, N''ahmed.a@example.com'', N''???? ???????'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01011112222'', ''2025-01-10T10:00:00.0000000Z''),
    (102, N''sara.g@example.com'', N''???? ????'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01122223333'', ''2025-02-05T12:30:00.0000000Z''),
    (103, N''mahmoud.h@example.com'', N''????? ???'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01233334444'', ''2025-03-12T09:15:00.0000000Z''),
    (104, N''nourhan.t@example.com'', N''?????? ????'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01544445555'', ''2025-04-18T14:45:00.0000000Z''),
    (105, N''khaled.s@example.com'', N''???? ????'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01055556666'', ''2025-05-20T16:20:00.0000000Z''),
    (106, N''yasmeen.k@example.com'', N''?????? ????'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01166667777'', ''2025-06-22T11:10:00.0000000Z''),
    (107, N''mostafa.f@example.com'', N''????? ????'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01277778888'', ''2025-07-30T08:50:00.0000000Z''),
    (108, N''raghda.s@example.com'', N''???? ????'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01588889999'', ''2025-08-14T13:25:00.0000000Z''),
    (109, N''omar.f@example.com'', N''??? ?????'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01099990000'', ''2025-09-05T15:55:00.0000000Z''),
    (110, N''laila.a@example.com'', N''???? ??? ??????'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''01100001111'', ''2025-10-01T10:05:00.0000000Z'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name', N'Password', N'Phone', N'RegisterDate') AND [object_id] = OBJECT_ID(N'[Donors]'))
        SET IDENTITY_INSERT [Donors] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411081355_SeedRealisticData'
)
BEGIN
    EXEC(N'UPDATE [Employees] SET [Address] = N''Ismailia , Egypt'', [Name] = N''Ibrahim Nasser''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411081355_SeedRealisticData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CategoryId', N'Date', N'Description', N'DonorId', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Cases]'))
        SET IDENTITY_INSERT [Cases] ON;
    EXEC(N'INSERT INTO [Cases] ([Id], [Amount], [CategoryId], [Date], [Description], [DonorId], [Status], [SupervisorId])
    VALUES (101, 3000.0, 101, ''2026-01-15T00:00:00.0000000Z'', N''?????? ???? ????? ????? ?? ?????? ??????'', 101, N''Open'', 1),
    (102, 15000.0, 102, ''2026-01-20T00:00:00.0000000Z'', N''????? ????? ??? ????? ???'', 102, N''In Progress'', 1),
    (103, 500.0, 103, ''2026-01-25T00:00:00.0000000Z'', N''???? ???? ????? ?????'', 103, N''Closed'', 1),
    (104, 7000.0, 104, ''2026-02-02T00:00:00.0000000Z'', N''????? ???? ??? ????? ?????'', 104, N''Open'', 1),
    (105, 12000.0, 105, ''2026-02-05T00:00:00.0000000Z'', N''???? ??????? ?????? ????? ?????'', 105, N''Open'', 1),
    (106, 2500.0, 102, ''2026-02-10T00:00:00.0000000Z'', N''????? ????? ????? ????'', 106, N''In Progress'', 1),
    (107, 1500.0, 101, ''2026-02-18T00:00:00.0000000Z'', N''????? ?????? ???? ????? ?????? ?????'', 107, N''Closed'', 1),
    (108, 40000.0, 104, ''2026-03-01T00:00:00.0000000Z'', N''???? ??? ???? ??? ??????'', 108, N''Open'', 1),
    (109, 800.0, 103, ''2026-03-04T00:00:00.0000000Z'', N''??? ????? ????? ?? 5 ?????'', 109, N''Closed'', 1),
    (110, 6000.0, 105, ''2026-03-12T00:00:00.0000000Z'', N''?????? ?????? ?? ????? ??????'', 110, N''In Progress'', 1),
    (111, 3500.0, 103, ''2026-03-15T00:00:00.0000000Z'', N''????? ????? ????? ????????'', 101, N''Open'', 1),
    (112, 20000.0, 102, ''2026-03-22T00:00:00.0000000Z'', N''????? ??? ????? ??? ???? ?????? ?????'', 102, N''Open'', 1),
    (113, 4500.0, 104, ''2026-03-28T00:00:00.0000000Z'', N''???? ??? ?? ??????'', 103, N''In Progress'', 1),
    (114, 1800.0, 101, ''2026-04-01T00:00:00.0000000Z'', N''??? ??????? ????? ????? ?????'', 104, N''Open'', 1),
    (115, 9000.0, 105, ''2026-04-03T00:00:00.0000000Z'', N''???? ????? ?????? ??????'', 105, N''Open'', 1),
    (116, 1200.0, 102, ''2026-04-05T00:00:00.0000000Z'', N''???? ???? ???? ??????'', 106, N''Closed'', 1),
    (117, 50000.0, 104, ''2026-04-07T00:00:00.0000000Z'', N''????? ??? ????? ??????? ?????'', 107, N''In Progress'', 1),
    (118, 2500.0, 103, ''2026-04-09T00:00:00.0000000Z'', N''????? ????? ???? ???????'', 108, N''Closed'', 1),
    (119, 4200.0, 101, ''2026-04-10T00:00:00.0000000Z'', N''????? ???????? ???? ????? ????? ?????'', 109, N''Open'', 1),
    (120, 8000.0, 102, ''2026-04-11T00:00:00.0000000Z'', N''???? ?????? ????? ??????'', 110, N''Open'', 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CategoryId', N'Date', N'Description', N'DonorId', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Cases]'))
        SET IDENTITY_INSERT [Cases] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411081355_SeedRealisticData'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260411081355_SeedRealisticData', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Sponsorship for a struggling university student''
    WHERE [Id] = 101;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Heart surgery for an elderly patient''
    WHERE [Id] = 102;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Monthly food basket for a family in need''
    WHERE [Id] = 103;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Installing a clean water well in a rural village''
    WHERE [Id] = 104;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Amount] = 1200.0, [CategoryId] = 103, [Description] = N''Winter clothing drive for orphanages''
    WHERE [Id] = 105;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Monthly insulin medication for diabetic patients''
    WHERE [Id] = 106;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''School uniforms and backpacks for 50 kids''
    WHERE [Id] = 107;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Amount] = 4000.0, [Description] = N''Repairing the roof of a collapsed house''
    WHERE [Id] = 108;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Emergency food supply for a refugee family''
    WHERE [Id] = 109;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [CategoryId] = 104, [Description] = N''Furniture and basics for a newly built shelter''
    WHERE [Id] = 110;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [CategoryId] = 101, [Description] = N''Laptops for high-achieving low-income students''
    WHERE [Id] = 111;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Urgent diagnostic center for specialized tests''
    WHERE [Id] = 112;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [CategoryId] = 105, [Description] = N''Clearing debts for single mothers''
    WHERE [Id] = 113;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Online course subscriptions for skill dev''
    WHERE [Id] = 114;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [CategoryId] = 104, [Description] = N''Solar panel installation for a community center''
    WHERE [Id] = 115;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Wheelchairs for disabled athletes''
    WHERE [Id] = 116;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Amount] = 5000.0, [CategoryId] = 101, [Description] = N''Restoration of a local library''
    WHERE [Id] = 117;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Nutrition kits for pregnant women''
    WHERE [Id] = 118;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [CategoryId] = 105, [Description] = N''Vocational training for unemployed youth''
    WHERE [Id] = 119;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Cases] SET [Description] = N''Rehabilitation center for post-surgery recovery''
    WHERE [Id] = 120;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Categories] SET [Description] = N''Student sponsorship and school supplies'', [Type] = N''Education''
    WHERE [Id] = 101;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Categories] SET [Description] = N''Medications, surgeries, and medical equipment'', [Type] = N''Healthcare''
    WHERE [Id] = 102;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Categories] SET [Description] = N''Food packages and meal distributions'', [Type] = N''Food Security''
    WHERE [Id] = 103;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Categories] SET [Description] = N''Home renovation and clean water access'', [Type] = N''Housing''
    WHERE [Id] = 104;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Categories] SET [Description] = N''Disaster response and urgent assistance'', [Type] = N''Emergency Relief''
    WHERE [Id] = 105;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CaseId', N'DistributionDate', N'HandledByEmployeeId', N'Recipient', N'Status') AND [object_id] = OBJECT_ID(N'[Distributions]'))
        SET IDENTITY_INSERT [Distributions] ON;
    EXEC(N'INSERT INTO [Distributions] ([Id], [Amount], [CaseId], [DistributionDate], [HandledByEmployeeId], [Recipient], [Status])
    VALUES (101, 500.0, 103, ''2026-01-26T00:00:00.0000000Z'', 1, N''Sarah Jenkins'', N''Completed''),
    (102, 1500.0, 107, ''2026-02-19T00:00:00.0000000Z'', 1, N''City General Hospital'', N''Completed''),
    (103, 800.0, 109, ''2026-03-05T00:00:00.0000000Z'', 1, N''Local Refugee Center'', N''Completed''),
    (104, 5000.0, 102, ''2026-01-22T00:00:00.0000000Z'', 1, N''Health Services Dept'', N''Processing''),
    (105, 2000.0, 106, ''2026-02-12T00:00:00.0000000Z'', 1, N''Diabetic Care Clinic'', N''Completed'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CaseId', N'DistributionDate', N'HandledByEmployeeId', N'Recipient', N'Status') AND [object_id] = OBJECT_ID(N'[Distributions]'))
        SET IDENTITY_INSERT [Distributions] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''james.w@example.com'', [Name] = N''James Wilson'', [Phone] = N''+12025550101''
    WHERE [Id] = 101;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''mary.j@example.com'', [Name] = N''Mary Johnson'', [Phone] = N''+12025550102''
    WHERE [Id] = 102;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''robert.s@example.com'', [Name] = N''Robert Smith'', [Phone] = N''+12025550103''
    WHERE [Id] = 103;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''patricia.b@example.com'', [Name] = N''Patricia Brown'', [Phone] = N''+12025550104''
    WHERE [Id] = 104;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''michael.d@example.com'', [Name] = N''Michael Davis'', [Phone] = N''+12025550105''
    WHERE [Id] = 105;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''linda.m@example.com'', [Name] = N''Linda Miller'', [Phone] = N''+12025550106''
    WHERE [Id] = 106;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''david.t@example.com'', [Name] = N''David Taylor'', [Phone] = N''+12025550107''
    WHERE [Id] = 107;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''elizabeth.a@example.com'', [Name] = N''Elizabeth Anderson'', [Phone] = N''+12025550108''
    WHERE [Id] = 108;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''richard.t@example.com'', [Name] = N''Richard Thomas'', [Phone] = N''+12025550109''
    WHERE [Id] = 109;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Email] = N''barbara.j@example.com'', [Name] = N''Barbara Jackson'', [Phone] = N''+12025550110''
    WHERE [Id] = 110;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    EXEC(N'UPDATE [Employees] SET [Address] = N''Ismailia, Egypt''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Email', N'Name', N'Password', N'Phone', N'Role', N'Username') AND [object_id] = OBJECT_ID(N'[Employees]'))
        SET IDENTITY_INSERT [Employees] ON;
    EXEC(N'INSERT INTO [Employees] ([Id], [Address], [Email], [Name], [Password], [Phone], [Role], [Username])
    VALUES (2, N''New York, USA'', N''sarah.c@example.com'', N''Sarah Connor'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''+12025550199'', N''Supervisor'', N''SarahC''),
    (3, N''London, UK'', N''john.doe@example.com'', N''John Doe'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''+12025550188'', N''FieldWorker'', N''JohnD'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Email', N'Name', N'Password', N'Phone', N'Role', N'Username') AND [object_id] = OBJECT_ID(N'[Employees]'))
        SET IDENTITY_INSERT [Employees] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260411085239_SeedRealisticEnglishData'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260411085239_SeedRealisticEnglishData', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260413094815_SeedMoreRecentData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CategoryId', N'Date', N'Description', N'DonorId', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Cases]'))
        SET IDENTITY_INSERT [Cases] ON;
    EXEC(N'INSERT INTO [Cases] ([Id], [Amount], [CategoryId], [Date], [Description], [DonorId], [Status], [SupervisorId])
    VALUES (123, 3000.0, 102, ''2026-04-13T10:00:00.0000000Z'', N''Medical kits for rural area'', 101, N''Open'', 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CategoryId', N'Date', N'Description', N'DonorId', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Cases]'))
        SET IDENTITY_INSERT [Cases] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260413094815_SeedMoreRecentData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name', N'Password', N'Phone', N'RegisterDate') AND [object_id] = OBJECT_ID(N'[Donors]'))
        SET IDENTITY_INSERT [Donors] ON;
    EXEC(N'INSERT INTO [Donors] ([Id], [Email], [Name], [Password], [Phone], [RegisterDate])
    VALUES (111, N''william.w@example.com'', N''William White'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''+12025550111'', ''2026-04-12T10:00:00.0000000Z''),
    (112, N''susan.g@example.com'', N''Susan Green'', N''$2a$11$/u2qj94UTkAB2m91.SYmX.WR6ShENYTBx2SK5SAKhr3RLq2Ux603W'', N''+12025550112'', ''2026-04-13T08:00:00.0000000Z'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Name', N'Password', N'Phone', N'RegisterDate') AND [object_id] = OBJECT_ID(N'[Donors]'))
        SET IDENTITY_INSERT [Donors] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260413094815_SeedMoreRecentData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CategoryId', N'Date', N'Description', N'DonorId', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Cases]'))
        SET IDENTITY_INSERT [Cases] ON;
    EXEC(N'INSERT INTO [Cases] ([Id], [Amount], [CategoryId], [Date], [Description], [DonorId], [Status], [SupervisorId])
    VALUES (121, 5500.0, 104, ''2026-04-12T10:30:00.0000000Z'', N''Clean Water for primary school'', 111, N''Open'', 1),
    (122, 2500.0, 103, ''2026-04-13T09:15:00.0000000Z'', N''Daily Bread for Homeless'', 112, N''Open'', 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CategoryId', N'Date', N'Description', N'DonorId', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Cases]'))
        SET IDENTITY_INSERT [Cases] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260413094815_SeedMoreRecentData'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CaseId', N'DistributionDate', N'HandledByEmployeeId', N'Recipient', N'Status') AND [object_id] = OBJECT_ID(N'[Distributions]'))
        SET IDENTITY_INSERT [Distributions] ON;
    EXEC(N'INSERT INTO [Distributions] ([Id], [Amount], [CaseId], [DistributionDate], [HandledByEmployeeId], [Recipient], [Status])
    VALUES (106, 1200.0, 121, ''2026-04-12T11:00:00.0000000Z'', 1, N''Alexandria School'', N''Completed''),
    (107, 2000.0, 122, ''2026-04-13T09:30:00.0000000Z'', 1, N''Public Shelter'', N''Completed'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CaseId', N'DistributionDate', N'HandledByEmployeeId', N'Recipient', N'Status') AND [object_id] = OBJECT_ID(N'[Distributions]'))
        SET IDENTITY_INSERT [Distributions] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260413094815_SeedMoreRecentData'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260413094815_SeedMoreRecentData', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    ALTER TABLE [Cases] DROP CONSTRAINT [FK_Cases_Donors_DonorId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    DROP INDEX [IX_Cases_DonorId] ON [Cases];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 101;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 104;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 105;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 108;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 110;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 111;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 112;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 113;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 114;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 115;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 116;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 117;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 118;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 119;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 120;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 123;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Distributions]
    WHERE [Id] = 101;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Distributions]
    WHERE [Id] = 102;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Distributions]
    WHERE [Id] = 103;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Distributions]
    WHERE [Id] = 104;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Distributions]
    WHERE [Id] = 105;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Distributions]
    WHERE [Id] = 106;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Distributions]
    WHERE [Id] = 107;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 102;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 103;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 106;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 107;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 109;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 121;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Cases]
    WHERE [Id] = 122;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Donors]
    WHERE [Id] = 104;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Donors]
    WHERE [Id] = 105;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Donors]
    WHERE [Id] = 108;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Donors]
    WHERE [Id] = 110;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Donors]
    WHERE [Id] = 106;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Donors]
    WHERE [Id] = 107;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Donors]
    WHERE [Id] = 109;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Donors]
    WHERE [Id] = 111;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'DELETE FROM [Donors]
    WHERE [Id] = 112;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cases]') AND [c].[name] = N'Amount');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Cases] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Cases] DROP COLUMN [Amount];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cases]') AND [c].[name] = N'DonorId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Cases] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Cases] DROP COLUMN [DonorId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC sp_rename N'[Distributions].[Recipient]', N'Notes', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC sp_rename N'[Cases].[Date]', N'RegistDate', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    ALTER TABLE [Donors] ADD [Address] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    ALTER TABLE [Donors] ADD [Type] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    ALTER TABLE [Distributions] ADD [DonationId] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    ALTER TABLE [Cases] ADD [Address] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    ALTER TABLE [Cases] ADD [Name] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    ALTER TABLE [Cases] ADD [Phone] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    CREATE TABLE [Donations] (
        [Id] int NOT NULL IDENTITY,
        [Amount] decimal(18,2) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [Date] datetime2 NOT NULL,
        [SupervisorId] int NULL,
        [DonorId] int NOT NULL,
        [CategoryId] int NOT NULL,
        CONSTRAINT [PK_Donations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Donations_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Donations_Donors_DonorId] FOREIGN KEY ([DonorId]) REFERENCES [Donors] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Donations_Employees_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [Employees] ([Id]) ON DELETE SET NULL
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'CategoryId', N'Description', N'Name', N'Phone', N'RegistDate', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Cases]'))
        SET IDENTITY_INSERT [Cases] ON;
    EXEC(N'INSERT INTO [Cases] ([Id], [Address], [CategoryId], [Description], [Name], [Phone], [RegistDate], [Status], [SupervisorId])
    VALUES (201, N''Detroit, USA'', 102, N''Needs a new wheelchair for educational mobility'', N''Alice Peterson'', N''+12025550501'', ''2026-01-05T00:00:00.0000000Z'', N''Approved'', 2),
    (202, N''Houston, USA'', 103, N''Monthly food support for a family of 6'', N''Robert''''s Family'', N''+12025550502'', ''2026-01-15T00:00:00.0000000Z'', N''Pending Review'', 2),
    (203, N''Nairobi, Kenya'', 104, N''Roof repairs for the main dormitory'', N''St. Paul Orphanage'', N''+12025550503'', ''2026-02-01T00:00:00.0000000Z'', N''Approved'', 2)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'CategoryId', N'Description', N'Name', N'Phone', N'RegistDate', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Cases]'))
        SET IDENTITY_INSERT [Cases] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CategoryId', N'Date', N'Description', N'DonorId', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Donations]'))
        SET IDENTITY_INSERT [Donations] ON;
    EXEC(N'INSERT INTO [Donations] ([Id], [Amount], [CategoryId], [Date], [Description], [DonorId], [Status], [SupervisorId])
    VALUES (301, 5000.0, 102, ''2026-03-01T00:00:00.0000000Z'', N''Annual CSR contribution for Healthcare'', 103, N''Completed'', 2),
    (302, 1000.0, 103, ''2026-03-10T00:00:00.0000000Z'', N''Personal gift for food drive'', 101, N''Completed'', 2),
    (303, 2500.0, 105, ''2026-04-01T00:00:00.0000000Z'', N''Emergency relief fund contribution'', 102, N''Pending'', 2)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CategoryId', N'Date', N'Description', N'DonorId', N'Status', N'SupervisorId') AND [object_id] = OBJECT_ID(N'[Donations]'))
        SET IDENTITY_INSERT [Donations] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Address] = N''Los Angeles, USA'', [Type] = N''Individual''
    WHERE [Id] = 101;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Address] = N''Chicago, USA'', [Type] = N''Individual''
    WHERE [Id] = 102;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    EXEC(N'UPDATE [Donors] SET [Address] = N''San Francisco, USA'', [Email] = N''donations@globaltech.com'', [Name] = N''Global Tech Corp'', [Phone] = N''+12025550300'', [Type] = N''Corporate''
    WHERE [Id] = 103;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CaseId', N'DistributionDate', N'DonationId', N'HandledByEmployeeId', N'Notes', N'Status') AND [object_id] = OBJECT_ID(N'[Distributions]'))
        SET IDENTITY_INSERT [Distributions] ON;
    EXEC(N'INSERT INTO [Distributions] ([Id], [Amount], [CaseId], [DistributionDate], [DonationId], [HandledByEmployeeId], [Notes], [Status])
    VALUES (401, 3000.0, 201, ''2026-03-15T00:00:00.0000000Z'', 301, 3, N''Funding provided for wheelchair procurement'', N''Completed''),
    (402, 500.0, 202, ''2026-03-20T00:00:00.0000000Z'', 302, 3, N''First monthly food basket distribution'', N''Completed'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Amount', N'CaseId', N'DistributionDate', N'DonationId', N'HandledByEmployeeId', N'Notes', N'Status') AND [object_id] = OBJECT_ID(N'[Distributions]'))
        SET IDENTITY_INSERT [Distributions] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    CREATE INDEX [IX_Distributions_DonationId] ON [Distributions] ([DonationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    CREATE INDEX [IX_Donations_CategoryId] ON [Donations] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    CREATE INDEX [IX_Donations_DonorId] ON [Donations] ([DonorId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    CREATE INDEX [IX_Donations_SupervisorId] ON [Donations] ([SupervisorId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    ALTER TABLE [Distributions] ADD CONSTRAINT [FK_Distributions_Donations_DonationId] FOREIGN KEY ([DonationId]) REFERENCES [Donations] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416124906_RefactorDonationAndCaseFinal'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260416124906_RefactorDonationAndCaseFinal', N'9.0.0');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Cases] DROP CONSTRAINT [FK_Cases_Categories_CategoryId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Distributions] DROP CONSTRAINT [FK_Distributions_Cases_CaseId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Distributions] DROP CONSTRAINT [FK_Distributions_Donations_DonationId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Donations] DROP CONSTRAINT [FK_Donations_Categories_CategoryId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Donations] DROP CONSTRAINT [FK_Donations_Donors_DonorId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Cases] ADD CONSTRAINT [FK_Cases_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Distributions] ADD CONSTRAINT [FK_Distributions_Cases_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [Cases] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Distributions] ADD CONSTRAINT [FK_Distributions_Donations_DonationId] FOREIGN KEY ([DonationId]) REFERENCES [Donations] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Donations] ADD CONSTRAINT [FK_Donations_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    ALTER TABLE [Donations] ADD CONSTRAINT [FK_Donations_Donors_DonorId] FOREIGN KEY ([DonorId]) REFERENCES [Donors] ([Id]) ON DELETE NO ACTION;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260416141309_FixCascadeDeleteCycles'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260416141309_FixCascadeDeleteCycles', N'9.0.0');
END;

COMMIT;
GO