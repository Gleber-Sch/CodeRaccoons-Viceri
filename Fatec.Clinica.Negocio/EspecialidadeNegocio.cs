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
        /// <returns></returns>
        public IEnumerable<Especialidade> Selecionar()
        {
            return _especialidadeRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se a especialidade com o ID indicado existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Especialidade SelecionarPorId(int id)
        {
            var obj = _especialidadeRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado nenhuma especialidade com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se o nome da especialidade já existe, antes de inserir os dados da especialidade
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Especialidade entity)
        {

            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException($"O Nome da Especialidade é obrigatório");
            }

            if (_especialidadeRepositorio.SelecionarPorNome(entity.Nome) != null)
            {
                throw new ConflitoException($"A especialidade: \"{entity.Nome}\", já está cadastrada");
            }

            return _especialidadeRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o nome da especialidade já existe, antes de alterar os dados da especialidade
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Especialidade Alterar(int id, Especialidade entity)
        {
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException($"O campo Nome da Especialidade é obrigatório!");
            }

            if (_especialidadeRepositorio.SelecionarPorNome(entity.Nome) != null)
            {
                throw new ConflitoException($"A especialidade {entity.Nome} já está cadastrado");
            }
            entity.Id = id;
            _especialidadeRepositorio.Alterar(entity);

            return _especialidadeRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se a especialidade existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);
            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: {id} não foi encontrado");
            }

            _especialidadeRepositorio.Deletar(obj.Id);
        }
    }
}
