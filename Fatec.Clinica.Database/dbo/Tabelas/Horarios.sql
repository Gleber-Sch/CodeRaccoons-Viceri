/*Cria tabela horarios.*/


CREATE TABLE Horarios (
[Id] INTEGER PRIMARY KEY IDENTITY NOT NULL,
[IdClinica] INTEGER NOT NULL,
[DiaHora] DATE NOT NULL,
[Tipoatt] CHAR(1) CHECK ( [Tipoatt] IN ('c' , 'e')) NOT NULL,
CONSTRAINT [FK_Horarios_Agenda] FOREIGN KEY (IdClinica) REFERENCES [Clinica] ([Id])
)