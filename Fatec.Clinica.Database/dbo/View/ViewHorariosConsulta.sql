/*Cria Views de HorariosConsulta*/

CREATE VIEW ViewHorariosConsulta AS
SELECT HC.Id, HC.IdAtendimento, C.Nome AS NomeClinica,
M.Nome AS NomeMedico, HC.Dia, HC.Hora, VC.Valor
FROM [HorariosConsulta] HC
INNER JOIN [Atendimento] ON Atendimento.Id = HC.IdAtendimento
INNER JOIN [Medico]  M ON M.Id = Atendimento.IdMedico
INNER JOIN [Clinica] C ON C.Id = Atendimento.IdClinica
INNER JOIN [ValorConsulta]VC ON VC.IdClinica = C.Id