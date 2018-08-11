/*Cria tabela atendimento.*/


CREATE TABLE Atendimento (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[IdClinica] INTEGER NOT NULL,
[IdMedico] INTEGER NOT NULL,
CONSTRAINT [FK_Atendimento_Clinica] FOREIGN KEY(IdClinica) REFERENCES [Clinica] ([Id]),
CONSTRAINT [FK_Atendimento_Medico] FOREIGN KEY(IdMedico) REFERENCES [Medico] ([Id])
)