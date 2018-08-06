using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Abstracao;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para a especialidade.
    /// </summary>
    public class EspecialidadeRepositorio : IRepositorioBase<Especialidade>
    {
        /// <summary>
        /// Seleciona todas as especialidades do Database.
        /// </summary>
        /// <returns>Lista de Especialidades.</returns>
        public IEnumerable<Especialidade> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Especialidade>("SELECT * FROM [Especialidade]");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona uma especialidade no Database através do nome especificado.
        /// </summary>
        /// <param name="nome">Usado para buscar uma especialidade no Database.</param>
        /// <returns>Especialidade selecionada</returns>
        public Especialidade SelecionarPorNome(string nome)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Especialidade>($"SELECT * " +
                                                                  $"FROM [Especialidade] " +
                                                                  $"WHERE (Nome) = ('{nome}')");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona uma especialidade no Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar uma especialidade no Database.</param>
        /// <returns>Especialidade selecionada</returns>
        public Especialidade SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Especialidade>($"SELECT * " +
                                                                  $"FROM [Especialidade] " +
                                                                  $"WHERE Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Insere uma especialidade no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da especialidade.</param>
        /// <returns>Retorna o ID da especialidade inserida no Database.</returns>
        public int Inserir(Especialidade entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                              $"INSERT INTO [Especialidade] " +
                                              $"(Nome) " +
                                              $"VALUES ('{entity.Nome}')" +
                                              $"SET @ID = SCOPE_IDENTITY();" +
                                              $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados da especialidade no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da especialidade.</param>
        public void Alterar(Especialidade entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Especialidade] " +
                                   $"SET  Nome = '{entity.Nome}'" +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta a especialidade do Database.
        /// </summary>
        /// <param name="id">Usado para selecionar a especialidade no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Especialidade] " +
                                   $"WHERE Id = {id}");
            }
        }
    }
}
