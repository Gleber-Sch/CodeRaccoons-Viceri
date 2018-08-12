using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para o HorarioConsulta.
    /// </summary>
    public class HorarioConsultaRepositorio : IRepositorioBase<HorarioConsulta>
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




    }
}