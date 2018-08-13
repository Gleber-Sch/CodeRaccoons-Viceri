var login = {
    email: document.querySelector('#email'),
    senha: document.querySelector('#senha'),
    tipoUsuario: document.querySelector('#usuario')
};

if(login.tipoUsuario.value == 'paciente')
{
    var api = 'http://localhost:53731/api/Paciente/Email/';
}
else if(login.usuario.value == 'medico')
{
    var api = 'http://localhost:53731/api/medico/login/';
}
else
{
    var api = 'http://localhost:53731/api/clinica/login/';
}

document.querySelector('.form-signin').addEventListener('submit', function(event)
{
    event.preventDefault();

    var obj = 
    {
        email: login.email.value,
        senha: login.senha.value
    };

    obterUsuario(obj);
});

function obterUsuario(obj)
{
    var request = new Request(api+ obj.email,
        {
            method: 'GET',
            headers: new Headers(
                {
                    'Content-Type': 'application/json'
                }),
                body: JSON.stringify(obj)
        });

        fetch(request)
        .then(function (response) {
            if (response.status == 200) {
                response.json()
                .then(function(usuario){
                    // window.location.href="../pacientes/paciente.html?";
                    alert(usuario.Id);
                    return response.json();
                });
            } else {
                alert("Email ou senha inválidos!");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });

        

/*document.querySelector('#form-login')
    .addEventListener('submit', function (event) {

        event.preventDefault();
        

        var obj = {
            login: elementosForm.login.value,
            senha: elementosForm.senha.value
        };

        autenticarUsuario(obj);

    });

function autenticarUsuario(obj){
    console.log(obj);

    window.location.href="../medico/medico.html";*/
}