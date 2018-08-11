using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;
using System.Collections.Generic;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o exame.
    /// </summary>
    public class ExameNegocio : INegocioBase<Exame>
    {
        /// <summary>
        /// Declara o repositório do Exame.
        /// </summary>
        private readonly ExameRepositorio _exameRepositorio;

        /// <summary>
        /// Construtor para instaciar o repositório.
        /// </summary>
        public ExameNegocio()
        {
            _exameRepositorio = new ExameRepositorio();
        }

        /// <summary>
        /// Seleciona todas os Exames.
        /// </summary>
        /// <returns>Lista de Exames.</returns>
        public IEnumerable<Exame> Selecionar()
        {
            return _exameRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe o exame com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um exame no Database.</param>
        /// <returns>Seleciona um exame ou gera uma exceção.</returns>
        public Exame SelecionarPorId(int id)
        {
            var obj = _exameRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new DadoInvalidoException($"Não foi encontrado nenhum Exame com este ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se o paciente com o ID indicado realizou algum exame.
        /// </summary>
        /// <param name="id">Usado para buscar o paciente no Database.</param>
        /// <returns>Seleciona todos os exames realizados pelo paciente ou gera uma exceção.</returns>
        public IEnumerable<Exame> SelecionarPorPaciente(int id)
        {
            var lista = _exameRepositorio.SelecionarPorPaciente(id);

            if (lista == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum Exame realidado pelo " +
                                                $"paciente que possui o ID: {id}");
            }

            return lista;
        }

        /// <summary>
        /// Verifica se o médico com o ID indicado solicitou algum exame.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        /// <returns>Seleciona todos os exames solicitados pelo médico ou gera uma exceção.</returns>
        public IEnumerable<Exame> SelecionarPorMedicoQueSolicitou(int id)
        {
            var lista = _exameRepositorio.SelecionarPorMedicoQueSolicitou(id);

            if (lista == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum Exame solicitado pelo " +
                                                $"médico que possui o ID: {id}");
            }

            return lista;
        }

        /// <summary>
        /// Verifica se o médico com o ID indicado realizou algum exame.
        /// </summary>
        /// <param name="id">Usado para buscar o médico no Database.</param>
        /// <returns>Seleciona todos os exames realizados pelo médico ou gera uma exceção.</returns>
        public IEnumerable<Exame> SelecionarPorMedicoQueRealizou(int id)
        {
            var lista = _exameRepositorio.SelecionarPorMedicoQueRealizou(id);

            if (lista == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum Exame realidado pelo " +
                                                $"médico que possui o ID: {id}");
            }

            return lista;
        }

        // <summary>
        /// Verifica se algum exame foi realizado na clínica com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar a clínica no Database.</param>
        /// <returns>Seleciona todos os exames realizados na clínica ou gera uma exceção.</returns>
        public IEnumerable<Exame> SelecionarPorClinica(int id)
        {
            var lista = _exameRepositorio.SelecionarPorClinica(id);

            if (lista == null)
                throw new DadoInvalidoException($"Não foi encontrado nenhum Exame realidado na " +
                                                $"clínica que possui o ID: {id}");

            return lista;
        }

        /// <summary>
        /// Verifica se os campos obrigátorios foram preenchidos e se o ID do tipo de exame,
        /// do atentidmento e da consulta são válidos. Antes de inserir o exame o Database.
        /// </summary>
        /// <param name="entity">Objeto com os dados do Exame.</param>
        /// <returns>Insere um exame no Database ou gera alguma exceção.</returns>
        public int Inserir(Exame entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("O campo DataHora é obrigatório e deve ser preenchido!");
            }

            //Verifica se existe o Id do TipoExame é válido.
            var RepositorioTipoExame = new TipoExameRepositorio();
            if (RepositorioTipoExame.SelecionarPorId(entity.IdTipoExame) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum Tipo de Exame " +
                                                $"com o ID: {entity.IdTipoExame}");
            }

            //Verifica se existe o Id do atendimento é válido.
            var RepositorioAtendimento = new AtendimentoRepositorio();
            if (RepositorioAtendimento.SelecionarPorId(entity.IdAtendimento) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma Clínica" +
                                                $" com o ID: {entity.IdTipoExame}");
            }

            //Verifica se existe o Id da consulta é válido.
            var RepositorioConsulta = new ConsultaRepositorio();
            if (RepositorioConsulta.SelecionarPorId(entity.IdConsulta) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma Clínica" +
                                                $" com o ID: {entity.IdConsulta}");
            }

            return _exameRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se os campos obrigátorios foram preenchidos e se o ID do tipo de exame,
        /// do atentidmento e da consulta são válidos. Antes de alterar os dados do exame o Database.
        /// </summary>
        /// <param name="id">Usado prara buscar o exame.</param>
        /// <param name="entity">Objeto com os dados do Exame.</param>
        /// <returns>Seleciona o exame alterado no Database ou gera uma exceção.</returns>
        public Exame Alterar(int id, Exame entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("O campo DataHora é obrigatório e deve ser preenchido!");
            }

            //Verifica se existe o Id do TipoExame é válido.
            var RepositorioTipoExame = new TipoExameRepositorio();
            if (RepositorioTipoExame.SelecionarPorId(entity.IdTipoExame) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhum Tipo de Exame " +
                                                $"com o ID: {entity.IdTipoExame}");
            }

            //Verifica se existe o Id do atendimento é válido.
            var RepositorioAtendimento = new AtendimentoRepositorio();
            if (RepositorioAtendimento.SelecionarPorId(entity.IdAtendimento) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma Clínica" +
                                                $" com o ID: {entity.IdTipoExame}");
            }

            //Verifica se existe o Id da consulta é válido.
            var RepositorioConsulta = new ConsultaRepositorio();
            if (RepositorioConsulta.SelecionarPorId(entity.IdConsulta) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma Clínica" +
                                                $" com o ID: {entity.IdConsulta}");
            }

            entity.Id = id;
            _exameRepositorio.Alterar(entity);

            return _exameRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o exame existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o exame no Database.</param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);

            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: \"{id}\" não foi encontrado!");
            }
            _exameRepositorio.Deletar(obj.Id);
        }
    }
}
