using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funcao de CRUD para o Atendimento
    /// </summary>
    public class AtendimentoRepositorio : IRepositorioBase<Atendimento>
    {
        /// <summary>
        /// Seleciona todos os Atendimentos do Database.
        /// </summary>
        /// <returns>Lista de Atendimentos.</returns>
        public IEnumerable<Atendimento> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Atendimento>($"Select *from ViewAtendimentos");

                return lista;
            }

        }

        /// <summary>
        /// Seleciona uma Atendimento do Database, através do Id.
        /// </summary>
        /// <returns>Atendimento seleciona.</returns>
        public Atendimento SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Atendimento>($"Select *From ViewAtendimentos"+
                                                                      $"where id={id}");

                return obj;
            }
        }

        /// <summary>
        /// Insere um Atendimento no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Atendimento.</param>
        /// <returns>ID do atendimento inserido no Database.</returns>
        public int Inserir(Atendimento entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Atendimento] " +
                                                   $"(IdClinica, IdMedico) " +
                                                   $"VALUES ({entity.Clinica.Id}," +
                                                   $" {entity.Medico.Id}," +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do Atendimento no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Atendimento.</param>
        public void Alterar(Atendimento entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Consulta] " +
                                   $"SET IdClinica= {entity.Clinica.Id}," +
                                   $"IdMedico= {entity.Medico.Id} " +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta um Atendimento do Database
        /// </summary>
        /// <param name="id">Usado para selecionar o Atendimento no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Atendimento] " +
                                   $"WHERE Id = {id}");
            }
        }

    }
}
