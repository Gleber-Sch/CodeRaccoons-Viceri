﻿CREATE VIEW ViewAtendimentos as
select  Atendimento.Id, Atendimento.IdMedico, Medico.Nome as medico, Medico.Crm,
Atendimento.IdClinica, Clinica.Nome as Clinica
From Atendimento
inner Join Medico on Atendimento.IdMedico = Medico.Id
inner join Clinica on Atendimento.IdClinica = Clinica.Id