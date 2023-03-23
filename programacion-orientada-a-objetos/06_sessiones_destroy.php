<?php
    session_start();

    $_SESSION["usuario"]="Harry";
    $_SESSION["var1"]="Hola";

    echo "Datos de la sesion (Antes):" . print_r($_SESSION);

    //Se manda a llamar session destroy para borra no solamente als variables
    //Tambien se borra la relacion del session ID que se tiene del cliente con el servidor
    session_destroy();

    echo "Hola, buen dia " . $_SESSION["usuario"] . "! <br/>";

    echo "Datos de la sesion (Despues):" . print_r($_SESSION);

?>