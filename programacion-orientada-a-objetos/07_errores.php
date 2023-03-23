<?php

class Calculadora{
    public $color;
    public $tamaño;
    public $marca;
    public $modelo;

    public function __construct($color,$tamaño,$marca,$modelo){
        $this->color=$color;
        $this->tamaño=$tamaño;
        $this->marca=$marca;
        $this->modelo=$modelo;
    }

    public function sumar($valorOriginal,$valorASumar){
        $result = 0;
        $result = $valorOriginal + $valorASumar;
        return $result;
    }

    public function restar($valorOriginal,$valorARestar){
        $result = 0;
        $result = $valorOriginal - $valorARestar;
        return $result;
    }

    public function multiplicar($valorOriginal,$valorParaMultiplicar){
        $result = 0;
        $result = $valorOriginal * $valorParaMultiplicar;
        return $result;
    }

    public function dividir($valorOriginal,$valorParaDividir){
        $result = 0;
        $result = $valorOriginal / $valorParaDividir;
        return $result;
    }
}

function writeToLog($string){
    
    $dia = date("d");
    $mes = date("F");
    $mes = substr($mes,0,3);
    $anio = date("Y");
    $hour = date("H:i:s");

    $string = "[" . $dia . "-".$mes."-".$anio." ".$hour." Europe/Berlin] FILE: ".basename($_SERVER['PHP_SELF'])." LOG INFO:  " . $string;

    //echo $string;
    
    $fp = fopen("C:\\xampp\\php\\logs\\php_error_log","a") or die("No se puede acceder al archivo");
    fwrite($fp,$string . "\n");
    fclose($fp);
}

function debugObjectToLog($object){
    ob_start();
    var_dump($object);
    $result =  ob_get_clean();
    writeToLog($result);
}

$calculadora = new Calculadora("Negra","Pocket","Casio","Basic");

$valorFinal = $calculadora->sumar(10,11);

$resultado = array(
    "resultado"=> "success",
    "operacion"=> "suma",
    "value"=>$valorFinal
);

$vars = get_defined_vars();

/*print_r($vars);

echo "<br/>";
echo "<br/>";
echo "<br/>";

echo var_dump($calculadora) . "<br/>";
echo var_dump($valorFinal) . "<br/>";
print_r($resultado);
echo "<br/>";
echo var_dump($resultado) . "<br/>";
echo "<br/>";
echo "<br/>";
echo "<br/>";*/

debugObjectToLog($calculadora);
debugObjectToLog($valorFinal);
debugObjectToLog($resultado);


?>