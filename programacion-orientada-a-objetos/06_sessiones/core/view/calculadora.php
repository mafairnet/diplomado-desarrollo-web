<html>
    <head>
        <?php include "core/view/styles_libraries.php";?>
        <?php //echo $holaError;?>
        
    </head>
    <body>
        <main class="form-signin text-center">
            <h1 class="h1 mb-3 fw-normal">Practicas DDW - Aztlan</h1>

            <p>Esta es nuestra calculadora!</p>

            <input type="text" class="form-control" id="valorOriginal" placeholder="0" name="valorOriginal" disabled="disabled">
            <br/>
            <input type="text" class="form-control" id="valorDeOperacion" placeholder="0" name="valorDeOperacion">
            <br/>
            <button class="w-23 btn btn-sm btn-primary" onclick="suma()" >Sumar</button>
            <button class="w-23 btn btn-sm btn-primary" onclick="resta()" >Restar</button>
            <button class="w-23 btn btn-sm btn-primary" onclick="divide()" >Dividir</button>
            <button class="w-23 btn btn-sm btn-primary" onclick="multiplica()" >Multiplicar</button>
            <br/><br/>
            <button class="w-100 btn btn-sm btn-primary" onclick="limpiar()" >Limpiar</button>
            <br/><br/>
            <a href="index.php?view=inicio"><button class="w-100 btn btn-lg btn-primary" >Pagina Principal</button></a>
            <br/><br/>
            <a href="logout.php"><button class="w-100 btn btn-lg btn-primary" >Cerrar Sesion</button></a>
        </main>
    </body>
</html>