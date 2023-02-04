using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private PlantObject plantObject;
    [SerializeField]
    private GameObject bulletPrefab;
    private bool onRest;
    private GameObject currentTarget;
    private List<GameObject> enemies;

    void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = plantObject.sprite;
        GetComponent<BoxCollider2D>().size = (2*plantObject.range + 1)*Vector2.one;
        enemies = new List<GameObject>();
    }

    void Update()
    {
        if (!onRest && enemies.Count!=0)
        {
            if (plantObject.plantType == PlantType.ShootingDamage)
                StartCoroutine(Shoot());
            else if (plantObject.plantType == PlantType.AuraDamage)
                StartCoroutine(AuraAttack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gnome"))
        {
            enemies.Add(collision.gameObject);
            if (enemies.Count == 1)
                currentTarget = enemies[0];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gnome"))
        {
            enemies.Remove(collision.gameObject);
            if (enemies.Count != 0)
                currentTarget = enemies[0];
            else
                currentTarget = null;
        } 
    }

    private IEnumerator Shoot()
    {
        currentTarget.GetComponent<EnemyDamage>().TakeDamage(plantObject.damage);

        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        onRest = true;
        var direction = (currentTarget.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * plantObject.bulletSpeed;

        var time = (currentTarget.transform.position - transform.position).magnitude / (direction * plantObject.bulletSpeed).magnitude;
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

    private IEnumerator AuraAttack()
    {
        foreach( GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyDamage>().TakeDamage(plantObject.damage);
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var direction = (enemy.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * plantObject.bulletSpeed;
            StartCoroutine(DestroyBullet(bullet, direction));
        }
        onRest = true;
        yield return new WaitForSeconds(plantObject.rechargeTime);
        onRest = false;
    }

    private IEnumerator DestroyBullet(GameObject bullet, Vector2 direction)
    {
        var time = (currentTarget.transform.position - transform.position).magnitude / (direction * plantObject.bulletSpeed).magnitude;
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }

    private void AreaAttack()
    {

    }
}
