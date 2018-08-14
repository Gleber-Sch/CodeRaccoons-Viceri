var apiEspecialidade = 'http://localhost:53731/api/especialidade/';
var apiMedico = 'http://localhost:53731/api/medico/especialidade/';

var tabela = document.querySelector('#con');
obterEspecialidades();

function obterEspecialidades(id){
    var request = new Request(apiEspecialidade, {
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
                .then(function(especialidades){
                    updateTemplateEspecialidades(especialidades, id);
                });
            } else {
                alert("Ocorreu um erro ao obter as especialidades");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

function updateTemplateEspecialidades(especialidades, id){
    especialidade.innerHTML = templateEspecialidades(especialidades, id);
}

function templateEspecialidades(especialidades = [], id = null){
    return `
        <option>Selecione</option>
        ${
            especialidades.map(function(especialidade){
                return `
                    <option value="${especialidade.id}" ${especialidade.id == id ? 'selected' : ''}>${especialidade.nome}</option>
                `;
            }).join('')
        }
    `;
}

var especialidademedico = 
{
    idEspecialidade: document.querySelector('#especialidade')
};

document.querySelector('#form-paciente').addEventListener('submit', function(event)
{
    event.preventDefault();

    var obj = 
    {
        idEspecialidade: especialidademedico.idEspecialidade.value 
    };

    obterMedico(obj);
});

function obterMedico(idEspecialidade) {
    var request = new Request(apiMedico + idEspecialidade, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
    .then(function (response) {
        console.log(response);
        if (response.status == 200) {
            response.json()
                .then(function (medicos) {
                    update(medicos);
                });
        } else {
            alert("Ocorreu um erro ao obter os médicos");
        }
    })
    .catch(function (response) {
        console.log(response);
        alert("Desculpe, ocorreu um erro no servidor.");
    });
}



function update(medicos) {
    tabela.innerHTML = template(medicos);
}

function template(medicos = []) {
    return `
    <table class="table table-hover table-bordered">
        <thead>
            <tr>
                <th>Nome</th>
                <th>Especialidade</th>
                <th>CPF</th>
                <th>Celular</th>
                <th>Estado</th>
                <th>Cidade</th>
                <th>Bairro</th>
                <th>Logradouro</th>
                <th>Numero</th>
                <th>Complemento</th>
            </tr>
        </thead>
        <tbody>
        ${
            medicos.map(function(medico){
                return `
                    <tr>
                        <td>${medico.id}</td>
                        <td>${medico.nome}</td>
                        <td>${medico.especialidade}</td>
                        <td>${medico.cpf}</td>
                        <td>${medico.celular}</td>
                        <td>${medico.estado}</td>
                        <td>${medico.cidade}</td>
                        <td>${medico.bairro}</td>
                        <td>${medico.logradouro}</td>
                        <td>${medico.numero}</td>
                        <td>${medico.complemento}</td>

                        <td>
                            <a href="#" onclick="alterarMedico(${medico.id})">Editar</a> | 
                            <a href="#" onclick="excluirMedico(${medico.id})">Excluir</a>
                        </td>
                    </tr>
                `;
            }).join('')
        }
        </tbody>
    </table>
    `;
}