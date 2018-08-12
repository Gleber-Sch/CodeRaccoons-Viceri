/*Cria view de HorariosExame*/

CREATE VIEW ViewHorariosExame AS
SELECT C.Nome AS NomeClinica, Tp.Nome, He.Dia, He.Hora, He.Valor
FROM [HorariosExame] He
INNER JOIN [Clinica]  C ON C.Id = He.IdClinica
INNER JOIN [TipoExame] Tp ON Tp.Id = He.IdTipoExame