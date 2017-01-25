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
    $("#InputPesquisaTime").click(function () {
        if (this.value == "Pesquisar Time...") {
            this.value = "";
        }
    });

    $("#InputPesquisaTime").focusout(function () {
        if (this.value == "") {
            this.value = "Pesquisar Time...";
            $('.ItemListaTime').show();
        } else {
            Pesquisa();
        }
    });

    $('#PesquisaTime').click(function () {
        Pesquisa();
    });

    $('#InputPesquisaTime').keyup(function (evento) {
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

    $('#LimpaCampoPesquisa').click(function () {
        $('#InputPesquisaTime').get(0).value = "Pesquisar Time...";
        $('.ItemListaTime').show();
    });

    /* Botões de CRUD */
    $('#CadastraTime').click(function () {
        LimpaCamposCadastraTime();
        ValidaCadastroTime();
        $('#ModalCadastraTime').modal('show');
    });

    $('.CorpoDaLista').on('click', '.RemoveTime', function () {
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

    $('.CorpoDaLista').on('click', '.FuncaoHorarioTime', function () {
        LimpaCamposFuncaoHorario();
        var nomeTime = $($(this).parent()).parent().find('.NomeTime').get(0).textContent;
        var idTime = $($(this).parent()).parent().find('.IdTime').get(0).textContent;

        $('#FuncaoHorarioTime_IdTime').val(idTime);
        $('.ModalFuncaoHorarioTimeTituloModal').text('Jogos - ' + nomeTime);

        $.getJSON('RetornaListaJogosTime', { IdTime: idTime }, function (retorno) {
            PreencheListaHorarios(retorno.ObjetoRetorno);
        });

        $('#ModalFuncaoHorarioTime').modal('show');
    });

    /* Fecha Modais */
    $('.close').click(function () {
        $('#ModalCadastraTime').modal('hide');
        $('#ModalFuncaoHorarioTime').modal('hide');
        MostrarBotaoFecharModalMensagem(false);
        MostrarBotoesRespostaSimNao(false);
    });

    /* Modal Cadastra Time */
    $('#BotaoCadastraTime').click(function () {
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
            if (retorno.MensagemRetorno.TituloMensagem == "Sucesso") {
                var idTime = retorno.ObjetoRetorno;

                /* Adiciona Time na Listagem da Página */
                var NovaTime = '<div class="ItemListaTime" value="' + idTime + '">';
                NovaTime += '<div class="InformacoesTime"  value="' + idTime + '">';
                NovaTime += '<div class="IdTime">' + idTime + '</div>';
                NovaTime += '<div class="NomeTime" value="' + nome + '">' + nome + '</div>';
                NovaTime += '<div class="Pontuacao">' + 0 + '</div>';
                NovaTime += '<div class="MenuTime">';
                NovaTime += '<span class="glyphicon glyphicon-trash RemoveTime" aria-hidden="true"></span>';
                NovaTime += '<span class="glyphicon glyphicon-time FuncaoHorarioTime" aria-hidden="true"></span>';
                NovaTime += '</div>';
                NovaTime += '</div>';
                NovaTime += '<div class="ModalidadesTime">';
                
                Modalidades.each(function () {
                    var ModalidadeCheck = $(this).find('.CadastraTime_ModalidadeItem');
                    var NumeroCriterio = $(this).find('.NumeroParaCriterioDeChaveDeClassificacao').val();
                    if (ModalidadeCheck.get(0).checked && ((NumeroCriterio != "" && NumeroCriterio != "0") || ($(this).find('.NumeroParaCriterioDeChaveDeClassificacao').get(0).disabled))) {
                        var DescricaoModalidade = $.trim((ModalidadeCheck.get(0).name).split(' - ')[1]);
                        if (NumeroCriterio != "") {
                            NovaTime += '<div class="ItemModalidade">' + DescricaoModalidade + '[' + NumeroCriterio + ']' + '</div>';
                        } else {
                            NovaTime += '<div class="ItemModalidade">' + DescricaoModalidade + '</div>';
                        }
                    }
                });

                NovaTime += '</div>';
                NovaTime += '</div>';

                $('.CorpoDaLista').append(NovaTime);

                $('.ItemListaTime').each(function () {
                    var Esconder = $(this).find('.ModalidadesTime');
                    $(Esconder).hide();
                });
            }

            LimpaCamposCadastraTime();

            InvocaModalMensagem(retorno.MensagemRetorno.TituloMensagem, retorno.MensagemRetorno.RetornoMensagens);
            MostrarBotaoFecharModalMensagem(true);
        });
    });

    $('#CadastraTime_Nome').keyup(function () {
        ValidaCadastroTime();
    });
    
    $('.CadastraTime_ModalidadeItem').on('click', function () {
        ValidaCadastroTime();
    });
    
    $('.NumeroParaCriterioDeChaveDeClassificacao').on('change', function () {
        ValidaCadastroTime();
    });

    $('.NumeroParaCriterioDeChaveDeClassificacao').keyup(function () {
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

    /* Funções Horários da Times */

    function PreencheListaHorarios(ListaDeHorarios) {
        $('#ListaHorarios').get(0).innerHTML = "";
        if (ListaDeHorarios == null || ListaDeHorarios.length == 0) {
            $("#ListaHorarios").append('<div class="ListaVazia">Não existem jogos Cadastrados!</div>');
        } else {
            if (ListaDeHorarios.length > 0) {
                var NovaListaHorario = '<div class="ListaHorario_Titulo">';
                NovaListaHorario += '<div class="ListaHorario_InicioTitulo">Ínicio</div>';
                NovaListaHorario += '<div class="ListaHorario_TerminoTitulo">Término</div>';
                NovaListaHorario += '<div class="ListaHorario_StatusTitulo">Status</div>';
                NovaListaHorario += '</div>';
                $.each(ListaDeHorarios, function (index) {
                    var Items = ListaDeHorarios[index].split("|");
                    NovaListaHorario += '<div class="ListaHorarioItem">';
                    NovaListaHorario += '<div class="ListaHorario_Inicio">' + Items[0] + '</div>';
                    NovaListaHorario += '<div class="ListaHorario_Termino">' + Items[1] + '</div>';
                    NovaListaHorario += '<div class="ListaHorario_Status">' + Items[2] + '</div>';
                    NovaListaHorario += '</div>';
                });
                $("#ListaHorarios").append(NovaListaHorario);
                DesabilitaExcluirHorario();
            }
        }
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

    $('#BotaoFecharModalMensagem').click(function () {
        $('#ModalMensagemPrincipal').modal('hide');
    });
});

