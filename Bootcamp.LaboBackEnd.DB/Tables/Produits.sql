CREATE TABLE [dbo].[Produits]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Nom] NVARCHAR(100) NOT NULL,
	[Prix] DECIMAL(18, 2) NOT NULL,
	[Quantite] INT NOT NULL,
	[Description] NVARCHAR(1000) NOT NULL,
	[CategorieId] INT NOT NULL,

	CONSTRAINT PK_Produits PRIMARY KEY (Id),
	CONSTRAINT UQ_Produits_Nom UNIQUE (Nom),
	FOREIGN KEY ([CategorieId]) REFERENCES [dbo].[Categories]([Id])
)
