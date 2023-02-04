using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public GnomeObject gnomeObject;
    private float life;
    void Start()
    {
        life = gnomeObject.endurance;
        GetComponent<SpriteRenderer>().sprite = gnomeObject.normalSprite;
    }

    public void TakeDamage(float lifeDecrease)
    {
        if (life > lifeDecrease)
            life -= lifeDecrease;
        else
            Destroy(this.gameObject);
    }
}
