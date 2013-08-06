$(document).ready(function () {
    $("#chkOutroAutor").change(function () {
        if (this.checked) {
            $('#outro-autor').show('slow');
            $('#ddlAutor').attr("disabled", true);
            $('#txtOutroAutor').focus();
        }
        else {
            $('#outro-autor').hide('slow');
            $('#ddlAutor').attr("disabled", false);
        }
    });

    $("#chkOutraCategoria").change(function () {
        if (this.checked) {
            $('#ddlCategoria').attr("disabled", true);
            $('#outra-categoria').show('slow');
            $('#txtOutraCategoria').focus();
        }
        else {
            $('#ddlCategoria').attr("disabled", false);
            $('#outra-categoria').hide('slow');
        }
    });

    $("#chkImpropria").change(function () {
        if (this.checked) {
            $('#impropria').hide('slow');
        }
        else {
            $('#impropria').show('slow');
        }
    });
});