using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnItem : MonoBehaviour {

    GameObject mainCamera;
    Gameplay gameplay;

    public void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        gameplay = mainCamera.GetComponent<Gameplay>();
    }

    public void SelectionItemInventaire(GameObject item)
    {
        // récupération du nom item (numero) 
        int id = int.Parse(item.name);
        Debug.Log("ID(" +id +") item: " + gameplay.listInventaire[id].name);
        Description(id);
        gameplay.InventaireItemSelection = id;
    }

    public void Description(int id)
    {
        GameObject.Find("PanelInventaire/Description").GetComponent<Text>().text = gameplay.listInventaire[id].name;
    }

}
