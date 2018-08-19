CREATE TABLE Consulta (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[IdPaciente] INTEGER NOT NULL,
[IdAtendimento] INTEGER NOT NULL,
[IdHorariosConsulta] INTEGER NOT NULL,
[IdValorConsulta] INTEGER NOT NULL,
[Historico] VARCHAR(300),
[Nota] TINYINT NOT NULL,
CONSTRAINT [FK_Consulta_ValorConsulta] FOREIGN KEY(IdValorConsulta) REFERENCES [ValorConsulta] ([Id]),
CONSTRAINT [FK_Consulta_HorariosConsulta] FOREIGN KEY(IdHorariosConsulta) REFERENCES [HorariosConsulta] ([Id]),
CONSTRAINT [FK_Consulta_Paciente] FOREIGN KEY(IdPaciente) REFERENCES [Paciente] ([Id]),
CONSTRAINT [FK_Consulta_Atendimento] FOREIGN KEY(IdAtendimento) REFERENCES [Atendimento] ([Id])
)