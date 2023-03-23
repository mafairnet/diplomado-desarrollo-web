<!DOCTYPE html>
<html>
    <head>
        <title>PHP "Hello, world!"</title>
    </head>
    <body>
        <?php
            $nombre = "Miguel";
            echo '<p>Hello, world!</p>';
            echo '<p>Hello also to '.$nombre.'!</p>';
        
            $empleados = array("Miguel","Carlos","Erasto");

            $nombreSuperUser = "Miguel";

            print_r($empleados);

            //die("");

            foreach($empleados as $empleado){
                if($empleado == $nombreSuperUser){
                    echo "<p>Hello, SuperUser ".$empleado."!</p>";
                } else {
                    echo "<p>Hello, SimpleUser ".$empleado."!</p>";
                }
            }
        
        ?>
    </body>
</html>