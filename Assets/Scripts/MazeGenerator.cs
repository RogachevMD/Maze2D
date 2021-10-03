using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private int widthX = 15;
    [SerializeField]
    private int heightY = 15;
    [SerializeField]
    private float scale = 0.1f;
    private int jumpLenght = 2;

    public Transform pfWall;
    public Transform pfFloor;
    public Transform pfStart;
    public Transform pfFinish;

    private Maze _maze;
    Vector3 gtstart;

    void Start()
    {
        _maze = new Maze(widthX, heightY, 2);
        _maze.Init();
        ShowMaze(_maze);
    }



    private void ShowMaze(Maze maze)
    {
        foreach (RenderData data in maze.GetRenderData())
        {
            if (data.Type == EnumCellType.Wall)
            {
                pfWall.localScale = data.Scale * scale;
                Instantiate(pfWall, data.Position * scale, Quaternion.identity, transform);
            }
            if (data.Type == EnumCellType.Floor)
            {
                pfFloor.localScale = data.Scale * scale;
                Instantiate(pfFloor, data.Position * scale, Quaternion.identity, transform);
            }
            if (data.Type == EnumCellType.Enter)
            {
                pfStart.localScale = data.Scale * scale * 0.5f;
                Instantiate(pfStart, data.Position * scale, Quaternion.identity, transform);
                gtstart = data.Position * scale;

            }
            if (data.Type == EnumCellType.Exit)
            {
                pfFinish.localScale = data.Scale * scale * 0.5f;
                Instantiate(pfFinish, data.Position * scale, Quaternion.identity, transform);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gtstart, ((widthX + heightY) / 4) * scale);
    }
}
