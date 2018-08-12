/*Cria Views de HorariosConsulta*/

CREATE VIEW ViewHorariosConsulta AS
SELECT M.Nome, HC.Dia, HC.Hora, HC.Valor
FROM [HorariosConsulta] HC
INNER JOIN Atendimento ON Atendimento.Id = Hc.IdAtendimento
INNER JOIN [Medico]  M ON M.Id = Atendimento.IdMedico
