CREATE TABLE ArquivoMortoHistoricos(
[Id] INTEGER IDENTITY NOT NULL,
[Historioco] VARCHAR(300) NOT NULL,
[IdPaciente] INTEGER NOT NULL,
CONSTRAINT [FK_ArquivoMortoHistorico] FOREIGN KEY (IdPaciente) REFERENCES [ArquivoMorto] ([Id])
)