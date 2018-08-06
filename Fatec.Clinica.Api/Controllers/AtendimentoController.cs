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
        private AtendimentoNegocio _atendimentoNegocio;

        public AtendimentoController()
        {
            _atendimentoNegocio = new AtendimentoNegocio();
        }

        /// <summary>
        /// Método que obtem uma lista de Atendimento
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Atendimento), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_atendimentoNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um atendimento por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Atendimento), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_atendimentoNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que insere um novo atendimento
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Atendimento), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] AtedimentoInput input)
        {
            var objAtendimento = new Atendimento()
            {
                Clinica = input.Clinica,
                Medico = input.Medico
            };

            var idAtendimento = _atendimentoNegocio.Inserir(objAtendimento);
            objAtendimento.Id = idAtendimento;
            return CreatedAtRoute(nameof(GetId), new { id = idAtendimento }, objAtendimento);
        }

        /// <summary>
        /// Método que altera os dados de um atendimento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Atendimento), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute] int id, [FromBody]AtedimentoInput input)
        {
            var objAtendimento = new Atendimento()
            {
                Clinica = input.Clinica,
                Medico = input.Medico
            };
            var obj = _atendimentoNegocio.Alterar(id, objAtendimento);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta um atendimento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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