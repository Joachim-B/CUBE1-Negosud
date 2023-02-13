window.onload=function(){
    loadMenuEvent()
};

function loadMenuEvent()
{
    var bouton = document.getElementById('logo_compte');
    var nav = document.getElementById('menu_mon_compte');
    bouton.onclick = function(e){
        if(nav.style.display=="block"){
            nav.style.display="none";
        }else{
            nav.style.display="block";
        }
    };
}

function GenerateWine(obj)
{
    let main = document.getElementsByTagName('main')[0]

    if (obj.length == 0)
    {
        main.innerHTML = "Produits momentanément indisponible"
    }
    else
    {
        let contenu =  ""

        let indexVin = 0

        while (indexVin < obj.length)
        {
            contenu += `<div class="row container_vin">\n`
            
            let nbVin = 0

            while (nbVin < 3 && indexVin < obj.length)
            {
                let vin = obj[indexVin]

                contenu += `<div class="col-4 card" style="width: 17rem; height: 40rem;">
                <img src="${vin.lien_image}" alt="" title="" class="img-fluid photo">
                <div class="texte">
                    <h1>Description</h1>
                    <p>${vin.nom}</p>
                    <p>${vin.nom_de_domaine}</p>
                    <p>${vin.description}</p>
                </div>
                <div style="display:flex; align-item:center; align-self:center;">
                    <div>
                        <input type="number" name="quantity" style="width:50px;</input>
                    </div>
                    <div class="bouton">
                        <button  style="background-color:white; padding:5px;">Panier</button>
                    </div>
                </div>
            </div>

            `
            indexVin++
            nbVin++
            }

            contenu += `</div>`
            
        }

        main.innerHTML = contenu
    }
}