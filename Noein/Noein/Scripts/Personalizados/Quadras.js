$(function () {
    /* Coloca a linha embaixo da página ativa */
    var ItemAtivo = document.getElementById("Menu_Superior_Quadras");
    $(ItemAtivo).addClass("Ativo");

    function ResetaExibicaoModalidades() {
        var ItensQuadra = $('.ItemListaQuadra');

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
    }

    function IniciaPagina() {
        /* Desativa itens do menu para quando não existir quadra */
        var ItensQuadra = $('.ItemListaQuadra');
        if (ItensQuadra.length < 1) {
            $('#CadastraHorarioQuadra').prop('disabled', true);
            $('#InputPesquisaQuadra').prop('disabled', true);
            $('#PesquisaQuadra').hide();
        }
        ResetaExibicaoModalidades();
    }

    IniciaPagina();
    // Interação que esconde as modalidades das outras quadras e mostra apenas da selecionada
    $('.CorpoDaLista').on('click', '.InformacoesQuadra', function () {
        $('.CorpoDaLista').find('.ModalidadesQuadra').hide()
        $($(this).parent()).find('.ModalidadesQuadra').show()
    });

    /* -------- Pesquisar -------- */
    $("#InputPesquisaQuadra").click( function () {
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

    $('#PesquisaQuadra').click(function () {
        Pesquisa();
    });

    $('#InputPesquisaQuadra').keyup(function (evento) {
        Pesquisa();        
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

    /* Botões de CRUD */
    $('#CadastraQuadra').click(function () {
        LimpaCamposCadastraQuadra();
        ValidaCadastroQuadra();
        $('#ModalCadastraQuadra').modal('show');
    });

    $('.CorpoDaLista').on('click', '.RemoveQuadra', function () {
        var nomeQuadra = $($(this).parent()).parent().find('.DescricaoQuadra').get(0).textContent;
        var idQuadra = $($(this).parent()).parent().find('.IdQuadra').get(0).textContent;

        InvocaModalMensagem("Excluir Quadra", 'Deseja realmente excluir a quadra "' + nomeQuadra + '"?');
        MostrarBotoesRespostaSimNao(true);

        $('#Resposta_Sim').click(function () {
            RemoverQuadra(idQuadra)
        });
        $('#Resposta_Nao').click(function () {
            MostrarBotoesRespostaSimNao(false);
            $('#ModalMensagemPrincipal').modal('hide');
        });
    });

    $('.CorpoDaLista').on('click', '.FuncaoHorarioQuadra', function () {
        LimpaCamposFuncaoHorario();
        

        var nomeQuadra = $($(this).parent()).parent().find('.DescricaoQuadra').get(0).textContent;
        var idQuadra = $($(this).parent()).parent().find('.IdQuadra').get(0).textContent;

        $('#FuncaoHorarioQuadra_IdQuadra').val(idQuadra);
        $('.ModalFuncaoHorarioQuadraTituloModal').text('Horários - ' + nomeQuadra);
        
        $.getJSON('RetornaListaHorarioDaQuadra', { IdQuadra: idQuadra }, function (retorno) {
            PreencheListaHorarios(retorno.ObjetoRetorno);
        });

        $('#ModalFuncaoHorarioQuadra').modal('show');
    });

    /* Fecha Modais */
    $('.close').click(function () {
        $('#ModalCadastraQuadra').modal('hide');
        $('#ModalFuncaoHorarioQuadra').modal('hide');        
        MostrarBotaoFecharModalMensagem(false);
        MostrarBotoesRespostaSimNao(false);
    });

    /* Modal Cadastra Quadra */
    $('#BotaoCadastraQuadra').click(function () {
        var descricao = $.trim($('#CadastraQuadra_Descricao').val());
        var localizacao = $.trim($('#CadastraQuadra_Localizacao').val());
        var Modalidades = $('.CadastraQuadra_Modalidades').find('.CadastraQuadra_ModalidadeItem');
        var ListaDeModalidades = new Array();

        Modalidades.each(function () {
            if (this.checked) {
                ListaDeModalidades.push(this.value);
            }
        });

        $.getJSON('CadastraQuadra', $.param({ Descricao: descricao, Localizacao: localizacao, ListaModalidades: ListaDeModalidades }, true), function (retorno) {
            if (retorno.MensagemRetorno.TituloMensagem == "Sucesso") {
                var idQuadra = retorno.ObjetoRetorno;

                /* Adiciona Quadra na Listagem da Página */
                var NovaQuadra = '<div class="ItemListaQuadra" value="' + idQuadra + '">';
                NovaQuadra += '<div class="InformacoesQuadra"  value="' + idQuadra + '">';
                NovaQuadra += '<div class="IdQuadra">' + idQuadra + '</div>';
                NovaQuadra += '<div class="DescricaoQuadra" value="' + descricao + '">' + descricao + '</div>';
                NovaQuadra += '<div class="Localizacao">' + localizacao + '</div>';
                NovaQuadra += '<div class="MenuQuadra">';
                NovaQuadra += '<span class="glyphicon glyphicon-trash RemoveQuadra" aria-hidden="true"></span>';
                NovaQuadra += '<span class="glyphicon glyphicon-time FuncaoHorarioQuadra" aria-hidden="true"></span>';
                NovaQuadra += '</div>';
                NovaQuadra += '</div>';
                NovaQuadra += '<div class="ModalidadesQuadra">';
                Modalidades.each(function () {
                    if (this.checked) {
                        var DescricaoModalidade = $.trim((this.name).split(' - ')[1]);
                        NovaQuadra += '<div class="ItemModalidade">' + DescricaoModalidade + '</div>';
                    }
                });

                NovaQuadra += '</div>';
                NovaQuadra += '</div>';

                $('.CorpoDaLista').append(NovaQuadra);

                $('.ItemListaQuadra').each(function () {
                    var Esconder = $(this).find('.ModalidadesQuadra');
                    $(Esconder).hide();
                });
            }

            LimpaCamposCadastraQuadra();

            InvocaModalMensagem(retorno.MensagemRetorno.TituloMensagem, retorno.MensagemRetorno.RetornoMensagens);

            MostrarBotaoFecharModalMensagem(true);
        });        
    });

    $('#CadastraQuadra_Descricao').keyup(function () {
        ValidaCadastroQuadra();
    });

    $('#CadastraQuadra_Localizacao').keyup(function () {
        ValidaCadastroQuadra();
    });
    
    $('.CadastraQuadra_ModalidadeItem').on('click', function () {
        ValidaCadastroQuadra();
    });

    function ValidaCadastroQuadra() {
        var descricao = $.trim($('#CadastraQuadra_Descricao').val());
        var localizacao = $.trim($('#CadastraQuadra_Localizacao').val());
        var Modalidades = $('.CadastraQuadra_Modalidades').find('.CadastraQuadra_ModalidadeItem');
        var ListaDeModalidades = new Array();

        Modalidades.each(function () {
            if (this.checked) {
                ListaDeModalidades.push(this.value);
            }
        });

        if (descricao != "" && localizacao != "" && ListaDeModalidades.length > 0) {
            $('#BotaoCadastraQuadra').prop('disabled', false);
        } else {
            $('#BotaoCadastraQuadra').prop('disabled', true);
        }
    }
    /* Limpa Campos do Modal Cadastra Quadra */
    function LimpaCamposCadastraQuadra() {
        $('#CadastraQuadra_Descricao').val("");
        $('#CadastraQuadra_Localizacao').val("");
        var Modalidades = $('.CadastraQuadra_Modalidades').find('.CadastraQuadra_ModalidadeItem');
        Modalidades.each(function () {
            this.checked = false;
        });
    }

    /* Remove Quadra */
    function RemoverQuadra(idQuadraSelecionada) {
        
        $.getJSON('RemoveQuadra', { IdQuadra: idQuadraSelecionada }, function (retorno) {
            if (retorno.MensagemRetorno.TituloMensagem == "Sucesso") {
                /* Remove Quadra da listagem da página */
                $('.ItemListaQuadra[value="' + idQuadraSelecionada + '"]').remove();
            }
            InvocaModalMensagem(retorno.MensagemRetorno.TituloMensagem, retorno.MensagemRetorno.RetornoMensagens);

            MostrarBotaoFecharModalMensagem(true);
            MostrarBotoesRespostaSimNao(false);
        });
    };

    /* Funções Horários da Quadras */

    function LimpaCamposFuncaoHorario() {  
        $('#BotaoCadastraHorarioQuadra').prop('disabled', true);
        $('#CadastraHorario_HorarioInicio').val("");
        $('#CadastraHorario_HorarioTermino').val("");
        $('#CadastraHorario_Intervalo').val("");
        PreencheListaHorarios(null);
    }

    function PreencheListaHorarios(ListaDeHorarios) {
        $('#ListaHorarios').get(0).innerHTML = "";
        if (ListaDeHorarios == null || ListaDeHorarios.length == 0) {
            $("#ListaHorarios").append('<div class="ListaVazia">Não existem horários Cadastrados!</div>');
        } else {
            if (ListaDeHorarios.length > 0) {
                var NovaListaHorario = '<div class="ListaHorario_Titulo">';
                NovaListaHorario += '<div class="ListaHorario_InicioTitulo">Ínicio</div>';
                NovaListaHorario += '<div class="ListaHorario_TerminoTitulo">Término</div>';
                NovaListaHorario += '<div class="ListaHorario_StatusTitulo">Status</div>';
                NovaListaHorario += '<div class="ListaHorario_ExcluirTitulo">Excluir</div>';
                NovaListaHorario += '</div>';
                $.each(ListaDeHorarios, function (index) {
                    var Items = ListaDeHorarios[index].split("|");
                    NovaListaHorario += '<div class="ListaHorarioItem">';
                    NovaListaHorario += '<div class="ListaHorario_Inicio">' + Items[0] + '</div>';
                    NovaListaHorario += '<div class="ListaHorario_Termino">' + Items[1] + '</div>';
                    NovaListaHorario += '<div class="ListaHorario_Status">' + Items[2] + '</div>';
                    NovaListaHorario += '<div class="ListaHorario_Excluir"><span class="glyphicon glyphicon-trash ExcluiHorarioQuadra" aria-hidden="true"></span></div>';
                    NovaListaHorario += '</div>';
                });
                $("#ListaHorarios").append(NovaListaHorario);
                DesabilitaExcluirHorario();
            }
        }
    }

    $('#BotaoCadastraHorarioQuadra').click(function () {
        var Inicio = $('#CadastraHorario_HorarioInicio').val();
        var Termino = $('#CadastraHorario_HorarioTermino').val();
        var intervalo = $('#CadastraHorario_Intervalo').val();
        var QuadraId = $('#FuncaoHorarioQuadra_IdQuadra').val();

        $.getJSON('CadastraHorarioDisponivelDaQuadra', { IdQuadra: QuadraId, DataInicio: Inicio, DataTermino: Termino, Intervalo: intervalo }, function (retorno) {
            PreencheListaHorarios(retorno.ObjetoRetorno);
            InvocaModalMensagem(retorno.MensagemRetorno.TituloMensagem, retorno.MensagemRetorno.RetornoMensagens);
            MostrarBotaoFecharModalMensagem(true);
        });        
    });

    $('.ListaHorarioQuadras').on('click', '.ExcluiHorarioQuadra', function () {
        if (!$(this).disabled) {
            var HorarioInicio = $($(this).parent()).parent().find('.ListaHorario_Inicio').get(0).innerText;
            var HorarioTermino = $($(this).parent()).parent().find('.ListaHorario_Termino').get(0).innerText;
            var QuadraId = $('#FuncaoHorarioQuadra_IdQuadra').val();

            $.getJSON('RemoveHorarioDaQuadra', { IdQuadra: QuadraId, DataInicio: HorarioInicio, DataTermino: HorarioTermino }, function (retorno) {
                PreencheListaHorarios(retorno.ObjetoRetorno);
                InvocaModalMensagem(retorno.MensagemRetorno.TituloMensagem, retorno.MensagemRetorno.RetornoMensagens);
                MostrarBotaoFecharModalMensagem(true);
            });
        }
    });

    function DesabilitaExcluirHorario() {
        var ListaExcluir = $('.ExcluiHorarioQuadra');

        ListaExcluir.each(function () {
            var Status = $($(this).parent()).parent().find('.ListaHorario_Status').get(0).innerText;
            if (Status == "Disponível") {
                $(this).prop('disabled', false);
                $(this).removeClass('disabled');
            } else {
                $(this).prop('disabled', true);
                $(this).addClass('disabled');
            }
        });
    }

    $('#CadastraHorario_HorarioInicio').focusout(function () {
        ValidaBotaoCadastrarHorario();
    });

    $('#CadastraHorario_HorarioTermino').focusout(function () {
        ValidaBotaoCadastrarHorario();
    });

    $('#CadastraHorario_Intervalo').keyup(function () {
        ValidaBotaoCadastrarHorario();
    });

    function ValidaBotaoCadastrarHorario() {
        var inicio = $.trim($('#CadastraHorario_HorarioInicio').val());
        var termino = $.trim($('#CadastraHorario_HorarioTermino').val());
        var intervalo = $.trim($('#CadastraHorario_Intervalo').val());

        if (inicio != "" && termino != "" && intervalo != "" && intervalo != "0") {
            $('#BotaoCadastraHorarioQuadra').prop('disabled', false);
        } else {
            $('#BotaoCadastraHorarioQuadra').prop('disabled', true);
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

