<?php

function filtrado($datos){
    $datos = trim($datos);
    $datos = stripslashes($datos);
    $datos = htmlspecialchars($datos);
    return $datos;
}

if(isset($_POST["submit"]) && $_SERVER["REQUEST_METHOD"] == "POST"){

    if(empty($_POST["nombre"])){
        $errores[] = "El nombre es requerido";
    }

    if(empty($_POST["password"]) || strlen($_POST["password"])<5){
        $errores[] = "La contraseña es requerida y debe ser mayor a 5 caracteres.";
    }

    if(!filter_var($_POST["email"], FILTER_VALIDATE_EMAIL) || empty($_POST["email"])){
        $errores[]="No se a indicado correo o este no es el formato correcto.";
    }

    if(empty($errores)){
        $nombre = filtrado($_POST["nombre"]);
        $password = filtrado($_POST["password"]);
        $educacion = filtrado($_POST["educacion"]);
        $nacionalidad = filtrado($_POST["nacionalidad"]);
        $idiomas = filtrado(implode(", ",$_POST["idiomas"]));
        $email = filtrado($_POST["email"]);
    }
}

?>
<html>
    <head>
        <title>Ejemplo de Formulario Completo</title>
    </head>
    <body>
            <?php if(isset($_POST["submit"]) && empty($errores)):?>
                <h2>Datos Enviados por Post</h2>
                Nombre: <?php isset($nombre) ? print $nombre : ""; ?><br/>
                Conraseña: <?php isset($password) ? print $password : ""; ?><br/>
                Educacion: <?php isset($educacion) ? print $educacion : ""; ?><br/>
                Nacionalidad: <?php isset($nacionalidad) ? print $nacionalidad : ""; ?><br/>
                Idiomas: <?php isset($idiomas) ? print $idiomas : ""; ?><br/>
                Email: <?php isset($email) ? print $email : ""; ?><br/>
                <br/>
            <?php endif;?>
            <ul>
            <?php
                if(isset($errores)){
                    foreach ($errores as $error) {
                        echo "<li>".$error."</li>";
                    }
                }
            ?>
            </ul>
            <form action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"])?>" method="POST">
                Nombre: <input type="text" name="nombre" maxlength="50"></br>
                Contraseña:<input type="password" name="password" ></br>
                Educacion:
                <select name="educacion">
                    <option value="sin-estudios" select="selected">Sin Estudios</option>
                    <option value="primaria">Primaria</option>
                    <option value="secundaria">Secundaria</option>
                    <option value="universidad">Universidad</option>
                </select>
                </br>
                Nacionalidad:
                    <input type="radio" name="nacionalidad" value="mexicana">Mexicana</input>
                    <input type="radio" name="nacionalidad" value="otra">Otra</input>
                </br>
                Idiomas:
                    <input type="checkbox" name="idiomas[]" value="ingles">English</input>
                    <input type="checkbox" name="idiomas[]" value="español">Spanish</input>
                    <input type="checkbox" name="idiomas[]" value="frances">French</input>
                </br>
                Email: <input type="text" name="email">
                </br>
                <input type="submit" name="submit" value="Enviar">
            </form>
    </body>
</html>