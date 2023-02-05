using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    public float life;
    public GameObject defeatedCanvas;
    public GameObject treeSlider;
    private void Start()
    {
        treeSlider.GetComponent<Slider>().maxValue = life;
        treeSlider.GetComponent<Slider>().value = life;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gnome"))
        {
            var gnome = collision.gameObject;
            TakeDamage(gnome.GetComponent<Gnome>().gnomeObject.damage);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(float lifeDecrease)
    {
        if (life > lifeDecrease)
        {
            life -= lifeDecrease;
            treeSlider.GetComponent<Slider>().value = life;
        }   
        else
        {
            defeatedCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
            
    }
}
