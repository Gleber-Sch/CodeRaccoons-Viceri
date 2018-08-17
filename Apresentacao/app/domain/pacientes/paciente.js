var api = 'http://localhost:53731/api/Paciente/';

var id = sessionStorage.getItem('id');

console.log(id);
var perfil = document.querySelector('#perfilPaciente');

obterPaciente(id);

function update(paciente) 
{
    perfil.innerHTML = template(paciente);
}

function template(paciente) 
{
    return `
        <i class="fa fa-user-circle icone"></i>
        <div class="media-body">
        <h5 class="mt-0"><i class="fa fa-user-circle mr-3"></i>Paciente</h5>
            <hr class="teal accent-3 mb-4 mt-0 d-inline-block mx-auto" style="width: 120px;">
            <p>
                <i class="fa fa-user mr-3"></i>Nome: ${paciente.nome}
            </p>
            <p>
                <i class="far fa-address-card mr-3"></i>CPF: ${paciente.cpf}
            </p>
            <p>
                <i class="fa fa-calendar-alt mr-3"></i>Data de nascimento: ${paciente.dataNasc}
            </p>
            <p>
                <i class="fa fa-envelope mr-3"></i>Email: ${paciente.email}
            </p>
            <p>
                <i class="fa fa-phone mr-3"></i>Telefone: ${paciente.telefoneRes}
            </p>
            <p>
                <i class="fa fa-mobile-alt mr-3"></i>Celular: ${paciente.celular}
            </p>
            <p>
                <i class="fas fa-venus-mars"></i>Genêro: ${paciente.genero}
            </p>
        </div>    
    `;
}

function obterPaciente(id) 
{
    var request = new Request(api + id, 
    {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });
    fetch(request)
        .then(function (response)
        {
            // console.log(response);
            if (response.status == 200)
            {
                response.json()
                .then(function (paciente)
                {
                    console.log(paciente);
                    update(paciente);
                });
            } 
            else
            {
                alert("Ocorreu um erro ao obter paciente");
            }
        })
        .catch(function (response) 
        {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}