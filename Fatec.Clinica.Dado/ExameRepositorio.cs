using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Abstracao;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;
namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para o Exame.
    /// </summary>
    public class ExameRepositorio : IRepositorioBase<Exame>
    {
        /// <summary>
        /// Seleciona todas os exames do Database.
        /// </summary>
        /// <returns>Lista de exames.</returns>
        public IEnumerable<Exame> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Exame>("SELECT * FROM [ViewExames]");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona um exame no Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar um exame no Database.</param>
        /// <returns>Exame selecionado.</returns>
        public Exame SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Exame>($"SELECT * " +
                                                                $"FROM [ViewExames] " +
                                                                $"WHERE Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona exames no Database através do ID do Paciente especificado.
        /// </summary>
        /// <param name="id">Usado para buscar o Paciente no Database.</param>
        /// <returns>Exames selecionados.</returns>
        public Exame SelecionarPorPaciente(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Exame>($"SELECT * " +
                                                                $"FROM [ViewExames] " +
                                                                $"WHERE IdPaciente = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona exames no Database através do ID do médico que os solicitou.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        /// <returns>Exames selecionados.</returns>
        public Exame SelecionarPorMedicoQueSolicitou(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Exame>($"SELECT * " +
                                                                $"FROM [ViewExames] " +
                                                                $"WHERE IdMedicoQueSolicitou = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona exames no Database através do ID do médico que os realizou.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        /// <returns>Exames selecionados.</returns>
        public Exame SelecionarPorMedicoQueRealizou(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Exame>($"SELECT * " +
                                                                $"FROM [ViewExames] " +
                                                                $"WHERE IdMedicoQueRealizou = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona exames no Database através do ID da clínica onde eles foram realizados.
        /// </summary>
        /// <param name="id">Usado para buscar a clínica no Database.</param>
        /// <returns>Exames selecionados.</returns>
        public Exame SelecionarPorClinica(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Exame>($"SELECT * " +
                                                                $"FROM [ViewExames] " +
                                                                $"WHERE IdClinica = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Insere um exame no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do exame.</param>
        /// <returns>Retorna o ID do exame inserido no Database.</returns>
        public int Inserir(Exame entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Exame] " +
                                                   $"(DataHora, IdTipoExame, IdAtendimento, IdConsulta) " +
                                                   $"VALUES ('{entity.DataHora}'," +
                                                   $"{entity.TipoExame.Id}," +
                                                   $"{entity.Atendimento.Id})," +
                                                   $"{entity.Consulta.Id}" +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do exame no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do exame.</param>
        public void Alterar(Exame entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Exame] " +
                                   $"SET  DataHora = '{entity.DataHora}'," +
                                   $"TipoExame = {entity.TipoExame.Id}" +
                                   $"IdAtendimento = {entity.Atendimento.Id}" +
                                   $"IdConsulta = {entity.Consulta.Id}" +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta o exame do Database.
        /// </summary>
        /// <param name="id">Usado para selecionar o exame no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Exame] " +
                                   $"WHERE Id = {id}");
            }
        }
    }
}
