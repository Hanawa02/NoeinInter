$(function () {
    $("#Login_Campeonato").on('change', function () {
        if (this.value == "") {
            $('#Botao_Login2_Acessar').prop('disabled', true);
        } else {
            $('#Botao_Login2_Acessar').prop('disabled', false);
        }
    });

    $('#Botao_Login2_Acessar').click(function () {
        /* redireciona para página de jogos */
    });
});