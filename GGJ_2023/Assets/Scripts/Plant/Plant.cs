using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public List<GameObject> enemies;
    [SerializeField]
    private PlantObject plantObject;
    [SerializeField]
    private GameObject bulletPrefab;
    private bool onRest;
    private GameObject currentTarget;
    private List<GameObject> bombTargets;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = plantObject.sprite;
        GetComponent<BoxCollider2D>().size = (2*plantObject.range + 1)*Vector2.one;
        enemies = new List<GameObject>();
        bombTargets = new List<GameObject>();
            
    }
    void Update()
    {
        if (!onRest && enemies.Count!=0)
        {
            currentTarget = enemies[0];
            if (plantObject.plantType == PlantType.ShootingDamage)
                StartCoroutine(Shoot());
            else if (plantObject.plantType == PlantType.AuraDamage)
                StartCoroutine(AuraAttack());
            else
                StartCoroutine(AreaAttack());
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
            if (enemies.Contains(collision.gameObject))
            {
                enemies.Remove(collision.gameObject);
                if (enemies.Count != 0)
                    currentTarget = enemies[0];
                else
                    currentTarget = null;
            }
        } 
    }

    private IEnumerator Shoot()
    {
        
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
            currentTarget.GetComponent<Gnome>().TakeDamage(plantObject.damage);
            yield return new WaitForSeconds(plantObject.rechargeTime - time);
            onRest = false;
        }
        else
        {
            yield return new WaitForSeconds(plantObject.rechargeTime);
            onRest = false;
            yield return new WaitForSeconds(time - plantObject.rechargeTime);
            Destroy(bullet);
            currentTarget.GetComponent<Gnome>().TakeDamage(plantObject.damage);
        }
    }

    private IEnumerator AuraAttack()
    {
        foreach( GameObject enemy in enemies)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var direction = (enemy.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * plantObject.bulletSpeed;
            StartCoroutine(DestroyBullet(bullet, direction, enemy));
        }
        onRest = true;
        yield return new WaitForSeconds(plantObject.rechargeTime);
        onRest = false;
    }

    private IEnumerator DestroyBullet(GameObject bullet, Vector2 direction, GameObject enemy = null)
    {
        var time = (currentTarget.transform.position - transform.position).magnitude / (direction * plantObject.bulletSpeed).magnitude;
        yield return new WaitForSeconds(time);
        Destroy(bullet);
        enemy.GetComponent<Gnome>().TakeDamage(plantObject.damage);
    }

    private IEnumerator DestroyBomb(GameObject bullet, Vector2 direction)
    {
        var time = (currentTarget.transform.position - transform.position).magnitude / (direction * plantObject.bulletSpeed).magnitude;
        yield return new WaitForSeconds(time);
        Destroy(bullet);
        foreach (GameObject enemy in bombTargets)
        {
            if(enemy != null)
                enemy.GetComponent<Gnome>().TakeDamage(plantObject.damage);
        }
    }

    private IEnumerator AreaAttack()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var direction = (currentTarget.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * plantObject.bulletSpeed;
        CheckPositions(currentTarget);
        StartCoroutine(DestroyBomb(bullet, direction));
        onRest = true;
        yield return new WaitForSeconds(plantObject.rechargeTime);
        onRest = false;
    }

    public void CheckPositions(GameObject bomb)
    {
        bombTargets.Clear();
        Vector2[] directions = {Vector2.up, Vector2.down, Vector2.left, Vector2.right, new Vector2(1.0f, 1.0f), new Vector2(-1.0f, 1.0f), new Vector2(1.0f, -1.0f), new Vector2(-1.0f, -1.0f)};
        foreach (var dir in directions)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(bomb.transform.position, dir, plantObject.bombRange + 0.5f);
            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Gnome") && !bombTargets.Contains(hit.collider.gameObject))
                    bombTargets.Add(hit.collider.gameObject);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(currentTarget != null)
        {
            Gizmos.DrawRay(currentTarget.transform.position, Vector2.up * (plantObject.bombRange+ 0.5f));
            Gizmos.DrawRay(currentTarget.transform.position, Vector2.down * (plantObject.bombRange + 0.5f));
            Gizmos.DrawRay(currentTarget.transform.position, Vector2.right * (plantObject.bombRange + 0.5f));
            Gizmos.DrawRay(currentTarget.transform.position, Vector2.left * (plantObject.bombRange + 0.5f));
            Gizmos.DrawRay(currentTarget.transform.position, new Vector2(1.0f, 1.0f) * (plantObject.bombRange + 0.5f));
            Gizmos.DrawRay(currentTarget.transform.position, new Vector2(-1.0f, 1.0f) * (plantObject.bombRange + 0.5f));
            Gizmos.DrawRay(currentTarget.transform.position, new Vector2(1.0f, -1.0f) * (plantObject.bombRange + 0.5f));
            Gizmos.DrawRay(currentTarget.transform.position, new Vector2(-1.0f, -1.0f) * (plantObject.bombRange + 0.5f));
        }
    }
}
