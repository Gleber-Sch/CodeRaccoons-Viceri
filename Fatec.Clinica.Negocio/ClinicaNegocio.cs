using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;
using System.Collections.Generic;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre a Clínica.
    /// </summary>
    public class ClinicaNegocio : INegocioBase<Clinicas>
    {
        /// <summary>
        /// Declara o repositório da Clínica.
        /// </summary>
        private readonly ClinicaRepositorio _clinicaRepositorio;

        /// <summary>
        /// Construtor para instaciar o repositório.
        /// </summary>
        public ClinicaNegocio()
        {
            _clinicaRepositorio = new ClinicaRepositorio();
        }

        /// <summary>
        /// Seleciona todas as Clínicas do Database.
        /// </summary>
        /// <returns>Lista de clínicas.</returns>
        public IEnumerable<Clinicas> Selecionar()
        {
            return _clinicaRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe alguma clínica com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar uma clínica no Database.</param>
        /// <returns>Seleciona uma clínica ou gera uma exceção.</returns>
        public Clinicas SelecionarPorId(int id)
        {
            var obj = _clinicaRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhuma Clínica com este ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se existe alguma clínica com o CNPJ indicado.
        /// </summary>
        /// <param name="cnpj">Usado para buscar uma clínica no Database.</param>
        /// <returns>Seleciona uma clínica ou gera uma exceção.</returns>
        public Clinicas SelecionarPorCnpj(string cnpj)
        {
            var obj = _clinicaRepositorio.SelecionarPorCnpj(cnpj);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhuma Clínica com este CNPJ: {cnpj}");

            return obj;
        }

        /// <summary>
        /// Verifica se o CNPJ não está cadastrado e se ele é válido, se existem campos obrigatórios
        /// sem serem preenchidos e se o telefone é válido. Antes de inserir uma clínica.
        /// </summary>
        /// <param name="entity">Objeto com os dados da clínica a ser inserida.</param>
        /// <returns>ID da clínica inserida no Database ou gera alguma exceção.</returns>
        public int Inserir(Clinicas entity)
        {
            //Verififica se há campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos que excedem o limite de caracteres permitidos!");
            }

            //Verifica a validação do CNPJ
            if (!ValidacaoCnpj.Verificar(entity.Cnpj))
            {
                throw new DadoInvalidoException("CNPJ inválido!");
            }

            //Verifica se o CNPJ já não está registrado.
            var obj = _clinicaRepositorio.SelecionarPorCnpj(entity.Cnpj);
            if (obj != null)
            {
                throw new ConflitoException("Já existe uma clínica registrada com este CNPJ!");
            }

            //Verifica se o formato e a quantidade de caracteres do telefone são válidos.
            if (TelefoneValido.Verificar(TelefoneValido.LimparFormatacao(entity.TelefoneCom)) == false)
            {
                throw new DadoInvalidoException($"O número de telefone:\"{entity.TelefoneCom}\" é inválido!");
            }

            //Verifica se o ID do endereço é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdEndereco) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum endereço " +
                                                $"com o ID: {entity.IdEndereco}");
            }

            return _clinicaRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o CNPJ não está cadastrado e se ele é válido, se existem campos obrigatórios
        /// sem serem preenchidos e se o telefone é válido. Antes de alterar uma clínica.
        /// </summary>
        /// <param name="entity">Objeto com os dados da clínica a ser inserida.</param>
        /// <returns>Seleciona a clínica alterada no Database ou gera alguma exceção.</returns>
        public Clinicas Alterar(int id, Clinicas entity)
        {
            Clinicas obj;

            //Verififica se há campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos que excedem o limite de caracteres permitidos!");
            }

            //Verifica se o CNPJ é válido e se ele já foi cadastrado
            if (ValidacaoCnpj.Verificar(entity.Cnpj) == false)
            {
                throw new DadoInvalidoException("CNPJ inválido!");
            }
            else
            {
                obj = _clinicaRepositorio.SelecionarPorCnpj(entity.Cnpj);
                if (obj != null && obj.Id != id)
                {
                    throw new ConflitoException("Já existe uma clínica registrada com este CNPJ!");
                }
            }

            //Verifica se o formato e a quantidade de caracteres do telefone são válidos.
            if (TelefoneValido.Verificar(TelefoneValido.LimparFormatacao(entity.TelefoneCom)) == false)
            {
                throw new DadoInvalidoException($"O número de telefone:\"{entity.TelefoneCom}\" é inválido!");
            }

            //Verifica se o ID do endereço é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdEndereco) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum endereço " +
                                                $"com o ID: {entity.IdEndereco}");
            }

            entity.Id = id;
            _clinicaRepositorio.Alterar(entity);

            return _clinicaRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se a clínica existe no Database antes de deleta-la.
        /// </summary>
        /// <param name="id">Usado para buscar a clínica no Database.</param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);

            if (obj == null)
            {
                throw new NaoEncontradoException($"Não foi encontrado nenhuma Clínica com este ID: {id}");
            }

            _clinicaRepositorio.Deletar(obj.Id);
        }

    }
}
