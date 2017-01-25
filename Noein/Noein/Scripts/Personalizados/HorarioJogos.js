$(function () {
    /* Coloca a linha embaixo da página ativa */
    var ItemAtivo = document.getElementById("Menu_Superior_Horarios");
    $(ItemAtivo).addClass("Ativo");

    var ElementosJogos = $('.celulaJogo');

    ElementosJogos.each(function () {
        var texto = this.innerText;
        texto = texto.replace("|", ":<br>");
        this.innerText = "";
        this.innerHTML = texto;
    });

});