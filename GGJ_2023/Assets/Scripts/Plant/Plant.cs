using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public List<GameObject> enemies;
    public PlantObject plantObject;
    [SerializeField]
    private GameObject bulletPrefab;
    private bool onRest;
    private GameObject currentTarget;
    private List<GameObject> bombTargets;
    private Animator anim;
    private SpriteRenderer sr;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = plantObject.sprite;
        GetComponent<BoxCollider2D>().size = (2*plantObject.range + 1)*Vector2.one;
        enemies = new List<GameObject>();
        bombTargets = new List<GameObject>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
            
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
        anim.SetBool("attacking", true);
        Flip(currentTarget.transform.position, bulletPrefab);
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
	    if (currentTarget != null){
	    currentTarget.GetComponent<Gnome>().TakeDamage(plantObject.damage);
	    }
            
            yield return new WaitForSeconds(plantObject.rechargeTime - time);
            onRest = false;
            
        }
        else
        {
            yield return new WaitForSeconds(plantObject.rechargeTime);
            onRest = false;
            yield return new WaitForSeconds(time - plantObject.rechargeTime);
            Destroy(bullet);
	    if (currentTarget != null){
	    currentTarget.GetComponent<Gnome>().TakeDamage(plantObject.damage);
	    }
        }
        anim.SetBool("attacking", false);
    }

    private IEnumerator AuraAttack()
    {
        anim.SetBool("attacking", true);
        foreach ( GameObject enemy in enemies)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var direction = (enemy.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * plantObject.bulletSpeed;
            StartCoroutine(DestroyBullet(bullet, direction, enemy));
        }
        onRest = true;
        yield return new WaitForSeconds(plantObject.rechargeTime);
        onRest = false;
        anim.SetBool("attacking", false);
    }

    private IEnumerator DestroyBullet(GameObject bullet, Vector2 direction, GameObject enemy = null)
    {
        var time = (currentTarget.transform.position - transform.position).magnitude / (direction * plantObject.bulletSpeed).magnitude;
        yield return new WaitForSeconds(time);
        Destroy(bullet);
        if (enemy != null)
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
        anim.SetBool("attacking", true);
        Flip(currentTarget.transform.position, bulletPrefab);
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var direction = (currentTarget.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * plantObject.bulletSpeed;
        CheckPositions(currentTarget);
        StartCoroutine(DestroyBomb(bullet, direction));
        onRest = true;
        yield return new WaitForSeconds(plantObject.rechargeTime);
        onRest = false;
        anim.SetBool("attacking", false);
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

    private void Flip(Vector3 target, GameObject gameObject)
    {
        if (transform.position.x < target.x)
        {
            sr.flipX = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x > target.x)
        {
            sr.flipX = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
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
