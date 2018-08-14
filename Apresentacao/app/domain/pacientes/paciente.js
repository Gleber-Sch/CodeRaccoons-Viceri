var api = 'http://localhost:53731/api/paciente/';

var idPaciente = localStorage.getItem("id")

var tabela = document.querySelector('#perfilpaciente');

obterPaciente(idPaciente);

function update(pacientes) {
    tabela.innerHTML = template(pacientes);
}

function template(pacientes = []) {
    return `
        ${
            pacientes.map(function(paciente){
                return `
                <div class="media-body">
                  <h4 class="mt-0">Paciente</h4>
                  <hr class="teal accent-3 mb-4 mt-0 d-inline-block mx-auto" style="width: 120px;">
                  <p>
                    <i class="fa fa-user mr-3"></i> Nome: ${paciente.nome}</p>
                    <p>
                            <i class="far fa-address-card mr-3"></i>CPF: ${paciente.cpf}</p>
                    <p>
                        <i class="fa fa-calendar-alt mr-3"></i>Data de nascimento: ${paciente.dataNasc}</p>
                  <p>
                    <i class="fa fa-envelope mr-3"></i> Email: ${paciente.email}</p>
                  <p>
                    <i class="fa fa-phone mr-3"></i>Telefone: ${paciente.telefone}</p>
                  <p>
                    <i class="fa fa-mobile-alt mr-3"></i> Celular: ${paciente.celular}</p>
                    <p>
                        <i class="fas fa-venus-mars"></i> Gênero: ${paciente.genero}</p>
                </div>
                    <a href="#" onclick="alterarPaciente(${paciente.id})">Editar</a> | 
                    <a href="#" onclick="excluirPaciente(${paciente.id})">Excluir</a>
                       
                `;
            }).join('')
        }
    `;
}

function obterPaciente(idPaciente) {

    var request = new Request(api + idPaciente, {
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
                    .then(function (pacientes) {
                        update(pacientes);
                    });
            } else {
                alert("Ocorreu um erro ao obter os paciente");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function alterarPaciente(idPaciente) {
    window.location.href = 'medicoCriar.html?id=' + idPaciente;
}

function excluirPaciente(idPaciente) {
    if (confirm('Tem certeza que deseja excluir esse paciente?')) {
        
        var request = new Request(api + idPaciente, {
            method: "DELETE",
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        fetch(request)
            .then(function (response) {
                // console.log(response);
                if (response.status == 200) {
                    obterPaciente();
                } else {
                    alert("Ocorreu um erro ao excluir o paciente");
                }
            })
            .catch(function (response) {
                // console.log(response);
                alert("Desculpe, ocorreu um erro no servidor.");
            });
    }


}