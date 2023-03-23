<?php 
    //die("Form submited");
    $message = "";

    if(isset($_POST['submit']))
    {

        //print_r($_POST);

        include 'core/config/db.php';  
        include 'core/dao/usuario.php';
        include 'core/business/usuario.php';
        include 'core/business/security.php'; 
        include 'core/model/usuario.php';
        
        if($_POST['username'] != "" && $_POST['password'] != "")
        {
            $message = validate_user($_POST['username'],$_POST['password']);
        }
        else
        {
            $message = "Provide all the required data!";
        }

        //die($message);
    }
?>
<!doctype html>
<html lang="en">
  <head>
    <?php include "core/view/styles_libraries.php";?>
  </head>
  <body class="text-center">
    
<main class="form-signin">
  <form method="post">
    <!--<img class="mb-4" src="assets/brand/bootstrap-logo.svg" alt="" width="72" height="57">-->
    <h1 class="h1 mb-3 fw-normal">Practicas DDW - Aztlan</h1>
    <h1 class="h5 mb-3 fw-normal">Please sign in</h1>
    <?php if($message!=""){?>
        <h3 class="h5 mb-3 fw-normal"><?php echo $message;?></h3>
    <?php } ?>
    <div class="form-floating">
      <input type="text" class="form-control" id="floatingInput" placeholder="usuario" name="username">
      <label for="floatingInput">Email address</label>
    </div>
    <div class="form-floating">
      <input type="password" class="form-control" id="floatingPassword" placeholder="Password"  name="password" >
      <label for="floatingPassword">Password</label>
    </div>

    <div class="checkbox mb-3">
      <label>
        <input type="checkbox" value="remember-me"> Remember me
      </label>
    </div>
    <button class="w-100 btn btn-lg btn-primary" type="submit" name="submit">Sign in</button>
    <p class="mt-5 mb-3 text-muted">GNU WebApp by @mafairnet</p>
    <!--<p class="mt-5 mb-3 text-muted">Based on Bootstrap and Asterisk OSP</p>-->
  </form>
</main>


    
  </body>
</html>
