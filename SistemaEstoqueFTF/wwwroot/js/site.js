function aumentarUnidade(itemId) {
    $.post(addUnidade, { id: itemId }, function (data) {
        if (data.quantidade !== undefined) {
            $('#quantidade-' + itemId).text(data.quantidade);
        }
    }).fail(function () {
        alert('Erro ao adicionar unidade.');
    });
}

function diminuirUnidade(itemId) {
    $.post(subUnidade, { id: itemId }, function (data) {
        if (data.quantidade !== undefined) {
            $('#quantidade-' + itemId).text(data.quantidade);
        }
    }).fail(function () {
        alert('Erro ao adicionar unidade.');
    });
}

$(document).ready(function () {
    $('#searchInput').on('keyup', function () {
        let query = $(this).val();
        if (query.length > 0) {
            $.ajax({
                url: '@Url.Action("LiveSearch", "Itens")',
                type: 'GET',
                data: { searchString: query },
                success: function (data) {
                    $('#searchResults').html(data);
                }
            });
        } else {
            $('#searchResults').empty();
        }
    });
});