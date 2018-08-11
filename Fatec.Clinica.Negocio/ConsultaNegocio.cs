using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre a consulta.
    /// </summary>
    public class ConsultaNegocio : INegocioBase<Consulta>
    {
        /// <summary>
        /// Declara o repositório da Consulta.
        /// </summary>
        ConsultaRepositorio _consultaRepositorio;

        /// <summary>
        /// Construtor que inicializa o repositório.
        /// </summary>
        public ConsultaNegocio()
        {
            _consultaRepositorio = new ConsultaRepositorio();
        }

        /// <summary>
        /// Seleciona todas as consultas do Database.
        /// </summary>
        /// <returns>Lista de consultas.</returns>
        public IEnumerable<Consulta> Selecionar()
        {
            return _consultaRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe alguma consulta com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar uma consulta no Database.</param>
        /// <returns>Seleciona uma consulta ou gera uma exceção.</returns>
        public Consulta SelecionarPorId(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado uma consulta com o ID: {id}");
                
            return obj;
        }

        /// <summary>
        /// Verifica se a consulta com o ID do paciente indicado existe.
        /// </summary>
        /// <param name="id">Usado para buscar um paciente no Database.</param>
        /// <returns>Seleciona um atendimento ou gera uma exceção.</returns>
        public Consulta SelecionarPorPaciente(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorPaciente(id);

            if (obj == null)
                throw new NaoEncontradoException("Não foi encontrado nenhuma consulta vinculada a este ID de paciente.");

            return obj;
        }

        /// <summary>
        /// Verifica se a consulta com o ID do medico indicado existe.
        /// </summary>
        /// <param name="id">Usado para buscar um médico no Database.</param>
        /// <returns>Seleciona um atendimento ou gera uma exceção.</returns>
        public Consulta SelecionarPorMedico(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorMedico(id);

            if (obj == null)
                throw new NaoEncontradoException("Não foi encontrado nenhuma consulta vinculada a este ID de médico");

            return obj;
        }

        /// <summary>
        /// Verifica se a consulta com o ID da clinica indicado existe.
        /// </summary>
        /// <param name="id">Usado para buscar uma clínica no Database.</param>
        /// <returns>Seleciona um atendimento ou gera uma exceção.</returns>
        public Consulta SelecionarPorClinica(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorClinica(id);

            if (obj == null)
                throw new NaoEncontradoException("Não foi encontrado uma consulta este ID de clinica");

            return obj;
        }

        /// <summary>
        /// Verifica se existem campos obrigatórios não preenchidos, se o ID do atendimento e do paciente
        /// são válidos e se os campos respeitam os limites de caracteres especificados no Database.
        /// Antes de inserir uma clínica.
        /// </summary>
        /// <param name="entity">Objeto com os dados da consulta.</param>
        /// <returns>ID da consulta inserida no Databse ou gera uma exceção.</returns>
        public int Inserir(Consulta entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos que excedem o limite de caracteres permitidos!");
            }

            //Verifica se o ID do atendimento é válido.
            var RepositorioMedico = new AtendimentoRepositorio();
            if (RepositorioMedico.SelecionarPorId(entity.IdAtendimento) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum antendimento " +
                                                $"com o ID: {entity.IdAtendimento}");
            }

            //Verifica se o ID do paciente é válido.
            var RepositorioPaciente = new PacienteRepositorio();
            if (RepositorioPaciente.SelecionarPorId(entity.IdPaciente) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum paciente " +
                                                $"com o ID: {entity.IdAtendimento}");
            }

            return _consultaRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se existem campos obrigatórios não preenchidos, se o ID do atendimento e do paciente
        /// são válidos e se os campos respeitam os limites de caracteres especificados no Database.
        /// Antes de alterar os dados de uma clínica.
        /// </summary>
        /// <param name="entity">Objeto com os dados da consulta.</param>
        /// <param name="id">Usado para buscar a consulta no Database.</param>
        /// <returns>Seleciona a consulta alterada no Database ou gera uma exceção.</returns>
        public Consulta Alterar(int id, Consulta entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos obrigatórios que não foram preenchidos!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos que excedem o limite de caracteres permitidos!");
            }

            //Verifica se o ID do atendimento é válido.
            var RepositorioMedico = new AtendimentoRepositorio();
            if (RepositorioMedico.SelecionarPorId(entity.IdAtendimento) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum antendimento " +
                                                $"com o ID: {entity.IdAtendimento}");
            }

            //Verifica se o ID do paciente é válido.
            var RepositorioPaciente = new PacienteRepositorio();
            if (RepositorioPaciente.SelecionarPorId(entity.IdPaciente) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum paciente " +
                                                $"com o ID: {entity.IdAtendimento}");
            }

            entity.Id = id;
            _consultaRepositorio.Alterar(entity);

            return _consultaRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se a consulta existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar a consulta no Database.</param>
        public void Deletar(int id)
        {
            var obj = _consultaRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"O ID: \"{id}\" não foi encontrado!");

            _consultaRepositorio.Deletar(id);
        }
    }
}
