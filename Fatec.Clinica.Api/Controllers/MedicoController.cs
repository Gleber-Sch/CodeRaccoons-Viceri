using System.Net;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Medico")]
    public class MedicoController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private MedicoNegocio _medicoNegocio;

        /// <summary>
        /// 
        /// </summary>
        public MedicoController()
        {
            _medicoNegocio = new MedicoNegocio();
        }

        /// <summary>
        /// Método que obtem uma lista de médicos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_medicoNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um médico..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = "MedicoGetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_medicoNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que seleciona um médico..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Especialidade/{id}", Name = "MedicoGetIdEspecialidade")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetEspecialidadeId(int id)
        {
            return Ok(_medicoNegocio.SelecionarPorEspecialidade(id));
        }

        /// <summary>
        /// Método que seleciona um médico..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Crm/{Crm}", Name = "MedicoGetIdCrm")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetCrm(int Crm)
        {
            return Ok(_medicoNegocio.SelecionarPorCrm(Crm));
        }

        /// <summary>
        /// Método que seleciona um médico..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Cpf}", Name = "MedicoGetIdCpf")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetCpf(string Cpf)
        {
            return Ok(_medicoNegocio.SelecionarPorCpf(Cpf));
        }

        /// <summary>
        /// Método que insere um médico..
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
                Genero = input.Genero,
                IdEspecialidade = input.IdEspecialidade,
                Senha = input.Senha,
                StatusAtividade = true
            };

            var idMedico = _medicoNegocio.Inserir(obj);
            obj.Id = idMedico;
            return CreatedAtRoute("MedicoGetId", new { id = idMedico }, obj);
        }

        /// <summary>
        /// Método que altera um médico
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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
                Genero = input.Genero,
                IdEspecialidade = input.IdEspecialidade,
                Senha = input.Senha,
                StatusAtividade = input.StatusAtividade
            };

            var obj = _medicoNegocio.Alterar(id, objMedico);
            return Accepted(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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