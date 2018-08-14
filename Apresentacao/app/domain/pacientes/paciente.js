var api = 'http://localhost:53731/api/paciente/';

var idPaciente = localStorage.getItem("id");

var tabela = document.querySelector('#perfilpaciente');

obterPaciente(idPaciente);

function update(pacientes) {
    tabela.innerHTML = template(pacientes);
}

function template(pacientes = []) {
    return `
    <table class="table table-hover table-bordered">
    <thead>
        <tr>
            <th>Email</th>
            <th>Nome</th>
            <th>CPF</th>
            <th>DataNasc</th>
            <th>Telefone</th>
            <th>Celular</th>
            <th>Genero</th>
        </tr>
    </thead>
    <tbody>
        ${
            
            pacientes.map(function(paciente){
                return `
                <tr>
                        <td>${paciente.email}</td>
                        <td>${paciente.nome}</td>
                        <td>${paciente.cpf}</td>
                        <td>${paciente.telefone}</td>
                        <td>${paciente.celular}</td>
                        <td>${paciente.genero}</td>
                        <td>
                            <a href="#" onclick="alterarClinica(${paciente.id})">Editar</a> | 
                            <a href="#" onclick="excluirClinica(${paciente.id})">Excluir</a>
                        </td>
                    </tr>
                `;
            }).join('')
        }
        </tbody>
        </table>
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
                alert("Ocorreu um erro ao obter paciente");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
    }