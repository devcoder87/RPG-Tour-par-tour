using UnityEngine;
using UnityEngine.UI;

public class Joueur
{
    public int niveau { get; protected set; }
    public float xp { get; protected set; }
    public float xpNextLevel { get; protected set; }
    public int attaque { get; protected set; }
    public int attaqueSpeciale { get; protected set; }
    public int consoEnergie { get; protected set; }
    public int vie { get; protected set; }
    public int vieMax { get; protected set; }
    public int energie { get; protected set; }
    public int energieMax { get; protected set; }
    public int reductionDegatsPhysiques { get; protected set; }
    public int reductionDegatsMagiques { get; protected set; }
    public int chanceToucher { get; protected set; }
    public int chanceCritique { get; protected set; }
    public int chanceEsquive { get; protected set; }
    public int chanceDeLoot { get; protected set; }
    public int chanceDeFuite { get; protected set; }

    public int ptsCompetences { get; protected set; }
    public int ptsCompetencesPanier { get; protected set; }

    // POINTS COMPETENCES

    public int vie_comp { get; protected set; }
    public int attaque_comp { get; protected set; }
    public int energie_comp { get; protected set; }

    public int[] panierCompetences = new int[10];

    // MEMORISE LA DERNIERE ATTAQUE (degats + nom)
    public int lastHit { get; protected set; }
    public string lastAttack { get; protected set; }

    // CREATION DES SLOTS D'EQUIPEMENT
    public Arme slotArmePrimaire { get; protected set; }
    public Arme slotArmeSecondaire { get; protected set; }
    public Armure slotArmure { get; protected set; }
    public Rune slotRune { get; protected set; }
    public Potion slotPotionPrimaire { get; protected set; }
    public Potion slotPotionSecondaire { get; protected set; }

    public Joueur(int xp = 0)
    {
        this.niveau = 1;
        this.xp = xp;
        this.xpNextLevel = 100;
        this.ptsCompetences = 5;
        InitPanierCompetences(); 
    }

    // Non utilisé pour le moment
    public void SetAllStuff(Arme armePrimaire, Arme armeSecondaire, Armure armure, Rune rune, Potion potionPrimaire, Potion potionSecondaire)
    {
        SetArmePrimaire(armePrimaire);
        SetArmeSecondaire(armeSecondaire);
        SetArmure(armure);
        SetRune(rune);
        SetPotionPrimaire(potionPrimaire);
        SetPotionSecondaire(potionSecondaire);
    }

    public void InitPanierCompetences()
    {
        for (int n = 0; n < panierCompetences.Length; n++)
        {
            panierCompetences[n] = 0;
        }
    }

    public void SetArmePrimaire(Arme armePrimaire)
    {
        this.slotArmePrimaire = armePrimaire;
    }

    public void SetArmeSecondaire(Arme armeSecondaire)
    {
        this.slotArmeSecondaire = armeSecondaire;
    }

    public void SetArmure(Armure armure)
    {
        this.slotArmure = armure;
    }

    public void SetRune(Rune rune)
    {
        this.slotRune = rune;
    }

    public void SetPotionPrimaire(Potion potion)
    {
        this.slotPotionPrimaire = potion;
    }

    public void SetPotionSecondaire(Potion potion)
    {
        this.slotPotionSecondaire = potion;
    }

    // GESTION XP & NIVEAU
    public void GetXP(int quantite)
    {
        float surplusXp;

        this.xp += quantite;
        Debug.Log("XP: " + this.xp + "/" + this.xpNextLevel);

        // Passage au niveau supérieur
        if (this.xp >= this.xpNextLevel)
        {
            surplusXp = this.xp - this.xpNextLevel;
            this.niveau += 1;
            this.xpNextLevel = Mathf.Floor(this.xpNextLevel * 1.5f);
            this.xp = 0 + surplusXp;
            this.ptsCompetences += 1;
            Debug.Log("Vous êtes niveau " + this.niveau + "(Prochain: " + this.xp + "/" + this.xpNextLevel + ")");
        }
    }

    // ATTAQUE
    public void Attaque(Ennemi cible, bool attaqueSpeciale = false)
    {
        int damage = 0;
        int damageCritique = 0;

        int randomCritique = Random.Range(0, 100);
        int randomToucher = Random.Range(0, 100);

        if (randomToucher <= this.chanceToucher)
        {

            if (attaqueSpeciale == false)
            {
                damage = this.attaque;
                this.lastAttack = "Attaque";
            }
            else
            {
                damage = this.attaqueSpeciale;
                this.energie -= this.consoEnergie;
                this.lastAttack = "Attaque spéciale";
            }
            

            if (randomCritique <= this.chanceCritique)
            {
                
                damageCritique = damage;
                Debug.Log("Coup critique !!!!");
            }

            cible.GetDamage(damage, damageCritique);
        }
        else
        {
            Debug.Log("Attaque échouée");
        }

        this.lastHit = damage + damageCritique;
        
    }


    public void GetDamage(int quantite, string text = "")
    {
        this.vie -= quantite;
        Debug.Log(text);
    }

    public void PotionSoin()
    {
        this.vie += slotPotionPrimaire.PV;
        if (this.vie > this.vieMax)
        {
            this.vie = this.vieMax;
        }
    }

