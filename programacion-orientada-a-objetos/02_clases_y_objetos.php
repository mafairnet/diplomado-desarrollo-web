<?php
class Calculadora{
    public $color;
    public $tamaño;
    public $marca;
    protected $modelo = "Generico";

    public function __construct(){
        //$this->color = $color;
        $params = func_get_args();

        $num_params = func_num_args();

        $funcion_constuctor = '__construct'.$num_params;

        if(method_exists($this,$funcion_constuctor)){
            call_user_func_array(array($this,$funcion_constuctor),$params);
        }

    }

    function __construct0(){
        $this->__construct1("Morada");
    }

    function __construct1($color){
        $this->__construct2($color,"Marca Generica");
    }

    function __construct2($color,$marca){
        $this->color = $color;
        $this->marca = $marca;
    }

    public function getColor(){
        return $this->color;
    }

    public function setColor($color){
        $this->color = $color;
    }

    public function getMarca(){
        return $this->marca;
    }

    public function suma(){
        return 0;
    }

    public function resta(){
        return 0;
    }

    public function __call($name,$arguments){
        echo "Nombre del Metodo:" . $name . "<br/>";
        echo "Marca: " . $arguments[0]. "<br/>";
        echo "Color: " . $arguments[1]. "<br/>";
    }

    static function __callStatic($name,$arguments){
        echo "Nombre del Metodo:" . $name . "<br/>";
        echo "Marca: " . $arguments[0]. "<br/>";
        echo "Color: " . $arguments[1]. "<br/>";
        echo "Tamaño: " . $arguments[2]. "<br/>";
    }

}

class CalculadoraCientifica extends Calculadora{

    public function getModelo(){
        return $this->modelo;
    }
}



$miCalculadora = new Calculadora();

//$miCalculadora->color ="Morada";
$miCalculadora->tamaño = "Pequeña";
$miCalculadora->marca = "Casio";

echo "El color inicial de mi calculadora es: ". $miCalculadora->getColor() ."<br/>";
$miCalculadora->setColor("Amarilla");
echo "El color modificado de mi calculadora es: ". $miCalculadora->getColor() ."<br/>";

$segundaCalculadora =  new Calculadora("Rojo");
echo "El color de la segunda calculadora es: ". $segundaCalculadora->getColor() ."<br/>";

$minuevaCalculadora = new Calculadora();
$minuevaCalculadora->informacionCalculadora("Generica","Morado");

$minuevaOtraCalculadora = new Calculadora();
$minuevaOtraCalculadora::informacionCalculadora("Generica","Morado","Grande");

$miNgCalculadora = new Calculadora("Negro","Casio");
echo "El color de la calculadora NG es: ". $miNgCalculadora->getColor() ."<br/>";
echo "La marca de la calculadora NG es: ". $miNgCalculadora->getMarca() ."<br/>";

$calculadoraCientifica = new CalculadoraCientifica();
echo "El modelo de la calculadora 1100TIFIK es: ". $calculadoraCientifica->getModelo() ."<br/>";
echo "El color de la calculadora 1100TIFIK es: ". $calculadoraCientifica->getColor() ."<br/>";




?>