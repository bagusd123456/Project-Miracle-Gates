using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public Material cubeMaterial;
    public bool activateSphere;
    public Color baseColor;

    public GameData()
    {
        cubeMaterial = null;
        activateSphere = false;
        baseColor = Color.black;
    }
}