    // Fonction qui permet de permuter l'arme primaire et secondaire
    public void SwitchArme()
    {
        byte selection = 1;
        selection++;

        Arme slotPrimaireAvant = this.slotArmePrimaire;
        Arme slotSecondaireAvant = this.slotArmeSecondaire;

        if (selection == 1)
        {
            this.slotArmePrimaire = slotPrimaireAvant;
            this.slotArmeSecondaire = this.slotArmePrimaire;
        }
        else if (selection == 2)
        {
            this.slotArmePrimaire = this.slotArmeSecondaire;
            this.slotArmeSecondaire = slotPrimaireAvant;
        }
        else
        {
            selection = 1;
        }

        // Met à jour les sats du joueur.
        ActualiserStatsArme();
    }


    // Ajouter un point de compétence
    public void AddCompetence(int c)
    {  
        if (ptsCompetencesPanier > 0)
        {
            panierCompetences[c] += 1;
            ptsCompetencesPanier -= 1;
        }
    }

    // Supprimer un point de compétence
    public void RemoveCompetence(int c)
    {
        if (panierCompetences[c] > 0)
        {
            panierCompetences[c] -= 1;
            ptsCompetencesPanier += 1;
        }

    }

    // Initialise les points du panier en se calquant sur les pts compétences disponibles
    public void SetPtsCompetencesPanier()
    {
        ptsCompetencesPanier = ptsCompetences;
    }

    // Validation de l'attribution des points de compétences
    public void ValiderPtsCompetences()
    {
        int totalUsedPoints = 0;

        // Ajoute les pts du panier aux pts competences
        this.attaque_comp += panierCompetences[0];
        this.vie_comp += panierCompetences[1];
        this.energie_comp += panierCompetences[2];

        // Calcule le nombre total de points dépensés
        for (int n = 0; n < panierCompetences.Length; n++)
        {
            totalUsedPoints += panierCompetences[n];
        }

        // décompte les points du panier aux pts compétences
        this.ptsCompetences -= totalUsedPoints;

        // Ré intialise le panier à 0
        this.InitPanierCompetences();
        this.ptsCompetencesPanier = ptsCompetences;

        Debug.Log("Attaque: " + attaque_comp);
        Debug.Log("vieMax: " + vie_comp);
        Debug.Log("energieMax: " + energie_comp);

        ActualiserStatsCompletes();

    }

    // Met à jour toutes les stats du joueur
    public void ActualiserStatsCompletes()
    {
        this.vie = this.slotArmure.vie + this.slotRune.vie + this.vie_comp;
        this.vieMax = vie;
        this.energie = this.slotArmure.energie + this.slotRune.energie + this.energie_comp;
        this.energieMax = energie;
        this.consoEnergie = this.slotArmePrimaire.consoEnergie;
        this.attaque = this.slotArmePrimaire.attaque + this.attaque_comp;
        this.attaqueSpeciale = this.slotArmePrimaire.attaqueSpeciale + this.attaque_comp;
        this.reductionDegatsPhysiques = this.slotArmure.reductionPhysique + this.slotRune.reductionPhysique;
        this.reductionDegatsMagiques = this.slotArmure.reductionMagie + this.slotRune.reductionMagique;
        this.chanceEsquive = this.slotArmure.chanceEsquive + this.slotRune.chanceEsquive;
        this.chanceToucher = this.slotArmePrimaire.chanceToucher + this.slotRune.chanceToucher;
        this.chanceCritique = this.slotArmePrimaire.chanceCritique + this.slotRune.chanceCritique;
        this.chanceDeFuite = this.slotRune.chanceFuite + 50;
    }

    // Met à jour uniquement les stats d'arme (utilisé en combat)
    public void ActualiserStatsArme()
    {
        this.consoEnergie = this.slotArmePrimaire.consoEnergie;
        this.attaque = this.slotArmePrimaire.attaque + this.attaque_comp;
        this.attaqueSpeciale = this.slotArmePrimaire.attaqueSpeciale + this.attaque_comp;
        this.chanceToucher = this.slotArmePrimaire.chanceToucher + this.slotRune.chanceToucher;
        this.chanceCritique = this.slotArmePrimaire.chanceCritique + this.slotRune.chanceCritique;
    }

    public void DebugStats()
    {
        Debug.Log("_____________________________________________");
        Debug.Log("Niveau: " + niveau);
        Debug.Log("Attaque: " + attaque + " | " + attaqueSpeciale);
        Debug.Log("ATT arme primaire: " + slotArmePrimaire.attaque + " | " + slotArmePrimaire.attaqueSpeciale);
        Debug.Log("ATT arme secondaire: " + slotArmeSecondaire.attaque + " | " + slotArmeSecondaire.attaqueSpeciale);
        Debug.Log("Vie: " +vie+ "/" +vieMax);
        Debug.Log("Energie: " + energie + "/" + energieMax);
        Debug.Log("ReductionPhysique " + reductionDegatsPhysiques);
        Debug.Log("ReductionMagique " + reductionDegatsMagiques);
        Debug.Log("ChanceToucher " + chanceToucher);
        Debug.Log("ChanceCritique " + chanceCritique);
        Debug.Log("ChanceEsquive " + chanceEsquive);
        Debug.Log("ChanceFuite " + chanceDeFuite);
        Debug.Log("_____________________________________________");
    }


}