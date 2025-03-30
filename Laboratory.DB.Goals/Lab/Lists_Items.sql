CREATE TABLE [lab].[Lists_Items]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[ListId] INT NOT NULL,
	[Name] Varchar(200) not null,
	[Description] varchar(500) null,
	[ImageUrl] varchar(250) null,
	[Complete] bit not null Default(0)
)
