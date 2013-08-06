$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() != 0) {
            $('#voltar-para-topo').fadeIn();
        } else {
            $('#voltar-para-topo').fadeOut();
        }
    });

    $('#voltar-para-topo').click(function () {
        $('body,html').animate({ scrollTop: 0 }, 800);
    });

    var fraseBusca = 'busque por uma frase, categoria ou autor';
    $("#txtBusca").val(fraseBusca);
    $("#txtBusca").focus(function () {
        if ($("#txtBusca").val() == fraseBusca)
            $("#txtBusca").val('');
    });

    $("#txtBusca").blur(function () {
        if ($("#txtBusca").val() == '')
            $("#txtBusca").val(fraseBusca);
    });

    $("#txtBusca").keydown(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13)
            window.location.href = '/home/buscar/' + $(this).val().replaceAll(".", "").replaceAll("&", "").replaceAll("*", "");
    });
    
    $(".popup").click(function (e) {
        window.open($(this).attr('href'), 'pop', "status = 1, height = 360, width = 700, resizable = 0");
        return false;
    });

    window.setTimeout(function () {
        $('#destaque').hide();
    }, 5000);
});

String.prototype.replaceAll = function (de, para) {
    var str = this;
    var pos = str.indexOf(de);
    while (pos > -1) {
        str = str.replace(de, para);
        pos = str.indexOf(de);
    }
    return (str);
}
