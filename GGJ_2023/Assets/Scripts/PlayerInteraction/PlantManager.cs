using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class PlantManager : MonoBehaviour
{
    [HideInInspector]
    public static PlantManager instance;
    public Tilemap plants;
    public GameObject selectionPanel;
    [HideInInspector]
    public bool removePlant;

    private Vector3 plantPosition;
    private bool onPlantSelection;
    private GameObject selectedPlant;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !onPlantSelection)
        {
            var pos = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            var tile = plants.GetTile(new Vector3Int(pos.x, pos.y, 0));
            if (tile != null)
            {
                LayerMask mask = LayerMask.GetMask("Plants");
                onPlantSelection = true;
                plantPosition = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
                RaycastHit2D hit = Physics2D.Raycast(plantPosition, Vector2.up, 0.5f, mask);
                if (hit.collider != null && hit.collider.gameObject.transform.position == plantPosition)
                {
                    removePlant = true;
                    selectedPlant = hit.collider.gameObject.transform.parent.gameObject;
                }
                else
                {
                    removePlant = false;
                }
                OpenPlantMenu();
            }
        }
    }

    public void OpenPlantMenu()
    {
        selectionPanel.transform.position = Input.mousePosition;
        foreach (var button in selectionPanel.GetComponentsInChildren<PlantButton>())
            button.SetButton();
        selectionPanel.SetActive(true);
    }

    public void PlantTree(GameObject plant)
    {
        Instantiate(plant, plantPosition, Quaternion.identity);
        selectionPanel.SetActive(false);
        onPlantSelection = false;
    }

    public void RemovePlant()
    {
        Destroy(selectedPlant);
        selectionPanel.SetActive(false);
        onPlantSelection = false;
    }

}

