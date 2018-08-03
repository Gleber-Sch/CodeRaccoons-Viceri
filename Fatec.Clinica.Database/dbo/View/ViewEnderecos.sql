create view ViewEnderecos as
select Clinica.Nome, Endereco.estado, Endereco.Cidade, Endereco.Bairro,Endereco.Logradouro,Endereco.Numero,Endereco.Complemento
FROM Endereco
INNER JOIN Clinica on Endereco.IdClinica=Clinica.Id