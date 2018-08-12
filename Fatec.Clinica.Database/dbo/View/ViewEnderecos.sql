create view ViewEnderecos as
select Endereco.Id as IdEndereco, Clinica.Nome, Endereco.estado,
Endereco.Cidade, Endereco.Bairro, Endereco.Logradouro,
Endereco.Numero, Endereco.Complemento
FROM Endereco
INNER JOIN Clinica on Endereco.Id = Clinica.IdEndereco