using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;
using System;
using System.Linq;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para o HorarioConsulta.
    /// </summary>
    public class HorariosConsultaRepositorio : IRepositorioBase<HorariosConsulta>
    {
        /// <summary>
        /// Seleciona todos os HorariosConsulta do Database.
        /// </summary>
        /// <returns>Lista de HorariosConsulta.</returns>
        public IEnumerable<HorariosConsulta> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<HorariosConsulta>($"SELECT *FROM ViewHorariosConsulta");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona um Horario Consulta no Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar um Horario Consulta no Database.</param>
        /// <returns>Horario Consulta selecionado.</returns>
        public HorariosConsulta SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<HorariosConsulta>($"SELECT * FROM ViewHorariosConsulta " +
                                                                 $"WHERE Id = {id}");
                return obj;
            }
        }



        /// <summary>
        /// Seleciona um Horario de consulta no Database através do ID especificado.
        /// </summary>
        /// <param name="Dia">Usado para buscar um Horarios de consulta no Database.</param>
        /// <returns>HorarioConsulta selecionado.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorDia(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosConsulta>($"SELECT * " +
                                                                $"FROM [ViewHorariosConsulta] " +
                                                                $"WHERE Dia = '{DiaHora.ToShortDateString()}'");
                return obj;
            }
        }

        public List<HorariosConsulta> SelecionarPorDiaNegocio(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosConsulta>($"SELECT * " +
                                                                $"FROM [ViewHorariosConsulta] " +
                                                                $"WHERE Dia = '{DiaHora.ToShortDateString()}'");
                return obj.ToList();
            }
        }

        /// <summary>
        /// Seleciona um Horario de consulta no Database através do ID especificado.
        /// </summary>
        /// <param name="Hora">Usado para buscar um Horarios de consulta no Database.</param>
        /// <returns>HorarioConsulta selecionado.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorHora(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosConsulta>($"SELECT * " +
                                                                $"FROM [ViewHorariosConsulta] " +
                                                                $"WHERE Hora = '{DiaHora.ToShortTimeString()}'");
                return obj;
            }
        }

        public List<HorariosConsulta> SelecionarPorHoraNegocio(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosConsulta>($"SELECT * " +
                                                                $"FROM [ViewHorariosConsulta] " +
                                                                $"WHERE Hora = '{DiaHora.ToShortTimeString()}'");
                return obj.ToList();
            }
        }

        /// <summary>
        /// Seleciona um Horario de consulta no Database através do ID especificado.
        /// </summary>
        /// <param name="Valor">Usado para buscar um Horarios de consulta no Database.</param>
        /// <returns>HorarioConsulta selecionado.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorValor(Decimal Valor)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosConsulta>($"SELECT * " +
                                                                $"FROM [ViewHorariosConsulta] " +
                                                                $"WHERE Valor = {Valor}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona um Horario de Consulta no Database através do IdAtendimento especificado.
        /// </summary>
        /// <param name="IdClinica">Usado para buscar Horarios de Consulta no Database.</param>
        /// <returns>HorariosConsulta selecionado.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorIdAtendimento(int IdAtendimento)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosConsulta>($"SELECT * " +
                                                                $"FROM [ViewHorariosConsulta] " +
                                                                $"WHERE IdAtendimento = {IdAtendimento}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona um Horario de Consulta no Database através do IdClinica especificado.
        /// </summary>
        /// <param name="IdClinica">Usado para buscar Horarios de Consulta no Database.</param>
        /// <returns>HorariosConsulta selecionado.</returns>
        public IEnumerable<HorariosConsulta> SelecionarPorNomeClinica(string Nome)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosConsulta>($"SELECT * " +
                                                                $"FROM [ViewHorariosConsulta] " +
                                                                $"where NomeClinica = '{Nome}'");
                return obj;
            }
        }

        /// <summary>
        /// Insere um Horario de consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Horario de consulta.</param>
        /// <returns>ID do Horario de consulta inserido no Database.</returns>
        public int Inserir(HorariosConsulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                              $"INSERT INTO [HorariosConsulta] " +
                                              $"(Dia, Hora, Valor, IdAtendimento) " +
                                                    $"VALUES ('{entity.DiaHora.ToString("dd/MM/yyyy")}'," +
                                                            $"'{entity.DiaHora.ToString("HH:mm")}'," +
                                                            $"{entity.Valor}," +
                                                            $"{entity.IdAtendimento}) " +
                                              $"SET @ID = SCOPE_IDENTITY();" +
                                              $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados de um Horario de Consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Horario de consulta.</param>
        public void Alterar(HorariosConsulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [HorariosConsulta] " +
                    $"SET Dia ='{entity.DiaHora.ToShortDateString()}', " +
                    $"Hora = '{entity.DiaHora.ToString("HH:mm")}', " +
                    $"Valor = {entity.Valor}, " +
                    $"IdAtendimento = {entity.IdAtendimento} " +
                    $"WHERE Id={entity.Id}");
            }

        }

        /// <summary>
        /// Deleta um horario Consulta do Database.
        /// </summary>
        /// <param name="id">Usado para selecionar o HorarioConsulta no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [HorariosConsulta] " +
                                   $"WHERE Id = {id}");
            }
        }

    }
}