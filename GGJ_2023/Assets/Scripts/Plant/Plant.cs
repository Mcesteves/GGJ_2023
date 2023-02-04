using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private PlantObject plantObject;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject enemy;
    private bool onRest;

    void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = plantObject.sprite;
    }

    void Update()
    {
        if (!onRest)
        {
            if(plantObject.plantType == PlantType.ShootingDamage)
                StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        enemy.GetComponent<EnemyDamage>().TakeDamage(plantObject.damage);

        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        onRest = true;
        var direction = (enemy.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * plantObject.bulletSpeed;

        var time = (enemy.transform.position - transform.position).magnitude / (direction * plantObject.bulletSpeed).magnitude;
        var waitTime = plantObject.rechargeTime - time;

        if (waitTime > 0)
        {
            yield return new WaitForSeconds(time);
            Destroy(bullet);
            yield return new WaitForSeconds(plantObject.rechargeTime - time);
            onRest = false;
        }
        else
        {
            yield return new WaitForSeconds(plantObject.rechargeTime);
            onRest = false;
            yield return new WaitForSeconds(time - plantObject.rechargeTime);
            Destroy(bullet);
        }

    }

    private void AuraAttack()
    {

    }

    private void AreaAttack()
    {

    }
}
