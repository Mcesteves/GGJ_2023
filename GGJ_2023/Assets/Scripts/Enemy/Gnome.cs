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
        StartCoroutine(TakeDamageCoroutine(lifeDecrease));
    }

    private IEnumerator TakeDamageCoroutine(float lifeDecrease)
    {
        particles.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.1f);
        if (life > lifeDecrease)
        {
            life -= lifeDecrease;
            
            if (life <= gnomeObject.endurance / 2)
                anim.SetBool("hurt", true);
        }
        else
        {
            AudioManager.instance.Play("gnome_death");
            Destroy(this.gameObject);
        }
            
    }
}
