using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public float life;
    void Start()
    {
        
    }

    void Update()
    {
        
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
            life -= lifeDecrease;
        //else
        //perde o jogo
            
    }
}
