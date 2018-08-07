using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using System;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class Validacao
    {
        #region Validar CPF
        /// <summary>
        /// Validação do CPF
        /// </summary>
        /// <param name="cpf">Dado a ser verificado.</param>
        /// <returns>TRUE se o CPF é válido ou FALSE se não for.</returns>
        public bool VerificarCPF(string cpf)
        {
            //Limpa a formatação da String
            cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (long.TryParse(cpf, out long somenteNumero) == false)
                return false;
            
            //Se o CPF não tem 11 digitos ele é inválido
            if (cpf.Length != 11)
                return false;

            string DigitosCpf, DigitoVerificador = null;
            int Soma = 0, Resto = 0, Multiplicador = 10;

            //Gera uma string sem os dígitos verificadores
            DigitosCpf = cpf.Substring(0, 9);

            while (Multiplicador < 12)
            {
                for (int i = 0; i < 9; i++, Multiplicador--)
                    Soma += int.Parse(DigitosCpf[i].ToString()) * Multiplicador;

                Resto = Soma % 11;

                if (Resto < 2)
                    Resto = 0;
                else
                    Resto = 11 - Resto;

                DigitoVerificador = Resto.ToString();
                DigitosCpf += DigitoVerificador;
                Soma = 0;
                Multiplicador++;
            }

            DigitoVerificador += Resto.ToString();

            //Compara se DigitoVerificador é igual ao foi recebido como parâmetro pelo método e retorna um booleano
            return cpf.EndsWith(DigitoVerificador);
        }
        #endregion

        #region Validar CNPJ
        /// <summary>
        /// Validação do CNPJ
        /// </summary>
        /// <param name="cnpj">Dado a ser verificado.</param>
        /// <returns>TRUE se o CNPJ é válido ou FALSE se não for.</returns>
        public bool VerificarCnpj(string cnpj)
        {
            //Limpa a formatação da String
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            //Verifica se existem caracteres diferentes de números
            if (long.TryParse(cnpj, out long somenteNumero) == false)
            {
                return false;
            }

            //Se o CNPJ não tem 14 digitos ele é inválido
            if (cnpj.Length != 14)
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma,resto;
            string digito,tempCnpj;

            //Gera uma string sem os dígitos verificadores
            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            //Compara se DigitoVerificador é igual ao foi recebido como parâmetro pelo método e retorna um booleano
            return cnpj.EndsWith(digito);
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de um Médico
        /// <summary>
        /// Verifica se os atributos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os atributos a serem verificados.</param>
        /// <returns>True se os atributos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public bool VerificarCamposVazios(Medico entity)
        {
            if ( String.IsNullOrEmpty(entity.Nome) || String.IsNullOrEmpty(entity.Cpf) ||
                 String.IsNullOrEmpty(Convert.ToString(entity.Crm)) || String.IsNullOrEmpty(entity.Celular) ||
                 String.IsNullOrEmpty(Convert.ToString(entity.IdEspecialidade)) ||
                 String.IsNullOrEmpty(Convert.ToString(entity.Genero)) || entity.DataNasc == null )
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de um Paciente
        /// <summary>
        /// Verifica se os atributos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os atributos a serem verificados.</param>
        /// <returns>True se os atributos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public bool VerificarCamposVazios(Paciente entity)
        {
            if ( String.IsNullOrEmpty(entity.Nome) || String.IsNullOrEmpty(entity.Cpf) ||
                 String.IsNullOrEmpty(entity.Celular) ||
                 String.IsNullOrEmpty(Convert.ToString(entity.Genero)) || entity.DataNasc == null )
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existe campo vazio na inserção ou alteração de uma Especialidade
        /// <summary>
        /// Verifica se o atributo obrigátorio não foi preenchido.
        /// </summary>
        /// <param name="entity">Objeto com os atributo a ser verificado.</param>
        /// <returns>True se o atributo Nome não estever preenchido ou False se ele estiver.</returns>
        public bool VerificarCamposVazios(Especialidade entity)
        {
            if ( String.IsNullOrEmpty(entity.Nome) )
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verifica se existe campo vazio na inserção ou alteração de um Endereco
        public bool VerificarCamposVazios(Endereco entity)
        {
            if (String.IsNullOrEmpty(Convert.ToString(entity.Estado)) || String.IsNullOrEmpty(entity.Cidade) ||
                 String.IsNullOrEmpty(entity.Bairro) || String.IsNullOrEmpty(entity.Logradouro) ||
                 String.IsNullOrEmpty(Convert.ToString(entity.Numero)) || String.IsNullOrEmpty(Convert.ToString(entity.IdClinica)))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de um Tipo de Exame
        /// <summary>
        /// Verifica se o atributo obrigátorio não foi preenchido.
        /// </summary>
        /// <param name="entity">Objeto com o atributo a ser verificado.</param>
        /// <returns>True se o atributo Nome não estever preenchido ou False se ele estiver.</returns>
        public bool VerificarCamposVazios(TipoExame entity)
        {
            if (String.IsNullOrEmpty(entity.Nome))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de um Atendimento
        public bool VerificarCamposVazios(Atendimento entity)
        {
            if(String.IsNullOrEmpty(Convert.ToString(entity.Clinica)) || String.IsNullOrEmpty(Convert.ToString(entity.Medico)))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de uma Consulta
        public bool VerificarCamposVazios(Consulta entity)
        {
            if (String.IsNullOrEmpty(Convert.ToString(entity.Atendimento)) || String.IsNullOrEmpty(Convert.ToString(entity.Paciente)) ||
                String.IsNullOrEmpty(Convert.ToString(entity.DataHora)) || String.IsNullOrEmpty(entity.Historico))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificar se existem campos vazios na inserção ou alteração de um Exame
        /// <summary>
        /// Verifica se o atributo obrigátorio não foi preenchido.
        /// </summary>
        /// <param name="entity">Objeto com o atributo a ser verificado.</param>
        /// <returns>True se o atributo DataHora não estever preenchido ou False se ele estiver.</returns>
        public bool VerificarCamposVazios(Exame entity)
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
        /// Verifica se os atributos obrigátorios não foram preenchidos.
        /// </summary>
        /// <param name="entity">Objeto com os dados da Cinica.</param>
        /// <returns>True se os atributos obrigátorios não foram preenchidos ou False se eles foram.</returns>
        public bool VerificarCamposVazios(Clinicas entity)
        {
            if ( string.IsNullOrEmpty(entity.Nome) || string.IsNullOrEmpty(entity.Cnpj) ||
                 string.IsNullOrEmpty(entity.TelefoneCom) )
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Verificação de Idade
        public bool VerificarIdade(DateTime DataNasc)
        {
            if(DateTime.Now.Year - DataNasc.Year > 18)
            {
                return true;
            }
            else if((DateTime.Now.Year - DataNasc.Year == 18) && (DateTime.Now.Month > DataNasc.Month || (DateTime.Now.Month == DataNasc.Month && DateTime.Now.Day >= DataNasc.Day)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Verifica se o Tipo de Exame existe
        /// <summary>
        /// Verifica se o ID do TipoExame existe no Database.
        /// </summary>
        /// <param name="id">Usado para buscar o TipoExame no Database.</param>
        /// <returns>TRUE se o tipo de exame existir ou FALSE se ele não existir.</returns>
        public bool VerificarIdTipoExame(int id)
        {
            var repositorio = new TipoExameRepositorio();
            if( repositorio.SelecionarPorId(id) == null)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Verifica se o Atendimento existe
        /// <summary>
        /// Verifica se o ID do Atendimento existe no Database.
        /// </summary>
        /// <param name="id">Usado para buscar o Atendimento no Database.</param>
        /// <returns>TRUE se o atendimento existir ou FALSE se ele não existir.</returns>
        public bool VerificarIdAtendimento(int id)
        {
            var repositorio = new AtendimentoRepositorio();
            if (repositorio.SelecionarPorId(id) == null)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Verifica se a Consulta existe
        /// <summary>
        /// Verifica se o ID da Consulta existe no Database.
        /// </summary>
        /// <param name="id">Usado para buscar a Consulta no Database.</param>
        /// <returns>TRUE se a consulta existir ou FALSE se ele não existir.</returns>
        public bool VerificarIdConsulta(int id)
        {
            var repositorio = new ConsultaRepositorio();
            if (repositorio.SelecionarPorId(id) == null)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}