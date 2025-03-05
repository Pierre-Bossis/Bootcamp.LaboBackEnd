CREATE TABLE [dbo].[Commandes_Produits]
(
	[CommandeId] INT NOT NULL,
	[ProduitId] INT NOT NULL,
	[Quantite] INT NOT NULL DEFAULT 1,

	CONSTRAINT PK_Commandes_Produits PRIMARY KEY (CommandeId, ProduitId)
)
