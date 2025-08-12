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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250331111639_initialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250331111639_initialMigration', N'6.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250331112238_initialMigrationDbSetsAdded')
BEGIN
    CREATE TABLE [Customers] (
        [CustomerId] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Email] varchar(80) NOT NULL,
        [CreditCardNumber] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250331112238_initialMigrationDbSetsAdded')
BEGIN
    CREATE TABLE [Products] (
        [ProductId] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Quantity] decimal(18,2) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250331112238_initialMigrationDbSetsAdded')
BEGIN
    CREATE TABLE [Stores] (
        [StoreId] int NOT NULL IDENTITY,
        [Name] nvarchar(80) NOT NULL,
        CONSTRAINT [PK_Stores] PRIMARY KEY ([StoreId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250331112238_initialMigrationDbSetsAdded')
BEGIN
    CREATE TABLE [Sales] (
        [SaleId] int NOT NULL IDENTITY,
        [Date] datetime2 NOT NULL,
        [ProductId] int NOT NULL,
        [CustomerId] int NOT NULL,
        [StoreId] int NOT NULL,
        CONSTRAINT [PK_Sales] PRIMARY KEY ([SaleId]),
        CONSTRAINT [FK_Sales_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Sales_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Sales_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Stores] ([StoreId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250331112238_initialMigrationDbSetsAdded')
BEGIN
    CREATE INDEX [IX_Sales_CustomerId] ON [Sales] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250331112238_initialMigrationDbSetsAdded')
BEGIN
    CREATE INDEX [IX_Sales_ProductId] ON [Sales] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250331112238_initialMigrationDbSetsAdded')
BEGIN
    CREATE INDEX [IX_Sales_StoreId] ON [Sales] ([StoreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250331112238_initialMigrationDbSetsAdded')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250331112238_initialMigrationDbSetsAdded', N'6.0.1');
END;
GO

COMMIT;
GO