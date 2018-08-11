/*Cria tabela de horarios de exames.*/

CREATE TABLE HorariosExame(
[Id] INTEGER IDENTITY NOT NULL,
[IdClinica] INTEGER NOT NULL,
[Data] DATE NOT NULL,
[Hora] TIME NOT NULL,
[Valor] MONEY  NOT NULL,
CONSTRAINT [FK_HorariosExame] FOREIGN KEY (IdClinica) REFERENCES [Clinica] ([Id]),
)