<?php
    $nombre = "usuario";
    $valor = "Bryan";

    $expiracion = time() - 3600;
    
    $ruta = "/";
    $dominio = "localhost";

    $encriptar = false;

    $manejar_http = true;

    setcookie($nombre,$valor,$expiracion,$ruta,$dominio,$encriptar,$manejar_http);

    echo "Se ha borrado la cookie <br/>";
?>