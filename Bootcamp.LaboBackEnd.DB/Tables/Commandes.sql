CREATE TABLE [dbo].[Commandes]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[EtatId] INT NOT NULL DEFAULT 1,
	[UtilisateurId] UNIQUEIDENTIFIER NOT NULL,
	[Date] DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT PK_Commandes PRIMARY KEY (Id),
	FOREIGN KEY ([UtilisateurId]) REFERENCES [dbo].[Utilisateurs]([Id]),
	FOREIGN KEY ([EtatId]) REFERENCES [dbo].[CommandesEtats]([Id])
)
