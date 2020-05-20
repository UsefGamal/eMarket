
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/20/2020 07:28:11
-- Generated from EDMX file: C:\Users\ecc\source\repos\eMarket\eMarket\Models\dbContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [store];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Cart__Product_id__4D94879B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cart] DROP CONSTRAINT [FK__Cart__Product_id__4D94879B];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Product_Category];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cart];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [id] int  NOT NULL,
    [name] nvarchar(max)  NULL,
    [number_of_products] int  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NULL,
    [price] float  NULL,
    [Image] nvarchar(50)  NULL,
    [description] varchar(max)  NULL,
    [categryId] int  NOT NULL,
    [Cart_product_Id] int  NOT NULL
);
GO

-- Creating table 'Carts'
CREATE TABLE [dbo].[Carts] (
    [product_Id] int IDENTITY(1,1) NOT NULL,
    [added_at] datetime  NOT NULL
);
GO

-- Creating table 'Cart1'
CREATE TABLE [dbo].[Cart1] (
    [Product_id] int  NOT NULL,
    [added_at] datetime  NULL
);
GO

-- Creating table 'Category1'
CREATE TABLE [dbo].[Category1] (
    [id] int  NOT NULL,
    [name] nvarchar(max)  NULL,
    [number_of_products] int  NULL
);
GO

-- Creating table 'Product1'
CREATE TABLE [dbo].[Product1] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NULL,
    [price] float  NULL,
    [Image] nvarchar(50)  NULL,
    [description] varchar(max)  NULL,
    [categryId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [product_Id] in table 'Carts'
ALTER TABLE [dbo].[Carts]
ADD CONSTRAINT [PK_Carts]
    PRIMARY KEY CLUSTERED ([product_Id] ASC);
GO

-- Creating primary key on [Product_id] in table 'Cart1'
ALTER TABLE [dbo].[Cart1]
ADD CONSTRAINT [PK_Cart1]
    PRIMARY KEY CLUSTERED ([Product_id] ASC);
GO

-- Creating primary key on [id] in table 'Category1'
ALTER TABLE [dbo].[Category1]
ADD CONSTRAINT [PK_Category1]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Product1'
ALTER TABLE [dbo].[Product1]
ADD CONSTRAINT [PK_Product1]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [categryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_Category]
    FOREIGN KEY ([categryId])
    REFERENCES [dbo].[Categories]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Category'
CREATE INDEX [IX_FK_Product_Category]
ON [dbo].[Products]
    ([categryId]);
GO

-- Creating foreign key on [Cart_product_Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductCart]
    FOREIGN KEY ([Cart_product_Id])
    REFERENCES [dbo].[Carts]
        ([product_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCart'
CREATE INDEX [IX_FK_ProductCart]
ON [dbo].[Products]
    ([Cart_product_Id]);
GO

-- Creating foreign key on [Product_id] in table 'Cart1'
ALTER TABLE [dbo].[Cart1]
ADD CONSTRAINT [FK__Cart__Product_id__4D94879B]
    FOREIGN KEY ([Product_id])
    REFERENCES [dbo].[Product1]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [categryId] in table 'Product1'
ALTER TABLE [dbo].[Product1]
ADD CONSTRAINT [FK_Product_Category1]
    FOREIGN KEY ([categryId])
    REFERENCES [dbo].[Category1]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Category1'
CREATE INDEX [IX_FK_Product_Category1]
ON [dbo].[Product1]
    ([categryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------