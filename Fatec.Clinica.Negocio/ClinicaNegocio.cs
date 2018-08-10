using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre a Clínica.
    /// </summary>
    public class ClinicaNegocio : INegocioBase<Clinicas>
    {
        private readonly ClinicaRepositorio _clinicaRepositorio;

        /// <summary>
        /// Construtor para instaciar o repositório.
        /// </summary>
        public ClinicaNegocio()
        {
            _clinicaRepositorio = new ClinicaRepositorio();
        }

        /// <summary>
        /// Seleciona todas as Clínicas.
        /// </summary>
        /// <returns>Lista de Exames.</returns>
        public IEnumerable<Clinicas> Selecionar()
        {
            return _clinicaRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se a clínica com o ID indicado existe.
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
        /// Verifica se a clínica com o CNPJ indicado existe.
        /// </summary>
        /// <param name="cnpj">Usado para buscar uma clínica no Database.</param>
        /// <returns>Seleciona uma clínica ou gera uma exceção.</returns>
        public Clinicas SelecionarPorCnpj(string cnpj)
        {
            var obj = _clinicaRepositorio.SelecionarPorCnpj(cnpj);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhuma Clínica com este CNPJ!");

            return obj;
        }

        /// <summary>
        /// Verifica se o CNPJ não está cadastrado e se ele é válido, se existem campos obrigatórios
        /// sem serem preenchidos e se o telefone é válido. Antes de inserir uma clínica.
        /// </summary>
        /// <param name="entity">Objeto com os dados da clínica a ser inserida.</param>
        /// <returns>ID da clínica inserida no Database ou exceção.</returns>
        public int Inserir(Clinicas entity)
        {
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Os seguintes campos são obrigatórios: Nome, Cnpj e Telefone Comercial");
            }

            if (ValidacaoCnpj.Verificar(entity.Cnpj) == false)
            {
                throw new DadoInvalidoException("CNPJ inválido!");
            }

            var obj = _clinicaRepositorio.SelecionarPorCnpj(entity.Cnpj);

            if (obj != null)
            {
                throw new ConflitoException("Já existe uma clínica registrada com este CNPJ!");
            }

            if (ValidacaoTelefone.Verificar(ValidacaoTelefone.LimparFormatacao(entity.TelefoneCom)) == false ||
                entity.TelefoneCom.Length > 10)
            {
                throw new DadoInvalidoException($"O telefone:\"{entity.TelefoneCom}\" é inválido!");
            }

            return _clinicaRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o CNPJ não está cadastrado e se ele é válido, se existem campos obrigatórios
        /// sem serem preenchidos e se o telefone é válido. Antes de alterar uma clínica.
        /// </summary>
        /// <param name="entity">Objeto com os dados da clínica a ser inserida.</param>
        /// <returns>ID da clínica inserida no Database ou exceção.</returns>
        public Clinicas Alterar(int id, Clinicas entity)
        {
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Os seguintes campos são obrigatórios: Nome, Cnpj e Telefone Comercial");
            }

            if (ValidacaoCnpj.Verificar(entity.Cnpj) == false)
            {
                throw new DadoInvalidoException("CNPJ inválido!");
            }

            var obj = _clinicaRepositorio.SelecionarPorCnpj(entity.Cnpj);

            if (obj != null)
            {
                throw new ConflitoException("Já existe uma clínica registrada com este CNPJ!");
            }

            if (ValidacaoTelefone.Verificar(ValidacaoTelefone.LimparFormatacao(entity.TelefoneCom)) == false ||
                entity.TelefoneCom.Length > 10)
            {
                throw new DadoInvalidoException($"O telefone:\"{entity.TelefoneCom}\" é inválido!");
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
