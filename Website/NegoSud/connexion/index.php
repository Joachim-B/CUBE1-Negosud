<!DOCTYPE html>
<html lang="en">

<!doctype html>
<html>
<head>
	<meta charset="utf-8">
	<title>Connexion</title>
	
    <?php require_once '../negosud_utils/pages/css_js_files.php'?>
    <link rel="stylesheet" href="./connexion.css" type="text/css">

</head>

<body>
    <div class="container">

         <!--En tête-->

         <?php require_once '../negosud_utils/pages/header.php'?>

        <!-- Corps de texte -->

        <div class="row mx-auto">
            <div class="col-6">
                <h1 class="mb-3">Pas encore inscrit ?</h1>
                <h2 style="text-transform: uppercase;" class="mb-5">Créer votre compte</h2>

                <a href="../inscription/">
                    <button type="button" class=" mb-5 bouton_validation" style="text-transform: uppercase;">Créer votre compte</button>
                </a>

            </div>

            <div class="col-6">
                <h1>Déjà inscrit ?</h1>
                <h2>CONNEXION</h2>

                <div>
                    <div class="flex-fill"><br><input type="text" name="name" placeholder="Entrez votre adresse e-mail *" class="form-control-sm form-only"></div>
                </div>
                <div>
                    <div class="flex-fill"><br><input type="text" name="password" placeholder="Entrez votre mot de passe *" class="form-control-sm form-only"></div>
                </div>

                <p><a href="#" class="lien_hypertext">Mot de passe oublié ?</a></p>

                <button type="button" href="#" class=" mb-5 bouton_validation" style="text-transform: uppercase;" id="bouton_connexion">CONNEXION</button>


            </div>

        </div>

        <!--Footer -->

        <?php require_once '../negosud_utils/pages/footer.php' ?>

        
    </div>
 
</body>
</html>

