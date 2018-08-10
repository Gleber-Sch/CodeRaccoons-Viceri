using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funcao de CRUD para os Horarios
    /// </summary>
    public class HorariosRepositorio : IRepositorioBase<Horarios>
    {
 

        /// <summary>
        /// Seleciona todos os Horarios.
        /// </summary>
        /// <returns>Lista de Horarios.</returns>
        public IEnumerable<Horarios> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Horarios>($"Select *from ViewHorarios");

                return lista;
            }

        }

        /// <summary>
        /// Seleciona todos os Horarios por Id Da Clinica.
        /// </summary>
        /// <returns>Lista de Horarios.</returns>
        public Horarios SelecionarPorId(int Id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var Obj = connection.QueryFirstOrDefault<Horarios>($"Select *from ViewHorarios " +
                                                       $"Where IdClinica = {Id}");

                return Obj;
            }

        }

        /// <summary>
        /// Seleciona todos os Horarios por Dia.
        /// </summary>
        /// <returns>Lista de Horarios.</returns>
        public IEnumerable<Horarios> SelecionarPorDia(string Dia)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Horarios>($"Select *from ViewHorarios " +
                                                       $"Where Horarios.Dia={Dia}");

                return lista;
            }

        }

        /// <summary>
        /// Seleciona todos os Horarios por Horario.
        /// </summary>
        /// <returns>Lista de Horarios.</returns>
        public IEnumerable<Horarios> SelecionarPorHorario(string Horario)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Horarios>($"Select *from ViewHorarios " +
                                                       $"Where Horarios.Horario = {Horario}");

                return lista;
            }

        }

        /// <summary>
        /// Insere um Horario no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Horario.</param>
        /// <returns>ID do Horario inserido no Database.</returns>
        public int Inserir(Horarios entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Horarios] " +
                                                   $"(Horario, Dia, IdClinica) " +
                                                   $"VALUES ('{entity.Horario}'," +
                                                   $" '{entity.Dia}'," +
                                                   $"{entity.IdClinica} " +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do Horario no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Horario.</param>
        public void Alterar(Horarios entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Horarios] " +
                                   $"SET IdClinica= {entity.IdClinica}," +
                                   $"Dia= '{entity.Dia}', " +
                                   $"Horario = '{entity.Horario}' " +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta um Horario do Database
        /// </summary>
        /// <param name="id">Usado para selecionar o Horario no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Horarios] " +
                                   $"WHERE Id = {id}");
            }


        }
    }
}