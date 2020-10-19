using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class Gameplay : MonoBehaviour {

    public GameObject PrefabItem;
    GameObject instanceEnCours;

    public List<Item> listInventaire;
    public int InventaireItemSelection;

    public void ActualiserInventaire()
    {
        // Nettoie la liste
        NettoyerListe();

        // Crée une nouvelle liste
        CreerListe();

        GameObject.Find(UI_panelInventaire.name + "/ArmureEquipee").GetComponent<Text>().text = joueur.slotArmure.name;
        GameObject.Find(UI_panelInventaire.name + "/ArmeEquipee").GetComponent<Text>().text = joueur.slotArmePrimaire.name;
        GameObject.Find(UI_panelInventaire.name + "/ArmeSecondaireEquipee").GetComponent<Text>().text = joueur.slotArmeSecondaire.name;
    }

    public void CreerListe()
    {
        Debug.Log("Inventaire.Count " + listInventaire.Count);

        for (int i = 0; i < listInventaire.Count; i++)
        {
            instanceEnCours = Instantiate(PrefabItem, GameObject.Find(UI_panelInventaire.name + "/Liste").transform);
            instanceEnCours.transform.name = i.ToString();
            instanceEnCours.transform.GetChild(0).GetComponent<Text>().text = listInventaire[i].name;
        }
    }

    public void NettoyerListe()
    {
        for (int o = 0; o < listInventaire.Count; o++)
        {
            Destroy(GameObject.Find(UI_panelInventaire.name + "/Liste/" + o));
        }
    }

    public void AjouterItemInventaire()
    {
        int r = Random.Range(0, listArmures.Count);
        listInventaire.Add(listArmures[r]);
        int r2 = Random.Range(0, listArmes.Count);
        listInventaire.Add(new Arme());
        Debug.Log("Nbr item dans listInventaire: " + listInventaire.Count);
        listRunes.Add(new Rune());
        ActualiserInventaire();
    }

    public void EquiperInventaire()
    {
        // pour stocker l'ancien item temporairement
        Item ancienItem;

        // TEST SI LACTION EST POSSIBLE
        if (listInventaire.Count != 0)
        {
            switch (listInventaire[InventaireItemSelection].type)
            {
                case "arme":
                    ancienItem = joueur.slotArmePrimaire;
                    if (listInventaire[InventaireItemSelection] is Arme)
                    {
                        joueur.SetArmePrimaire(listInventaire[InventaireItemSelection] as Arme);
                        listInventaire.Add(ancienItem);
                        listInventaire.Remove(listInventaire[InventaireItemSelection]);

                        Debug.Log("Ancien ITEM: " + ancienItem.name);
                        Debug.Log("Nouveau ITEM: " + joueur.slotArmePrimaire.name);
                    }
                    break;
                case "armure":
                    ancienItem = joueur.slotArmure;
                    if (listInventaire[InventaireItemSelection] is Armure)
                    {
                        joueur.SetArmure(listInventaire[InventaireItemSelection] as Armure);
                        listInventaire.Add(ancienItem);
                        listInventaire.Remove(listInventaire[InventaireItemSelection]);

                        Debug.Log("Ancien ITEM: " + ancienItem.name);
                        Debug.Log("Nouveau ITEM: " + joueur.slotArmure.name);
                    }
                    break;
                case "rune":
                    ancienItem = joueur.slotRune;
                    if (listInventaire[InventaireItemSelection] is Rune)
                    {
                        joueur.SetRune(listInventaire[InventaireItemSelection] as Rune);
                        listInventaire.Add(ancienItem);
                        listInventaire.Remove(listInventaire[InventaireItemSelection]);

                        Debug.Log("Ancien ITEM: " + ancienItem.name);
                        Debug.Log("Nouveau ITEM: " + joueur.slotRune.name);
                    }
                    break;
            }

        }

        ActualiserInventaire();

    }

}
