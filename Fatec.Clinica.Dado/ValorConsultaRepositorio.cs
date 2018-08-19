using Dapper;
using Fatec.Clinica.Dado.Abstracao;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para o ValorConsulta.
    /// </summary>
    public class ValorConsultaRepositorio : IRepositorioBase<ValorConsulta>
    {
        /// <summary>
        /// Seleciona todas os valores de consulta do Database.
        /// </summary>
        /// <returns>Lista de valores de consulta.</returns>
        public IEnumerable<ValorConsulta> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<ValorConsulta>("SELECT * FROM [ValorConsulta]");

                return lista;
            }
        }

        /// <summary>
        /// Seleciona um valor de consulta no Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar o valor da consulta no Database.</param>
        /// <returns>Valor de consulta selecionado.</returns>
        public ValorConsulta SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<ValorConsulta>($"SELECT * " +
                                                                        $"FROM [ValorConsulta] " +
                                                                        $"WHERE Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Insere um valor de consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do ValorConsulta.</param>
        /// <returns>Retorna o ID do ValorConsulta inserido no Database.</returns>
        public int Inserir(ValorConsulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [ValorConsulta] " +
                                                   $"(IdEspecialidade, IdClinica, Valor) " +
                                                   $"VALUES ({entity.IdEspecialidade}, " +
                                                   $"{entity.IdClinica}, " +
                                                   $"{entity.Valor}) " +
                                                   $"SET @ID = SCOPE_IDENTITY(); " +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados do valor da consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do valor da consulta.</param>
        public void Alterar(ValorConsulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [ValorConsulta] " +
                                   $"SET  IdEspecialidade = {entity.IdEspecialidade}, " +
                                   $"IdClinica = {entity.IdClinica}, " +
                                   $"Valor = {entity.Valor} " +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta o valor de consulta do Database.
        /// </summary>
        /// <param name="id">Usado para selecionar o valor de consulta no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [ValorConsulta] " +
                                   $"WHERE Id = {id}");
            }
        }
    }
}
