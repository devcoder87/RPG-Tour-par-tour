using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LISTE DES ATTAQUES

public class EnnemiAttaque
{
    public int damage { get; protected set; }
    public string name { get; protected set; }
    public string description { get; protected set; }

    public EnnemiAttaque(int damage, string name = "aucun nom", string description = "aucune description")
    {
        this.damage = damage;
        this.name = name;
        this.description = description;
    }
}