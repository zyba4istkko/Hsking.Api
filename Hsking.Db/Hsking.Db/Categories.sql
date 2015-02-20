CREATE TABLE [dbo].[Categories]
(
	[Id] BIGINT NOT NULL PRIMARY KEY Identity(1,1), 
    [Name] NVARCHAR(40) NOT NULL, 
    [Description] NVARCHAR(80) NULL,
   
)
