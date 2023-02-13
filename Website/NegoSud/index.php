<!DOCTYPE html>
<html lang="en">

<!doctype html>
<html>
<head>
	<meta charset="utf-8">
	<title>Negosud</title>
	
  <?php require_once './negosud_utils/accueil/css_js_files.php'?>

  <script src="/path/to/flickity.pkgd.min.js"></script>
  <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
</head>

<body>

<div class="container">

  <!--En tête-->
  <?php require_once './negosud_utils/accueil/header.php'?>

 
  <!--Corps de page avec les carrousel-->

  <!-- Carrousel vin rouge-->

  <div class="col-lg-12 portfolio-item portfolio_vin mb-5">
			
    <h1 class="title_card">Nos vins rouges</h1>		  
    <div class="carrousel mb-2" data-flickity='{ "wrapAround": true }'>
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="./img/uby-n°7-merlot-tannat-cotes-de-gascogne-rouge.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="./img/uby-n°7-merlot-tannat-cotes-de-gascogne-rouge.jpg" class="img-fluid" alt="uby vin rouge" style="height: 200px; margin-left: 25px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Domaine Uby n°7</h2>
           <p class="text-center">Vin rouge merlot</p>
           <p class="text-center" style="color:dimgray">Côte de Gascogne fruité et tannique</p>											
         </div>
      </div>
     </div>
     <div class="carousel-cell">
     <div class="card" style="width: 17rem; height: 25rem;">
        <a href="./img/pellehaut_rouge.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="./img/pellehaut_rouge.jpg" class="img-fluid" alt="vin rouge Pellehaut" style="height: 200px; margin-left: 80px;"></a>
        <div class="card-body">
         <h2 class="card-title text-center">Domaine de Pellehaut</h2>	
         <p class="text-center">Vin rouge merlot</p>
         <p class="text-center" style="color:dimgray">Côte de Gascogne aux goût de fruits rouge</p>				
       </div>
      </div>
     </div>
     
     <div class="carousel-cell">
      <div class="card" style="width: 17rem; height: 25rem;">
        <a href="img/chateau ahut marbuzet.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/chateau ahut marbuzet.jpg" class="img-fluid" alt="" style="height: 200px; margin-left: 25px;"></a>
        <div class="card-body">
         <h2 class="card-title text-center">Domaine Marbuzet</h2>
         <p class="text-center">Chateau Mahut Marbuzet</p>
         <p class="text-center" style="color:dimgray">Merlot rouge, arômes de cerises</p>						
       </div>
      </div> 
     </div>
     
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/domaine de Bilé.png" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/domaine de Bilé.png" class="img-fluid" alt="vin rouge domaine de bilé" style="height:200px; margin-left: 50px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Domaine de Bilé</h2>
           <p class="text-center">Côte de Gascogne rouge</p>
           <p class="text-center" style="color:dimgray">Souple et léger en bouche</p>			
         </div>
      </div>
     </div>
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/domaine La Bruyère.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/domaine La Bruyère.jpg" class="img-fluid" alt="Côte de Gascogne rouge domaine La Bruyère" style="height: 200px; margin-left: 75px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Domaine La Bruyère</h2>
           <p class="text-center">Côte de Gascogne rouge</p>
           <p class="text-center" style="color:dimgray">Souple et léger en bouche</p>													
         </div>
      </div>
     </div>
         
    </div>   
   </div>	

   <!-- Carrousel vin rosé-->
   
   <div class="col-lg-12 portfolio-item portfolio_vin mb-5">
			
    <h1 class="title_card">Nos vins rosé</h1>		  
    <div class="carrousel mb-2" data-flickity='{ "wrapAround": true }'>
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/rosé_pellehaut.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/rosé_pellehaut.jpg" class="img-fluid" alt="vin rosé domaine de Pellehaut" style="height: 200px; margin-left: 100px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Domaine de Pellehaut</h2>
           <p class="text-center">Vin rosé</p>
           <p class="text-center" style="color:dimgray">Côte de Gascogne fruité aux arômes orangers</p>											
         </div>
      </div>
     </div>
     <div class="carousel-cell">
     <div class="card" style="width: 17rem; height: 25rem;">
        <a href="img/rosé_pellehaut_2.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/rosé_pellehaut_2.jpg" class="img-fluid" alt="vin rosé domaine de Pellehaut" style="height: 200px;margin-left: 90px;"></a>
        <div class="card-body">
         <h2 class="card-title text-center">Domaine de Pellehaut</h2>	
         <p class="text-center">Vin rosé</p>
         <p class="text-center" style="color:dimgray">Côte de Gascogne fruité</p>				
       </div>
      </div>
     </div>
     
     <div class="carousel-cell">
      <div class="card" style="width: 17rem; height: 25rem;">
        <a href="img/Saint André 2021 Domaine de Joy.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/Saint André 2021 Domaine de Joy.jpg" class="img-fluid" alt="vin rosé Saint André" style="height: 200px; margin-left: 40px;"></a>
        <div class="card-body">
         <h2 class="card-title text-center">Domaine de Joy</h2>
         <p class="text-center">Vin rosé Saint André</p>
         <p class="text-center" style="color:dimgray">Idéal pour accompagner vos soirées d'été</p>						
       </div>
      </div> 
     </div>
     
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/domaine de Tariquet-rosé-Côte de Gascogne.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/domaine de Tariquet-rosé-Côte de Gascogne.jpg" class="img-fluid" alt="vin rosé domaine de Tariquet" style="height:200px; margin-left: 50px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Domaine de Tariquet</h2>
           <p class="text-center">Vin rosé</p>
           <p class="text-center" style="color:dimgray">Côte de Gascogne léger, fruité, féminin</p>			
         </div>
      </div>
     </div>
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/domaine-de-joy-rose.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/domaine-de-joy-rose.jpg" class="img-fluid" alt="Côte de Gascogne rouge domaine de Joy" style="height: 200px; margin-left: 65px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Domaine de Joy</h2>
           <p class="text-center">Vin rosé</p>
           <p class="text-center" style="color:dimgray">Côte de Gascogne léger, fruité, féminin</p>												
         </div>
      </div>
     </div>
         
    </div>   
   </div>	

   <!--Carrousel vinc blanc-->

   <div class="col-lg-12 portfolio-item portfolio_vin mb-5">
			
    <h1 class="title_card">Nos vins blancs</h1>		  
    <div class="carrousel mb-2" data-flickity='{ "wrapAround": true }'>
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/côtes de gascogne 2021.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/côtes de gascogne 2021.jpg" class="img-fluid" alt="Côte de Gascogne blanc moelleux" style="height: 200px; margin-left: 90px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Domaine Uby n°7</h2>
           <p class="text-center">Vin blanc moelleux</p>
           <p class="text-center" style="color:dimgray">Côte de Gascogne fruité et sucré</p>											
         </div>
      </div>
     </div>
     <div class="carousel-cell">
     <div class="card" style="width: 17rem; height: 25rem;">
        <a href="img/uby n°3 côtes de gascogne 2022.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/uby n°3 côtes de gascogne 2022.jpg" class="img-fluid" alt="vin blanc domaine Uby" style="height: 200px; margin-left: 90px;"></a>
        <div class="card-body">
         <h2 class="card-title text-center">Domaine Uby</h2>	
         <p class="text-center">Vin blanc uby n°4</p>
         <p class="text-center" style="color:dimgray">Vin blanc sec et fruité</p>				
       </div>
      </div>
     </div>
     
     <div class="carousel-cell">
      <div class="card" style="width: 17rem; height: 25rem;">
        <a href="img/Uby n°4 grand et petit mensang.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/Uby n°4 grand et petit mensang.jpg" class="img-fluid" alt="vin blanc domaine uby" style="height: 200px; margin-left: 35px;"></a>
        <div class="card-body">
         <h2 class="card-title text-center">Domaine Uby</h2>
         <p class="text-center">Vin blanc uby n°4 petit mensang</p>
         <p class="text-center" style="color:dimgray">Idéal pour accompagner poissons et fruits de mer</p>						
       </div>
      </div> 
     </div>
     
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/domaine de tariquet 2019.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/domaine de tariquet 2019.jpg" class="img-fluid" alt="vin blanc domaine de Tariquet" style="height:200px; margin-left: 80px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Domaine de Tariquet</h2>
           <p class="text-center">Vin blanc, Côtes de Gascogne</p>
           <p class="text-center" style="color:dimgray">Puissant et sec, ideal pour les fruits de mer</p>			
         </div>
      </div>
     </div>
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/Domaine_De_Joy_St_Andre_Cotes_De_Gascogne.png" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/Domaine_De_Joy_St_Andre_Cotes_De_Gascogne.png" class="img-fluid" alt="Vin blanc domaine de Joy" style="height: 200px; margin-left: 40px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Domaine de Joy</h2>
           <p class="text-center">Vin blanc, Côtes de Gascogne</p>
           <p class="text-center" style="color:dimgray">Fruité et léger et sucré en bouche, idéal pour vos aperitifs</p>													
         </div>
      </div>
     </div>
         
    </div>   
   </div>	

   <!--Carrousel Armagnac-->

   <div class="col-lg-12 portfolio-item portfolio_vin mb-5">
			
    <h1 class="title_card">Nos armagnacs</h1>
    <div class="carrousel mb-2" data-flickity='{ "wrapAround": true }'>
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/armagnac-bas-de-Montal-1990.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/armagnac-bas-de-Montal-1990.jpg" class="img-fluid" alt="Armangiac de Montal" style="height: 200px; margin-left: 25px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Bas Armagnac</h2>
           <p class="text-center">Armagnac De Montal 1990</p>
           <p class="text-center" style="color:dimgray">Une histoire ancestral pour un goût magique</p>											
         </div>
      </div>
     </div>
     <div class="carousel-cell">
     <div class="card" style="width: 17rem; height: 25rem;">
        <a href="img/armagnac-bas_Dartilongue_1990.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/armagnac-bas_Dartilongue_1990.jpg" class="img-fluid" alt="Armangiac Dartilongue" style="height: 200px; margin-left: 55px;"></a>
        <div class="card-body">
         <h2 class="card-title text-center">Bas Armagnac</h2>	
         <p class="text-center">Armagnac Dartilongue 1990</p>
         <p class="text-center" style="color:dimgray">Une histoire de savoir faire pour un goût savoureux</p>				
       </div>
      </div>
     </div>
     
     <div class="carousel-cell">
      <div class="card" style="width: 17rem; height: 25rem;">
        <a href="img/armagnac-bas_jean-cave-1988.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/armagnac-bas_jean-cave-1988.jpg" class="img-fluid" alt="Armagniac Jean Cavé 1988" style="height: 200px; margin-left: 45px;"></a>
        <div class="card-body">
         <h2 class="card-title text-center">Bas Armagnac</h2>
         <p class="text-center">Armagnac Jean Cavé 1988</p>
         <p class="text-center" style="color:dimgray">Un millésime exceptionnel pour un produit d'exception</p>						
       </div>
      </div> 
     </div>
     
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/armagniac-modèle-oslo_2010-domaine-bilé.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/armagniac-modèle-oslo_2010-domaine-bilé.jpg" class="img-fluid" alt="Armagnac Oslo" style="height:200px; margin-left: 70px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Bas Armagnac</h2>
           <p class="text-center">Armagnac Oslo 2010</p>
           <p class="text-center" style="color:dimgray">Cette liqueur fait ressurgir tout le savoir faire du domaine de Bilé</p>			
         </div>
      </div>
     </div>
     <div class="carousel-cell">
       <div class="card" style="width: 17rem; height: 25rem;">
         <a href="img/armagniac_modèle-ariane_domaine de bilé.jpg" data-gallery="portfolioGallery" class="portfolio-lightbox preview-link" title=""><img src="img/armagniac_modèle-ariane_domaine de bilé.jpg" class="img-fluid" alt="Armagnac Ariane" style="height: 200px; margin-left: 75px;"></a>
         <div class="card-body">
           <h2 class="card-title text-center">Bas Armagnac</h2>
           <p class="text-center">Armagnac Ariane 2010</p>
           <p class="text-center" style="color:dimgray">Cet armagnac c'est le savoir faire du domaine de Bilé</p>												
         </div>
      </div>
     </div>
         
    </div>   
   </div>	

   <!--Footer-->

   <?php require_once "./negosud_utils/accueil/footer.php" ?>
</body>

</html>