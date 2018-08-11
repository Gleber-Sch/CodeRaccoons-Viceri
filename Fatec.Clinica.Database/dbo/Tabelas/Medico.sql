﻿/*Cria tabela Medico*/


CREATE TABLE Medico (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[Nome] VARCHAR(50) NOT NULL,
[Cpf] VARCHAR(11) NOT NULL,
[Crm] VARCHAR(10) NOT NULL,
[Email] VARCHAR(50) NOT NULL,
[Senha] VARCHAR(20) NOT NULL,
[DataNasc] DATE NOT NULL,
[StatusAtividade] BIT NOT NULL,
[Genero] CHAR(1) NOT NULL,
[Celular] VARCHAR(11) NOT NULL,
[IdEspecialidade] INTEGER NOT NULL,
CONSTRAINT [FK_Medico_Especialidade] FOREIGN KEY(IdEspecialidade) REFERENCES [Especialidade] ([Id])
)