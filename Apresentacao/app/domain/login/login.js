var login = {
    email: document.querySelector('#email'),
    senha: document.querySelector('#senha'),
    tipoUsuario: document.querySelector('#usuario')
};

if(login.tipoUsuario.value == 'paciente')
{
    var api = 'http://localhost:53731/api/Paciente/Login/';
    var direciona ='../pacientes/paciente.html';
}
else if(login.usuario.value == 'medico')
{
    var api = 'http://localhost:53731/api/medico/Login/';
    var direciona ='../medico/medico.html';
}
else
{
    var api = 'http://localhost:53731/api/clinica/Login/';
    var direciona ='../clinicas/clinica.html';
}

document.querySelector('.form-signin').addEventListener('submit', function(event)
{
    event.preventDefault();

    var obj = {    
        email: login.email.value,
        senha: login.senha.value
    };

    obterUsuario(obj);
});

function obterUsuario(objUsuario) 
{
    var request = new Request(api + objUsuario.senha +"&"+ objUsuario.email, 
    {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) 
        {
            //console.log(response);
            if (response.status == 200) 
            {
                response.json()
                .then(function(IdUsuario)
                {
                    //console.log(IdUsuario);
                    sessionStorage.setItem('id', IdUsuario);
                    window.location.href = direciona;
                    //console.log(sessionStorage.getItem('id'));
                });
            } 
            else
            {
                //console.log(response);
                alert("Email ou senha inválidos!");
            }
        })
        .catch(function (response)
        {
            //console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}