$(window).scroll(function () {
    if ($(window).scrollTop() + $(window).height() >= $(document).height()) {
        CarregarImagens();
    }
});

var _inCallback = false;
var _inicio = 15;
function CarregarImagens() {
    if (!_inCallback) {
        _inCallback = true;
        $.get("/Imagem/RecuperarImagens/" + _inicio, function (data) {
            if (data != '') {
                $("#lista-imagens ul").append(data);
                _inicio += 15;
            }
            _inCallback = false;
        });
    }
}