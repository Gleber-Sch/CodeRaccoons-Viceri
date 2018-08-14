using System.Net;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Medico")]
    public class MedicoController : Controller
    {
        /// <summary>
        /// Contêm as regras de negócio do médico. 
        /// </summary>
        private MedicoNegocio _medicoNegocio;

        /// <summary>
        /// Construtor para instanciar as regras de negócio.
        /// </summary>
        public MedicoController()
        {
            _medicoNegocio = new MedicoNegocio();
        }

        /// <summary>
        /// Método que obtêm todos os médicos.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_medicoNegocio.Selecionar());
        }

        /// <summary>
        /// Método que obtêm um médico.
        /// </summary>
        /// <param name="id">Usado para selecionar o médico.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um médico pelo Id do médico.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("{id}", Name = "MedicoGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_medicoNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtêm médicos por especialidade.
        /// </summary>
        /// <param name="id">Usado para selecionar a especialidade.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um médico pelo Id da especialidade.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Especialidade/{id}", Name = "MedicoGetIdEspecialidade")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetEspecialidadeId(int id)
        {
            return Ok(_medicoNegocio.SelecionarPorEspecialidade(id));
        }

        /// <summary>
        /// Método que obtêm um médico.
        /// </summary>
        /// <param name="id">Usado para selecionar o médico.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um médico pelo Crm do médico.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Crm/{Crm}", Name = "MedicoGetIdCrm")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetCrm(int Crm)
        {
            return Ok(_medicoNegocio.SelecionarPorCrm(Crm));
        }

        /// <summary>
        /// Método que obtêm um médico.
        /// </summary>
        /// <param name="id">Usado para selecionar o médico.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um médico pelo Cpf do médico.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Cpf/{Cpf}", Name = "MedicoGetIdCpf")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetCpf(string Cpf)
        {
            return Ok(_medicoNegocio.SelecionarPorCpf(Cpf));
        }

        /// <summary>
        /// Método que obtêm um médico.
        /// </summary>
        /// <param name="email">Usado para selecionar o médico.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um paciente pelo email do médico.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Email/{email}", Name = "MedicoGetEmail")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Paciente), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetEmail(string email)
        {
            return Ok(_medicoNegocio.SelecionarPorEmail(email));
        }

        /// <summary>
        /// Método que obtêm o Id de um médico para realizar o login.
        /// </summary>
        /// <param name="email">Usado para selecionar o médico.</param>
        /// <param name="senha">Usado para selecionar o médico.</param>
        /// <returns></returns>
        /// <remarks>Obtêm um paciente pelo Id do paciente.</remarks>
        /// <response code="200">OK</response>
        /// <response code="404">NotFoud</response>
        [HttpGet]
        [Route("Login/{senha}&{email}", Name = "MedicoGetLogin")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Medico), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetLogin(string email, string senha)
        {
            return Ok(_medicoNegocio.Login(email, senha));
        }

        /// <summary>
        /// Método que insere um médico.
        /// </summary>
        /// <param name="input">Objeto com os dados do médico.</param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Medico), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]MedicoInput input)
        {
            var obj = new Medico()
            {
                Nome = input.Nome,
                Cpf = input.Cpf,
                Celular = input.Celular,
                Email = input.Email,
                Crm = input.Crm,
                CrmEstado = input.CrmEstado,
                DataNasc = input.DataNasc,
                Genero = input.Genero,
                IdEspecialidade = input.IdEspecialidade,
                Senha = input.Senha
            };

            var idMedico = _medicoNegocio.Inserir(obj);
            obj.Id = idMedico;
            return CreatedAtRoute("MedicoGetId", new { id = idMedico }, obj);
        }

        /// <summary>
        /// Método que altera os dados de um médico.
        /// </summary>
        /// <param name="id">Usado para selecionar um médico.</param>
        /// <param name="input">Objeto que contêm os dados a serem alterados.</param>
        /// <returns></returns>
        /// <response code="202">Accepted</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Medico), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]MedicoInput input)
        {
            var objMedico = new Medico()
            {
                Nome = input.Nome,
                Cpf = input.Cpf,
                Celular = input.Celular,
                Email = input.Email,
                Crm = input.Crm,
                CrmEstado = input.CrmEstado,
                DataNasc = input.DataNasc,
                Genero = input.Genero,
                IdEspecialidade = input.IdEspecialidade,
                Senha = input.Senha
            };

            var obj = _medicoNegocio.Alterar(id, objMedico);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta um médico.
        /// </summary>
        /// <param name="id">Usado para selecionar um médico.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">NotFound</response>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _medicoNegocio.Deletar(id);
            return Ok();
        }
    }
}