using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private float life;
    public void TakeDamage(float lifeDecrease)
    {
        if (life > lifeDecrease)
            life -= lifeDecrease;
        else
            Destroy(this.gameObject);
    }
}
