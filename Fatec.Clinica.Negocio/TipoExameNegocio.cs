using System.Collections.Generic;
using Fatec.Clinica.Negocio.Validacoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio.Excecoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o tipo de exame.
    /// </summary>
    public class TipoExameNegocio : Validacao, INegocioBase<TipoExame>
    {

        private readonly TipoExameRepositorio _tipoExameRepositorio;

        /// <summary>
        /// Construtor para instaciar o repositório.
        /// </summary>
        public TipoExameNegocio()
        {
            _tipoExameRepositorio = new TipoExameRepositorio();
        }

        /// <summary>
        /// Seleciona todas os Tipos de Exames.
        /// </summary>
        /// <returns>Lista de tipos de exame.</returns>
        public IEnumerable<TipoExame> Selecionar()
        {
            return _tipoExameRepositorio.Selecionar();
        }


        /// <summary>
        /// Seleciona um Tipo de Exame pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TipoExame SelecionarPorId(int id)
        {
            var obj = _tipoExameRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new DadoInvalidoException($"Não foi encontrado nenhum Tipo de Exame com este Id {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se o nome do Tipo de Exame já existe, antes de inserir os dados
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(TipoExame entity)
        {
            if (VerificarCamposVazios(entity))
            {
                throw new DadoInvalidoException("O Nome do Tipo do Exame é obrigatório");
            }
            if(_tipoExameRepositorio.SelecionarPorNome(entity.Nome) == null)
            {
                throw new DadoInvalidoException("O Nome do Tipo do Exame não existe");
            }

            return _tipoExameRepositorio.Inserir(entity);
        }


        /// <summary>
        /// Verifica se o nome do Tipo de Exame já existe, antes de alterar os dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TipoExame Alterar(int id, TipoExame entity)
        {
            if (VerificarCamposVazios(entity))
            {
                throw new DadoInvalidoException("O Nome do Tipo do Exame é obrigatório");
            }
            if (_tipoExameRepositorio.SelecionarPorNome(entity.Nome) == null)
            {
                throw new DadoInvalidoException("O Nome do Tipo do Exame não existe");
            }
            entity.Id = id;
            _tipoExameRepositorio.Alterar(entity);

            return _tipoExameRepositorio.SelecionarPorId(id);
        }


        /// <summary>
        /// Verifica se o Tipo de Exame existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = _tipoExameRepositorio.SelecionarPorId(id);
            if(obj == null)
            {
                throw new NaoEncontradoException($"O ID {id} não foi encontrado");
            }

            _tipoExameRepositorio.Deletar(id);
        }

    }
}
