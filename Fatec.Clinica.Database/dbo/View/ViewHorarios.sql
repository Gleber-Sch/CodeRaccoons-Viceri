/* Cria uma View para Exebição dos Horarios De cada clinica */




CREATE VIEW ViewHorarios AS
SELECT Clinica.Nome, Horarios.DiaHora, Horarios.Tipoatt
FROM Horarios
INNER JOIN Clinica ON Clinica.Id = Horarios.IdClinica
GROUP BY Nome;