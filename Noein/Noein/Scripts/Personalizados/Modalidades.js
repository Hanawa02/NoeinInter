$(function () {
    /* Coloca a linha embaixo da página ativa */
    var ItemAtivo = document.getElementById("Menu_Superior_Modalidades");
    $(ItemAtivo).addClass("Ativo");
    
    /* -------- Pesquisar -------- */
    $("#InputPesquisaModalidade").click(function () {
        if (this.value == "Pesquisar Modalidade...") {
            this.value = "";
        }
    });

    $("#InputPesquisaModalidade").focusout(function () {
        if (this.value == "") {
            this.value = "Pesquisar Modalidade...";
            $('.ItemListaModalidade').show();
        } else {
            Pesquisa();
        }
    });

    $('#PesquisaModalidade').click(function () {
        Pesquisa();
    });

    $('#InputPesquisaModalidade').keyup(function (evento) {
        Pesquisa();
    });
});