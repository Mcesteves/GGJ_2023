using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GnomeWalking : MonoBehaviour
{
    private Gnome gnome;
    private int stepCount = 1;
    private bool canMove;
    private GameManager gameManager;
    void Start()
    {
        gnome = GetComponent<Gnome>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(Wait());
    }

    void FixedUpdate()
    {
        if(stepCount != gameManager.path.Count && canMove)
            Move();

    }
    public void Move()
    {
        var step = gnome.gnomeObject.speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, gameManager.GetPathPosition(stepCount), step);
        if (Vector3.Distance(transform.position, gameManager.GetPathPosition(stepCount)) < 0.001f)
            stepCount++;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        canMove = true;
    }

    private void OnDestroy()
    {
        gameManager.totalGnomes--;
    }
}