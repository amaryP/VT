
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/26/2021 10:21:30
-- Generated from EDMX file: C:\Users\amaryp\source\repos\WindowsFormsApp_VTBI\WindowsFormsApp1\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VTBI_BDD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[EvenementSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EvenementSet];
GO
IF OBJECT_ID(N'[dbo].[EssaiSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EssaiSet];
GO
IF OBJECT_ID(N'[dbo].[SuiviValeurSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SuiviValeurSet];
GO
IF OBJECT_ID(N'[dbo].[TradeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TradeSet];
GO
IF OBJECT_ID(N'[dbo].[SuiviSignalSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SuiviSignalSet];
GO
IF OBJECT_ID(N'[dbo].[MethodeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MethodeSet];
GO
IF OBJECT_ID(N'[dbo].[MethodeSuiviSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MethodeSuiviSet];
GO
IF OBJECT_ID(N'[dbo].[MethodeSortieSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MethodeSortieSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EvenementSet'
CREATE TABLE [dbo].[EvenementSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Symbol] nvarchar(max)  NOT NULL,
    [DateHeure] datetime  NOT NULL,
    [valeur] decimal(18,5)  NULL,
    [RSI14] decimal(18,2)  NULL,
    [RSI5] decimal(18,2)  NULL,
    [Eventlog] nvarchar(max)  NULL,
    [typeintervaltime] nvarchar(max)  NULL,
    [TypeOrder] nvarchar(max)  NULL
);
GO

-- Creating table 'EssaiSet'
CREATE TABLE [dbo].[EssaiSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nom] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SuiviValeurSet'
CREATE TABLE [dbo].[SuiviValeurSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Symbol] nvarchar(max)  NOT NULL,
    [DateHeure] datetime  NOT NULL,
    [valeur] decimal(18,5)  NULL
);
GO

-- Creating table 'TradeSet'
CREATE TABLE [dbo].[TradeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Symbol] nvarchar(max)  NOT NULL,
    [DateHeureDebut] datetime  NOT NULL,
    [DateHeureFin] datetime  NULL,
    [ValeurAchat] decimal(18,5)  NULL,
    [ValeurVente] decimal(18,5)  NULL,
    [ValeurCourante] decimal(18,5)  NULL,
    [QuantiteAchat] decimal(18,0)  NULL,
    [QuantiteVente] decimal(18,0)  NULL,
    [Statut] nvarchar(max)  NULL,
    [ID_Exchange] nvarchar(max)  NULL,
    [FraisAchat] decimal(18,5)  NOT NULL,
    [FraisVente] decimal(18,5)  NOT NULL,
    [R0] decimal(18,5)  NULL,
    [R1] decimal(18,5)  NULL,
    [R2Trailling] decimal(18,5)  NULL,
    [R2] decimal(18,5)  NULL,
    [STOP_COURANT] decimal(18,5)  NULL,
    [Gain] decimal(18,5)  NULL,
    [Perte] decimal(18,5)  NULL,
    [CodeMethodeTriggerTrade] nvarchar(20)  NOT NULL,
    [CodeMethodeSuivi] nvarchar(20)  NOT NULL,
    [CodeMethodeSortie] nvarchar(20)  NOT NULL,
    [TypeTrade] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SuiviSignalSet'
CREATE TABLE [dbo].[SuiviSignalSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Symbol] nvarchar(max)  NOT NULL,
    [DateHeure] datetime  NOT NULL,
    [Prix] decimal(18,5)  NULL,
    [RSI14] decimal(18,2)  NULL,
    [RSI5] decimal(18,2)  NULL,
    [PricedirectionBTC] decimal(18,2)  NULL,
    [PriceDirectionSymbol] decimal(18,2)  NULL,
    [Interval] nvarchar(max)  NULL
);
GO

-- Creating table 'MethodeSet'
CREATE TABLE [dbo].[MethodeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nom] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [code_methode] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'MethodeSuiviSet'
CREATE TABLE [dbo].[MethodeSuiviSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nom] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [codemethodesuivi] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'MethodeSortieSet'
CREATE TABLE [dbo].[MethodeSortieSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nom] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [codemethodesortie] nvarchar(20)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'EvenementSet'
ALTER TABLE [dbo].[EvenementSet]
ADD CONSTRAINT [PK_EvenementSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EssaiSet'
ALTER TABLE [dbo].[EssaiSet]
ADD CONSTRAINT [PK_EssaiSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SuiviValeurSet'
ALTER TABLE [dbo].[SuiviValeurSet]
ADD CONSTRAINT [PK_SuiviValeurSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TradeSet'
ALTER TABLE [dbo].[TradeSet]
ADD CONSTRAINT [PK_TradeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SuiviSignalSet'
ALTER TABLE [dbo].[SuiviSignalSet]
ADD CONSTRAINT [PK_SuiviSignalSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [code_methode] in table 'MethodeSet'
ALTER TABLE [dbo].[MethodeSet]
ADD CONSTRAINT [PK_MethodeSet]
    PRIMARY KEY CLUSTERED ([code_methode] ASC);
GO

-- Creating primary key on [codemethodesuivi] in table 'MethodeSuiviSet'
ALTER TABLE [dbo].[MethodeSuiviSet]
ADD CONSTRAINT [PK_MethodeSuiviSet]
    PRIMARY KEY CLUSTERED ([codemethodesuivi] ASC);
GO

-- Creating primary key on [codemethodesortie] in table 'MethodeSortieSet'
ALTER TABLE [dbo].[MethodeSortieSet]
ADD CONSTRAINT [PK_MethodeSortieSet]
    PRIMARY KEY CLUSTERED ([codemethodesortie] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------