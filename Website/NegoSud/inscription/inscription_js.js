window.onload = function() {

    loadMenuEvent()

    var bouton = document.getElementById('bouton_inscription');
    bouton.addEventListener("click", tryInscription)
}

function tryInscription() {
    
    // TODO : vérifier si tous les champs sont remplis

    // TODO : envoyer une requête à l'API pour créer le nouvel utilisateur
    // Si le nouvel utilisateur a été créé, on fait ce code :

    window.location.href = "./?connect=1"
}