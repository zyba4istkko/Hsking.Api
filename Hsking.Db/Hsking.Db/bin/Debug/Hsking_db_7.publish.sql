﻿/*
Deployment script for Hsking_db

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Hsking_db"
:setvar DefaultFilePrefix "Hsking_db"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Creating [dbo].[Categories]...';


GO
CREATE TABLE [dbo].[Categories] (
    [Id]          BIGINT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (40)      NOT NULL,
    [Description] NVARCHAR (80)      NULL,
    [CreatedAt]   DATETIMEOFFSET (7) NULL,
    [Deleted]     BIT                NOT NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) NULL,
    [Version]     VARBINARY (50)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Habits]...';


GO
CREATE TABLE [dbo].[Habits] (
    [Id]          BIGINT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (60)      NOT NULL,
    [Description] NVARCHAR (600)     NULL,
    [Solution]    NVARCHAR (600)     NULL,
    [Feature]     NVARCHAR (600)     NULL,
    [ImageUrl]    NVARCHAR (150)     NULL,
    [CategoryId]  BIGINT             NOT NULL,
    [TypeId]      BIGINT             NOT NULL,
    [CreatedAt]   DATETIMEOFFSET (7) NULL,
    [Deleted]     BIT                NOT NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) NULL,
    [Version]     VARBINARY (50)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Types]...';


GO
CREATE TABLE [dbo].[Types] (
    [Id]          BIGINT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (40)      NOT NULL,
    [Description] NVARCHAR (80)      NULL,
    [CreatedAt]   DATETIMEOFFSET (7) NULL,
    [Deleted]     BIT                NOT NULL,
    [UpdatedAt]   DATETIMEOFFSET (7) NULL,
    [Version]     VARBINARY (50)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[Categories]...';


GO
ALTER TABLE [dbo].[Categories]
    ADD DEFAULT 0 FOR [Deleted];


GO
PRINT N'Creating unnamed constraint on [dbo].[Habits]...';


GO
ALTER TABLE [dbo].[Habits]
    ADD DEFAULT 0 FOR [Deleted];


GO
PRINT N'Creating unnamed constraint on [dbo].[Types]...';


GO
ALTER TABLE [dbo].[Types]
    ADD DEFAULT 0 FOR [Deleted];


GO
PRINT N'Creating [dbo].[FK_Habits_Category]...';


GO
ALTER TABLE [dbo].[Habits] WITH NOCHECK
    ADD CONSTRAINT [FK_Habits_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Habits_Types]...';


GO
ALTER TABLE [dbo].[Habits] WITH NOCHECK
    ADD CONSTRAINT [FK_Habits_Types] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[Types] ([Id]);


GO
