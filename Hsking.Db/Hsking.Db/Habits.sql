CREATE TABLE [dbo].[Habits]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDentity(1,1), 
    [Name] NVARCHAR(60) NOT NULL, 
    [Description] NVARCHAR(600) NULL, 
	[Solution] NVARCHAR(600) NULL,
    [Feature] NVARCHAR(600) NULL, 
    [ImageUrl] NVARCHAR(150) NULL, 
    [CategoryId] bigint NOT NULL, 
    [TypeId] BIGINT NOT NULL, 
	
    CONSTRAINT [FK_Habits_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]),
	CONSTRAINT [FK_Habits_Types] FOREIGN KEY ([TypeId]) REFERENCES [Types]([Id])
)
