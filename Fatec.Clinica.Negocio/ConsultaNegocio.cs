using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre a consulta
    /// </summary>
    public class ConsultaNegocio : Validacao, INegocioBase<Consulta>
    {

        ConsultaRepositorio _consultaRepositorio;

        /// <summary>
        /// Construtor que inicializa o repositório
        /// </summary>
        public ConsultaNegocio()
        {
            _consultaRepositorio = new ConsultaRepositorio();
        }

        /// <summary>
        /// Seleciona todas as consultas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Consulta> Selecionar()
        {
            return _consultaRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se a consulta com o ID indicado existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Consulta SelecionarPorId(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorId(id);

            if(obj == null)
                throw new NaoEncontradoException($"Não foi encontrado uma consulta com o ID {id}!");

            return obj;
        }

        /// <summary>
        /// Verifica se a consulta com o ID do paciente indicado existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Consulta SelecionarPorPaciente(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorPaciente(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado uma consulta com o ID do paciente {id}!");

            return obj;
        }

        /// <summary>
        /// Verifica se a consulta com o ID do medico indicado existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Consulta SelecionarPorMedico(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorMedico(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado uma consulta com o ID do medico {id}!");

            return obj;
        }

        /// <summary>
        /// Verifica se a consulta com o ID da clinica indicado existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Consulta SelecionarPorClinica(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorClinica(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado uma consulta com o ID da clinica {id}!");

            return obj;
        }

        /// <summary>
        /// Verifica se os campos estão preenchidos, antes de inserir os dados da consulta
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Consulta entity)
        {
            if (VerificarCamposVazios(entity))
                throw new DadoInvalidoException($"Todos os dados são obrigatórios");

            return _consultaRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se os campos estão preenchidos, antes de alterar os dados da consulta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Consulta Alterar(int id, Consulta entity)
        {
            if (VerificarCamposVazios(entity))
                throw new DadoInvalidoException($"Todos os dados são obrigatórios");

            entity.Id = id;
            _consultaRepositorio.Alterar(entity);

            return _consultaRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se a consulta existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorId(id);

            if(obj == null)
                throw new NaoEncontradoException($"O ID {id} não foi encontrado");

            _consultaRepositorio.Deletar(id);
        }
    }
}
