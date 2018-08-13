create view ViewClinicas as
select Clinica.Id, Clinica.Email, Clinica.Cnpj, Clinica.StatusAtividade,
Clinica.Nome, Endereco.Estado, Endereco.Cidade, Endereco.Logradouro,
Endereco.Numero, Endereco.Complemento
from Clinica
inner join Endereco on Clinica.IdEndereco = Endereco.Id
