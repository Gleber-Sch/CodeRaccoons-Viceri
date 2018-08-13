using Microsoft.AspNetCore.Mvc;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;


namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/TipoExame")]
    public class TipoExameController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio do Tipo de Exame.
        /// </summary>
        TipoExameNegocio _tipoExameNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public TipoExameController()
        {
            _tipoExameNegocio = new TipoExameNegocio();
        }

        /// <summary>
        /// Método que obtêm todos os Tipos dos Exames.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(TipoExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_tipoExameNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm um Tipo de Exame.
        /// </summary>
        /// <param name="id">Usado para selecionar o tipo de exame.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um tipo de exame pelo Id do tipo de exame.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "TipoExameGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(TipoExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_tipoExameNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que insere um Tipo de Exame.
        /// </summary>
        /// <param name="input">Objeto com os dados do tipo de exame.</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(TipoExame), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] TipoExameInput input)
        {
            var obj = new TipoExame()
            {
                Nome = input.Nome
            };

            var idTipoExame = _tipoExameNegocio.Inserir(obj);
            obj.Id = idTipoExame;
            return CreatedAtRoute("TipoExameGetId", new { id = idTipoExame }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um Tipo de Exame.
        /// </summary>
        /// <param name="id">Usado para selecionar o tipo de exame.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(TipoExame), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute] int id, [FromBody]TipoExameInput input)
        {
            var objTipoExame = new TipoExame()
            {
                Nome = input.Nome
            };

            var obj = _tipoExameNegocio.Alterar(id, objTipoExame);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta um Tipo de Exame.
        /// </summary>
        /// <param name="id">Usado para selecionar o Tipo de Exame.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _tipoExameNegocio.Deletar(id);
            return Ok();
        }
    }
}