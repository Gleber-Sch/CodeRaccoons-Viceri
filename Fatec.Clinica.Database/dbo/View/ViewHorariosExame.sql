/*Cria view de HorariosExame*/

CREATE VIEW ViewHorariosExame AS
SELECT  He.Id, A.Id AS IdAtendimento, C.Nome AS NomeClinica,
M.Nome AS NomeMedico,Tp.Id AS IdTipoExame, Tp.Nome AS NomeExame,
He.Dia, He.Hora
FROM [HorariosExame] He
INNER JOIN [Atendimento] A  ON A.Id = He.IdAtendimento
INNER JOIN [Clinica] C ON C.Id = A.IdClinica
INNER JOIN [TipoExame] Tp ON Tp.Id = He.IdTipoExame
INNER JOIN [Medico] M ON M.Id = A.IdMedico