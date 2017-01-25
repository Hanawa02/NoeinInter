$(function () {
    /* Coloca a linha embaixo da página ativa */
    var ItemAtivo = document.getElementById("Menu_Superior_Resultados");
    $(ItemAtivo).addClass("Ativo");

    $('#FiltraPorData').focusout(function () {
        if (this.valueAsDate == null) {
            $(this).val("");
        }
        AplicarFiltros();
    });
    
    $('#FiltraPorData').on('change', function () {
        if (this.valueAsDate == null || $(this).val() == "") {
            AplicarFiltros();
        }        
    });

    $('#FiltraPorData').keyup(function (evento) {
        if (this.valueAsDate != null || $(this).val() == "") {
            AplicarFiltros();
        }
    });

    $('#FiltrarPorModalidade').keyup(function (evento) {        
            AplicarFiltros();        
    });

    function AplicarFiltros() {
        if ($.trim($('#FiltrarPorModalidade').val()) != "" || $.trim($('#FiltraPorData').val()) != "") {
            var data = "";

            if ($('#FiltraPorData').val() != "") {

                var itensData = $('#FiltraPorData').val().split('-');

                data = itensData[2] + "-" + itensData[1] + "-" + itensData[0];
            }
            var modalidade = $.trim($('#FiltrarPorModalidade').val()).toUpperCase();

            linhas = $('#ListaJogos').find('.ItemListaJogos');

            linhas.each(function () {
                var dataLinha = $(this).find('.HorarioJogo').get(0).innerText;

                var modalidadeLinha = $(this).find('.ModalidadeJogo').get(0).innerText.toUpperCase();

                var DataDiferente = false;

                if (data != "") {
                    DataDiferente = dataLinha.indexOf(data) == -1;
                }

                var ModalidadeDiferente = false;

                if (modalidade != "") {
                    ModalidadeDiferente = modalidadeLinha.indexOf(modalidade) == -1;
                }

                if (DataDiferente || ModalidadeDiferente) {
                    $(this).hide();
                } else {
                    $(this).show();
                }
            });
        } else {
            linhas = $('#ListaJogos').find('.ItemListaJogos');

            linhas.each(function () {
                $(this).show();
            });
        }
    }

    $('#SalvarResultados').click(function () {
        InvocaModalMensagem("Salvar Resultados", 'Deseja realmente salvar os resultados?');
        MostrarBotoesRespostaSimNao(true);

        $('#Resposta_Sim').click(function () {
            SalvarResultados();
        });

        $('#Resposta_Nao').click(function () {
            MostrarBotoesRespostaSimNao(false);
            $('#ModalMensagemPrincipal').modal('hide');
        });
    });

    function SalvarResultados() {
        var Resultados = new Array();
        linhas = $('#ListaJogos').find('.ItemListaJogos');

        linhas.each(function () {
            if ($(this).css('display') != 'none') {
                var idJogo = $(this).find('.IdJogo').get(0).innerText;
                var resultadoPrincipalTime1 = $(this).find('.InputResultadoTime1').val();
                var resultadoPrincipalTime2 = $(this).find('.InputResultadoTime2').val();
                var resultadoAdicionalTime1 = $(this).find('.InputResultadoAdicionalTime1').val();
                var resultadoAdicionalTime2 = $(this).find('.InputResultadoAdicionalTime2').val();

                if (resultadoPrincipalTime1 != "" && resultadoPrincipalTime2 != "") {
                    var infoInserir = idJogo + "|" + resultadoPrincipalTime1 + "|" + resultadoPrincipalTime2 + "|";
                    if (resultadoAdicionalTime1 != "" && resultadoAdicionalTime2 != "") {
                        infoInserir += resultadoAdicionalTime1 + "|" + resultadoAdicionalTime2
                    } else {
                        infoInserir += "|";
                    }
                    Resultados.push(infoInserir);
                }
            }
        });

        $.getJSON('SalvaResultados', $.param({ ListaResultados: Resultados }, true), function (retorno) {
            InvocaModalMensagem(retorno.MensagemRetorno.TituloMensagem, retorno.MensagemRetorno.RetornoMensagens);

            MostrarBotaoFecharModalMensagem(true);
        });
    }
    
    /* ----------- Modal Mensagem ----------- */
    function InvocaModalMensagem(Titulo, Mensagem) {
        $('#ModalMensagemTituloModal').get(0).innerText = Titulo;
        $('#ModalMensagemPrincipal_Body').get(0).innerHTML = "";
        $('#ModalMensagemPrincipal_Body').append('<p>' + Mensagem + '</p>');
        $('#ModalMensagemPrincipal').modal('show');
    }

    function MostrarBotaoFecharModalMensagem(booleanMostrar) {
        if (booleanMostrar) {
            $('#BotaoFecharModalMensagem').show();
            MostrarBotoesRespostaSimNao(false);
        } else {
            $('#BotaoFecharModalMensagem').hide();
        }
    }

    function MostrarBotoesRespostaSimNao(booleanMostrar) {
        if (booleanMostrar) {
            MostrarBotaoFecharModalMensagem(false);
            $('#Resposta_Sim').show();
            $('#Resposta_Nao').show();
        } else {
            $('#Resposta_Sim').hide();
            $('#Resposta_Nao').hide();
        }
    }

    $('#BotaoFecharModalMensagem').click(function () {
        $('#ModalMensagemPrincipal').modal('hide');
    });
});