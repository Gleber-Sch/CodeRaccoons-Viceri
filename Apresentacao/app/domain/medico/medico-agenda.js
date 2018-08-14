var apiEspecialidade = 'http://localhost:53731/api/especialidade/';
var apiClinica = 'http://localhost:53731/api/clinica/';

obterEspecialidades();
obterClinicas();

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




function obterClinicas(id){
    var request = new Request(apiClinica, {
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
                .then(function(clinicas){
                    updateTemplateClinicas(clinicas, id);
                });
            } else {
                alert("Ocorreu um erro ao obter as clinicas");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

function updateTemplateClinicas(clinicas, id){
    clinica.innerHTML = templateEspecialidades(clinicas, id);
}

function templateEspecialidades(clinicas = [], id = null){
    return `
        <option>Selecione</option>
        ${
            clinicas.map(function(clinica){
                return `
                    <option value="${clinica.id}" ${clinica.id == id ? 'selected' : ''}>${clinica.nome}</option>
                `;
            }).join('')
        }
    `;
}


var ConsultaDisponivel =
{
    Valor: document.querySelector('#agenda-valor'),
    Data: document.querySelector('#agenda-data'),
    Hora: document.querySelector('#agenda-hora'),
    especialidade: document.querySelector('#especialidade'),
    Clinica: document.querySelector('#clinica')
};

document.querySelector('.form-inline').addEventListener('submit', function (event) 
{
    event.preventDefault();

    var obj = 
    {
        Valor: ConsultaDisponivel.Valor.value,
        Data: ConsultaDisponivel.Data.value,
        Hora: ConsultaDisponivel.Hora.value,
        especialidade: ConsultaDisponivel.especialidade.value,
        Clinica: ConsultaDisponivel.Clinica.value
    };

    inserirConsulta(obj);
    
});
function inserirConsulta(obj)
{
    var request = new Request(apiClinica, 
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
        .catch(function(response)
        {
            console.log(reponse);
        })
}
