using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public GnomeObject gnomeObject;
    private float life;
    private Animator anim;
    void Start()
    {
        life = gnomeObject.endurance;
        GetComponent<SpriteRenderer>().sprite = gnomeObject.normalSprite;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float lifeDecrease)
    {
        if (life > lifeDecrease)
        {
            life -= lifeDecrease;
            if (life <= gnomeObject.endurance / 2)
                anim.SetBool("hurt", true);
        }
        else
            Destroy(this.gameObject);
    }
}
