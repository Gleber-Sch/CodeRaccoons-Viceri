﻿using Fatec.Clinica.Dominio;
using System;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class ExcedeLimiteDeCaracteres
    {
        #region Clinicas
        /// <summary>
        /// Verifica se o tamanho dos campos do objeto excedem o limite estabelecido no banco de dados.
        /// </summary>
        /// <param name="entity">Contêm os dados do campo.</param>
        /// <returns>
        /// True se algum campo possuir mais caracteres do que o limite declarado no banco de dados ou
        /// False caso todos os campos respeitem esta especificação.
        /// </returns>
        public static bool Verificar(Clinicas entity)
        {
            if (entity.Email.Length >50 ||
                entity.Cnpj.Length > 18 ||
                entity.Nome.Length > 50 ||
                entity.TelefoneCom.Length > 14)
            {
                return true; 
            }

            return false;
        }
        #endregion

        #region Consulta
        /// <summary>
        /// Verifica se o tamanho dos campos do objeto excedem o limite estabelecido no banco de dados.
        /// </summary>
        /// <param name="entity">Contêm os dados do campo.</param>
        /// <returns>
        /// True se algum campo possuir mais caracteres do que o limite declarado no banco de dados ou
        /// False caso todos os campos respeitem esta especificação.
        /// </returns>
        public static bool Verificar(Consulta entity)
        {
            if (entity.Historico.Length > 300 || entity.Nota> 5)
            {
                return true;
            }

            return false;
        }
        #endregion


        #region Especialidade
        /// <summary>
        /// Verifica se o tamanho dos campos do objeto excedem o limite estabelecido no banco de dados.
        /// </summary>
        /// <param name="entity">Contêm os dados do campo.</param>
        /// <returns>
        /// True se algum campo possuir mais caracteres do que o limite declarado no banco de dados ou
        /// False caso todos os campos respeitem esta especificação.
        /// </returns>
        public static bool Verificar(Especialidade entity)
        {
            if (entity.Nome.Length > 50)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Médico
        /// <summary>
        /// Verifica se o tamanho dos campos do objeto excedem o limite estabelecido no banco de dados.
        /// </summary>
        /// <param name="entity">Contêm os dados do campo.</param>
        /// <returns>
        /// True se algum campo possuir mais caracteres do que o limite declarado no banco de dados ou
        /// False caso todos os campos respeitem esta especificação.
        /// </returns>
        public static bool Verificar(Medico entity)
        {
            if (entity.Nome.Length > 50 ||
                entity.Cpf.Length != 14 ||
                entity.Email.Length > 50 ||
                entity.Senha.Length >20 ||
                Convert.ToString(entity.Genero).Length > 1 ||
                entity.Celular.Length > 14)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Paciente
        /// <summary>
        /// Verifica se o tamanho dos campos do objeto excedem o limite estabelecido no banco de dados.
        /// </summary>
        /// <param name="entity">Contêm os dados do campo.</param>
        /// <returns>
        /// True se algum campo possuir mais caracteres do que o limite declarado no banco de dados ou
        /// False caso todos os campos respeitem esta especificação.
        /// </returns>
        public static bool Verificar(Paciente entity)
        {
            if (entity.Nome.Length > 50 ||
                entity.Cpf.Length != 14 ||
                entity.Email.Length > 50 ||
                entity.Senha.Length > 20 ||
                Convert.ToString(entity.Genero).Length  > 1 ||
                entity.Celular.Length > 14 ||
                entity.TelefoneRes.Length > 13)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region TipoExame
        /// <summary>
        /// Verifica se o tamanho dos campos do objeto excedem o limite estabelecido no banco de dados.
        /// </summary>
        /// <param name="entity">Contêm os dados do campo.</param>
        /// <returns>
        /// True se algum campo possuir mais caracteres do que o limite declarado no banco de dados ou
        /// False caso todos os campos respeitem esta especificação.
        /// </returns>
        public static bool Verificar(TipoExame entity)
        {
            if (entity.Nome.Length > 50)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
