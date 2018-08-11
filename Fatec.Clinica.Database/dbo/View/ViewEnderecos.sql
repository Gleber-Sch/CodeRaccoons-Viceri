/*Cria View para mostrar todos os endereços cadastrados no banco de dados
*/



CREATE view ViewEnderecos as
select Endereco.Id as IdEndereco, Clinica.Nome, Endereco.estado,
Endereco.Cidade, Endereco.Bairro, Endereco.Logradouro,
Endereco.Numero, Endereco.Complemento
FROM Endereco
INNER JOIN Clinica on Endereco.IdClinica = Clinica.Id