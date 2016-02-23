CREATE TABLE [dbo].[IAInvoice]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ProductName] VARCHAR(50) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Price] INT NOT NULL, 
    [OrderTime] DATETIME NOT NULL, 
    [UserAccountId] INT NOT NULL, 
    CONSTRAINT [FK_IAUserAccount_IAInvoice] FOREIGN KEY ([UserAccountId]) REFERENCES [IAUserAccount]([Id]) 
)
