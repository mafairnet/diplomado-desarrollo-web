<?php
    $nombre = "usuario";
    $valor = "Bryan";

    $expiracion = time() + 30 * 60;
    
    $ruta = "/";
    $dominio = "localhost";

    $encriptar = false;

    $manejar_http = true;

    setcookie($nombre,$valor,$expiracion,$ruta,$dominio,$encriptar,$manejar_http);

    echo "Se ha creado la cookie <br/>";

    echo "Valor de la cookie: " .  $_COOKIE["usuario"];
?>