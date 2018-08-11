/*Cria tabela Especialidades*/

CREATE TABLE [dbo].[Especialidade]
(
	[Id] INTEGER NOT NULL PRIMARY KEY IDENTITY,
	[Nome] VARCHAR(50) UNIQUE NOT NULL
)
