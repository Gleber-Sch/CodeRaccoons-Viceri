using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;
using Fatec.Clinica.Dominio;

namespace Fatec.Clinica.Dado
{   
    /// <summary>
    /// Funcao de CRUD para a Clinica
    /// </summary>
     public class ClinicaRepositorio : IRepositorioBase<Clinicas>
    {
        /// <summary>
        /// Seleciona todos as consultas do Database.
        /// </summary>
        /// <returns>Lista de consultas.</returns>
        public IEnumerable<Clinicas> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Clinicas>($"SELECT *From ViewClinicas");

                return lista;
            }
        }

        /// <summary>
        /// Seleciona uma clinica do Database, através do Id.
        /// </summary>
        /// <returns>clinica seleciona.</returns>
        public Clinicas SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Clinicas>($"SELECT *FROM ViewClinicas," +
                                                                   $"WHERE C.Id = {id} ");

                return obj;
            }
        }

        /// <summary>
        /// Insere uma Clinica no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da Clinica.</param>
        /// <returns>ID da Clinica inserido no Database.</returns>
        public int Inserir(Clinicas entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Clinica] " +
                                                   $"(Cnpj, StatusAtividade, TelefoneCom, Nome) " +
                                                   $"VALUES ('{entity.Cnpj}'," +
                                                   $" '{entity.StatusAtividade}'," +
                                                   $" '{entity.TelefoneCom}'" +
                                                   $" '{entity.Nome}'" +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados da Clinica no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da consulta.</param>
        public void Alterar(Clinicas entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Clinica] " +
                                   $"SET Cnpj = '{entity.Cnpj}'," +
                                   $"StatusAtividade = '{entity.StatusAtividade}', " +
                                   $"TelefoneCom = '{entity.TelefoneCom}' " +
                                   $"Nome = '{entity.Nome}'" +
                                   $"WHERE Id = {entity.Id}");
            }
        }
        /// <summary>
        /// Deleta uma Clinica do Database
        /// </summary>
        /// <param name="id">Usado para Deletar uma Clinica no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Clinica] " +
                                   $"WHERE Id = {id}");
            }
        }

    }
}
