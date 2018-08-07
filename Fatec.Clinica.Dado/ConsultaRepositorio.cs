using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para a Consulta.
    /// </summary>
    public class ConsultaRepositorio : IRepositorioBase<Consulta>
    {

        /// <summary>
        /// Seleciona todos as consultas do Database.
        /// </summary>
        /// <returns>Lista de consultas.</returns>
        public IEnumerable<Consulta> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Consulta>($"SELECT * FROM [ViewConsultas]");

                return lista;
            }
        }

        /// <summary>
        /// Seleciona consultas no Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar a consulta no Database.</param>
        /// <returns>Consultas selecionados.</returns>
        public Consulta SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Consulta>($"SELECT * FROM [ViewConsultas] " +
                                                                   $"WHERE C.Id = {id} ");

                return obj;
            }
        }

        /// <summary>
        /// Seleciona uma a consulta do Database através do Id do paciente.
        /// </summary>
        /// <param name="id">Usado para buscar o paciente no Database.</param>
        /// <returns>Consulta seleciona.</returns>
        public Consulta SelecionarPorPaciente(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Consulta>($"SELECT * FROM [ViewConsultas] " +
                                                                   $"WHERE IdPaciente = {id} ");

                return obj;
            }
        }

        /// <summary>
        /// Seleciona uma a consulta do Database através do Id do médico.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        /// <returns>Consulta seleciona.</returns>
        public Consulta SelecionarPorMedico(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Consulta>($"SELECT * FROM [ViewConsultas] " +
                                                                   $"WHERE IdMedico = {id} ");

                return obj;
            }
        }

        /// <summary>
        /// Seleciona uma a consulta do Database através do Id da clinica.
        /// </summary>
        /// <param name="id">Usado para buscar a clinica no Database.</param>
        /// <returns>Consulta seleciona.</returns>
        public Consulta SelecionarPorClinica(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Consulta>($"SELECT * FROM [ViewConsultas] " +
                                                                   $"WHERE IdClinica = {id} ");

                return obj;
            }
        }

        /// <summary>
        /// Insere uma consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da consulta.</param>
        /// <returns>ID da consulta inserido no Database.</returns>
        public int Inserir(Consulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Consulta] " +
                                                   $"(Historico, DataHora, IdPaciente, IdAtendimento, Nota) " +
                                                   $"VALUES ('{entity.Historico}'," +
                                                   $" '{entity.DataHora}'," +
                                                   $" {entity.Paciente.Id}," +
                                                   $" {entity.Atendimento.Id}" +
                                                   $" {entity.Nota} " +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados da consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da consulta.</param>
        public void Alterar(Consulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Consulta] " +
                                   $"SET Historico = '{entity.Historico}'," +
                                   $"DataHora = '{entity.DataHora}'," +
                                   $"IdPaciente = {entity.Paciente.Id}, " +
                                   $"IdAtendimento = {entity.Atendimento.Id} " +
                                   $"Nota = {entity.Nota} " +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta uma Consulta do Database
        /// </summary>
        /// <param name="id">Usado para Deletar um consulta no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Consulta] " +
                                   $"WHERE Id = {id}");
            }
        }
    }

}
