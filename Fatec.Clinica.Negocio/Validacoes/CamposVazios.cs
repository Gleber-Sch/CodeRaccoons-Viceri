using Fatec.Clinica.Dominio;
using System;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class CamposVazios
    {
        #region Verificar se existem campos vazios na inserção ou alteração de um Médico
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public static bool Verificar(Medico entity)
        {
            if (String.IsNullOrEmpty(entity.Nome) || 
                String.IsNullOrEmpty(entity.Cpf) ||
                String.IsNullOrEmpty(Convert.ToString(entity.Crm)) ||
                String.IsNullOrEmpty(entity.Celular) ||
                String.IsNullOrEmpty(Convert.ToString(entity.IdEspecialidade)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.Genero)) ||
                entity.DataNasc == null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de um Paciente
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public static bool Verificar(Paciente entity)
        {
            if (String.IsNullOrEmpty(entity.Nome) || 
                String.IsNullOrEmpty(entity.Cpf) ||
                String.IsNullOrEmpty(entity.Celular) ||
                String.IsNullOrEmpty(Convert.ToString(entity.Genero)) ||
                entity.DataNasc == null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existe campo vazio na inserção ou alteração de uma Especialidade
        /// <summary>
        /// Verifica se o camopo obrigátorio não foi preenchido.
        /// </summary>
        /// <param name="entity">Objeto com o campo a ser verificado.</param>
        /// <returns>True se o campo Nome não estiver preenchido ou False se ele estiver.</returns>
        public static bool Verificar(Especialidade entity)
        {
            if (String.IsNullOrEmpty(entity.Nome))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verifica se existe campo vazio na inserção ou alteração de um Endereco
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se o atributo Nome não estever preenchido ou False se ele estiver.</returns>
        public static bool Verificar(Endereco entity)
        {
            if (String.IsNullOrEmpty(Convert.ToString(entity.Estado)) ||
                String.IsNullOrEmpty(entity.Cidade) ||
                 String.IsNullOrEmpty(entity.Bairro) ||
                 String.IsNullOrEmpty(entity.Logradouro) ||
                 String.IsNullOrEmpty(Convert.ToString(entity.Numero)) || 
                 String.IsNullOrEmpty(Convert.ToString(entity.IdClinica)))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de um Tipo de Exame
        /// <summary>
        /// Verifica se o campo obrigátorio não foi preenchido.
        /// </summary>
        /// <param name="entity">Objeto com o campo a ser verificado.</param>
        /// <returns>True se o campo Nome não estiver preenchido ou False se ele estiver.</returns>
        public static bool Verificar(TipoExame entity)
        {
            if (String.IsNullOrEmpty(entity.Nome))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de um Atendimento
        /// <summary>
        /// Verifica se os campos obrigátorios foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios estiverem vazios ou False se não estiverem.</returns>
        public static bool Verificar(Atendimento entity)
        {
            if (String.IsNullOrEmpty(Convert.ToString(entity.IdClinica)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.IdMedico)))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de uma Consulta
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios estiverem vazios ou False se não estiverem.</returns>
        public static bool Verificar(Consulta entity)
        {
            if (String.IsNullOrEmpty(Convert.ToString(entity.IdAtendimento)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.IdPaciente)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.DataHora)) ||
                String.IsNullOrEmpty(entity.Historico))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de um Exame
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public static bool Verificar(Exame entity)
        {
            if (entity.DataHora == null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de uma Clinica
        /// <summary>
        /// Verifica se os campos obrigátorios foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios estiverem vazios ou False se não estiverem.</returns>
        public static bool Verificar(Clinicas entity)
        {
            if (string.IsNullOrEmpty(entity.Nome) || 
                string.IsNullOrEmpty(entity.Cnpj) ||
                string.IsNullOrEmpty(entity.TelefoneCom) ||
                string.IsNullOrEmpty(entity.Email))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção de um Horario
        /// <summary>
        /// Verifica se os campos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os campos a serem verificados.</param>
        /// <returns>True se os campos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public static bool Verificar(Horario entity)
        {
            if (entity.DiaHora == null)
            {
                return true;
            }
            return false;
        }
        #endregion

    }
}
