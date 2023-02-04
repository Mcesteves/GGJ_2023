using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private float life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float lifeDecrease)
    {
        if (life > lifeDecrease)
            life -= lifeDecrease;
        else
            Destroy(this.gameObject);
    }
}
