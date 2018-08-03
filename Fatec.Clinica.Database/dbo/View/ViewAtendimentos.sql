CREATE VIEW ViewAtendimentos as
select  Atendimento.Id,Medico.Nome as medico, Clinica.Nome as Clinica
From Atendimento
inner Join Medico on Atendimento.IdMedico=Medico.Id
inner join Clinica on Atendimento.IdClinica=Clinica.Id
