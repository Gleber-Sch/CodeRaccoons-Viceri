using System.Net;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ValorExame")]
    public class ValorExameController : Controller
    {
        /// <summary>
        /// Contêm as regras de negócio do valor de Exame. 
        /// </summary>
        private ValorExameNegocio _valorExameNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public ValorExameController()
        {
            _valorExameNegocio = new ValorExameNegocio();
        }

        /// <summary>
        /// Método que obtêm todas os valores de exames.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ValorExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_valorExameNegocio.Selecionar());
        }


        /// <summary>
        /// Método que obtêm um valor de exame.
        /// </summary>
        /// <param name="id">Usado para selecionar o valor de exame.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um valor de exame pelo Id do valor de exame.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "ValorExameGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ValorExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_valorExameNegocio.SelecionarPorId(id));
        }


        /// <summary>
        /// Método que insere um valor de exame.
        /// </summary>
        /// <param name="input">Objeto com os dados do valor de exame.</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(ValorExame), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]ValorExameInput input)
        {
            var obj = new ValorExame()
            {
                IdTipoExame = input.IdTipoExame,
                IdClinica = input.IdClinica,
                Valor = input.Valor
                 
            };

            var idValorExame = _valorExameNegocio.Inserir(obj);
            obj.Id = idValorExame;
            return CreatedAtRoute("ValorExameGetId", new { id = idValorExame }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um valor de exame.
        /// </summary>
        /// <param name="id">Usado para selecionar um valor de exame.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(ValorExame), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]ValorExameInput input)
        {
            var obj = new ValorExame()
            {
                IdTipoExame = input.IdTipoExame,
                IdClinica = input.IdClinica,
                Valor = input.Valor
            };

            var objReturn = _valorExameNegocio.Alterar(id, obj);
            return Accepted(objReturn);
        }

        /// <summary>
        /// Método que deleta um valor de exame.
        /// </summary>
        /// <param name="id">Usado para selecionar um valor de exame.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _valorExameNegocio.Deletar(id);
            return Ok();
        }

    }
}