var api = 'http://localhost:53731/api/medico/';
var apiEspecialidade = 'http://localhost:53731/api/especialidade/';

var titulo = document.querySelector('#titulo-medico');
var especialidade = document.querySelector('#especialidade');

var elementosMedico = {
    Email: document.querySelector('#login-med'),
    Senha: document.querySelector('#senha-med'),
    Cpf: document.querySelector('#cpf-med'),
    Crm: document.querySelector('#crm-med'),
    CrmEstado: document.querySelector('#inputState'),
    Nome: document.querySelector('#nome-med'),
    IdEspecialidade: document.querySelector('#especialidade'),
    DataNasc: document.querySelector('#datanasc-med'),
    Celular: document.querySelector('#celular-med'),
    Genero: document.querySelector('#inlineRadio')
};

var query = location.search.slice(1); // Pega as informações enviadas após o ponto de interrogação na URL

var partes = query.split('&'); // Caso tenha mais de um parâmetro eles serão separados pelo caracter & - A função split quebra a string em arrays, conforme um caracter informado

var data = {}; // Cria um objeto que conterá as informações passadas pela url

partes.forEach(function (parte) { // percorre as informações passadas

    var chaveValor = parte.split('='); // quebra em array o nome da informação e seu valor
    var chave = chaveValor[0]; // o nome da informação fica na posição 0
    var valor = chaveValor[1]; // o valor da informação fica na posição 1
    data[chave] = valor; // Essa é uma notação de array, porém, como chave não é um número, isso acaba também funcioando para criar um objeto
});

console.log(data); 

if(data.id) { // Se voi passado um id, quer dizer que eu estou alterando
    obterMedico(data.id);
    titulo.innerHTML = 'Alterar Médico';
}
else{
    obterEspecialidades();
    titulo.innerHTML = 'Adicionar Médico';
}

document.querySelector('#form-medico').addEventListener('submit', function (event) {

    event.preventDefault();

    var medico = {
        Email: medico.Email.value,
        Senha: medico.Senha.value,
        Cpf: medico.Cpf.value,
        Crm: medico.Crm.value,
        CrmEstado: medico.CrmEstado.value,
        Nome: medico.Nome.value,
        IdEspecialidade: parseInt(medico.IdEspecialidade.value),
        DataNasc: medico.DataNasc.value,
        Celular: medico.Celular.value,
        Genero: medico.Genero.value
    };

    if(data.id){
        alterarMedico(data.id, medico);
    }
    else{
        inserirMedico(medico);
    }

});

function inserirMedico(medico) {

    var request = new Request(api, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(medico)
    });

    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 201) {
                alert("Médico inserido com sucesso");
                atribuirValorAoFormulario();
            } else {
		
		response.json().then(function(message){
			alert(message.error);
		});

            }
        })
        .catch(function (response) {
            console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function alterarMedico(idMedico, medico) {

    var request = new Request(api + idMedico, {
        method: "PUT",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(medico)
    });

    fetch(request)
        .then(function (response) {
            // console.log(response);
            if (response.status == 202) {
                alert("Médico alterado com sucesso");
                window.location.href="medico.html";
            } else {
		response.json().then(function(message){
			alert(message.error);
		});
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function obterMedico(idMedico) {
    var request = new Request(api + idMedico, {
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
                .then(function(medico){
                    atribuirValorAoFormulario(medico);
                    obterEspecialidades(medico.idEspecialidade);
                });
            } else {
                alert("Ocorreu um erro ao obter o médico");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

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

function atribuirValorAoFormulario(medico = {}) {
    elementosMedico.Email.value = medico.Email || '';
    elementosMedico.Senha.value = medico.Senha || '';
    elementosMedico.Cpf.value = medico.Cpf || '';
    elementosMedico.medico.value = medico.medico || '';
    elementosMedico.Nome.value = medico.nome || '';
    elementosMedico.IdEspecialidade.value = medico.IdEspecialidade || '';
    elementosMedico.DataNasc.value = medico.DataNasc || '';
    elementosMedico.Celular.value = medico.Celular || '';
    elementosMedico.Genero.value = medico.Genero || '';
}