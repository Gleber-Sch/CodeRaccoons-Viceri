/*Cria tabela de horarios de exames.*/

CREATE TABLE HorariosExame(
[Id] INTEGER PRIMARY KEY IDENTITY NOT NULL,
[Dia] DATE NOT NULL,
[Hora] TIME NOT NULL,
[Valor] MONEY  NOT NULL,
[IdAtendimento] INTEGER NOT NULL,
[IdTipoExame] INTEGER NOT NULL,
CONSTRAINT [FK_HorariosExame_Atendimento] FOREIGN KEY (IdAtendimento) REFERENCES [Atendimento] ([Id]),
CONSTRAINT [FK_HorariosExame_TipoExame] FOREIGN KEY (IdTipoExame) REFERENCES [TipoExame] ([Id])
)
