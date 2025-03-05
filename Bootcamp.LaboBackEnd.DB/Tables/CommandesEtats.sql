CREATE TABLE [dbo].[CommandesEtats]
(
	[Id] INT NOT NULL IDENTITY(1,1),
    [Etat] NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_EtatsCommandes PRIMARY KEY (Id),
    CONSTRAINT UQ_EtatsCommandes_Etat UNIQUE (Etat)
)
