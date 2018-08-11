using Fatec.Clinica.Dominio;
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
                entity.Cnpj.Length > 14 ||
                entity.Nome.Length > 50 ||
                entity.TelefoneCom.Length > 10)
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

        #region Endereço
        /// <summary>
        /// Verifica se o tamanho dos campos do objeto excedem o limite estabelecido no banco de dados.
        /// </summary>
        /// <param name="entity">Contêm os dados do campo.</param>
        /// <returns>
        /// True se algum campo possuir mais caracteres do que o limite declarado no banco de dados ou
        /// False caso todos os campos respeitem esta especificação.
        /// </returns>
        public static bool Verificar(Endereco entity)
        {
            if (entity.Estado != 2 ||
                entity.Cidade.Length > 50 ||
                entity.Bairro.Length > 50 ||
                entity.Complemento.Length > 50 ||
                entity.Logradouro.Length > 50)
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
                entity.Cpf.Length != 11 ||
                entity.Crm > 10 ||
                entity.Email.Length > 50 ||
                entity.Senha.Length >20 ||
                entity.Genero > 1 ||
                entity.Celular.Length > 11)
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
                entity.Cpf.Length != 11 ||
                entity.Email.Length > 50 ||
                entity.Senha.Length > 20 ||
                entity.Genero > 1 ||
                entity.Celular.Length > 11 ||
                entity.TelefoneRes.Length > 10)
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
