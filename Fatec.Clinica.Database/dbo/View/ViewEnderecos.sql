create view ViewEnderecos as
select Endereco.Id, Clinica.Nome, Endereco.Estado,
Endereco.Cidade, Endereco.Bairro, Endereco.Logradouro,
Endereco.Numero, Endereco.Complemento
FROM Endereco
INNER JOIN Clinica on Endereco.Id = Clinica.IdEndereco