var api = 'http://localhost:53731/api/clinica/';

var tabela = document.querySelector('#perfilclinica');

obterClinica(idClinica);

function update(clinicas) {
    tabela.innerHTML = template(clinicas);
}

function template(clinicas = []) {
    return `
    <table class="table table-hover table-bordered">
        <thead>
            <tr>
                <th>#</th>
                <th>Email</th>
                <th>Nome</th>
                <th>CNPJ</th>
                <th>Telefone</th>
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
            clinicas.map(function(clinica){
                return `
                    <tr>
                        <td>${clinica.id}</td>
                        <td>${clinica.email}</td>
                        <td>${clinica.nome}</td>
                        <td>${clinica.cnpj}</td>
                        <td>${clinica.telefone}</td>
                        <td>${clinica.estado}</td>
                        <td>${clinica.cidade}</td>
                        <td>${clinica.bairro}</td>
                        <td>${clinica.logradouro}</td>
                        <td>${clinica.numero}</td>
                        <td>${clinica.complemento}</td>
                        <td>
                            <a href="#" onclick="alterarClinica(${clinica.id})">Editar</a> | 
                            <a href="#" onclick="excluirClinica(${clinica.id})">Excluir</a>
                        </td>
                    </tr>
                `;
            }).join('')
        }
        </tbody>
    </table>
    `;
}

function obterClinica(idClinica) {

    var request = new Request(api + idClinica, {
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
                    .then(function (clinicas) {
                        update(clinicas);
                    });
            } else {
                alert("Ocorreu um erro ao obter a clinica");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function alterarClinica(idClinica) {
    window.location.href = 'medicoCriar.html?id=' + idClinica;
}

function excluirClinica(idClinica) {
    if (confirm('Tem certeza que deseja excluir essa clinica?')) {
        
        var request = new Request(api + idClinica, {
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
                    alert("Ocorreu um erro ao excluir a clinica");
                }
            })
            .catch(function (response) {
                // console.log(response);
                alert("Desculpe, ocorreu um erro no servidor.");
            });
    }


}