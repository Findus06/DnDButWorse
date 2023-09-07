using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridS : MonoBehaviour
{
    Node[,] grid;
    [SerializeField] int width = 25;
    [SerializeField] int length = 25;
    [SerializeField] float cellSize = 1f;

    void Start()
    {
        grid = new Node[width, length];
    }

    void OnDrawGizmos()
    {
        for(int y = 0; y < width; y++)
        {
            for(int x = 0; x < length; x++)
            {
                Vector3 pos = new Vector3(transform.position.x + (x * cellSize), 0f,transform.position.z + (y * cellSize));
                Gizmos.DrawCube(pos, Vector3.one/4);
            }
        }
    }
}
