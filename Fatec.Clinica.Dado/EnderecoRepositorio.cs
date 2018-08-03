using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funçoes de CRUD para o Endereço
    /// </summary>
    class EnderecoRepositorio : IRepositorioBase<Endereco>
    {
        /// <summary>
        /// Seleciona todos as consultas do Database.
        /// </summary>
        /// <returns>Lista de consultas.</returns>
        public IEnumerable<Endereco> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Endereco>($"SELECT *FROM ViewEnderecos");

                return lista;
            }
        }

        /// <summary>
        /// Seleciona uma o Endereco da Database, através do Id.
        /// </summary>
        /// <returns>Endereco seleciona.</returns>
        public Endereco SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Endereco>($"SELECT *FROM ViewEnderecos"+
                                                                   $"where id={id}");

                return obj;
            }
        }

        /// <summary>
        /// Insere um Endereco no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Endereco.</param>
        /// <returns>ID do Endereco inserido no Database.</returns>
        public int Inserir(Endereco entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Endereco] " +
                                                   $"(Estado, Cidade, Bairro, Logradouro, Numero, Complemento,IdClinica) " +
                                                   $"VALUES ('{entity.Estado}'," +
                                                   $" '{entity.Cidade}'," +
                                                   $" '{entity.Bairro}'," +
                                                   $" '{entity.Logradouro}'" +
                                                   $" '{entity.Numero}'" +
                                                   $" '{entity.Complemento}'" +
                                                   $"  {entity.Clinica.Id}" +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do endereco na Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do endereco.</param>
        public void Alterar(Endereco entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Endereco] " +
                                   $"SET Estado = '{entity.Estado}'," +
                                   $"Cidade = '{entity.Cidade}'," +
                                   $"Bairro = '{entity.Bairro}', " +
                                   $"Logradouro = '{entity.Logradouro}' " +
                                   $"Numero = '{entity.Numero}'" +
                                   $"Complemento = '{entity.Complemento}'" +
                                   $"IdClinica ='{entity.Clinica.Id}'" +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta um endereco do Database
        /// </summary>
        /// <param name="id">Usado para Deletar um endereco no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Endereco] " +
                                   $"WHERE Id = {id}");
            }
        }

    }

}
