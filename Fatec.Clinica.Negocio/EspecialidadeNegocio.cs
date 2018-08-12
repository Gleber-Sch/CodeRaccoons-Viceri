using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre a Especialidade do Médico
    /// </summary>
    public class EspecialidadeNegocio : INegocioBase<Especialidade>
    {
        /// <summary>
        /// Declara o repositório da Especialidade.
        /// </summary>
        private readonly EspecialidadeRepositorio _especialidadeRepositorio;

        /// <summary>
        /// Construtor para instanciar o repositório.
        /// </summary>
        public EspecialidadeNegocio()
        {
            _especialidadeRepositorio = new EspecialidadeRepositorio();
        }

        /// <summary>
        /// Seleciona todas as Especialidades do Database.
        /// </summary>
        /// <returns>Lista de Especialidades.</returns>
        public IEnumerable<Especialidade> Selecionar()
        {
            return _especialidadeRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe alguma especialidade com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar uma especialidade no Database.</param>
        /// <returns>Seleciona uma especialidade ou gera uma exceção.</returns>
        public Especialidade SelecionarPorId(int id)
        {
            var obj = _especialidadeRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhuma especialidade com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se nome da especialidade foi preenchido, se ele respeita o limite de caracteres
        /// determinado no Database e se ele já foi casatrado. Antes de inserir a especialidade.
        /// </summary>
        /// <param name="entity">Objeto com os dados da especialidade.</param>
        /// <returns>ID da especialidade inserido no Databse ou gera alguma exceção.</returns>
        public int Inserir(Especialidade entity)
        {
            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("O campo \"Nome\" é obrigatório e deve ser preenchido!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("O campo \"Nome\" excedeu o limite de caracteres permitidos!");
            }

            //Verifica se a especialidade já não foi cadastrada.
            if (_especialidadeRepositorio.SelecionarPorNome(entity.Nome) != null)
            {
                throw new ConflitoException($"A especialidade: \"{entity.Nome}\", já foi cadastrada!");
            }

            return _especialidadeRepositorio.Inserir(entity);
        }

        /// <summary>
        ///Verifica se nome da especialidade foi preenchido, se ele respeita o limite de caracteres
        /// determinado no Database e se ele já foi cadastrado. Antes de alterar os dados da especialidade.
        /// </summary>
        /// <param name="entity">Objeto com os dados da especialidade.</param>
        /// <param name="id">Usado para buscar a especialidade no Database.</param>
        /// <returns>Seleciona a especialidade alterada no Database ou gera uma exceção.</returns>
        public Especialidade Alterar(int id, Especialidade entity)
        {
            Especialidade obj;

            //Verifica se existem campos vazios.
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException("O campo \"Nome\" é obrigatório e deve ser preenchido!");
            }

            //Verifica se nenhum campo do objeto entity excede o limite de caracteres estipulado no Database.
            if (ExcedeLimiteDeCaracteres.Verificar(entity))
            {
                throw new DadoInvalidoException("Existem campos que excedem o limite de caracteres permitidos!");
            }

            //Verifica se a especialidade já não foi cadastrada.
            obj = _especialidadeRepositorio.SelecionarPorNome(entity.Nome);
            if (obj != null && obj.Id != id)
            {
                throw new ConflitoException($"A especialidade: \"{entity.Nome}\", já foi cadastrada!");
            }

            entity.Id = id;
            _especialidadeRepositorio.Alterar(entity);

            return _especialidadeRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se a especialidade existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar a especialidade no Database.</param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);
            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: \"{id}\" não foi encontrado!");
            }

            _especialidadeRepositorio.Deletar(obj.Id);
        }
    }
}
