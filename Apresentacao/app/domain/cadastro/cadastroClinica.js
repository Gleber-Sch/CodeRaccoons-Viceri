var urlApi = 'http://localhost:53731/api/Clinica/';

var clinica = 
{
    Email: document.querySelector('#login-cli'),
    Senha: document.querySelector('#senha-cli'),
    Nome: document.querySelector('#nome-cli'),
    Cnpj: document.querySelector('#cnpj-cli'),
    TelefoneCom: document.querySelector('#telefone-cli')
};

document.querySelector('.form-signin').addEventListener('submit', function(event)
{
    event.preventDefault();

    var obj = 
    {
        Email: clinica.Email.value,
        Senha: clinica.Senha.value,
        Nome: clinica.Nome.value,
        Cnpj: clinica.Cnpj.value,
        TelefoneCom: clinica.TelefoneCom.value
    };

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
            console.log(response);
        })
}