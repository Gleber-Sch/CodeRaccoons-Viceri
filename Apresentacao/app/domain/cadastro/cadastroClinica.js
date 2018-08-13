var urlApi = 'http://localhost:53731/api/Clinica/';
var urlEndereco = 'http://localhost:53731/api/Endereco'


var endereco =
{
    Estado: document.querySelector('#validationCustom04'),
    Cidade: document.querySelector('#validationCustom03'),
    Bairro: document.querySelector('#validationCustom01'),
    Logradouro: document.querySelector('#validationCustom00'),
    Numero: document.querySelector('#validationCustom02'),
    Complemento: document.querySelector('#validationCustom05')
}

document.querySelector('.form-signin').addEventListener('submit',function(event)
{
    event.preventDefault();
    var objendereco = 
    {
        Estado: endereco.Estado.value,
        Cidade: endereco.Cidade.value,
        Bairro: endereco.Bairro.value,
        Logradouro: endereco.Cidade.value,
        Numero: endereco.Numero.value,
        Complemento: endereco.Complemento.value
    }

    inserirEndereco(objendereco);
});

function inserirEndereco(objendereco)
{
    var request = new Request(urlEndereco, 
        {
            method: 'POST',
            headers: new Headers(
                {
                    'Content-Type': 'application/json'
                }),
                body: JSON.stringify(objendereco)
        });

        fetch(request)
        .then(function(response)
        {
            alert('Adicionado o Endereço');
            console.log(objendereco);
            return response.json(); 
        })
        .catch(function(response)
        {
            console.log(response);
        });
}




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
            alert("Incluído com sucesso");
            window.location.href="../login/login.html";
            
        })
        .catch(function(response)
        {
            console.log(response);
        })
}
