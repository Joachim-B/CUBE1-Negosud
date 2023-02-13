<header>
        <div class="col-6 logo_negosud">
          <a href="./">
            <img src="./img/logo_vin_4-removebg-preview.jpg" alt="logo negosud" title="logo negosud" class="img-fluid">
          </a>
        </div>  
  
        <div class="container_bouton_header">
            <div>
              <img src="./img/logo_mon_compte.jpg" alt="" id="logo_compte">
              <ul id="menu_mon_compte">
              <?php
                  if (session_status() != 2)
                  {
                      session_start();
                  }

                  if( isset( $_SESSION['connected'] ) == false ) {
                      $_SESSION['connected'] = false;
                  }

                  if ($_SESSION['connected'] == false)
                  {
                      echo '
                      <li><a href="./connexion/"><div>Se connecter</div></a></li>
                      <li><a href="./inscription/"><div>S\'inscrire</div></a></li>';
                  }
                      
                  else {
                      echo '<li><a href="./inscription/"><div>Se déconnecter</div></a></li>';
                  }
                  ?>
              </ul>
            </div>
            
            <div>   
              <a href="./panier_commandes">     
                <img src="./img/logo_panier.jpg" alt="logo Mon panier" title="" class="img-fluid">
              </a>
            </div>              
        </div>
  </header>
    
  <nav>
      <ul class="nav nav-justified">      
          <li class="nav-item">
            <a class="nav-link  lien" href="./vins_rouges">Vins rouges</a>
          </li>
          <li class="nav-item">
            <a class="nav-link  lien" href="./vins_roses">Vins rosés</a>
          </li>
          <li class="nav-item">
            <a class="nav-link lien" href="./vins_blancs">Vins blancs</a>
          </li>
          <li class="nav-item">
            <a class="nav-link  lien" href="./armagniacs">Armagniacs</a>
          </li>
      </ul>
    </nav>
