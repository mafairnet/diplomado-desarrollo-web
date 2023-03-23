<?php
    //ini_set('display_errors', 0);
    //error_reporting(E_ALL);
    //ini_set('display_errors', 1);
    include 'core/business/auth.php';

    if(isset($_GET['view'])){
        switch($_GET['view']){
            case "calculadora":
                $view="calculadora";
                break;
            case "inicio":
                $view="inicio";
                break;
            default:
                $view="inicio";
                break;
        }
    } else {
        $view = "inicio";
    }

    $path = "core/view/".$view.".php";

    //echo "PATH: " . $path;

    include $path;
?>