using Fatec.Clinica.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o Atendimento.
    /// </summary>
    public class AtendimentoNegocio : INegocioBase<Atendimento>
    {
        /// <summary>
        /// Declara o repositório do Atendimento.
        /// </summary>
        AtendimentoRepositorio _atendimentoRepositorio;

        /// <summary>
        /// Construtor para instanciar o repositório.
        /// </summary>
        public AtendimentoNegocio()
        {
            _atendimentoRepositorio = new AtendimentoRepositorio();
        }

        /// <summary>
        /// Seleciona todos os Atendimentos do Database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Atendimento> Selecionar()
        {
            return _atendimentoRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se o atendimento com o ID indicado existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Atendimento SelecionarPorId(int id)
        {
            var obj = _atendimentoRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado um atendimento com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se os campos foram inseridos, antes de inserir os dados do atendimento
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Atendimento entity)
        {
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException($"O ID da clínica e o ID do Médico são campos obrigatórios");
            }
            return _atendimentoRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se os campos foram inseridos, antes de alterar os dados do atendimento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Atendimento Alterar(int id, Atendimento entity)
        {
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException($"O ID da clínica e o ID do Médico são campos obrigatórios");
            }

            entity.Id = id;
            _atendimentoRepositorio.Alterar(entity);

            return _atendimentoRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o atendimento existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = _atendimentoRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"O ID: {id} não foi encontrado");

            _atendimentoRepositorio.Deletar(id);
        }
    }
}
