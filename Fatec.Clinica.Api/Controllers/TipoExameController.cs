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

        TipoExameNegocio _tipoExameNegocio;

        public TipoExameController()
        {
            _tipoExameNegocio = new TipoExameNegocio();
        }

        /// <summary>
        /// Método que obtem uma lista de TipoExame
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(TipoExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_tipoExameNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um TipoExame por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = "TipoExameGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(TipoExame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_tipoExameNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que insere um novo TipoExame
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// Método que altera os dados de um atendimento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// Método que deleta um TipoExame
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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