using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Endereco")]
    public class EnderecoController : Controller
    {
        /// <summary>
        /// Contêm as regras de negócio do Endereço. 
        /// </summary>
        private EnderecoNegocio _enderecoNegocio;

        /// <summary>
        /// Construtor para instaciar as regras de negócio do Endereço.
        /// </summary>
        public EnderecoController()
        {
            _enderecoNegocio = new EnderecoNegocio();
        }

        /// <summary>
        /// Método que obtêm os endereços.
        /// </summary>
        /// <returns>Todos os endereços registrados.</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Endereco), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_enderecoNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um endereço.
        /// </summary>
        /// <param name="id">Usado para buscar o endereço.</param>
        /// <returns>Endereço selecionado.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Endereco), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_enderecoNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que insere um endereço.
        /// </summary>
        /// <param name="input">Objeto com os dados do endereço.</param>
        /// <returns>ID criado para o endereço inserido.</returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Endereco), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]EnderecoInput input)
        {
            var obj = new Endereco()
            {
                Estado = input.Estado,
                Cidade = input.Cidade,
                Bairro = input.Bairro,
                Logradouro = input.Logradouro,
                Numero = input.Numero,
                Complemento = input.Complemento,
                IdClinica = input.IdClinica
            };

            var idEndereco = _enderecoNegocio.Inserir(obj);
            obj.Id = idEndereco;
            return CreatedAtRoute("GetId", new { id = idEndereco }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um endreço.
        /// </summary>
        /// <param name="id">Usado para buscar o endereço.</param>
        /// <param name="input">Objeto com os dados do endereço.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Endereco), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]EnderecoInput input)
        {
            var obj = new Endereco()
            {
                Estado = input.Estado,
                Cidade = input.Cidade,
                Bairro = input.Bairro,
                Logradouro = input.Logradouro,
                Numero = input.Numero,
                Complemento = input.Complemento,
                IdClinica = input.IdClinica
            };

            var objReturn = _enderecoNegocio.Alterar(id, obj);
            return Accepted(objReturn);
        }

        /// <summary>
        /// Método que deleta um endereço.
        /// </summary>
        /// <param name="id">Usado para selecionar o endereço a ser deletado.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _enderecoNegocio.Deletar(id);
            return Ok();
        }
    }
}