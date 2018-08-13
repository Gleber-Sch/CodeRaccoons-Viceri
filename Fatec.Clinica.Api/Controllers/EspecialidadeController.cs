using System.Net;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Especialidade")]
    public class EspecialidadeController : Controller
    {
        /// <summary>
        /// Declara as regras de negócio da Especialidade. 
        /// </summary>
        private EspecialidadeNegocio _especialidadeNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public EspecialidadeController()
        {
            _especialidadeNegocio = new EspecialidadeNegocio();
        }

        /// <summary>
        /// Método que obtêm todas as especialidades.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Especialidade), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_especialidadeNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm uma especialidade.
        /// </summary>
        /// <param name="id">Usado para selecionar a especialidade.</param>
        /// <returns></returns>
        /// <remarks>Obtêm uma especialidade pelo Id da especialidade.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "EspecialidadeGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Especialidade), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_especialidadeNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que insere uma especialidade.
        /// </summary>
        /// <param name="input">Objeto com os dados da especialidade.</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Especialidade), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]EspecialidadeInput input)
        {
            var obj = new Especialidade()
            {
                Nome = input.Nome
            };

            var idEspecialidade = _especialidadeNegocio.Inserir(obj);
            obj.Id = idEspecialidade;
            return CreatedAtRoute("EspecialidadeGetId", new { id = idEspecialidade }, obj);
        }

        /// <summary>
        /// Método que altera os dados de uma especialidade.
        /// </summary>
        /// <param name="id">Usado para selecionar a especialidade.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Especialidade), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]EspecialidadeInput input)
        {
            var obj = new Especialidade()
            {
                Nome = input.Nome
            };

            var objReturn = _especialidadeNegocio.Alterar(id, obj);
            return Accepted(objReturn);
        }

        /// <summary>
        /// Método que deleta uma especialidade.
        /// </summary>
        /// <param name="id">Usado para selecionar a especialidade.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _especialidadeNegocio.Deletar(id);
            return Ok();
        }
    }
}