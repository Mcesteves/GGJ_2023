using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterSlider : MonoBehaviour
{
    private PlantManager plantManager;
    void Start()
    {
        plantManager = PlantManager.instance;
        plantManager.OnWaterUse += UpdateSlider;
        GetComponent<Slider>().maxValue = plantManager.waterTotal;
        GetComponent<Slider>().value = plantManager.waterTotal;
    }
    private void UpdateSlider()
    {
        GetComponent<Slider>().value = plantManager.currentWaterTotal;
    }
}
