function aumentarUnidade(itemId) {
    $.post(addUnidade, { id: itemId }, function (data) {
        if (data.quantidade) {
            $('#quantidade-' + itemId).text(data.quantidade);
        }
    }).fail(function () {
        alert('Erro ao adicionar unidade.');
    });
}

function diminuirUnidade(itemId) {
    $.post(subUnidade, { id: itemId }, function (data) {
        if (data.quantidade) {
            $('#quantidade-' + itemId).text(data.quantidade);
        }
    }).fail(function () {
        alert('Erro ao adicionar unidade.');
    });
}