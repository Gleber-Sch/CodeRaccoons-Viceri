var api = 'http://localhost:53731/api/medico/';

var tabela = document.querySelector('#perfil');

obterTodos();

function update(medicos) {
    tabela.innerHTML = template(medicos);
}

function template(medicos = []) {
    return `
    <h4 class="mt-0">Médico</h4>
                  <hr class="teal accent-3 mb-4 mt-0 d-inline-block mx-auto" style="width: 120px;">
                    <p><i class="fa fa-user mr-3"></i>Nome:</p>
                    <p><i class="fas fa-stethoscope"></i>Especialidade:</p>
                    <p><i class="far fa-address-card mr-3"></i>CPF:</p>
                    <p><i class="fas fa-id-card-alt"></i> CRM:</p>
                    <p><i class="fa fa-calendar-alt mr-3"></i>Data de nascimento:</p>
                    <p><i class="fa fa-envelope mr-3"></i>Email:</p>
                    <p><i class="fa fa-phone mr-3"></i>Telefone:</p>
                    <p><i class="fa fa-mobile-alt mr-3"></i>Celular:</p>
                    <p><i class="fas fa-venus-mars"></i>Genêro:</p>
        ${
            medicos.map(function(medico){
                return `
                    <tr>
                        <td>${medico.id}</td>
                        <td>${medico.nome}</td>
                        <td>${medico.cpf}</td>
                        <td>${medico.crm}</td>
                        <td>${medico.especialidade}</td>
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

function obterTodos() {

    var request = new Request(api, {
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
                    .then(function (medicos) {
                        update(medicos);
                    });
            } else {
                alert("Ocorreu um erro ao obter os médicos");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function alterarMedico(idMedico) {
    window.location.href = 'medicoCriar.html?id=' + idMedico;
}

function excluirMedico(idMedico) {
    if (confirm('Tem certeza que deseja excluir esse médico?')) {
        
        var request = new Request(api + idMedico, {
            method: "DELETE",
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        fetch(request)
            .then(function (response) {
                // console.log(response);
                if (response.status == 200) {
                    obterTodos();
                } else {
                    alert("Ocorreu um erro ao excluir os médico");
                }
            })
            .catch(function (response) {
                // console.log(response);
                alert("Desculpe, ocorreu um erro no servidor.");
            });
    }


}