/* Cria uma view para exibição das consultas,
permitindo selecionar elemetos pelo Id da Consulta, 
Id do Paciente e Id do Médico */

CREATE VIEW ViewConsultas AS
SELECT C.Id, P.Id AS IdPaciente, P.Nome AS Paciente,
M.Id AS IdMedico, M.Nome AS Medico,C.DataHora, C.Historico,
Cl.Id AS IdClinica, Cl.Nome AS Clinica, C.Nota
FROM [Consulta] C
JOIN [Atendimento] A ON C.IdAtendimento = A.Id
JOIN [Medico] M ON A.IdMedico = M.Id
JOIN [Paciente] P ON C.IdPaciente = P.Id
JOIN [Clinica] Cl ON A.IdClinica = Cl.Id