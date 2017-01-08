$(function () {
    /* Coloca a linha embaixo da página ativa */
    var ItemAtivo = document.getElementById("Menu_Superior_Quadras");
    $(ItemAtivo).addClass("Ativo");

    /* Desativa itens do menu para quando não existir quadra */
    var ItensQuadra = $('.ItemListaQuadra');
    if (ItensQuadra.length < 1) {
        $('#RemoveQuadra').prop('disabled', true);
        $('#CadastraHorarioQuadra').prop('disabled', true);
        $('#InputPesquisaQuadra').prop('disabled', true);
        $('#PesquisaQuadra').hide();
    }

    /* Esconde Modalidades das outras quadras */
    if (ItensQuadra.length > 1) {
        var contador = 1;
        ItensQuadra.each(function () {
            if (contador > 1) {
                var Esconder = $(this).find('.ModalidadesQuadra');
                $(Esconder).hide();
            }
            contador++;
        });
    }

    $('.InformacoesQuadra').click(function () {
        $('.CorpoDaLista').find('.ModalidadesQuadra').hide()
        $($(this).parent()).find('.ModalidadesQuadra').show()
    });

    $("#InputPesquisaQuadra").click(function () {
        if (this.value == "Pesquisar Quadra...") {
            this.value = "";
        }
    });

    $("#InputPesquisaQuadra").focusout(function () {
        if (this.value == "") {
            this.value = "Pesquisar Quadra...";
            $('.ItemListaQuadra').show();
        } else {
            Pesquisa();
        }
    });

    $('#CadastraQuadra').click(function () {
        $('#ModalCadastraQuadra').modal('show');
    });

    $('.close').click(function () {
        $('#ModalCadastraQuadra').modal('hide');
        $('#ModalRemoveQuadra').modal('hide');
        $('#ModalFuncaoHorarioQuadra').modal('hide');
    });

    $('#RemoveQuadra').click(function () {
        $('#ModalRemoveQuadra').modal('show');
    });


    $('#FuncaoHorarioQuadra').click(function () {
        $('#ModalFuncaoHorarioQuadra').modal('show');
    });


    /* Função de Pesquisar */
    $('#PesquisaQuadra').click(function () {
        Pesquisa();
    });

    $('#InputPesquisaQuadra').keypress(function (evento) {
        if (evento.which == 13) {
            Pesquisa();
        }
    });

    function Pesquisa() {
        $('.ItemListaQuadra').show();
        var ParametroPesquisa = $('#InputPesquisaQuadra').get(0).value.toUpperCase();
        var listaQuadras = $('.ItemListaQuadra');

        listaQuadras.each(function () {
            var DescricaoQuadra = $.trim($(this).find('.DescricaoQuadra').get(0).innerText.toUpperCase());
            if (DescricaoQuadra.indexOf(ParametroPesquisa) == -1) {
                $(this).hide();
            }
        });

    };

    $('#LimpaCampoPesquisa').click(function () {
        $('#InputPesquisaQuadra').get(0).value = "Pesquisar Quadra...";
        $('.ItemListaQuadra').show();
    });

    $('#BotaoCadastraQuadra').click(function () {
        var descricao = $('#CadastraQuadra_Descricao').val();
        var localizacao = $('#CadastraQuadra_Localizacao').val();
        var Modalidades = $('.CadastraQuadra_Modalidades').find('.CadastraQuadra_ModalidadeItem');
        var ListaDeModalidades = new Array();

        Modalidades.each(function () {
            if (this.checked) {
                ListaDeModalidades.push(this.value);
            }
        });

        $.getJSON('CadastraQuadra', { Descricao: descricao, Localizacao: localizacao, ListaModalidades: ListaDeModalidades }, function (retorno) {
        });
    });
});