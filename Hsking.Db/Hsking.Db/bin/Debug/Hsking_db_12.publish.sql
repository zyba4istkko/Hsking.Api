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
/*
The column [dbo].[Users].[Email] is being dropped, data loss could occur.

The column [dbo].[Users].[EmailStamp] is being dropped, data loss could occur.

The column [dbo].[Users].[Phone] on table [dbo].[Users] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Users])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_Profiles_USERS]...';


GO
ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [FK_Profiles_USERS];


GO
PRINT N'Starting rebuilding table [dbo].[Users]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Users] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Phone]         NVARCHAR (11)  NOT NULL,
    [Password]      NVARCHAR (300) NOT NULL,
    [DateRegister]  DATETIME       NOT NULL,
    [SecurityStamp] NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Users])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Users] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Users] ([Id], [Password], [DateRegister])
        SELECT   [Id],
                 [Password],
                 [DateRegister]
        FROM     [dbo].[Users]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Users] OFF;
    END

DROP TABLE [dbo].[Users];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Users]', N'Users';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_Profiles_USERS]...';


GO
ALTER TABLE [dbo].[Profiles] WITH NOCHECK
    ADD CONSTRAINT [FK_Profiles_USERS] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([Id]);


GO