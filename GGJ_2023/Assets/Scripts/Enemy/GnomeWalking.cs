using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GnomeWalking : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap gnomesPath;
    private Gnome gnome;
    private int stepCount = 1;
    private bool canMove;
    public GameManager gameManager;
    void Start()
    {
        gnome = GetComponent<Gnome>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(gameManager.path.Count);
        if(stepCount != gameManager.path.Count && canMove)
        {
            Move();
        }

    }
    public void Move()
    {
        var step = gnome.gnomeObject.speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, gameManager.GetPathPosition(stepCount), step);
        if (Vector3.Distance(transform.position, gameManager.GetPathPosition(stepCount)) < 0.001f)
        {
            stepCount++;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        canMove = true;
    }
}
