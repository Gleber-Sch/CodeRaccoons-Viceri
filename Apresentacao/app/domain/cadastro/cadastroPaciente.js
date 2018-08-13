var urlApi = 'http://localhost:53731/api/Paciente/';

obterTodos();

var paciente = {
    Email: document.querySelector('#login-pac'),
    Senha: document.querySelector('#senha-pac'),
    Cpf: document.querySelector('#cpf-pac'),
    Nome: document.querySelector('#nome-pac'),
    DataNasc: document.querySelector('#datanasc-pac'),
    TelefoneRes: document.querySelector('#telefone-pac'),
    Celular: document.querySelector('#celular-pac'),
    Genero: document.querySelector('#inlineRadio')
};

document.querySelector('.form-signin').addEventListener('submit', function(event)
{
    event.preventDefault();
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
            alert("Incluído com sucesso");
            return response.json();
        })
        .then(function(paciente){

            obterTodos();
        })
        .catch(function(response)
        {
            console.log(reponse);
        })
}

function obterTodos(){

    var request = new Request(urlApi, {
        method: 'GET',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function(response){
            if(response.status == 200)
                return response.json();
            alert('Sua requisição falhou.');
        })
        .then(function(paciente){
            console.log(paciente);
            update(paciente);
        })
        .catch(function(response){
            console.log(response);
        });

}
