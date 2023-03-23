<?php
    //Se indica a PHP que se va a iniciar uan sesion
    session_start();

    $_SESSION["usuario"] = "Harry";

    echo "Hola, buen dia " . $_SESSION["usuario"] . "<br/>";
?>