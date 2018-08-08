using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre a Endereco da Clinica
    /// </summary>
    public class EnderecoNegocio : INegocioBase<Endereco>
    {
        private readonly EnderecoRepositorio _enderecoRepositorio;

        /// <summary>
        /// Construtor para instanciar o Endereco.
        /// </summary>
        public EnderecoNegocio()
        {
            _enderecoRepositorio = new EnderecoRepositorio();
        }

        /// <summary>
        /// Seleciona todos os Enderecos da Database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Endereco> Selecionar()
        {
            return _enderecoRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se o Endereco com o ID indicado existe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Endereco SelecionarPorId(int id)
        {
            var obj = _enderecoRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado um Endereco com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se o nome da especialidade já existe, antes de inserir os dados da especialidade
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Endereco entity)
        {

            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException($"Os Campos: Estado, Cidade, Bairro, Logradouro, Numero, Complemento, Clinica são obrigatorios.");
            }

            if (_enderecoRepositorio.SelecionarPorEndereco(entity) != null)
            {
                throw new ConflitoException($"O Endereco informado ja foi cadastrado");
            }

            return _enderecoRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se o endereco informado ja foi cadastrado.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Endereco Alterar(int id, Endereco entity)
        {
            if (CamposVazios.Verificar(entity))
            {
                throw new DadoInvalidoException($"Os campos: Estado, Cidade, Bairro, Logradouro," +
                                                $" Numero, Complemento, Clinica são obrigatorios.");
            }

            if (_enderecoRepositorio.SelecionarPorEndereco(entity) != null)
            {
                throw new ConflitoException($"O Endereco informado ja foi cadastrado");
            }
            entity.Id = id;
            _enderecoRepositorio.Alterar(entity);

            return _enderecoRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o endereco existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);
            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: {id} não foi encontrado");
            }

            _enderecoRepositorio.Deletar(obj.Id);
        }


    }
}
