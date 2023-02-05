using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap gnomesPath;
    public List<Vector2> path;
    public Vector3 pathStart;

    private void Awake()
    {
        BuildPath();
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
}
