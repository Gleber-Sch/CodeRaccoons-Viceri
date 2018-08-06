create view ViewClinicas as
select Clinica.Id , Clinica.Cnpj, Clinica.StatusAtividade,
Clinica.Nome, endereco.estado, Endereco.Cidade, Endereco.Logradouro,
Endereco.Numero, Endereco.Complemento
from Clinica
inner join Endereco on Endereco.IdClinica=Clinica.Id
