using Fatec.Clinica.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Negocio.Validacoes
{
    public class Validacao
    {
        #region Validar CPF
        /// <summary>
        /// Validação do CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
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
        /// <param name="entity">Objeto com os atributos a serem verificados.</param>
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
                 String.IsNullOrEmpty(Convert.ToString(entity.Numero)) || String.IsNullOrEmpty(Convert.ToString(entity.Clinica)))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Verificar se existem campos vazios na inserção ou alteração de um Tipo de Exame
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
    }
}