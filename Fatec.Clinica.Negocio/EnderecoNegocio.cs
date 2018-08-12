using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;
using Fatec.Clinica.Negocio.Validacoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Regras de Negócio sobre o Endereco da Clinica.
    /// </summary>
    public class EnderecoNegocio : INegocioBase<Endereco>
    {
        /// <summary>
        /// Declara o repositório do Endereço.
        /// </summary>
        private readonly EnderecoRepositorio _enderecoRepositorio;

        /// <summary>
        /// Construtor que inicializa o repositório.
        /// </summary>
        public EnderecoNegocio()
        {
            _enderecoRepositorio = new EnderecoRepositorio();
        }

        /// <summary>
        /// Seleciona todos os Enderecos da Database.
        /// </summary>
        /// <returns>Lista de Endereços.</returns>
        public IEnumerable<Endereco> Selecionar()
        {
            return _enderecoRepositorio.Selecionar();
        }

        /// <summary>
        /// Verifica se existe algum endereço com o ID indicado.
        /// </summary>
        /// <param name="id">Usado para buscar um endereço no Database.</param>
        /// <returns>Seleciona um atendimento ou gera uma exceção.</returns>
        public Endereco SelecionarPorId(int id)
        {
            var obj = _enderecoRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException($"Não foi encontrado um Endereco com o ID: {id}");

            return obj;
        }

        /// <summary>
        /// Verifica se existem campos obrigatórios não preenchidos, se o ID da clínica é válidos,
        /// se os campos respeitam os limites de caracteres especificados no Database e se o endereço
        /// já não foi cadastrado. Antes de inserir os dados do endereço.
        /// </summary>
        /// <param name="entity">Objeto com os dados do endereco.</param>
        /// <returns>ID do endereco inserido no Databse ou gera uma exceção.</returns>
        public int Inserir(Endereco entity)
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
            
            //Verifica se o endereço informado já foi cadastrado.
            if (_enderecoRepositorio.SelecionarPorEndereco(entity) != null)
            {
                throw new ConflitoException("O Endereco informado está cadastrado!");
            }

            //Verifica se o ID da clínica é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdClinica) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma clínica " +
                                                $"com o ID: {entity.IdClinica}");
            }

            return _enderecoRepositorio.Inserir(entity);
        }

        /// <summary>
        /// Verifica se existem campos obrigatórios não preenchidos, se o ID da clínica é válidos,
        /// se os campos respeitam os limites de caracteres especificados no Database e se o endereço
        /// já não foi cadastrado. Antes de inserir os dados do endereço.
        /// </summary>
        /// <param name="id">Usado para buscar o endereço.</param>
        /// <param name="entity">Objeto com os dados do endereço.</param>
        /// <returns>Seleciona o endereço alterado no Database ou gera uma exceção.</returns>
        public Endereco Alterar(int id, Endereco entity)
        {
            Endereco obj;

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

            //Verifica se o endereço informado já foi cadastrado.
            obj = _enderecoRepositorio.SelecionarPorEndereco(entity);
            if (obj != null && obj.Id != id)
            {
                throw new ConflitoException("O Endereco informado está cadastrado!");
            }

            //Verifica se o ID da clínica é válido.
            var RepositorioClinica = new ClinicaRepositorio();
            if (RepositorioClinica.SelecionarPorId(entity.IdClinica) == null)
            {
                throw new DadoInvalidoException($"Não foi encontrado nenhuma clínica " +
                                                $"com o ID: {entity.IdClinica}");
            }

            entity.Id = id;
            _enderecoRepositorio.Alterar(entity);

            return _enderecoRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// Verifica se o endereco existe no Database antes de deleta-lo.
        /// </summary>
        /// <param name="id">Usado para buscar o endereço no Database.</param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);
            if (obj == null)
            {
                throw new NaoEncontradoException($"O ID: \"{id}\" não foi encontrado!");
            }

            _enderecoRepositorio.Deletar(obj.Id);
        }


    }
}
