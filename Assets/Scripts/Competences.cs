using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gameplay : MonoBehaviour {

    public void PanelCompetences()
    {
        joueur.SetPtsCompetencesPanier();
        PanelVisible("Competences");
        RefreshPanelCompetences();
    }

    public void CompetencesAdd(int c)
    {
        joueur.AddCompetence(c);
        RefreshPanelCompetences();
    }

    public void CompetencesRemove(int c)
    {
        joueur.RemoveCompetence(c);
        RefreshPanelCompetences();
    }

    public void ValiderPointsCompetencer()
    {
        joueur.ValiderPtsCompetences();
        RefreshPanelCompetences();
    }

    void RefreshPanelCompetences()
    {
        GameObject.Find(UI_panelCompetences.name + "/PointsDisponibles").GetComponent<Text>().text = "Vous avez actuellement " + joueur.ptsCompetences + "pts";
        GameObject.Find(UI_panelCompetences.name + "/Attaque").GetComponent<Text>().text = joueur.panierCompetences[0].ToString();
        GameObject.Find(UI_panelCompetences.name + "/VieMax").GetComponent<Text>().text = joueur.panierCompetences[1].ToString();
        GameObject.Find(UI_panelCompetences.name + "/EnergieMax").GetComponent<Text>().text = joueur.panierCompetences[2].ToString();
    }

}
