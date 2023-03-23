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
?>