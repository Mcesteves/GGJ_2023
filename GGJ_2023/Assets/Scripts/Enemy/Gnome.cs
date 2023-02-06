using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public GnomeObject gnomeObject;
    public GameObject particles;
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
            particles.GetComponent<ParticleSystem>().Play();
            if (life <= gnomeObject.endurance / 2)
                anim.SetBool("hurt", true);
        }
        else
            Destroy(this.gameObject);
    }
}
