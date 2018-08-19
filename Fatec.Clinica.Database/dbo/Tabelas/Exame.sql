CREATE TABLE Exame (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[IdAtendimento] INTEGER NOT NULL,
[IdConsulta] INTEGER NOT NULL,
[IdTipoExame] INTEGER NOT NULL,
[IdHorariosExame] INTEGER NOT NULL,
[IdValorExame] INTEGER NOT NULL,
CONSTRAINT [FK_Exame_ValorExame] FOREIGN KEY(IdValorExame) REFERENCES [ValorExame] ([Id]),
CONSTRAINT [FK_Exame_HorariosExame] FOREIGN KEY(IdHorariosExame) REFERENCES [HorariosExame] ([Id]),
CONSTRAINT [FK_Exame_TipoExame] FOREIGN KEY(IdTipoExame) REFERENCES [TipoExame] ([Id]),
CONSTRAINT [FK_Exame_Atendimento] FOREIGN KEY(IdAtendimento) REFERENCES [Atendimento] ([Id]),
CONSTRAINT [FK_Exame_Consulta] FOREIGN KEY(IdConsulta) REFERENCES [Consulta] ([Id])
)