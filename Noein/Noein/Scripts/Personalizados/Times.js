$(function () {
    /* Coloca a linha embaixo da página ativa */
    var ItemAtivo = document.getElementById("Menu_Superior_Times");
    $(ItemAtivo).addClass("Ativo");

    function ResetaExibicaoModalidades() {
        var ItensTime = $('.ItemListaTime');

        /* Esconde Modalidades das outras times */
        if (ItensTime.length > 1) {
            var contador = 1;
            ItensTime.each(function () {
                if (contador > 1) {
                    var Esconder = $(this).find('.ModalidadesTime');
                    $(Esconder).hide();
                }
                contador++;
            });
        }
    }

    function IniciaPagina() {
        /* Desativa itens do menu para quando não existir time */
        var ItensTime = $('.ItemListaTime');
        if (ItensTime.length < 1) {
            $('#CadastraHorarioTime').prop('disabled', true);
            $('#InputPesquisaTime').prop('disabled', true);
            $('#PesquisaTime').hide();
        }
        ResetaExibicaoModalidades();
    }

    IniciaPagina();

    // Interação que esconde as modalidades das outras times e mostra apenas da selecionada
    $('.CorpoDaLista').on('click', '.InformacoesTime', function () {
        $('.CorpoDaLista').find('.ModalidadesTime').hide()
        $($(this).parent()).find('.ModalidadesTime').show()
    });

    /* -------- Pesquisar -------- */
    $('#Times').on('click', "#InputPesquisaTime", function () {
        this.value = "";
    });

    $('#Times').on('focusout', "#InputPesquisaTime", function () {
        if (this.value == "") {
            this.value = "Pesquisar Time...";
            $('.ItemListaTime').show();
        } else {
            Pesquisa();
        }
    });

    $('#Times').on('click', '#PesquisaTime', function () {
        Pesquisa();
    });

    $('#Times').on('keyup', '#InputPesquisaTime', function (evento) {
        Pesquisa();
    });

    function Pesquisa() {
        $('.ItemListaTime').show();
        var ParametroPesquisa = $('#InputPesquisaTime').get(0).value.toUpperCase();
        var listaTimes = $('.ItemListaTime');

        listaTimes.each(function () {
            var NomeTime = $.trim($(this).find('.NomeTime').get(0).innerText.toUpperCase());
            if (NomeTime.indexOf(ParametroPesquisa) == -1) {
                $(this).hide();
            }
        });

    };

    $('#Times').on('click', '#LimpaCampoPesquisa', function () {
        $('#InputPesquisaTime').get(0).value = "Pesquisar Time...";
        $('.ItemListaTime').show();
    });

    /* Botões de CRUD */
    $('#Times').on('click', '#CadastraTime', function () {
        $.ajax({
            url: "/Noein/Times_Cadastro",
            success: function (retorno) {
                $('#Times').get(0).innerHTML = retorno;
                LimpaCamposCadastraTime();
                ValidaCadastroTime();
            }
        });        
    });

    $('#Times').on('click', '.RemoveTime', function () {
        var nomeTime = $($(this).parent()).parent().find('.NomeTime').get(0).textContent;
        var idTime = $($(this).parent()).parent().find('.IdTime').get(0).textContent;

        InvocaModalMensagem("Excluir Time", 'Deseja realmente excluir a time "' + nomeTime + '"?');
        MostrarBotoesRespostaSimNao(true);

        $('#Resposta_Sim').click(function () {
            RemoverTime(idTime)
        });

        $('#Resposta_Nao').click(function () {
            MostrarBotoesRespostaSimNao(false);
            $('#ModalMensagemPrincipal').modal('hide');
        });
    });

    $('#Times').on('click', '.FuncaoHorarioTime', function () {
        var idTime = $($(this).parent()).parent().find('.IdTime').get(0).textContent;

        $.ajax({
            url: "/Noein/Times_Funcoes_Horario",
            data: { IdTime: idTime },
            success: function (retorno) {
                $('#Times').get(0).innerHTML = retorno;
            }
        });        
    });

    /* Fecha Modais */
    $('#Times').on('click', '.close', function () {
        $('#ModalCadastraTime').modal('hide');
        $('#ModalFuncaoHorarioTime').modal('hide');
        MostrarBotaoFecharModalMensagem(false);
        MostrarBotoesRespostaSimNao(false);
    });

    /* Modal Cadastra Time */
    $('#Times').on('click', '#BotaoCadastraTime', function () {
        var nome = $.trim($('#CadastraTime_Nome').val());
        var Modalidades = $('.CadastraTime_Modalidades').find('.CadastraTime_ItemModalidade');
        var DicionarioDeModalidadesCriterio = new Array();

        Modalidades.each(function () {
            var ModalidadeCheck = $(this).find('.CadastraTime_ModalidadeItem');
            var NumeroCriterio = $(this).find('.NumeroParaCriterioDeChaveDeClassificacao').val();
            if (ModalidadeCheck.get(0).checked && ((NumeroCriterio != "" && NumeroCriterio != "0") || ($(this).find('.NumeroParaCriterioDeChaveDeClassificacao').get(0).disabled))) {
                DicionarioDeModalidadesCriterio.push(ModalidadeCheck.val() + "|" + NumeroCriterio);
            }
        });
        
        $.getJSON('CadastraTime', $.param({ Nome: nome, ListaModalidades: DicionarioDeModalidadesCriterio }, true), function (retorno) {
            LimpaCamposCadastraTime();
            InvocaModalMensagem(retorno.MensagemRetorno.TituloMensagem, retorno.MensagemRetorno.RetornoMensagens);
            MostrarBotaoFecharModalMensagem(true);
        });
    });

    $('#Times').on('keyup', '#CadastraTime_Nome', function () {
        ValidaCadastroTime();
    });
    
    $('#Times').on('click', '.CadastraTime_ModalidadeItem', 'click', function () {
        ValidaCadastroTime();
    });
    
    $('.NumeroParaCriterioDeChaveDeClassificacao').on('change', function () {
        ValidaCadastroTime();
    });

    $('#Times').on('keyup', '.NumeroParaCriterioDeChaveDeClassificacao', function () {
        ValidaCadastroTime();
    });

    function ValidaCadastroTime() {
        var nome = $.trim($('#CadastraTime_Nome').val());
        var Modalidades = $('.CadastraTime_Modalidades').find('.CadastraTime_ItemModalidade');
        var ListaDeModalidades = new Array();


        Modalidades.each(function () {
            var ModalidadeCheck = $(this).find('.CadastraTime_ModalidadeItem').get(0).checked;
            var NumeroCriterio = $(this).find('.NumeroParaCriterioDeChaveDeClassificacao').val();
                        
            if (ModalidadeCheck && ((NumeroCriterio != "" && NumeroCriterio > 0) || ($(this).find('.NumeroParaCriterioDeChaveDeClassificacao').get(0).disabled))) {
                ListaDeModalidades.push(this.value);
            }
        });

        if (nome != "" && ListaDeModalidades.length > 0) {
            $('#BotaoCadastraTime').prop('disabled', false);
        } else {
            $('#BotaoCadastraTime').prop('disabled', true);
        }
    }

    /* Limpa Campos do Modal Cadastra Time */
    function LimpaCamposCadastraTime() {
        $('#CadastraTime_Nome').val("");
        $('.NumeroParaCriterioDeChaveDeClassificacao').val("");
        $('#BotaoCadastraTime').prop('disabled', true);

        var Modalidades = $('.CadastraTime_Modalidades').find('.CadastraTime_ModalidadeItem');
        Modalidades.each(function () {
            this.checked = false;
        });        
    }

    /* Remove Time */
    function RemoverTime(idTimeSelecionada) {
        $.getJSON('RemoveTime', { IdTime: idTimeSelecionada }, function (retorno) {
            if (retorno.MensagemRetorno.TituloMensagem == "Sucesso") {
                /* Remove Time da listagem da página */
                $('.ItemListaTime[value="' + idTimeSelecionada + '"]').remove();
            }

            InvocaModalMensagem(retorno.MensagemRetorno.TituloMensagem, retorno.MensagemRetorno.RetornoMensagens);
            MostrarBotaoFecharModalMensagem(true);
        });
    };


    /* ----------- Modal Mensagem ----------- */
    function InvocaModalMensagem(Titulo, Mensagem) {
        $('#ModalMensagemTituloModal').get(0).innerText = Titulo;
        $('#ModalMensagemPrincipal_Body').get(0).innerHTML = "";
        $('#ModalMensagemPrincipal_Body').append('<p>' + Mensagem + '</p>');
        $('#ModalMensagemPrincipal').modal('show');
    }

    function MostrarBotaoFecharModalMensagem(booleanMostrar) {        
        if (booleanMostrar) {
            MostrarBotoesRespostaSimNao(false);
            $('#BotaoFecharModalMensagem').show();
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

    $('#BotaoFecharModalMensagem').click( function () {
        $('#ModalMensagemPrincipal').modal('hide');
        VoltarParaPaginaListagemTime();
    });

    $('#Times').on('click', '.VoltarParaPaginaTimes', function () {
        VoltarParaPaginaListagemTime();
    });

    function VoltarParaPaginaListagemTime() {
        $.ajax({
            url: "/Noein/Times_Listagem",
            success: function (retorno) {
                $('#Times').get(0).innerHTML = retorno;
            }
        });
    }
});

