
function suma(){
    valorOriginal = $("#valorOriginal").val();
    valorDeOperacion = $("#valorDeOperacion").val();

    if(valorOriginal==""){
        valorOriginal = 0;
    }

    if(valorDeOperacion==""){
        valorDeOperacion = 0;
    }

    //http://localhost:88/aztlan/06_sessiones/core/controller/calculadora.php?request=restar&valor_original=9&valor_operacion=3

    requestUrl = "core/controller/calculadora.php?request=sumar&valor_original="+valorOriginal+"&valor_operacion="+valorDeOperacion;

    $.ajax({url:requestUrl,success: function(result){
        
        console.log(result);

        payload = JSON.parse(result);

        console.log(payload);

        valorFinal = payload.value;

        $("#valorOriginal").val(valorFinal);
        $("#valorDeOperacion").val("");

        console.log("Valor Original: "+valorOriginal+", Valor de Operacion: "+valorDeOperacion+", Valor Final: " + valorFinal);
    }});
}

function resta(){

    valorOriginal = $("#valorOriginal").val();
    valorDeOperacion = $("#valorDeOperacion").val();

    if(valorOriginal==""){
        valorOriginal = 0;
    }

    if(valorDeOperacion==""){
        valorDeOperacion = 0;
    }

    requestUrl = "core/controller/calculadora.php?request=restar&valor_original="+valorOriginal+"&valor_operacion="+valorDeOperacion;

    $.ajax({url:requestUrl,success: function(result){
        
        console.log(result);

        payload = JSON.parse(result);

        console.log(payload);

        valorFinal = payload.value;

        $("#valorOriginal").val(valorFinal);
        $("#valorDeOperacion").val("");

        console.log("Valor Original: "+valorOriginal+", Valor de Operacion: "+valorDeOperacion+", Valor Final: " + valorFinal);
    }});
}

function multiplica(){

    valorOriginal = $("#valorOriginal").val();
    valorDeOperacion = $("#valorDeOperacion").val();

    if(valorOriginal==""){
        valorOriginal = 0;
    }

    if(valorDeOperacion==""){
        valorDeOperacion = 0;
    }

    requestUrl = "core/controller/calculadora.php?request=multiplicar&valor_original="+valorOriginal+"&valor_operacion="+valorDeOperacion;

    $.ajax({url:requestUrl,success: function(result){
        
        console.log(result);

        payload = JSON.parse(result);

        console.log(payload);

        valorFinal = payload.value;

        $("#valorOriginal").val(valorFinal);
        $("#valorDeOperacion").val("");

        console.log("Valor Original: "+valorOriginal+", Valor de Operacion: "+valorDeOperacion+", Valor Final: " + valorFinal);
    }});
}

function divide(){

    valorOriginal = $("#valorOriginal").val();
    valorDeOperacion = $("#valorDeOperacion").val();

    if(valorOriginal==""){
        valorOriginal = 0;
    }

    if(valorDeOperacion==""){
        valorDeOperacion = 0;
    }

    requestUrl = "core/controller/calculadora.php?request=dividir&valor_original="+valorOriginal+"&valor_operacion="+valorDeOperacion;

    $.ajax({url:requestUrl,success: function(result){
        
        console.log(result);

        payload = JSON.parse(result);

        console.log(payload);

        valorFinal = payload.value;

        $("#valorOriginal").val(valorFinal);
        $("#valorDeOperacion").val("");

        console.log("Valor Original: "+valorOriginal+", Valor de Operacion: "+valorDeOperacion+", Valor Final: " + valorFinal);
    }});
}

function limpiar(){
    $("#valorOriginal").val("");
    $("#valorDeOperacion").val("");
}

$(document).ready(function(){
    console.log("Pagina Cargada");
});
