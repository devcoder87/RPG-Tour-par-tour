using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : Item
{
    public int attaque { get; private set; }
    public int attaqueSpeciale { get; private set; }
    public int chanceToucher { get; private set; }
    public int chanceCritique { get; private set; }
    public int consoEnergie { get; private set; }

    // Arme définie par l'utilisateur
    public Arme(int attaque, int attaqueSpeciale, int consoEnergie, int chanceToucher, int chanceCritique, float prixAchat)
    {
        NamesArmes();
        this.name = GetName();
        this.type = "arme";
        this.attaque = attaque;
        this.attaqueSpeciale = attaqueSpeciale;
        this.consoEnergie = consoEnergie;
        this.chanceToucher = chanceToucher;
        this.chanceCritique = chanceCritique;
        this.prixAchat = prixAchat;
        this.prixVente = prixAchat / this.ratioPrixVente;
    }

    // Arme créé aleatoirement
    public Arme(int niveau = 1)
    {
        NamesArmes();
        this.name = GetName();
        this.type = "arme";

        switch (niveau)
        {
            case 1:
                attaque = Random.Range(1, 10);
                attaqueSpeciale = Random.Range(11, 20);
                consoEnergie = Random.Range(1, 4);
                chanceToucher = Random.Range(60, 80);
                chanceCritique = Random.Range(0, 10);
                break;
        }

        this.attaque = attaque;
        this.attaqueSpeciale = attaqueSpeciale;
        this.consoEnergie = consoEnergie;
        this.chanceToucher = chanceToucher;
        this.chanceCritique = chanceCritique;
        this.prixAchat = prixAchat;
        this.prixVente = prixAchat / this.ratioPrixVente;
    }

    public override int GetStat1()
    {
        return this.attaque;
    }

    public void NamesArmes()
    {
        this.listNames = new List<string>();
        this.listNames.Add("hache de barbare");
        this.listNames.Add("fléau du viking");
        this.listNames.Add("petite pointe");
        this.listNames.Add("lame d'acolyte");
        this.listNames.Add("épée moyenne");
        this.listNames.Add("hache de bucheron");
    }

    public string GetName()
    {
        int rnd = Random.Range(0, this.listNames.Count);
        return this.listNames[rnd];
    }


}