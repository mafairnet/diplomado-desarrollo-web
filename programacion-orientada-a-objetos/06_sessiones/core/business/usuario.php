<?php

/*include "core/config/db.php";
include "core/dao/usuario.php";
include "core/model/usuario.php";*/

function getUsers(){
    $usuariosData = userDao("get_all","","");
    $usuarios = Array();
    if($usuariosData["result"]=="success"){
        
        foreach($usuariosData["data"] as $usuarioData){
            $usuario = new Usuario($usuarioData["id"],$usuarioData["first_name"],$usuarioData["second_name"],$usuarioData["username"],$usuarioData["password"]);
            $usuarios[] = $usuario;
        }
    }
    return $usuarios;
}

function getUser($id){

}

function insertUser($usuario){

}

function editUser($usuario){

}

function delteUser($usuario){

}

?>