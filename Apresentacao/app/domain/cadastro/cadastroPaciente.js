var urlApi = 'http://localhost:53731/api/Paciente/';

var paciente = {
    Email: document.querySelector('#login-pac'),
    Senha: document.querySelector('#senha-pac'),
    Cpf: document.querySelector('#cpf-pac'),
    Nome: document.querySelector('#nome-pac'),
    DataNasc: document.querySelector('#datanasc-pac'),
    TelefoneRes: document.querySelector('#telefone-pac'),
    Celular: document.querySelector('#celular-pac'),
    Genero: document.getElementById('#inlineRadio1').checked = true,
    Genero: document.getElementById('#inlineRadio2').checked = true,
    Genero: document.getElementById('#inlineRadio3').checked = true

};

document.querySelector('.form-signin').addEventListener('submit', function(event)
{
    var obj = 
    {
        Email: paciente.Email.value,
        Senha: paciente.Senha.value,
        Cpf: paciente.Cpf.value,
        Nome: paciente.Nome.value,
        DataNasc: paciente.DataNasc.value,
        TelefoneRes: paciente.TelefoneRes.value,
        Celular: paciente.Celular.value,
        Genero: paciente.Genero.value

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