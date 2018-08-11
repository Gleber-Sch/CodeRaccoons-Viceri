﻿/*Cria tabela endereco*/

CREATE TABLE Endereco (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[Estado] CHAR(2) NOT NULL,
[Cidade] VARCHAR (50) NOT NULL,
[Bairro] VARCHAR(50) NOT NULL,
[Logradouro] VARCHAR(50) NOT NULL,
[Numero] INTEGER NOT NULL,
[Complemento] VARCHAR(50),
[IdClinica] INTEGER NOT NULL,
CONSTRAINT [FK_Endereco_Clinica] FOREIGN KEY(IdClinica) REFERENCES [Clinica] ([Id])
)