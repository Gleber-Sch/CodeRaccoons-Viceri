/*Cria view para listar todas as clinicas cadastradas no sistema.
*/


CREATE view ViewClinicas as
select Clinica.Id , Clinica.Cnpj, Clinica.StatusAtividade,
Clinica.Nome, endereco.Estado, Endereco.Cidade, Endereco.Logradouro,
Endereco.Numero, Endereco.Complemento
from Clinica
inner join endereco on endereco.IdClinica=Clinica.Id
