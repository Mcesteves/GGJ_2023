using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap gnomesPath;
    public List<Vector2> path;
    public Vector3 pathStart;
    public List<HordeObject> hordes;
    public List<GameObject> gnomePrefabs;
    public float hordesRestTime;
    public GameObject winnerCanvas;
    [HideInInspector]
    public int totalGnomes;
    [HideInInspector]
    public int currentHorde;

    private Dictionary<int, int> gnomeTotals;
    private bool onGnomeRest;
    private bool onHordeRest;
    private bool isOver;
    private void Awake()
    {
        BuildPath();
    }

    private void Start()
    {
        gnomeTotals = new Dictionary<int, int>();
        gnomeTotals.Add(0, hordes[0].jorgeQtd);
        gnomeTotals.Add(1,hordes[0].otaviaQtd);
        gnomeTotals.Add(2, hordes[0].garibaldisQtd);
    }

    private void FixedUpdate()
    {
        if (!onHordeRest && !isOver)
        {
            if (!onGnomeRest)
                StartCoroutine(StartHorde());
        }
        else if(!isOver)
        {
            if(currentHorde > hordes.Count - 1)
            {
                if (totalGnomes == 0)
                {
                    isOver = true;
                    WinGame();
                }    
            }
        }
    }

    public bool CheckDirection(Vector3 position)
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        var pos = Vector3Int.FloorToInt(position);
        foreach (var dir in directions)
        {
            var next = new Vector3Int(pos.x, pos.y, 0) + new Vector3Int(dir.x, dir.y, 0);
            if (gnomesPath.GetTile(next) != null)
            {
                var nextpos = new Vector3(next.x + 0.5f, next.y + 0.5f, 0f);
                if (!path.Contains(nextpos))
                {
                    path.Add(nextpos);
                    return true;
                }
            }
        }
        return false;
    }

    public void BuildPath()
    {
        int i = 1;
        var pos = pathStart;
        path.Add(pos);
        while (CheckDirection(pos))
        {
            pos = new Vector3(path[i].x, path[i].y, 0f);
            i++;
        }
    }

    public Vector3 GetPathPosition(int index)
    {
        return path[index];
    }

    private IEnumerator StartHorde()
    {
        int gnomeType = Random.Range(0, 3);
        if(gnomeTotals[gnomeType] != 0)
        {
            Instantiate(gnomePrefabs[gnomeType], pathStart, Quaternion.identity);
            gnomeTotals[gnomeType] -= 1;
            totalGnomes++;
        }
        onGnomeRest = true;
        yield return new WaitForSeconds(0.5f);
        onGnomeRest = false;
        if (gnomeTotals[0] + gnomeTotals[1] + gnomeTotals[2] == 0)
        {
            onHordeRest = true;
            yield return new WaitForSeconds(hordesRestTime);
            currentHorde++;
            if (currentHorde < hordes.Count)
            {
                SetHordeQtds(hordes[currentHorde]);
            }
            onHordeRest = false;
        }
            
    }
    private void SetHordeQtds(HordeObject horde)
    {
        gnomeTotals[0] =  horde.jorgeQtd;
        gnomeTotals[1] = horde.otaviaQtd;
        gnomeTotals[2] = horde.garibaldisQtd;
    }
    private void WinGame()
    {
        winnerCanvas.SetActive(true);
        AudioManager.instance.Stop("Battle");
        AudioManager.instance.Play("win");
    }
}
