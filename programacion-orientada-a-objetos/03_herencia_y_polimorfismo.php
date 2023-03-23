<?php

interface Calculadora2 {
    function getColor();

   //}
}

interface Reloj {
    function getHora();
       /*$this->hora = date("h:i:sa");
       return $this->hora;*/
   //}
}

class RelojCalculadora implements Reloj, Calculadora2{

   public $color;
   public $hora;

   public function getColor(){
       $this->color = "Verde";
       return $this->color; 
   }
   public function getHora(){
       $this->hora = date("h:i:sa");
       return $this->hora;
   }
}

$miRelojCalculadora = new RelojCalculadora();
echo "La hora de mi reloj calculadora es: ". $miRelojCalculadora->getHora() ."<br/>";
echo "El color de mi reloj calculadora es: ". $miRelojCalculadora->getColor() ."<br/>";

abstract class CalculadoraAbstract{
    public function suma($a,$b){
        return $a + $b;
    }
    abstract public function setColor($color);
    abstract public function getMarca();
}

class Casio extends CalculadoraAbstract{
    public $marca = "Casio";
    protected $color;
    public function setColor($color){
        $this->color = $color;
    }
    public function getColor(){
        return $this->color;
    }
    public function getMarca(){
        return $this->marca;
    }
}

$casio = new Casio();
$totalSuma = $casio->suma(10,11);
$casio->setColor("Morado");
$marca = $casio->getMarca();

echo "Calculadora " . $casio->marca . ", color " . $casio->getColor() . " da resultado de la suma 11 y 11 como: " . $totalSuma ."! <br/>";

?>