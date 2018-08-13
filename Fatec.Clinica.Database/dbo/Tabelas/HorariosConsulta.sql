

/*Cria tabela horarios.*/

CREATE TABLE HorariosConsulta (
[Id] INTEGER PRIMARY KEY IDENTITY NOT NULL,
[Dia] DATE NOT NULL,
[Hora] TIME NOT NULL,
[Valor] MONEY NOT NULL,
[IdAtendimento] INTEGER NOT NULL,
CONSTRAINT [FK_HorariosConsulta_Atendimento] FOREIGN KEY (IdAtendimento) REFERENCES [Atendimento] ([Id])
)