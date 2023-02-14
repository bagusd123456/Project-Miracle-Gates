using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour,IData
{
    public Material cubeMaterial;
    public Material defaultMaterial;
    public GameObject sphereGO;

    public bool activateSphere;

    [ColorUsage(true,true)]
    public Color baseColor;

    public void LoadData(GameData data)
    {
        this.cubeMaterial = data.cubeMaterial;
        this.activateSphere = data.activateSphere;
        this.baseColor = data.baseColor;
    }

    public void SaveData(GameData data)
    {
        data.cubeMaterial = this.cubeMaterial;
        data.activateSphere = this.activateSphere;
        data.baseColor = this.baseColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sphereGO.SetActive(activateSphere);
        if (cubeMaterial != null)
            cubeMaterial.SetColor("_BaseColor", baseColor);
        else
            this.cubeMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    public void changeColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        baseColor = newColor;
    }

    public void activateHat()
    {
        if (activateSphere)
            activateSphere = false;
        else
            activateSphere = true;
    }
}
