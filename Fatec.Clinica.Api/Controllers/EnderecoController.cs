using System.Net;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Endereco")]
    public class EnderecoController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio do Endereço. 
        /// </summary>
        private EnderecoNegocio _enderecoNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public EnderecoController()
        {
            _enderecoNegocio = new EnderecoNegocio();
        }

        /// <summary>
        /// Método que obtêm todas os Endereços.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Endereco), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_enderecoNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm um endereço.
        /// </summary>
        /// <param name="id">Usado para selecionar o endereço.</param>
        /// <returns></returns>
        /// <remarks>Obtêm uma consulta pelo Id do endereço.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "EnderecoGetId")]
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
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
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
            return CreatedAtRoute("EnderecoGetId", new { id = idEndereco }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um endereço.
        /// </summary>
        /// <param name="id">Usado para selecionar o endereço.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
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
        /// <param name="id">Usado para selecionar o endereço.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
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