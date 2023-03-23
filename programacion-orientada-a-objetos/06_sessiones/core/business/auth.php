<?php
    include "core/business/security.php";
    //include "core/dao/user.php";

    if(!check_user_auth()){
        header("Location: /aztlan/06_sessiones/login.php");
        exit();
    }
   
?>