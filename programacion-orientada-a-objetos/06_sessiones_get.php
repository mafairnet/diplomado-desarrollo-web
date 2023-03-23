<?php
    //Se indica a PHP que se va a iniciar uan sesion
    session_start();

    echo "Valor de usuario almacenado en sesion: " . $_SESSION["usuario"] . "<br/>";
?>