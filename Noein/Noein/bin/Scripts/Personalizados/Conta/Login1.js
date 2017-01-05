$(function () {
    $(".Input_Login_Email").click(function () {
        if (this.value == "Digite aqui o e-mail") {
            this.value = "";
        }
    });

    $(".Input_Login_Senha").click(function () {
        if (this.value == "Digite aqui sua senha") {
            this.value = "";
        }
    });

    $(".Input_Login_Email").focusout(function () {
        if (this.value == "") {
            this.value = "Digite aqui o e-mail";
            $("#Botao_Login1_Fazer_Login").prop('disabled', true);
        } else {
            if (this.value != "Digite aqui o e-mail" && $('.Input_Login_Email').get(0).value) {
                $("#Botao_Login1_Fazer_Login").prop('disabled', false);
            }
        }
    });

    $(".Input_Login_Senha").focusout(function () {
        if (this.value == "") {
            this.value = "Digite aqui sua senha";
            $("#Botao_Login1_Fazer_Login").prop('disabled', true);
        } else {
            if ($('.Input_Login_Email').get(0).value != "Digite aqui o e-mail" && $('.Input_Login_Email').get(0).value != "") {
                $("#Botao_Login1_Fazer_Login").prop('disabled', false);
            }
        }
    });

    $("#Botao_Login1_Fazer_Login").click(function () {
        InvocaModalMensagem("Aguarde", "Realizando Log in");
        MostrarBotaoFecharModalMensagem(false);
        var email = $('.Input_Login_Email').get(0).value;
        var senha = $('.Input_Login_Senha').get(0).value;
        /* Trocar para post */
        $.getJSON('RealizarLogin', { Email: email, Senha: senha }, function (retorno) {
            InvocaModalMensagem(retorno.mensagemRetorno.TituloMensagem, retorno.mensagemRetorno.RetornoMensagens);
            MostrarBotaoFecharModalMensagem(true);
            if (retorno.mensagemRetorno.TituloMensagem == 'Sucesso') {
                $.getJSON('Login2', { CampeonatosUsuario: retorno.ListaCampeonatos, NomeUsuario: retorno.NomeUsuario }, function () { })
            }
        });
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

    $('#BotaoFecharModalMensagem').click(function () {
        $('#ModalMensagem').modal('hide');
    });
});