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
        InvocaModalMensagem("Aguarde", "Carregando Campeonato, aguarde");
        MostrarBotaoFecharModalMensagem(false);
    });



    function InvocaModalMensagem(Titulo, Mensagem) {
        $('#ModalMensagemTituloModal').get(0).innerText = Titulo;
        $('.ModalBodyPadrao').get(0).innerHTML = "";
        $('.ModalBodyPadrao').append('<p>' + Mensagem + '</p>');
        $('#ModalMensagem').modal('show');
    }

    function MostrarBotaoFecharModalMensagem(booleanMostrar) {
        if (booleanMostrar) {
            $('#BotaoFecharModalMensagem').show();
        } else {
            $('#BotaoFecharModalMensagem').hide();
        }
    }
});