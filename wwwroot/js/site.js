//Funcoes javascript de utilizades gerais para as paginas

function RetiraPonto(elemento) {

    var precoInput = document.getElementById(elemento);

    var precoValue = precoInput.value;

    if (!isNaN(precoValue)) {
        var precoFormatado = precoValue.replace(/\./g, ',');

        precoInput.value = precoFormatado;
    }
}

function formatarMoeda(input) {
    // Obtém o valor atual do campo
    var valorDigitado = input.value;

    // Remove todos os caracteres não numéricos
    var valorNumerico = valorDigitado.replace(/[^\d]/g, '');

    // Converte o valor para um número inteiro
    var valorInteiro = parseInt(valorNumerico, 10);

    // Formata o valor como moeda brasileira usando accounting.js
    var formatoMoeda = accounting.formatMoney(valorInteiro / 100, { symbol: "R$", precision: 2, thousand: ".", decimal: "," });

    // Define o valor formatado de volta no campo
    input.value = formatoMoeda;
}

function exibirNotificacao(titulo, mensagem) {
    $("#notificationModalLabel").text(titulo);
    $("#notificationMessage").text(mensagem);

    $("#notificationModal").modal("show");
}

