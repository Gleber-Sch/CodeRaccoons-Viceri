﻿CREATE TABLE ValorExame(
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[IdTipoExame] INTEGER NOT NULL,
[IdClinica] INTEGER NOT NULL,
[Valor] MONEY NOT NULL
)