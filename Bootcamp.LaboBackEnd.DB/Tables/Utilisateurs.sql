﻿CREATE TABLE [dbo].[Utilisateurs]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[Nom] NVARCHAR(100) NOT NULL,
	[Prenom] NVARCHAR(100) NOT NULL,
	[Email] NVARCHAR(100) NOT NULL,
	[PasswordHash] NVARCHAR(255) NOT NULL,
	[DateInscription] DATETIME NOT NULL DEFAULT GETDATE(),
	[IsAdmin] BIT NOT NULL DEFAULT 0,

	CONSTRAINT PK_Utilisateurs PRIMARY KEY (Id),
	CONSTRAINT UQ_Utilisateurs_Email UNIQUE (Email),
	CONSTRAINT UQ_Utilisateurs_NomPrenom UNIQUE (Prenom, Nom)
)
