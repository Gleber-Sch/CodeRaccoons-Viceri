var urlApi = 'http://localhost:53731/api/Medico/';

var medico =
{
    Email: document.querySelector('#login-med'),
    Senha: document.querySelector('#senha-med'),
    Cpf: document.querySelector('#cpf-med'),
    Crm: document.querySelector('#crm-med'),
    CrmEstado: document.querySelector('#inputState'),
    Nome: document.querySelector('#nome-med'),
    IdEspecialidade: document.querySelector('#especialidade-med'),
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
        IdEspecialidade: medico.IdEspecialidade.value,
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
            return response.json();
        })
        .catch(function(response)
        {
            console.log(reponse);
        })
}