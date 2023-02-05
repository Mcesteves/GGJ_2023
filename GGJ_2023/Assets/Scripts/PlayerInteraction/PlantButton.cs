using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantButton : MonoBehaviour
{
    public GameObject plantPrefab;
    public bool removeButton;

    public void SetPlant()
    {
        PlantManager.instance.PlantTree(plantPrefab);
    }
    public void RemovePlant()
    {
        PlantManager.instance.RemovePlant();
    }
    public void SetButton()
    {
        if (PlantManager.instance.removePlant)
            GetComponent<Button>().interactable = removeButton;
        else
            GetComponent<Button>().interactable = !removeButton;
    }            
}
