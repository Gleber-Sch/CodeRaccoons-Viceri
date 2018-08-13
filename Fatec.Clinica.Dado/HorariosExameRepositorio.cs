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
    /// Funções de CRUD para o HorarioExame.
    /// </summary>
    public class HorariosExameRepositorio : IRepositorioBase<HorariosExame>
    {
        /// <summary>
        /// Seleciona todos os HorariosExame do Database.
        /// </summary>
        /// <returns>Lista de HorariosExame.</returns>
        public IEnumerable<HorariosExame> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<HorariosExame>($"SELECT *FROM ViewHorariosExame");
                return lista;
            }
        }

        /// <summary>
        /// Seleciona um Horario Exame no Database através do ID especificado.
        /// </summary>
        /// <param name="id">Usado para buscar um Horario Exame no Database.</param>
        /// <returns>Horarios Exame selecionado.</returns>
        public HorariosExame SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<HorariosExame>($"SELECT * FROM ViewHorariosExame " +
                                                                           $"WHERE Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona um Horario de Exame no Database através do Dia especificado.
        /// </summary>
        /// <param name="Dia">Usado para buscar um Horarios de Exame no Database.</param>
        /// <returns>HorariosExame selecionado.</returns>
        public IEnumerable<HorariosExame> SelecionarPorDia(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosExame>($"SELECT * " +
                                                                $"FROM [ViewHorariosExame] " +
                                                                $"WHERE Dia = '{DiaHora.ToShortDateString()}'");
                return obj;
            }
        }

        public List<HorariosExame> SelecionarPorDiaNegocio(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosExame>($"SELECT * " +
                                                                $"FROM [ViewHorariosExame] " +
                                                                $"WHERE Dia = '{DiaHora.ToShortDateString()}'");
                return obj.ToList();
            }
        }

        /// <summary>
        /// Seleciona um Horario de Exame no Database através da Hora especificado.
        /// </summary>
        /// <param name="Hora">Usado para buscar um Horarios de Exame no Database.</param>
        /// <returns>HorariosExame selecionado.</returns>
        public IEnumerable<HorariosExame> SelecionarPorHora(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosExame>($"SELECT * " +
                                                                $"FROM [ViewHorariosExame] " +
                                                                $"WHERE Hora = '{DiaHora.ToShortTimeString()}'");
                return obj;
            }
        }

        public List<HorariosExame> SelecionarPorHoraNegocio(DateTime DiaHora)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosExame>($"SELECT * " +
                                                                $"FROM [ViewHorariosExame] " +
                                                                $"WHERE Hora = '{DiaHora.ToShortTimeString()}'");
                return obj.ToList();
            }
        }

        /// <summary>
        /// Seleciona um Horario de exame no Database através do valor especificado.
        /// </summary>
        /// <param name="Valor">Usado para buscar um Horarios de exame no Database.</param>
        /// <returns>HorariosExame selecionado.</returns>
        public IEnumerable<HorariosExame> SelecionarPorValor(Decimal Valor)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosExame>($"SELECT * " +
                                                                $"FROM [ViewHorariosExame] " +
                                                                $"WHERE Valor = {Valor}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona um Horario de Exame no Database através do IdAtendimento especificado.
        /// </summary>
        /// <param name="IdClinica">Usado para buscar Horarios de Exame no Database.</param>
        /// <returns>HorariosExame selecionado.</returns>
        public IEnumerable<HorariosExame> SelecionarPorIdAtendimento(int IdAtendimento)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosExame>($"SELECT * " +
                                                                $"FROM [ViewHorariosExame] " +
                                                                $"WHERE IdAtendimento = {IdAtendimento}");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona um Horario de exame no Database através do IdClinica especificado.
        /// </summary>
        /// <param name="IdClinica">Usado para buscar Horarios de exame no Database.</param>
        /// <returns>HorariosExame selecionado.</returns>
        public IEnumerable<HorariosExame> SelecionarPorNomeClinica(string Nome)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosExame>($"SELECT * " +
                                                                $"FROM [ViewHorariosExame] " +
                                                                $"where NomeClinica = '{Nome}'");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona um Horario de Exame no Database através do IdTipoExame especificado.
        /// </summary>
        /// <param name="idTipoExame">Usado para buscar Horarios de Exame no Database.</param>
        /// <returns>HorariosExame selecionado.</returns>
        public IEnumerable<HorariosExame> SelecionarPorIdTipoExame(int IdTipoExame)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<HorariosExame>($"SELECT * " +
                                                                $"FROM [ViewHorariosExame] " +
                                                                $"WHERE HE.IdTipoExame = {IdTipoExame}");
                return obj;
            }
        }

        /// <summary>
        /// Insere um Horario de Exame no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Horario de Exame.</param>
        /// <returns>ID do Horario de Exame inserido no Database.</returns>
        public int Inserir(HorariosExame entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                              $"INSERT INTO [HorariosExame] " +
                                              $"(Dia, Hora, Valor, IdAtendimento, IdtipoExame) " +
                                                    $"VALUES ('{entity.DiaHora.ToString("dd/MM/yyyy")}'," +
                                                            $"'{entity.DiaHora.ToString("HH:mm")}'," +
                                                            $"{entity.Valor}," +
                                                            $"{entity.IdAtendimento} ," +
                                                            $"{entity.IdTipoExame}) " +
                                              $"SET @ID = SCOPE_IDENTITY();" +
                                              $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados de um Horario de Exame no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados do Horario de Exame.</param>
        public void Alterar(HorariosExame entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [HorariosExame] " +
                    $"SET Dia ='{entity.DiaHora.ToShortDateString()}', " +
                    $"Hora = '{entity.DiaHora.ToString("HH:mm")}', " +
                    $"Valor = {entity.Valor}, " +
                    $"IdAtendimento = {entity.IdAtendimento}, " +
                    $"IdTipoExame = {entity.IdTipoExame}" +
                    $"WHERE Id={entity.Id}");
            }

        }

        /// <summary>
        /// Deleta um horario Exame do Database.
        /// </summary>
        /// <param name="id">Usado para selecionar o Horario Exame no Database.</param>
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