<?php
    session_start();

    $_SESSION["usuario"]="Harry";
    $_SESSION["var1"]="Hola";

    //Unset nos permite limpiar el valor de una variable en especifico
    unset($_SESSION["usuario"]);

    echo "Hola, buen dia " . $_SESSION["usuario"] . "! <br/>";

    echo "Datos de la sesion (Antes):" . print_r($_SESSION);

    //Session_unset borra todas las variables de la sesion
    session_unset();

    echo "Datos de la sesion (Despues):" . print_r($_SESSION);

?>