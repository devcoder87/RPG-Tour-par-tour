using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gameplay : MonoBehaviour {

    public List<Arme> listArmes;
    public List<Armure> listArmures;
    public List<Rune> listRunes;
    public List<Potion> listPotions;
    

    public Joueur joueur;

    GameObject UI_panelExplorer;
    GameObject UI_panelCombat;
    GameObject UI_panelCompetences;
    GameObject UI_panelInventaire;
    

    void Start () {

        // Création des objets
        Initialisation();

        // Interface
        InitialisationUI();
  
    }

    void Update()
    { 
        if (Input.GetKeyUp(KeyCode.Space)) joueur.GetXP(Random.Range(10, 35));
        if (Input.GetKeyUp(KeyCode.A)) AjouterItemInventaire();
        if (Input.GetKeyUp(KeyCode.D)) NettoyerListe();
        if (Input.GetKeyUp(KeyCode.I)) joueur.DebugStats();
    }

    // Création des Items de départ et du joueur;
    void Initialisation()
    {
        ListeArmes();
        ListeArmures();
        ListeRunes();
        ListePotions();
        ListeInventaire();
        CreationJoueur();  
    }

    void InitialisationUI()
    { 
        UI_panelExplorer = GameObject.Find("PanelExplorer");
        UI_panelCombat = GameObject.Find("PanelCombat");
        UI_panelCompetences = GameObject.Find("PanelCompetences");
        UI_panelInventaire = GameObject.Find("PanelInventaire");

        PanelVisible("Explorer");
    }

    void ListeArmes()
    {
        listArmes = new List<Arme>();

        // Armes (TYPE, ATTAQUE, ATTAQUE SPE, CONSO ENERGIE, CHANCE TOUCHER, CHANCE CRITIQUE, PRIX)
        listArmes.Add(new Arme(2, 3, 1, 80, 5, 10));
        listArmes.Add(new Arme(3, 5, 2, 75, 4, 10));
        listArmes.Add(new Arme(3, 5, 2, 75, 4, 10));
        listArmes.Add(new Arme(3, 5, 2, 75, 4, 10));
    }

    void ListeArmures()
    {
        listArmures = new List<Armure>();

        // Armures (VIE, ENERGIE, ESQUIVE, REDUC PHYS, REDUC MAGIE, PRIX ACHAT)
        listArmures.Add(new Armure(20, 3, 3, 0, 0, 0));
        listArmures.Add(new Armure(30, 5, 6, 2, 2, 0));
        listArmures.Add(new Armure(40, 5, 6, 2, 2, 0));
        listArmures.Add(new Armure(50, 5, 6, 2, 2, 0));

        Debug.Log("Nbr armures: " + listArmures.Count);
    }

    void ListeRunes()
    {
        listRunes = new List<Rune>();

        // Runes (ENERGIE, CHANCE TOUCHER, CHANCE CRITIQUE)
        listRunes.Add(new Rune());
    }

    void ListePotions()
    {
        listPotions = new List<Potion>();
       
        // Potions
        listPotions.Add(new Potion(25, 0));
    }

    void ListeInventaire()
    {
        listInventaire = new List<Item>();
    }

    void CreationJoueur()
    {
        joueur = new Joueur();
        joueur.SetArmePrimaire(listArmes[0]);
        joueur.SetArmeSecondaire(listArmes[1]);
        joueur.SetArmure(listArmures[0]);
        joueur.SetRune(listRunes[0]);
        joueur.SetPotionPrimaire(listPotions[0]);

        // Methode à appeller après chaque Set (met à jour les stats du joueur)
        joueur.ActualiserStatsCompletes();
    }

    public void Explorer()
    {
        Debug.Log("Exploration...");

        int rando = Random.Range(0, 4);
        switch (rando)
        {
            case 0: Balade();  break;
            case 1: Combattre(); break;
            case 3: ButinAleatoire(); break;
        }
    }

    public void Balade()
    {
        Debug.Log("Vous avez marché x mètres");
    }

    public void ButinAleatoire()
    {
        Debug.Log("Vous avez trouvé un butin");

        float random = Mathf.Floor(Random.Range(0, 100));

        Debug.Log("random: " + random);

        // Droprate 80% OR / 15% ITEM / 5% STUFF
        if (random < 80)
        {
            Debug.Log("Vous recevez OR");
            RecevoirOr();
        } else if (random < 95)
        {
            Debug.Log("Vous recevez ITEM");
        } else
        {
            Debug.Log("Vous recevez STUFF");
            StuffAleatoire();
        }

    }

    public void RecevoirOr(bool beaucoup = false)
    {
        int montant;

        if (beaucoup == false)
        {
            montant = Random.Range(1, 20);
        }
        else
        {
            montant = Random.Range(100, 200);
        }

        Debug.Log("Vous recevez " + montant +" pièces d'or");
    }

    public void StuffAleatoire()
    {
        int randomType = Random.Range(0, 2);
        switch(randomType)
        {
            // Arme
            case 0: break;

            // Armure
            case 1: break;

            // Rune
            case 2: break;
        }
    }

    public void SwitchArme()
    {
        joueur.SwitchArme();
        Debug.Log("Changement d'arme");
    }

    public void PanelVisible(string name = "")
    {
        RectTransform explorer = UI_panelExplorer.GetComponent<RectTransform>();
        RectTransform combat = UI_panelCombat.GetComponent<RectTransform>();
        RectTransform competences = UI_panelCompetences.GetComponent<RectTransform>();
        RectTransform inventaire = UI_panelInventaire.GetComponent<RectTransform>();

        // On cache tous les panels

        explorer.localScale = new Vector3(0, 0);
        combat.localScale = new Vector3(0, 0);
        competences.localScale = new Vector3(0, 0);
        inventaire.localScale = new Vector3(0, 0);

        // Affiche uniquement le panel voulu

        switch (name)
            {
                case "Explorer": explorer.localScale = new Vector3(1, 1); break;
                case "Combat": combat.localScale = new Vector3(1, 1); break;
                case "Competences": competences.localScale = new Vector3(1, 1); break;
                case "Inventaire": inventaire.localScale = new Vector3(1, 1); ActualiserInventaire(); break;
            }

    }



}
