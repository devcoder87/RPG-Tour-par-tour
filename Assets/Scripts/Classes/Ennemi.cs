using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ennemi
{
    public string name { get; protected set; }
    public int xp { get; protected set; }
    public int vie { get; protected set; }
    public int vieMax { get; protected set; }
    public int chanceDeToucher { get; protected set; }
    public string DamageType { get; protected set; }
    public string difficulte { get; protected set; }
    public int attNumber { get; protected set; }

    public int lastHit { get; protected set; }
    public string lastAttack { get; protected set; }

    public List<EnnemiAttaque> listeAttaques;

    public Ennemi()
    {      
        listeAttaques = new List<EnnemiAttaque>();      
    }

    // Attaque
    public void Attaquer(Joueur cible)
    {

        int damage = 0;

        // Choisi une attaque parmis la liste des attaques
        int randomAttaque = Random.Range(0, listeAttaques.Count);
        
        // Chance de toucher
        int randomToucher = Random.Range(0, 100);

        // Vérifie la chance de toucher de l'ennemi - chance d'esquive du joueur
        if (randomToucher <= this.chanceDeToucher - cible.chanceEsquive)
        {
            damage = listeAttaques[randomAttaque].damage;
            cible.GetDamage(damage, listeAttaques[randomAttaque].name);
            this.lastAttack = listeAttaques[randomAttaque].name;
        }
        else
        {
            Debug.Log("Attaque de l'ennemi à échouée");
        }

        this.lastHit = damage;
    }

    public void GetDamage(int qteDamage, int qteDamageCritique)
    {
        this.vie -= (qteDamage + qteDamageCritique);
    }

}

public class RatGeant : Ennemi
{
    public RatGeant()
    {
        // Ajoute les attaques à la liste
        listeAttaques.Add(new EnnemiAttaque(1, "Griffe"));
        listeAttaques.Add(new EnnemiAttaque(2, "Morsure"));

        this.name = "Rat Géant";
        this.xp = 10;
        this.vie = 15;
        this.vieMax = vie;
        this.chanceDeToucher = 90;
        this.DamageType = "Dégats Physiques"; // 0 PHYSIQUE 1 MAGIQUES
        this.difficulte = "Facile";
        this.attNumber = listeAttaques.Count;

    }
}

public class VerDesEntrailles : Ennemi
{
    public VerDesEntrailles()
    {
        // Ajoute les attaques à la liste
        listeAttaques.Add(new EnnemiAttaque(1, "Dent de scie"));
        listeAttaques.Add(new EnnemiAttaque(2, "Charge"));
        listeAttaques.Add(new EnnemiAttaque(4, "Etreinte"));

        this.name = "Ver des entrailles";
        this.xp = 10;
        this.vie = 18;
        this.vieMax = vie;
        this.chanceDeToucher = 85;
        this.DamageType = "Dégats Physiques"; // 0 PHYSIQUE 1 MAGIQUES
        this.difficulte = "Facile";
        this.attNumber = listeAttaques.Count;

    }
}

public class Gargouille : Ennemi
{
    public Gargouille()
    {
        // Ajoute les attaques à la liste
        listeAttaques.Add(new EnnemiAttaque(10, "Cracher venin"));

        this.name = "Gargouille";
        this.xp = 20;
        this.vie = 7;
        this.vieMax = vie;
        this.chanceDeToucher = 50;
        this.DamageType = "Dégats Physiques"; // 0 PHYSIQUE 1 MAGIQUES
        this.difficulte = "Facile";
        this.attNumber = listeAttaques.Count;

    }
}

