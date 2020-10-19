using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{

    public int id { get; protected set; }
    public string type { get; protected set; }
    public float prixAchat { get; protected set; }
    public float prixVente { get; protected set; }
    
    public string description { get; protected set; }
    public float ratioPrixVente = 1.25f;

    public List<string> listNames { get; protected set; }
    public string name { get; protected set; }

    public abstract int GetStat1();

}

