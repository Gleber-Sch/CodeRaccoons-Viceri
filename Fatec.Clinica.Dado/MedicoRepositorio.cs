using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para o Médico.
    /// </summary>
    public class MedicoRepositorio : IRepositorioBase<Medico>
    {
        /// <summary>
        /// Seleciona todos os médicos do Database.
        /// </summary>
        /// <returns>Lista de médicos.</returns>
        public IEnumerable<Medico> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Medico>($"SELECT M.Id, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Celular," +
                                                     $" M.Email, M.DataNasc, M.StatusAtividade, M.Genero , E.Nome As Especialidade " +
                                                     $"FROM [Medico] M " +
                                                     $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id");
                return lista;
            }
        }


        /// <summary>
        /// Seleciona um médico no Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar um médico no Database.</param>
        /// <returns>Médico selecionado.</returns>
        public Medico SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Medico>($"SELECT M.Id,  M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Celular," +
                                                                 $" M.Email, M.DataNasc, M.StatusAtividade, M.Genero , E.Nome As Especialidade " +
                                                                 $"FROM [Medico] M " +
                                                                 $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id " +
                                                                 $"WHERE M.Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona todos médicos, no Database, que possuem a especialidade indicada pelo parâmetro ID.
        /// </summary>
        /// <param name="id">Usado para buscar o ID da especialidade.</param>
        /// <returns>Lista de médicos que possuem a especialidade indicado como parâmetro.</returns>
        public IEnumerable<Medico> SelecionarPorEspecialidade(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Medico>($"SELECT M.Id, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Celular," +
                                                                    $" M.Email, M.DataNasc, M.StatusAtividade, M.Genero , E.Nome As Especialidade " +
                                                                    $"FROM [Medico] M " +
                                                                    $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id " +
                                                                    $"WHERE E.Id = {id}");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona um médico no Database através do CRM especificado.
        /// </summary>
        /// <param name="crm">Usado para buscar um médico no Database.</param>
        /// <returns>Médico selecionado.</returns>
        public Medico SelecionarPorCrm(int crm)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Medico>($"SELECT M.Id, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Celular, " +
                                                                  $" M.Email, M.DataNasc, M.StatusAtividade, M.Genero , E.Nome As Especialidade " +
                                                                  $"FROM [Medico] M " +
                                                                  $"JOIN [ESPECIALIDADE] E ON M.IdEspecialidade = E.Id " +
                                                                  $"WHERE Crm = {crm}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona um médico no Database através do CPF especificado.
        /// </summary>
        /// <param name="cpf">Usado para buscar um médico no Database.</param>
        /// <returns>Médico selecionado.</returns>
        public Medico SelecionarPorCpf(string cpf)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Medico>($"SELECT M.Id, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Celular, " +
                                                                 $" M.Email, M.DataNasc, M.StatusAtividade, M.Genero , E.Nome As Especialidade " +
                                                                 $"FROM [Medico] M " +
                                                                 $"JOIN [ESPECIALIDADE] E ON M.IdEspecialidade = E.Id " +
                                                                 $"WHERE Cpf = '{cpf}'");
                return obj;
            }
        }

        public Medico SelecionarPorEmail(string email)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Medico>($"SELECT M.Id, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Celular," +
                                                                 $"M.Email, M.DataNasc, M.StatusAtividade, M.Genero , E.Nome As Especialidade" +
                                                                 $"FROM [Medico] M" +
                                                                 $"FROM [Medico] M " +
                                                                 $"JOIN [ESPECIALIDADE] E ON M.IdEspecialidade = E.Id " +
                                                                 $"WHERE Email = '{email}'");
                return obj;
            }
        }

        /// <summary>
        /// Insere um médico no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do médico.</param>
        /// <returns>ID do médico inserido no Database.</returns>
        public int Inserir(Medico entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                              $"INSERT INTO [Medico] " +
                                              $"(IdEspecialidade, Nome, Cpf, Crm, Celular, Email," +
                                              $" DataNasc, StatusAtividade, Genero) " +
                                              $"VALUES ({entity.IdEspecialidade}," +
                                                     $"'{entity.Nome}', " +
                                                     $"'{entity.Cpf}', " +
                                                     $"{entity.Crm}, " +
                                                     $"'{entity.Celular}', " +
                                                     $"'{entity.Email}', " +
                                                     $"'{entity.DataNasc.ToString("dd/MM/yyyy")}', " +
                                                     $" '{entity.StatusAtividade}', " +
                                                     $"'{entity.Genero}') " +
                                              $"SET @ID = SCOPE_IDENTITY();" +
                                              $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do médico no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do médico.</param>
        public void Alterar(Medico entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Medico] " +
                                   $"SET  IdEspecialidade = {entity.IdEspecialidade}," +
                                   $"Nome = '{entity.Nome}', " +
                                   $"Cpf = '{entity.Cpf}', " +
                                   $"Crm = {entity.Crm}, " +
                                   $"Celular = '{entity.Celular}', " +
                                   $"Email = '{entity.Email}', " +
                                   $"DataNasc = '{entity.DataNasc.ToString("dd/MM/yyyy")}', " +
                                   $"StatusAtividade = '{entity.StatusAtividade}', " +
                                   $"Genero = '{entity.Genero}' " +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta um médico do Database.
        /// </summary>
        /// <param name="id">Usado para selecionar o médico no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Medico] " +
                                   $"WHERE Id = {id}");
            }
        }
    }
}
