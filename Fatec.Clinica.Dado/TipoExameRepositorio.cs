using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Abstracao;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para a TipoExame.
    /// </summary>
    public class TipoExameRepositorio : IRepositorioBase<TipoExame>
    {
        /// <summary>
        /// Seleciona todas os tipos de exames do Database.
        /// </summary>
        /// <returns>Lista de tipos de exames.</returns>
        public IEnumerable<TipoExame> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<TipoExame>("SELECT * FROM [TipoExame]");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona um tipo de exame no Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar um tipo de exame no Database.</param>
        /// <returns>Tipo de exame selecionado.</returns>
        public TipoExame SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<TipoExame>($"SELECT * " +
                                                                    $"FROM [TipoExame] " +
                                                                    $"WHERE Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona um tipo de exame no Database através do nome especificado.
        /// </summary>
        /// <param name="nome">Usado para buscar um tipo de exame no Database.</param>
        /// <returns>Tipo de exame selecionado.</returns>
        public TipoExame SelecionarPorNome(string nome)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<TipoExame>($"SELECT * " +
                                                                    $"FROM [TipoExame] " +
                                                                    $"WHERE Nome = '{nome}' ");
                return obj;
            }
        }

        /// <summary>
        /// Insere um tipo de exame no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do tipo de exame.</param>
        /// <returns>Retorna o ID do tipo de exame inserido no Database.</returns>
        public int Inserir(TipoExame entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [TipoExame] " +
                                                   $"(Nome) " +
                                                   $"VALUES ('{entity.Nome}')" +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do tipo de exame no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do tipo de exame.</param>
        public void Alterar(TipoExame entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [TipoExame] " +
                                   $"SET  Nome = '{entity.Nome}' " +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta o tipo de exame do Database.
        /// </summary>
        /// <param name="id">Usado para selecionar o tipo de exame no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [TipoExame] " +
                                   $"WHERE Id = {id}");
            }
        }
    }
}
