CREATE TABLE Consulta (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[Historico] VARCHAR(300) NOT NULL,
[Nota] DECIMAL(10) NOT NULL,
[DataHora] DATETIME NOT NULL,
[IdPaciente] INTEGER NOT NULL,
[IdAtendimento] VARCHAR(10) NOT NULL,
CONSTRAINT [FK_Consulta_Paciente] FOREIGN KEY(IdPaciente) REFERENCES [Paciente] ([Id]),
CONSTRAINT [FK_Consulta_Atendimento] FOREIGN KEY(IdAtendimento) REFERENCES [Atendimento] ([Id])
)