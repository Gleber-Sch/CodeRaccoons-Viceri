var urlApi = 'http://localhost:53731/api/Medico/';
var apiEspecialidade = 'http://localhost:53731/api/especialidade/';

obterEspecialidades();
var medico =
{
    Email: document.querySelector('#login-med'),
    Senha: document.querySelector('#senha-med'),
    Cpf: document.querySelector('#cpf-med'),
    Crm: document.querySelector('#crm-med'),
    CrmEstado: document.querySelector('#inputState'),
    Nome: document.querySelector('#nome-med'),
    IdEspecialidade: document.querySelector('#especialidade'),
    DataNasc: document.querySelector('#datanasc-med'),
    Celular: document.querySelector('#celular-med'),
    Genero: document.querySelector('#inlineRadio')
};

document.querySelector('.form-signin').addEventListener('submit', function(event)
{
    event.preventDefault();
    var obj = 
    {
        Email: medico.Email.value,
        Senha: medico.Senha.value,
        Cpf: medico.Cpf.value,
        Crm: medico.Crm.value,
        CrmEstado: medico.CrmEstado.value,
        Nome: medico.Nome.value,
        IdEspecialidade: parseInt(medico.IdEspecialidade.value),
        DataNasc: medico.DataNasc.value,
        Celular: medico.Celular.value,
        Genero: medico.Genero.value
    }
    inserir(obj);
});

function inserir(obj)
{
    var request = new Request(urlApi, 
        {
            method: 'POST',
            headers: new Headers(
                {
                    'Content-Type': 'application/json'
                }),
                body: JSON.stringify(obj)
        });

        fetch(request)
        .then(function(response)
        {
            alert("Incluído com sucesso");
            window.location.href="../login/login.html";
            return response.json();
        })
        .catch(function(response)
        {
            console.log(reponse);
        })
}

function obterEspecialidades(id){
    var request = new Request(apiEspecialidade, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            // console.log(response);
            if (response.status == 200) {
                response.json()
                .then(function(especialidades){
                    updateTemplateEspecialidades(especialidades, id);
                });
            } else {
                alert("Ocorreu um erro ao obter as especialidades");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

function updateTemplateEspecialidades(especialidades, id){
    especialidade.innerHTML = templateEspecialidades(especialidades, id);
}

function templateEspecialidades(especialidades = [], id = null){
    return `
        <option>Selecione</option>
        ${
            especialidades.map(function(especialidade){
                return `
                    <option value="${especialidade.id}" ${especialidade.id == id ? 'selected' : ''}>${especialidade.nome}</option>
                `;
            }).join('')
        }
    `;
}