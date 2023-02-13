window.onload = function() {

    loadMenuEvent()
    
    let json = `
    [
        {
            "nom": "Uby n°7",
            "nom_de_domaine": "Domaine uby",
            "description": "Vin rouge merlot. Côte de Gascogne fruité et tannique.",
            "annee": 2021,
            "lien_image":"https://www.vinatis.com/64266-detail_default/uby-n7-rouge-merlot-tannat-2021-domaine-uby.png"
        },
    
        {
            "nom": "Uby n°7",
            "nom_de_domaine": "Domaine uby",
            "description": "Vin rouge merlot. Côte de Gascogne fruité et tannique.",
            "annee": 2021,
            "lien_image":"https://www.vinatis.com/64266-detail_default/uby-n7-rouge-merlot-tannat-2021-domaine-uby.png"
        },
    
        {
            "nom": "Uby n°7",
            "nom_de_domaine": "Domaine uby",
            "description": "Vin rouge merlot. Côte de Gascogne fruité et tannique.",
            "annee": 2021,
            "lien_image":"https://www.vinatis.com/64266-detail_default/uby-n7-rouge-merlot-tannat-2021-domaine-uby.png"
        },
    
        {
            "nom": "Uby n°7",
            "nom_de_domaine": "Domaine uby",
            "description": "Vin rouge merlot. Côte de Gascogne fruité et tannique.",
            "annee": 2021,
            "lien_image":"https://www.vinatis.com/64266-detail_default/uby-n7-rouge-merlot-tannat-2021-domaine-uby.png"
        },
    
        {
            "nom": "Uby n°7",
            "nom_de_domaine": "Domaine uby",
            "description": "Vin rouge merlot. Côte de Gascogne fruité et tannique.",
            "annee": 2021,
            "lien_image":"https://www.vinatis.com/64266-detail_default/uby-n7-rouge-merlot-tannat-2021-domaine-uby.png"
        }
        
    ]`

    const obj = JSON.parse(json)

    GenerateWine(obj)
};