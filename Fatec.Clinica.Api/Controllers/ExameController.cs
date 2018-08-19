using System.Net;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Exame")]
    public class ExameController : Controller
    {
        /// <summary>
        /// Contêm as regras de negócio do Exame. 
        /// </summary>
        private ExameNegocio _exameNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public ExameController()
        {
            _exameNegocio = new ExameNegocio();
        }

        /// <summary>
        /// Método que obtêm todas os valores de exames.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_exameNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm um exame.
        /// </summary>
        /// <param name="id">Usado para selecionar o exame.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um exame pelo Id do exame.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "ExameGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_exameNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtêm todos os exames agendados ou realizados por um paciente.
        /// </summary>
        /// <param name="id">Usado para selecionar o paciente.</param>
        /// <returns></returns>
        /// <remarks>Obtêm todos os exames pelo Id do paciente.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Paciente/{id}", Name = "ExameGetIdPaciente")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetPaciente(int id)
        {
            return Ok(_exameNegocio.SelecionarPorPaciente(id));
        }

        /// <summary>
        /// Método que obtêm todos os exames solicitados por um medico.
        /// </summary>
        /// <param name="id">Usado para selecionar o médico.</param>
        /// <returns></returns>
        /// <remarks>Obtêm todos os exames pelo Id do médico.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Solicitados/Medico/{id}", Name = "ExameGetIdMedicoQueSolicitou")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetMedicoQueSolicitou(int id)
        {
            return Ok(_exameNegocio.SelecionarPorMedicoQueSolicitou(id));
        }

        /// <summary>
        /// Método que obtêm todos os exames realizados por um medico.
        /// </summary>
        /// <param name="id">Usado para selecionar o médico.</param>
        /// <returns></returns>
        /// <remarks>Obtêm todos os exames pelo Id do médico.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Realizados/Medico/{id}", Name = "ExameGetIdMedicoQueRealizou")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetMedicoQueRealizou(int id)
        {
            return Ok(_exameNegocio.SelecionarPorMedicoQueRealizou(id));
        }

        /// <summary>
        /// Método que obtêm todos os exames realizados em uma clínica.
        /// </summary>
        /// <param name="id">Usado para selecionar a clínica.</param>
        /// <returns></returns>
        /// <remarks>Obtêm todos os exames pelo Id da clínica.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Realizados/Clinica/{id}", Name = "ExameGetIdClinicaQueRealizou")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Exame), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetClinica(int id)
        {
            return Ok(_exameNegocio.SelecionarPorClinica(id));
        }

        /// <summary>
        /// Método que insere um exame.
        /// </summary>
        /// <param name="input">Objeto com os dados do exame.</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Exame), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]ExameInput input)
        {
            var obj = new Exame()
            {
                DataHora = input.DataHora,
                IdAtendimento = input.IdAtendimento,
                IdConsulta = input.IdConsulta,
                IdTipoExame = input.IdTipoExame
            };

            var idExame = _exameNegocio.Inserir(obj);
            obj.Id = idExame;
            return CreatedAtRoute("ExameGetId", new { id = idExame }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um exame.
        /// </summary>
        /// <param name="id">Usado para selecionar um exame.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Exame), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]ExameInput input)
        {
            var obj = new Exame()
            {
                DataHora = input.DataHora,
                IdAtendimento = input.IdAtendimento,
                IdConsulta = input.IdConsulta,
                IdTipoExame = input.IdTipoExame
            };

            var objReturn = _exameNegocio.Alterar(id, obj);
            return Accepted(objReturn);
        }

        /// <summary>
        /// Método que deleta um exame.
        /// </summary>
        /// <param name="id">Usado para selecionar um exame.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _exameNegocio.Deletar(id);
            return Ok();
        }
    }
}