/* Cria uma View para Exebição dos Horarios De cada clinica */




CREATE VIEW ViewHorarios AS
SELECT Clinica.Nome, Horarios.Dia, Horarios.Horario
FROM Horarios
INNER JOIN Clinica ON Clinica.Id = Horarios.IdClinica
GROUP BY Nome;