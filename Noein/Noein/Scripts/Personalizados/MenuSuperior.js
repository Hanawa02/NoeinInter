$(function () {
    $('#Menu_Superior_Times').click(function () {
        var UrlParticionada = (window.location.href).split("/Noein/");
        window.location.href = UrlParticionada[0] + "/Noein/Times";
    });

    $('#Menu_Superior_Quadras').click(function() {
        var UrlParticionada = (window.location.href).split("/Noein/");
        window.location.href = UrlParticionada[0] + "/Noein/Quadras";
    });

    $('#Menu_Superior_Fases_De_Classificacao').click(function () {
        var UrlParticionada = (window.location.href).split("/Noein/");
        window.location.href = UrlParticionada[0] + "/Noein/FasesDeClassificacao";
    });

    $('#Menu_Superior_Horarios').click(function() {
        var UrlParticionada = (window.location.href).split("/Noein/");
        window.location.href = UrlParticionada[0] + "/Noein/HorarioJogos";
    });

    $('#Menu_Superior_Resultados').click(function() {
        var UrlParticionada = (window.location.href).split("/Noein/");
        window.location.href = UrlParticionada[0] + "/Noein/Resultados";
    });
});