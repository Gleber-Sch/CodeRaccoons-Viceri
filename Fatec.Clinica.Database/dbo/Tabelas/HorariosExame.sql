/*Cria tabela de horarios de exames.*/

CREATE TABLE HorariosExame(
[Id] INTEGER IDENTITY NOT NULL,
[Dia] DATE NOT NULL,
[Hora] TIME NOT NULL,
[Valor] MONEY  NOT NULL,
[IdClinica] INTEGER NOT NULL,
[IdTipoExame] INTEGER NOT NULL,
CONSTRAINT [FK_HorariosExame_Clinica] FOREIGN KEY (IdClinica) REFERENCES [Clinica] ([Id]),
CONSTRAINT [FK_HorariosExame_TipoExame] FOREIGN KEY (IdTipoExame) REFERENCES [TipoExame] ([Id])
)