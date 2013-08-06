function RecuperarFrases() {
    $('.paginacao a').click(function () {
        $.get($(this).attr('href'), function (response) {
            $('#caixa-ultimas-frases').replaceWith($('#caixa-ultimas-frases', response));
        })
       .done(function () {
           RecuperarFrases();
       });
        return false;
    });
}

$(document).ready(function () {
    RecuperarFrases();
});