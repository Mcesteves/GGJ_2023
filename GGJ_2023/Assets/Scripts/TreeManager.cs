using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public float life;
    public GameObject defeatedCanvas;
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
            life -= lifeDecrease;
        else
        {
            defeatedCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
            
    }
}
