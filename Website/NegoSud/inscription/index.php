<?php
session_start();

if (isset($_SESSION["connected"]) == false)
{
    $_SESSION["connected"] = false;
}

if (isset($_GET["connect"]) == true)
{
    if ($_GET["connect"] == 3 && $_COOKIE["valid_connection"] == 1)
    {
        $_SESSION["connected"] = true;
        $_COOKIE["valid_connection"] = 0;
        header("Location: ../");
    }
    else
    {
        header("Location: ./");
    }
}
?>

<!DOCTYPE html>
<html lang="en">

<!doctype html>
<html>
<head>
	<meta charset="utf-8">
	<title>Inscription</title>
	
  <?php require_once '../negosud_utils/pages/css_js_files.php'?>
  <link rel="stylesheet" href="./inscription.css" type="text/css">
  <script src="./inscription_js.js" type="text/javascript"></script>
  
</head>

<body>
    <div class="container">

         <!--En tête-->

         <?php require_once '../negosud_utils/pages/header.php'?>

        <!--Formulaire-->
        <div class="title text-center">
            <h1>Créer votre compte</h1>
        </div>

        <div class="row div_formulaire mx-auto">
            <div class="col-lg-8">
                <div class="bouton_radio">
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="flexRadioDefault" id="monsieur">
                        <label class="form-check-label" for="flexRadioDefault1">
                         Mr
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="flexRadioDefault" id="monsieur"checked>
                        <label class="form-check-label" for="flexRadioDefault2">
                         Mme
                        </label>
                    </div>
                </div>

                <div class="d-flex">
                    <div class="mr-2 flex-fill"> Nom *<br><input type="text" name="name"  placeholder="Votre Nom *" class="form-control-sm input_margin"></div>
                    <div class="flex-fill">  Prénom *<br><input type="text" name="surname" placeholder="Votre Prénom *" class="form-control-sm input_espace"></div>
                </div>
                 
                <div>
                    <div class="flex-fill"> Adresse e-mail *<br><input type="text" name="login" placeholder="Adresse e-mail *" class="form-control-sm form-only"></div>
                </div>
                
                <div class="d-flex">
                    <div class="mr-2 flex-fill"> Nouveau mot de passe *<br><input type="text" name="password"  placeholder="Entrez votre mot de passe *" class="form-control-sm input_margin"></div>
                    <div class="flex-fill">  Confirmer votre mot de passe *<br><input type="text" name="password" placeholder="Confimer votre mot de passe *" class="form-control-sm input_espace"></div>
                </div>       
                
                <div>
                    <div class="flex-fill"> Adresse *<br><input type="text" name="prenom" placeholder="Votre Adresse *" class="form-control-sm form-only"></div>
                </div>
                
                <div class="d-flex">
                    <div class="mr-2 flex-fill"> Code Postal *<br><input type="text" name="name"  placeholder="Votre Code Postal *" class="form-control-sm input_margin"></div>
                    <div class="flex-fill">  Ville *<br><input type="text" name="city" placeholder="Votre Ville *" class="form-control-sm input_espace"></div>
                </div>
                
                
                <button type="button" href="#" class="mb-5 bouton_validation" id="bouton_inscription">INSCRIPTION</button>
                
                
            </div>

            <div class="col-lg-4 image_formulaire">
                <img src="../img/domaine La Bruyère.jpg" alt="">
            </div>

        </div>
   

        <!--Footer-->

        <?php require_once '../negosud_utils/pages/footer.php' ?>
   
    </div>
</body>

</html>