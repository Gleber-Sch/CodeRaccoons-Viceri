using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Abstracao;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;

namespace Fatec.Clinica.Dado
{

    /// <summary>
    /// Funções de CRUD para o Valor do Exame.
    /// </summary>
    public class ValorExameRepositorio : IRepositorioBase<ValorExame>
    {
        /// <summary>
        /// Seleciona todas os valores dos exames do Database.
        /// </summary>
        /// <returns>Lista com todos os exames.</returns>
        public IEnumerable<ValorExame> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<ValorExame>("SELECT * FROM [ValorExame]");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona um valor de exame no Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar um valor de exame no Database.</param>
        /// <returns>Exame selecionado.</returns>
        public ValorExame SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<ValorExame>($"SELECT * FROM [ValorExame] " +
                                                                     $"WHERE Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Insere um valor de exame no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do valor de exame.</param>
        /// <returns>Retorna o ID do valor de exame inserido no Database.</returns>
        public int Inserir(ValorExame entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int; " +
                                                   $"INSERT INTO [ValorExame] " +
                                                   $"(IdTipoExame, IdClinica, Valor) " +
                                                   $"VALUES ({entity.IdClinica}, " +
                                                   $"{entity.IdTipoExame}, " +
                                                   $"{entity.Valor}) " +
                                                   $"SET @ID = SCOPE_IDENTITY(); " +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do valor de exame no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do valor de exame.</param>
        public void Alterar(ValorExame entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [ValorExame] " +
                                   $"SET IdTipoExame = {entity.IdTipoExame}, " +
                                   $"IdClinica = {entity.IdClinica}, " +
                                   $"Valor = {entity.Valor} " +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta o valor de exame do Database.
        /// </summary>
        /// <param name="id">Usado para selecionar o valor de exame no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [ValorExame] " +
                                   $"WHERE Id = {id}");
            }
        }
    }
}