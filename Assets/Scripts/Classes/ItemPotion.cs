using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public int PV { get; private set; }

    public Potion(int PV, int price)
    {
        this.type = "potion";
        this.PV = PV;
        this.prixAchat = price;
        this.prixVente = prixAchat / this.ratioPrixVente;
    }

    public override int GetStat1()
    {
        return this.PV;
    }

}