using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para o Paciente.
    /// </summary>
    public class PacienteRepositorio : IRepositorioBase<Paciente>
    {

        /// <summary>
        /// Seleciona todos os pacientes do Database.
        /// </summary>
        /// <returns>Lista de Pacientes</returns>
        public IEnumerable<Paciente> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Paciente>($"SELECT P.Id, P.Nome, P.Cpf, P.Email, P.DataNasc," +
                                                       $"P.Genero, P.Celular, P.TelefoneRes " +
                                                       $"FROM [Paciente] P ");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona um paciente do Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar um paciente no Database.</param>
        /// <returns>Paciente selecionado.</returns>
        public Paciente SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Paciente>($"SELECT P.Id, P.Nome, P.Cpf, P.Email, " +
                                                                   $"P.DataNasc, P.Genero, P.Celular, P.TelefoneRes " +
                                                                   $"FROM [Paciente] P " +
                                                                   $"WHERE P.Id = {id}");

                return obj;
            }
        }

        /// <summary>
        /// Seleciona um paciente no Database através do CPF especificado.
        /// </summary>
        /// <param name="cpf">Usado para buscar um paciente no Database.</param>
        /// <returns>Paciente selecionado.</returns>
        public Paciente SelecionarPorCpf(string cpf)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Paciente>($"SELECT P.Id, P.Nome, P.Cpf, P.Email," +
                                                                 $"P.DataNasc, P.Genero, P.Celular, P.TelefoneRes " +
                                                                 $"FROM [Paciente] P " +
                                                                 $"WHERE Cpf = '{cpf}'");
                return obj;
            }
        }

        public Paciente SelecionarPorEmail(string email)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Paciente>($"SELECT P.Id, P.Nome, P.Cpf, P.Celular," +
                                                                 $"P.Email, P.DataNasc, P.Genero, P.TelefoneRes " +
                                                                 $"FROM [Paciente] P " +
                                                                 $"WHERE Email = '{email}'");
                return obj;
            }
        }

        /// <summary>
        /// Insere um paciente no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do paciente</param>
        /// <returns>ID do paciente inserido no Database.</returns>
        public int Inserir(Paciente entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Paciente] " +
                                                   $"(Nome, Cpf, Email, Senha, DataNasc, Genero, Celular, TelefoneRes) " +
                                                   $"VALUES ('{entity.Nome}'," +
                                                   $" '{entity.Cpf}'," +
                                                   $" '{entity.Email}'," +
                                                   $" '{entity.Senha}'," +
                                                   $" '{entity.DataNasc}'," +
                                                   $" '{entity.Genero}'," +
                                                   $" '{entity.Celular}'," +
                                                   $" '{entity.TelefoneRes}') " +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do paciente no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do paciente.</param>
        public void Alterar(Paciente entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Paciente] " +
                                   $"SET Nome = '{entity.Nome}'," +
                                   $"CPF = '{entity.Cpf}'," +
                                   $"Email = '{entity.Email}'," +
                                   $"DataNasc = '{entity.DataNasc}', " +
                                   $"Genero = '{entity.Genero}' " +
                                   $"Celular = '{entity.Celular}', " +
                                   $"TelefoneRes = '{entity.TelefoneRes}' " +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta um Paciente do Database
        /// </summary>
        /// <param name="id">Usado para selecionar o paciente no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Paciente] " +
                                   $"WHERE Id = {id}");
            }
        }

    }
}