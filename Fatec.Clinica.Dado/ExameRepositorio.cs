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
        /// <returns>Lista com todos os exames.</returns>
        public IEnumerable<Exame> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Exame>("SELECT * FROM [ViewExame]");
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
                                                                $"FROM [ViewExame] " +
                                                                $"WHERE Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona exames no Database através do ID do Paciente especificado.
        /// </summary>
        /// <param name="id">Usado para buscar o Paciente no Database.</param>
        /// <returns>Lista de exames realizados por um paciente.</returns>
        public IEnumerable<Exame> SelecionarPorPaciente(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Exame>($"SELECT * " +
                                                    $"FROM [ViewExame] " +
                                                    $"WHERE IdPaciente = {id}");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona exames no Database através do ID do médico que os solicitou.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        /// <returns>Lista de exames solicitados por um médico.</returns>
        public IEnumerable<Exame> SelecionarPorMedicoQueSolicitou(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Exame>($"SELECT * " +
                                                    $"FROM [ViewExame] " +
                                                    $"WHERE IdMedicoQueSolicitou = {id}");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona exames no Database através do ID do médico que os realizou.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        /// <returns>Lista de exames realizados por um médico.</returns>
        public IEnumerable<Exame> SelecionarPorMedicoQueRealizou(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Exame>($"SELECT * " +
                                                    $"FROM [ViewExame] " +
                                                    $"WHERE IdMedicoQueRealizou = {id}");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona exames no Database através do ID da clínica onde eles foram realizados.
        /// </summary>
        /// <param name="id">Usado para buscar a clínica no Database.</param>
        /// <returns>Lista de exames realizados em uma clínica.</returns>
        public IEnumerable<Exame> SelecionarPorClinica(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Exame>($"SELECT * " +
                                                    $"FROM [ViewExame] " +
                                                    $"WHERE IdClinica = {id}");
                return lista;
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
                                                   $"VALUES ('{entity.DataHora}', " +
                                                   $"{entity.IdTipoExame}, " +
                                                   $"{entity.IdAtendimento}, " +
                                                   $"{entity.IdConsulta})" +
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
                                   $"SET  DataHora = '{entity.DataHora}', " +
                                   $"IdTipoExame = {entity.IdTipoExame}, " +
                                   $"IdAtendimento = {entity.IdAtendimento}, " +
                                   $"IdConsulta = {entity.IdConsulta} " +
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
