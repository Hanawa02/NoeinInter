$(function () {
    /* Coloca a linha embaixo da página ativa */
    var ItemAtivo = document.getElementById("Menu_Superior_Fases_De_Classificacao");

    $(ItemAtivo).addClass("Ativo");

    $('#SelecionaModalidades').click(function () {
        var idModalidade = $('#SelectModalidade').val();

        LimpaExibicaoFasesDeClassificacao();

        $('.TituloExibicao').append('Fases: ' + $('#SelectModalidade').find('option[value=' + idModalidade + ']').get(0).innerText);
        
        $.ajax({
            type: 'POST',
            url: 'FasesDeClassificacaoPartial',
            data: idModalidade,
            success: function (retorno) {
                $('#ExibicaoFasesDeClassificacao').append(retorno);
            }
        });
    })

    $('#SelectModalidade').on('change', function () {
        LimpaExibicaoFasesDeClassificacao();

        var valor = $('#SelectModalidade').val();

        if (valor != "") {
            $('#SelecionaModalidades').prop('disabled', false);
        } else {
            $('#SelecionaModalidades').prop('disabled', true);
        }
    })

    function LimpaExibicaoFasesDeClassificacao() {
        elementosFilhos = $('#ExibicaoFasesDeClassificacao').children();

        elementosFilhos.each(function (index) {
            if (index != 0) {
                elementosFilhos[index].remove();
            }
        });

        $('.TituloExibicao').get(0).innerText = "";
    }
});