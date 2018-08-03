create VIEW ViewConsultas as
select C.Id, C.DataHora, C.Historico
from [Consulta] C, [Medico] M, [Paciente] P