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
    /// Regras de Negócio sobre o valor da consulta.
    /// </summary>
    public class ValorConsultaNegocio : INegocioBase<ValorConsulta>
    {
        /// <summary>
        /// Declara o repositório do valor da consulta.
        /// </summary>
        private readonly ValorConsultaRepositorio _valorConsultaRepositorio;

        /// <summary>
        /// Construtor para instaciar o repositório.
        /// </summary>
        public ValorConsultaNegocio()
        {
            _valorConsultaRepositorio = new ValorConsultaRepositorio();
        }

        /// <summary>
        /// Seleciona todas os valores de consultas.
        /// </summary>
        /// <returns>Lista de valores de consulta.</returns>
        public IEnumerable<ValorConsulta> Selecionar()
        {
            return _valorConsultaRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe algum valor de consulta com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um valor de consulta no Database.</param>
        /// <returns>Seleciona um valor de consulta ou gera uma exceção.</returns>
        public ValorConsulta SelecionarPorId(int id)
        {
            var obj = _valorConsultaRepositorio.SelecionarPorId(id);

            if (obj == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum Valor" +
                                                $" de consulta com este ID: {id}");
            }

            return obj;
        }

        /// <summary>
        /// Verifica se o ID da especialidade, da clínica e do valor da consulta
        /// são válidos e se os campos foram preenchidos.
        /// Antes de inserir os dados do valor da consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto com os dados do valor da consulta.</param>
        /// <returns>ID do valor da consulta inserido no Databse ou gera uma exceção.</returns>
        public int Inserir(ValorConsulta entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se o ID da especialidade é válido.
            var RepositorioEspecialidade = new EspecialidadeRepositorio();
            if (RepositorioEspecialidade.SelecionarPorId(entity.IdEspecialidade) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma especialidade " +
                                                $"com o ID: {entity.IdEspecialidade}");
            }

            //Verifica se o ID da clínica é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdClinica) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma clínica " +
                                                $"com o ID: {entity.IdClinica}");
            }

            //Verifica se o valor valor da consulta é válido.
            if (entity.Valor <= 0)
            {
                throw new DadoInvalidoException($"O valor: \"{entity.Valor}\", é inválido!");
            }

            return _valorConsultaRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o ID da especialidade, da clínica e do valor da consulta
        /// são válidos e se os campos foram preenchidos.
        /// Antes de alterar os dados do valor da consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto com os dados do valor da consulta.</param>
        /// <returns>Seleciona o valor da consulta alterado no Databse ou gera uma exceção.</returns>
        public ValorConsulta Alterar(int id, ValorConsulta entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se o ID da especialidade é válido.
            var RepositorioEspecialidade = new EspecialidadeRepositorio();
            if (RepositorioEspecialidade.SelecionarPorId(entity.IdEspecialidade) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma especialidade " +
                                                $"com o ID: {entity.IdEspecialidade}");
            }

            //Verifica se o ID da clínica é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdClinica) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma clínica " +
                                                $"com o ID: {entity.IdClinica}");
            }

            //Verifica se o valor valor da consulta é válido.
            if (entity.Valor <= 0)
            {
                throw new DadoInvalidoException($"O valor: \"{entity.Valor}\", é inválido!");
            }

            entity.Id = id;
            _valorConsultaRepositorio.Alterar(entity);

            return _valorConsultaRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o valor da consulta existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o valor da consulta no Database.</param>
        public void Deletar(int id)
        {
            var obj = _valorConsultaRepositorio.SelecionarPorId(id);
            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID {id} não foi encontrado");
            }

            _valorConsultaRepositorio.Deletar(id);
        }
    }
}
