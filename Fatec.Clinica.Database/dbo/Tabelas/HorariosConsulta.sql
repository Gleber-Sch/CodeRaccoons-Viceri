CREATE TABLE HorariosConsulta (
[Id] INTEGER PRIMARY KEY IDENTITY NOT NULL,
[Dia] DATE NOT NULL,
[Hora] TIME NOT NULL,
[IdAtendimento] INTEGER NOT NULL,
CONSTRAINT [FK_HorariosConsulta_Atendimento] FOREIGN KEY (IdAtendimento) REFERENCES [Atendimento] ([Id])
)