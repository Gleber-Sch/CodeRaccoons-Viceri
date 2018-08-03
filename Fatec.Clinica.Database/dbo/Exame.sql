CREATE TABLE Exame (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[DataHora] DATETIME NOT NULL,
[IdAtendimento] INTEGER NOT NULL,
[IdConsulta] INTEGER NOT NULL,
[IdTipoExame] INTEGER NOT NULL,
CONSTRAINT [FK_Exame_TipoExame] FOREIGN KEY(IdTipoExame) REFERENCES [TipoExame] ([Id]),
CONSTRAINT [FK_Exame_Atendimento] FOREIGN KEY(IdAtendimento) REFERENCES [Atendimento] ([Id]),
CONSTRAINT [FK_Exame_Consulta] FOREIGN KEY(IdConsulta) REFERENCES [Consulta] ([Id])
)