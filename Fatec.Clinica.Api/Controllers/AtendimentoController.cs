using Microsoft.AspNetCore.Mvc;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Atendimento")]
    public class AtendimentoController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio do Atendimento.
        /// </summary>
        private AtendimentoNegocio _atendimentoNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public AtendimentoController()
        {
            _atendimentoNegocio = new AtendimentoNegocio();
        }

        /// <summary>
        /// Método que obtêm uma lista com todos os Atendimentos.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Atendimento), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_atendimentoNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um atendimento.
        /// </summary>
        /// <param name="id">Usado para selecionar o atendimento.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "AtendimentoGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Atendimento), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_atendimentoNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que insere um novo atendimento.
        /// </summary>
        /// <param name="input">Objeto com os dados do atendimento.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Atendimento), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] AtendimentoInput input)
        {
            var obj = new Atendimento()
            {
                IdClinica = input.IdClinica,
                IdMedico = input.IdMedico
            };

            var idAtendimento = _atendimentoNegocio.Inserir(obj);
            obj.Id = idAtendimento;
            return CreatedAtRoute("AtendimentoGetId", new { id = idAtendimento }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um atendimento.
        /// </summary>
        /// <param name="id">Usado para buscar o atendimento.</param>
        /// <param name="input">Objeto que contêm os dados a serem alteradas.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Atendimento), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute] int id, [FromBody]AtendimentoInput input)
        {
            var objAtendimento = new Atendimento()
            {
                IdClinica = input.IdClinica,
                IdMedico = input.IdMedico
            };
            var obj = _atendimentoNegocio.Alterar(id, objAtendimento);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta um atendimento
        /// </summary>
        /// <param name="id">Usado para buscar o atendimento.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _atendimentoNegocio.Deletar(id);
            return Ok();
        }
    }
}