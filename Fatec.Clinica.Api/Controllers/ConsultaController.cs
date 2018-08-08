using Microsoft.AspNetCore.Mvc;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;


namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Consulta")]
    public class ConsultaController : Controller
    {
        ConsultaNegocio _consultaNegocio;

        public ConsultaController()
        {
            _consultaNegocio = new ConsultaNegocio();
        }

        /// <summary>
        /// Método que obtem uma lista de Consultas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_consultaNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona uma consulta por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = "ConsultaGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_consultaNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que seleciona uma consulta pelo Id do paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Paciente/{id}", Name = "ConsultaGetIdPaciente")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetPaciente(int id)
        {
            return Ok(_consultaNegocio.SelecionarPorPaciente(id));
        }

        /// <summary>
        /// Método que seleciona uma consulta pelo Id do medico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Medico/{id}", Name = "ConsultaGetIdMedico")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetMedico(int id)
        {
            return Ok(_consultaNegocio.SelecionarPorMedico(id));
        }

        /// <summary>
        /// Método que seleciona uma consulta pelo Id da clinica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Clinica/{id}", Name = "ConsultaGetIdClinica")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Consulta), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetClinica(int id)
        {
            return Ok(_consultaNegocio.SelecionarPorClinica(id));
        }

        /// <summary>
        /// Método que insere uma novo consulta
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Consulta), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] ConsultaInput input)
        {
            var obj = new Consulta()
            {
                IdAtendimento = input.IdAtendimento,
                DataHora = input.DataHora,
                Historico = input.Historico,
                Nota = input.Nota,
                IdPaciente = input.IdPaciente
            };

            var idConsulta = _consultaNegocio.Inserir(obj);
            obj.Id = idConsulta;
            return CreatedAtRoute("ConsultaGetId", new { id = idConsulta }, obj);
        }

        /// <summary>
        /// Método que altera os dados de uma consulta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Consulta), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute] int id, [FromBody]ConsultaInput input)
        {
            var objConsulta = new Consulta()
            {
                IdAtendimento = input.IdAtendimento,
                DataHora = input.DataHora,
                Historico = input.Historico,
                Nota = input.Nota,
                IdPaciente = input.IdPaciente
            };

            var obj = _consultaNegocio.Alterar(id, objConsulta);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta uma consulta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _consultaNegocio.Deletar(id);
            return Ok();
        }
    }
}