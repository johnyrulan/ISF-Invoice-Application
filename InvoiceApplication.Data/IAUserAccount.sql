CREATE TABLE [dbo].[IAUserAccount]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(100) NULL, 
    [WarningLimit] INT NOT NULL, 
    [ErrorLimit] INT NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [Updated] DATETIME NOT NULL
)
