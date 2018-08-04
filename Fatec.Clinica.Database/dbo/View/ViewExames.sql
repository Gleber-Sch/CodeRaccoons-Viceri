/* Cria uma view para exibição dos Exames,
permitindo selecionar elemetos pelo Id do Exame, 
Id do Paciente, Id do Médico que solicitou o exame,
Id do médico que realizou o exame e Id da Clinica onde
o exame foi realizado*/

CREATE VIEW ViewExame AS
SELECT E.Id, TE.Nome AS TipoDoExame, P.Id AS IdPaciente,
P.Nome AS NomeDoPaciente, MedQueSolicitou.Id AS IdMedicoQueSolicitou,
MedQueSolicitou.Nome AS SolicitadoPeloMedico,
Co.DataHora AS DataHora_ExameSolicitado,
E.DataHora AS DataHora_ExameRealizado,
MedQueRealizou.Id AS IdMedicoQueRealizou, MedQueRealizou.Nome AS RealizadoPeloMedico,
Cl.Id AS IdClinica, Cl.Nome AS RealizadoNaClinica
FROM [Exame] E
JOIN [Atendimento] AtRealizado ON E.IdAtendimento = AtRealizado.Id
JOIN [Medico] MedQueRealizou ON AtRealizado.IdMedico = MedQueRealizou.Id
JOIN [TipoExame] TE ON E.IdTipoExame = TE.Id
JOIN [Clinica] Cl ON AtRealizado.IdClinica = Cl.Id
JOIN [Consulta] Co ON E.IdConsulta = Co.Id
JOIN [Paciente] P ON Co.IdPaciente = P.Id
JOIN [Atendimento] AtSolicitado ON Co.IdAtendimento = AtSolicitado.Id
JOIN [Medico] MedQueSolicitou ON AtSolicitado.IdMedico = MedQueSolicitou.Id