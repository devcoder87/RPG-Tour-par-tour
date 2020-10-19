using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armure : Item
{
    public int vie { get; private set; }
    public int vieMax { get; private set; }
    public int energie { get; private set; }
    public int energieMax { get; private set; }
    public int chanceEsquive { get; private set; }
    public int reductionPhysique { get; private set; }
    public int reductionMagie { get; private set; }

    // Constructeur ARMURE
    public Armure(int vie, int energie, int chanceEsquive, int reductionPhysique, int reductionMagie, int prixAchat)
    {
        NamesArmures();
        this.name = GetName();
        this.type = "armure";
        this.vie = vie;
        this.vieMax = vie;
        this.energie = energie;
        this.energieMax = energie;
        this.chanceEsquive = chanceEsquive;
        this.reductionPhysique = reductionPhysique;
        this.reductionMagie = reductionMagie;
        this.prixAchat = prixAchat;
        this.prixVente = prixAchat / this.ratioPrixVente;
    }

    public override int GetStat1()
    {
        return this.vieMax;
    }

    public void NamesArmures()
    {
        this.listNames = new List<string>();
        this.listNames.Add("Gilet en mailles");
        this.listNames.Add("Tenue du moine");
        this.listNames.Add("Cape en cuir");
        this.listNames.Add("Armure de fer");
        this.listNames.Add("Gilet en bois");
        this.listNames.Add("Plastron en cuivre");
    }

    public string GetName()
    {
        int rnd = Random.Range(0, this.listNames.Count);
        return this.listNames[rnd];
    }

}