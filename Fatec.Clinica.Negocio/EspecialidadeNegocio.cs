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
    public class EspecialidadeNegocio : Validacao, INegocioBase<Especialidade>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly EspecialidadeRepositorio _especialidadeRepositorio;

        /// <summary>
        /// 
        /// </summary>
        public EspecialidadeNegocio()
        {
            _especialidadeRepositorio = new EspecialidadeRepositorio();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Especialidade> Selecionar()
        {
            return _especialidadeRepositorio.Selecionar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Especialidade SelecionarPorId(int id)
        {
            var obj = _especialidadeRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado uma especialidade com o ID {id}!");

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Especialidade entity)
        {
          
            if (VerificarCamposVazios(entity))
            {
                throw new DadoInvalidoException($"O campo Nome da Especialidade é obrigatório");
            }

            if (_especialidadeRepositorio.SelecionarPorNome(entity.Nome) != null)
            {
                throw new ConflitoException($"A especialidade {entity.Nome} já está cadastrado");
            }
            
            return _especialidadeRepositorio.Inserir(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Especialidade Alterar(int id, Especialidade entity)
        {
            if (VerificarCamposVazios(entity))
            {
                throw new DadoInvalidoException($"O campo Nome da Especialidade é obrigatório");
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);
            if(obj == null)
            {
                throw new NaoEncontradoException($"O ID {id} não foi encontrado");
            }

            _especialidadeRepositorio.Deletar(obj.Id);
        }
    }
}
