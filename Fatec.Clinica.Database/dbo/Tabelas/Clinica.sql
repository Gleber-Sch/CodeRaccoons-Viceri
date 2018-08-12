﻿CREATE TABLE Clinica (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[Email] VARCHAR (50) UNIQUE NOT NULL,
[Senha] VARCHAR (20) NOT NULL,
[Cnpj] VARCHAR(14) UNIQUE NOT NULL,
[StatusAtividade] BIT NOT NULL,
[TelefoneCom] VARCHAR (10) NOT NULL,
[Nome] VARCHAR(50) NOT NULL,
[IdEndereco] INTEGER NOT NULL,
CONSTRAINT [FK_Clinica_Endereco] FOREIGN KEY(IdEndereco) REFERENCES [Endereco] ([Id])
)