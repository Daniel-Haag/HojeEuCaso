//Funcoes javascript de utilizades gerais para as paginas

function RetiraPonto(elemento) {

    var precoInput = document.getElementById(elemento);

    var precoValue = precoInput.value;

    if (!isNaN(precoValue)) {
        var precoFormatado = precoValue.replace(/\./g, ',');

        precoInput.value = precoFormatado;
    }
}