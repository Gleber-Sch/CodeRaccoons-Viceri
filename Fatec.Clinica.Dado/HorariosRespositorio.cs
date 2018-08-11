using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;
using System;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funcao de CRUD para os Horarios
    /// </summary>
    public class HorariosRepositorio : IRepositorioBase<Horario>
    {
 

        /// <summary>
        /// Seleciona todos os Horarios.
        /// </summary>
        /// <returns>Lista de Horarios.</returns>
        public IEnumerable<Horario> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Horario>($"Select *from ViewHorarios");
                return lista;
            }

        }

        /// <summary>
        /// Seleciona todos os Horarios por Id Da Clinica.
        /// </summary>
        /// <param name="Id">Seleciona Horario por Id</param>
        /// <returns>Lista de Horarios.</returns>
        public Horario SelecionarPorId(int Id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var Obj = connection.QueryFirstOrDefault<Horario>($"Select *from ViewHorarios " +
                                                       $"Where IdClinica = {Id}");

                return Obj;
            }

        }

        /// <summary>
        /// Seleciona todos os Horarios por Dia.
        /// </summary>
        /// <param name="DiaHora">datetime para selecionar o horario atraves do dia
        /// </param>
        /// <returns>Lista de Horarios.</returns>
        public Horario SelecionarPorDia(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var Obj = connection.QueryFirstOrDefault<Horario>($"Select *from ViewHorarios " +
                                                       $"Where Horarios.DiaHora='{DiaHora.Day}'");

                return Obj;
            }

        }

        /// <summary>
        /// Seleciona todos os Horarios por Horario.
        /// </summary>
        /// <param name="DiaHora">Datetime para selecionar horarios atraves do horario</param>
        /// <returns>Lista de Horarios.</returns>
        public Horario SelecionarPorHorario(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Horario>($"Select *from ViewHorarios " +
                                                       $"Where Horarios.DiaHora = '{DiaHora.Hour}'");

                return obj;
            }

        }

        /// <summary>
        /// Seleciona todos os Horarios pela Clinica.
        /// </summary><param name="IdClinica">Seleciona horario por IdClinica </param>
        /// <returns>Lista de Horarios.</returns>
        public Horario SelecionarPorClinica(int Id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Horario>($"Select *from ViewHorarios " +
                                                       $"Where Horarios.IdClinica = {Id}");

                return obj;
            }

        }

        /// <summary>
        /// Insere um Horario no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Horario.</param>
        /// <returns>ID do Horario inserido no Database.</returns>
        public int Inserir(Horario entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Horarios] " +
                                                   $"(Horario, Dia, IdClinica) " +
                                                   $"VALUES ('{entity.DiaHora}'," +
                                                   $"{entity.IdClinica} " +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do Horario no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Horario.</param>
        public void Alterar(Horario entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Horarios] " +
                                   $"SET IdClinica= {entity.IdClinica}," +
                                   $"Horario = '{entity.DiaHora}' " +
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


            /// <summary>
            /// Deleta um Horario do Database
            /// </summary>
            /// <param name="DiaHora">Datetime para deletar um horario atraves do dia e do horario</param>
            public void Deletar(DateTime DiaHora)
            {
                using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
                {
                    connection.Execute($"DELETE " +
                                       $"FROM [Horarios] " +
                                       $"WHERE DiaHora = '{DiaHora}'");
                }



            }


        }
   
}