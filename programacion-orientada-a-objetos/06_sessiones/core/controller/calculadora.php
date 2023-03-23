<?php
    $url = $_SERVER["REQUEST_URI"];

    if(strpos($url,"?request=")!==false){
        include "../../core/model/calculadora.php";
        //include "../../core/business/calculadora.php";
    }

    $request ="";

    $valorOriginal = 0;
    $valorOperacion = 0;
    $valorFinal = 0;
    $result = "success";

    if(isset($_GET["request"])){
        $request =  $_GET["request"];

        if ($request == "sumar" ||
            $request == "restar" ||
            $request == "dividir" ||
            $request == "multiplicar"
        ){
            $valorOriginal =  $_GET["valor_original"];
            $valorOperacion = $_GET["valor_operacion"];
        } else {
            $request = "";
        }

        if($request!= ""){
            $calculadora = new Calculadora("Negra","Pocket","Casio","Basic");
            if($request == "sumar"){
                $valorFinal = $calculadora->sumar($valorOriginal,$valorOperacion);
            }
            if($request == "restar"){
                $valorFinal = $calculadora->restar($valorOriginal,$valorOperacion);
            }
            if($request == "dividir"){
                $valorFinal = $calculadora->dividir($valorOriginal,$valorOperacion);
            }
            if($request == "multiplicar"){
                $valorFinal = $calculadora->multiplicar($valorOriginal,$valorOperacion);
            }
        }

        $resultado = array(
            "resultado"=> $result,
            "operacion"=> $request,
            "value"=>$valorFinal
        );

        $json_data = json_encode($resultado);
        print $json_data;
    }
?>