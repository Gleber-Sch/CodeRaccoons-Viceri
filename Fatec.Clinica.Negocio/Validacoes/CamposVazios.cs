using Fatec.Clinica.Dominio;
using System;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class CamposVazios
    {
        #region Médico
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public static bool Verificar(Medico entity)
        {
            if (String.IsNullOrWhiteSpace(entity.Nome) || 
                String.IsNullOrWhiteSpace(entity.Cpf) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.Crm)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.CrmEstado)) ||
                String.IsNullOrWhiteSpace(entity.Celular) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.IdEspecialidade)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.Genero)) ||
                String.IsNullOrWhiteSpace(entity.Email) ||
                String.IsNullOrWhiteSpace(entity.Senha) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.StatusAtividade)) ||
                String.IsNullOrWhiteSpace(entity.DataNasc.ToString()))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Paciente
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public static bool Verificar(Paciente entity)
        {
            if (String.IsNullOrWhiteSpace(entity.Nome) || 
                String.IsNullOrWhiteSpace(entity.Cpf) ||
                String.IsNullOrWhiteSpace(entity.Celular) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.Genero)) ||
                String.IsNullOrWhiteSpace(entity.Email) ||
                String.IsNullOrWhiteSpace(entity.Senha) ||
                String.IsNullOrWhiteSpace(entity.DataNasc.ToString()))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Especialidade
        /// <summary>
        /// Verifica se o camopo obrigátorio não foi preenchido.
        /// </summary>
        /// <param name="entity">Objeto com o campo a ser verificado.</param>
        /// <returns>True se o campo Nome não estiver preenchido ou False se ele estiver.</returns>
        public static bool Verificar(Especialidade entity)
        {
            if (String.IsNullOrWhiteSpace(entity.Nome))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Tipo de Exame
        /// <summary>
        /// Verifica se o campo obrigátorio não foi preenchido.
        /// </summary>
        /// <param name="entity">Objeto com o campo a ser verificado.</param>
        /// <returns>True se o campo Nome não estiver preenchido ou False se ele estiver.</returns>
        public static bool Verificar(TipoExame entity)
        {
            if (String.IsNullOrWhiteSpace(entity.Nome))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Atendimento
        /// <summary>
        /// Verifica se os campos obrigátorios foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios estiverem vazios ou False se não estiverem.</returns>
        public static bool Verificar(Atendimento entity)
        {
            if (String.IsNullOrWhiteSpace(Convert.ToString(entity.IdClinica)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.IdMedico)))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Consulta
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios estiverem vazios ou False se não estiverem.</returns>
        public static bool Verificar(Consulta entity)
        {
            if (String.IsNullOrWhiteSpace(Convert.ToString(entity.IdAtendimento)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.IdPaciente)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.DataHora)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.Nota)) ||
                String.IsNullOrWhiteSpace(entity.Historico))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Exame
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public static bool Verificar(Exame entity)
        {
            if (String.IsNullOrWhiteSpace(Convert.ToString(entity.DataHora)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.IdAtendimento)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.IdConsulta)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.IdTipoExame)))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Clinica
        /// <summary>
        /// Verifica se os campos obrigátorios foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios estiverem vazios ou False se não estiverem.</returns>
        public static bool Verificar(Clinicas entity)
        {
            if (String.IsNullOrWhiteSpace(entity.Nome) ||
                String.IsNullOrWhiteSpace(entity.Cnpj) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.StatusAtividade)) ||
                String.IsNullOrWhiteSpace(entity.TelefoneCom) ||
                String.IsNullOrWhiteSpace(entity.Email) ||
                String.IsNullOrWhiteSpace(entity.Senha))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region HorarioExame
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public static bool Verificar(HorariosExame entity)
        {
            if (String.IsNullOrEmpty(Convert.ToString(entity.DiaHora)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.Valor)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.IdAtendimento)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.IdTipoExame)))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region HorarioConsulta
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public static bool Verificar(HorariosConsulta entity)
        {
            if (String.IsNullOrEmpty(Convert.ToString(entity.DiaHora)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.Valor)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.IdAtendimento)))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region ValorConsulta
        /// <summary>
        /// Verifica se os campos obrigátorios foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios estiverem vazios ou False se não estiverem.</returns>
        public static bool Verificar(ValorConsulta entity)
        {
            if (String.IsNullOrWhiteSpace(Convert.ToString(entity.IdClinica)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.IdEspecialidade)) ||
                String.IsNullOrWhiteSpace(Convert.ToString(entity.Valor)))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
