using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    [SerializeField] GameObject WallPrefab;
    [SerializeField] GameObject arena;
    [SerializeField] int maxCount;
    List<GameObject> wall;

    public void GenerateNewMaze()
    {
        if (wall == null)
            wall = new List<GameObject>();
        foreach (GameObject j in wall)
            Destroy(j);
        wall.Clear();
        for(int i = 0; i < maxCount; i++)
        {
            wall.Add(Instantiate(WallPrefab, new Vector3(Random.Range(0, 10*arena.transform.localScale.x-1.5f)+0.5f, 0, Random.Range(0,10*arena.transform.localScale.z-1.5f)+0.5f), Quaternion.identity));
        }
    }
}
