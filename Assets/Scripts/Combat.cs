using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class Gameplay : MonoBehaviour {

    bool modeCombat = false;
    bool tourJoueur = false;
    public int actionDuJoueur = 0;

    public Ennemi ennemi;

    //public Ennemi ennemi;

    public void Combattre()
    {
        PanelVisible("Combat");

        // Ré initialise les stats PV/ENERGIE pour le début du combat
        joueur.ActualiserStatsCompletes();
        JoueurChoixAction(0);

        // Lance le combat uniquement si le joueur à ses points de vie supérieur à 0
        if (joueur.vie > 0)
        {
            modeCombat = true;
            tourJoueur = true;

            GameObject.Find(UI_panelCombat.name + "/Journal/InfoText").GetComponent<Text>().text = "";

            // Création d'un ennemi aléatoire
            CreerEnnemi();

            RefreshPanelCombatInfo();

        }

    }

    public void CreerEnnemi()
    {
        int randomEnnemi = Random.Range(0, 3);

        switch (randomEnnemi)
        {
            case 0: ennemi = new RatGeant(); break;
            case 1: ennemi = new VerDesEntrailles(); break;
            case 2: ennemi = new Gargouille(); break;
        }
    }

    public void FinDuCombat(string message = "fin du combat")
    {
        modeCombat = false;
        Debug.Log(message);

        // Ré initialise les stats PV/ENERGIE
        joueur.ActualiserStatsCompletes();
        PanelVisible("Explorer");
    }

    public void JoueurChoixAction(int choix)
    {
        Text description = GameObject.Find(UI_panelCombat.name + "/Joueur/Description").GetComponent<Text>();

        actionDuJoueur = choix;
        switch (choix)
        {
            case 0:
                description.text = "Vous n'avez sélectionné aucun choix";
                CouleurBoutonsReset();
                break;
            case 1:
                description.text = "Inflige une attaque de " + joueur.slotArmePrimaire.attaque + " pts de dégats";
                CouleurBoutonActive("ButtonAtt");
                break;
            case 2:
                description.text = "Inflige une attaque spéciale de " + joueur.slotArmePrimaire.attaqueSpeciale + " pts de dégats et consomme " + joueur.slotArmePrimaire.consoEnergie + "pts d'énergie";
                CouleurBoutonActive("ButtonAttSpe");
                break;
            case 3:
                description.text = "Passez votre tour pour changer d'arme par " + joueur.slotArmeSecondaire.type;
                CouleurBoutonActive("ButtonSwitch");
                break;
            case 4:
                description.text = "Consomme une potion " + joueur.slotPotionPrimaire.description;
                CouleurBoutonActive("ButtonPotion1");
                break;
            case 5:
                description.text = "Consomme une potion " + joueur.slotPotionSecondaire.description;
                CouleurBoutonActive("ButtonPotion2");
                break;
            case 6:
                description.text = "Tentez de fuir le combat " + joueur.chanceDeFuite + "% chance de réussite";
                CouleurBoutonActive("ButtonFuite");
                break;
        }
    }

    private void CouleurBoutonActive(string name)
    {
        CouleurBoutonsReset();
        Color32 couleurBoutonON = new Color32(255, 150, 0, 255);
        GameObject.Find(UI_panelCombat.name + "/Joueur/" + name).GetComponent<Image>().color = couleurBoutonON;
    }

    public void CouleurBoutonsReset()
    {
        Color32 couleurBoutonOFF = new Color32(118, 118, 118, 255);

        GameObject.Find(UI_panelCombat.name + "/Joueur/ButtonAtt").GetComponent<Image>().color = couleurBoutonOFF;
        GameObject.Find(UI_panelCombat.name + "/Joueur/ButtonAttSpe").GetComponent<Image>().color = couleurBoutonOFF;
        GameObject.Find(UI_panelCombat.name + "/Joueur/ButtonSwitch").GetComponent<Image>().color = couleurBoutonOFF;
        GameObject.Find(UI_panelCombat.name + "/Joueur/ButtonPotion1").GetComponent<Image>().color = couleurBoutonOFF;
        GameObject.Find(UI_panelCombat.name + "/Joueur/ButtonPotion2").GetComponent<Image>().color = couleurBoutonOFF;
        GameObject.Find(UI_panelCombat.name + "/Joueur/ButtonFuite").GetComponent<Image>().color = couleurBoutonOFF;

    }

    public void JoueurAction()
    {
        if (modeCombat)
        {
            if (tourJoueur)
            {
                switch (actionDuJoueur)
                {
                    // Attaque
                    case 1:
                        joueur.Attaque(ennemi);
                        JournalCombat("Vous infligez " + joueur.lastHit + "dégats avec " + joueur.lastAttack);
                        //EnnemiAction();
                        StartCoroutine(EnnemiActionTimer());
                        break;
                    // Attaque Spéciale
                    case 2:
                        if (joueur.energie >= joueur.slotArmePrimaire.consoEnergie)
                        {
                            joueur.Attaque(ennemi, true);
                            JournalCombat("Vous infligez " + joueur.lastHit + "dégats avec " + joueur.lastAttack);
                            //EnnemiAction();
                            StartCoroutine(EnnemiActionTimer());
                        }
                        else
                        {
                            JournalCombat("Pas assez d'énergie");
                        }

                        break;
                    // Switch d'arme
                    case 3:
                        SwitchArme();
                        JournalCombat("Vous avez changé d'arme, vous passez votre tour");
                        StartCoroutine(EnnemiActionTimer());

                        break;
                    // Potion soin
                    case 4:
                        if (listPotions.Count > 0)
                        {
                            joueur.PotionSoin();
                            listPotions.RemoveAt(0);
                            JournalCombat("Vous utilisez une potion");
                            StartCoroutine(EnnemiActionTimer());
                        }
                        else
                        {
                            JournalCombat("Vous n'avez pas de potion");
                        }
                        break;
                    // Tenter de fuir le combat (random)
                    case 6:

                        JournalCombat("Vous essayez de fuir...");
                        int random = Random.Range(0, 100);

                        if (random <= joueur.chanceDeFuite)
                        {
                            FinDuCombat("Fin du combat, vous avez réussi à fuir bravo !");
                        }
                        else
                        {
                            Debug.Log("Vous n'avez pas réussi à vous enfuir");
                            StartCoroutine(EnnemiActionTimer());
                        }
                        break;
                }

                RefreshPanelCombatInfo();

            }
        }
    }

    IEnumerator EnnemiActionTimer()
    {
        tourJoueur = false;
        yield return new WaitForSeconds(1);
        EnnemiAction();

    }

    void EnnemiAction()
    {
        if (modeCombat)
        {
            if (ennemi.vie > 0)
            {
                if (tourJoueur == false)
                {

                    Debug.Log("L'ennemi joue... (PV: " + ennemi.vie + " / " + ennemi.vieMax);
                    Debug.Log("Ennemi ATTAQUE");
                    ennemi.Attaquer(joueur);

                    if (ennemi.lastHit > 0) JournalCombat("Ennemi vous inflige " + ennemi.lastHit + "avec " + ennemi.lastAttack);
                    else JournalCombat("L'ennemi à échoué son attaque");


                    if (joueur.vie > 0)
                    {
                        tourJoueur = true;
                    }
                    else
                    {
                        FinDuCombat("Fin du combat, vous êtes mort");
                    }
                }
            }
            else
            {
                FinDuCombat("Fin du combat, vous avez vaincu l'ennemi !");
                joueur.GetXP(ennemi.xp);
            }

            RefreshPanelCombatInfo();
        }
    }

    public void JournalCombat(string t = "empty message")
    {
        Text message = GameObject.Find(UI_panelCombat.name + "/Journal/InfoText").GetComponent<Text>();
        message.text = message.text + "\n" + t;
    }

    public void RefreshPanelCombatInfo()
    {
        // ENNEMI

        GameObject.Find(UI_panelCombat.name + "/Ennemi/Nom").GetComponent<Text>().text = ennemi.name;
        GameObject.Find(UI_panelCombat.name + "/Ennemi/Difficulte").GetComponent<Text>().text = ennemi.difficulte;
        GameObject.Find(UI_panelCombat.name + "/Ennemi/PV").GetComponent<Text>().text = "PV: " + ennemi.vie + "/" + ennemi.vieMax;
        GameObject.Find(UI_panelCombat.name + "/Ennemi/Type").GetComponent<Text>().text = ennemi.DamageType;
        GameObject.Find(UI_panelCombat.name + "/Ennemi/NbrAtt").GetComponent<Text>().text = ennemi.attNumber.ToString() + " Attaques";

        // JOUEUR

        GameObject.Find(UI_panelCombat.name + "/Joueur/Att").GetComponent<Text>().text = "ATT: " + joueur.slotArmePrimaire.attaque;
        GameObject.Find(UI_panelCombat.name + "/Joueur/AttSpe").GetComponent<Text>().text = "ATT SPE: " + joueur.slotArmePrimaire.attaqueSpeciale;
        GameObject.Find(UI_panelCombat.name + "/Joueur/ConsoEnergie").GetComponent<Text>().text = "ENG: " + joueur.slotArmePrimaire.consoEnergie;
        GameObject.Find(UI_panelCombat.name + "/Joueur/PV").GetComponent<Text>().text = "PV: " + joueur.vie + "/" + joueur.vieMax;
        GameObject.Find(UI_panelCombat.name + "/Joueur/Eng").GetComponent<Text>().text = "ENG: " + joueur.energie + "/" + joueur.energieMax;
    }


}
