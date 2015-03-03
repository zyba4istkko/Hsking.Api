CREATE TABLE [dbo].[Users]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Phone] NVARCHAR(12) NOT NULL, 
    [Password] NVARCHAR(300) NULL, 
    [DateRegister] DATETIME NOT NULL,
	[SecurityStamp] NVARCHAR(200) NULL ,
	[Confirm] bit NOT NULL DEFAULT 0 

)
