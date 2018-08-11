/*Cria tabela consulta*/


CREATE TABLE Consulta (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[Historico] VARCHAR(300) NOT NULL,
[Nota] TINYINT NOT NULL,
[DataHora] DATETIME NOT NULL,
[IdPaciente] INTEGER NOT NULL,
[IdAtendimento] INTEGER NOT NULL,
CONSTRAINT [FK_Consulta_Paciente] FOREIGN KEY(IdPaciente) REFERENCES [Paciente] ([Id]),
CONSTRAINT [FK_Consulta_Atendimento] FOREIGN KEY(IdAtendimento) REFERENCES [Atendimento] ([Id])
)