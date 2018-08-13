create view ViewClinicas as
select Clinica.Id, Clinica.Email, Clinica.Cnpj, Clinica.StatusAtividade,
Clinica.Nome, Clinica.Estado, Clinica.Cidade, Clinica.Logradouro,
Clinica.Numero, Clinica.Complemento
FROM Clinica
