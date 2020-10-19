using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : Item
{
    // Stats possibles sur une rune
    public int vie { get; private set; }
    public int energie { get; private set; }
    public int reductionPhysique { get; private set; }
    public int reductionMagique { get; private set; }
    public int chanceEsquive { get; private set; }
    public int chanceToucher { get; private set; }
    public int chanceCritique { get; private set; }
    public int chanceFuite { get; private set; }
    public int nbrStatistiques { get; private set; }

    private List<Stat> listeStatsDisponibles;
    public List<Stat> listeStats { get; protected set; }

    // Constructeur Rune
    public Rune(int energie, int chanceToucher, int chanceCritique)
    {
        NamesRunes();
        this.name = GetName();
        this.type = "rune";
        this.energie = energie;
        this.chanceToucher = chanceToucher;
        this.chanceCritique = chanceCritique;
    }

    // Rune créé aleatoirement
    public Rune(int niveau = 1)
    {
        NamesRunes();
        this.name = GetName();
        this.type = "rune";
        this.nbrStatistiques = 2 + niveau;

        /* 
        FONCTIONNEMENT ALGORITHME
        0. Initialise les variables à 0
        1. Crée une liste de stats disponibles (listeStatsDisponibles)
        2. Crée une liste de stats vide (listeStats)
        3. Pioche X nombre de fois (X déterminé par le niveau d'objet)
        4. Pour chaque pioche, prend une stat de listeStatsDisponibles et la copie dans la listeStats
        5. Supprime la stat en question de listeStatsDisponibles afin de ne pas créer de doublons.
        6. Met à jour la variable qui doit l'être
        */

        // Initialisation des variables à zero

        this.vie = 0;
        this.energie = 0;
        this.reductionPhysique = 0;
        this.reductionMagique = 0;
        this.chanceEsquive = 0;
        this.chanceToucher = 0;
        this.chanceCritique = 0;
        this.chanceFuite = 0;

        // Défini les stats disponibles

        this.listeStatsDisponibles = new List<Stat>();
        this.listeStatsDisponibles.Add(new Stat("vie", Random.Range(0, 10)));
        this.listeStatsDisponibles.Add(new Stat("energie", Random.Range(0, 5)));
        this.listeStatsDisponibles.Add(new Stat("reductionPhysique", Random.Range(0, 5)));
        this.listeStatsDisponibles.Add(new Stat("reductionMagique", Random.Range(0, 5)));
        this.listeStatsDisponibles.Add(new Stat("chanceEsquive", Random.Range(0, 5)));
        this.listeStatsDisponibles.Add(new Stat("chanceToucher", Random.Range(0, 5)));
        this.listeStatsDisponibles.Add(new Stat("chanceCritique", Random.Range(0, 5)));
        this.listeStatsDisponibles.Add(new Stat("chanceFuite", Random.Range(0, 5)));

        // Défini les stats de l'objet

        this.listeStats = new List<Stat>();

        for (int i = 0; i < this.nbrStatistiques; i++)
        {
            int randomStat = Random.Range(0, listeStatsDisponibles.Count);
            this.listeStats.Add(listeStatsDisponibles[randomStat]);
            this.listeStatsDisponibles.RemoveAt(randomStat);
        }

        // Attribution des stats de la List aux Variables

        for (int c = 0; c < listeStats.Count; c++)
        {
            switch (listeStats[c].nom)
            {
                case "vie":
                    this.vie = listeStats[c].valeur;
                    break;
                case "energie":
                    this.energie = listeStats[c].valeur;
                    break;
                case "reductionPhysique":
                    this.reductionPhysique = listeStats[c].valeur;
                    break;
                case "reductionMagique":
                    this.reductionMagique = listeStats[c].valeur;
                    break;
                case "chanceEsquive":
                    this.chanceEsquive = listeStats[c].valeur;
                    break;
                case "chanceToucher":
                    this.chanceToucher = listeStats[c].valeur;
                    break;
                case "chanceCritique":
                    this.chanceCritique = listeStats[c].valeur;
                    break;
                case "chanceFuite:":
                    this.chanceFuite = listeStats[c].valeur;
                    break;
            }

        }

        // Debug le résultat
        for (int s = 0; s < listeStats.Count; s++) Debug.Log(listeStats[s].nom + " : " + listeStats[s].valeur);

    }

    public override int GetStat1()
    {
        return this.energie;
    }

    public void NamesRunes()
    {
        this.listNames = new List<string>();
        this.listNames.Add("Rune du soleil");
        this.listNames.Add("Rune de vie");
        this.listNames.Add("Rune enchantée");
        this.listNames.Add("Rune démoniaque");
        this.listNames.Add("Rune équitable");
        this.listNames.Add("Rune maléfique");
    }

    public string GetName()
    {
        int rnd = Random.Range(0, listNames.Count);
        return this.listNames[rnd];
    }

    public class Stat
    {
        public string nom { get; protected set; }
        public int valeur { get; protected set; }

        public Stat(string _nom, int _valeur)
        {
            nom = _nom;
            valeur = _valeur;
        }
    }
}