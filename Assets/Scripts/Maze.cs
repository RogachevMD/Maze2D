using System;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    private int _widthX;
    private int _heightY;
    private int _jumpSpace = 2;
    private Cell[,] _mazeMap;
    private Stack<Cell> _routeMap;
    private int _unvisitedCell;

    public Maze(int widthX, int heightY, int jumpSpace)
    {
        _widthX = widthX;
        _heightY = heightY;
        _jumpSpace = jumpSpace;
    }

    public void Init()
    {
        PrepareMap();
        PrepareMaze();
        //PrintTextMap();
    }

    private void PrepareMap()
    {
        _mazeMap = new Cell[_widthX, _heightY];
        _unvisitedCell = 0;
        for (int x = 0; x < _widthX; x++)
        {
            for (int y = 0; y < _heightY; y++)
            {
                if ((x % 2 != 0 && y % 2 != 0) && (x < _widthX - 1 && y < _heightY - 1))
                {
                    _mazeMap[x, y] = new Cell(x, y, EnumCellType.Floor);
                    _unvisitedCell++;
                }
                else
                {
                    _mazeMap[x, y] = new Cell(x, y, EnumCellType.Wall);
                }
            }
        }
        //Debug.Log($"Unvisited: {_unvisitedCell}");
        //PrintTextMap();
    }

    private void PrepareMaze()
    {
        Cell currentCell = _mazeMap[1, 1];
        Cell previousCell = currentCell;

        currentCell.Visited = true;
        _unvisitedCell--;

        _routeMap = new Stack<Cell>();
        _routeMap.Push(previousCell);

        while (_unvisitedCell > 0)
        {
            Cell targetCell = GetRandomUnvisitedNeighbour(currentCell, _jumpSpace);

            if (targetCell == null)
            {
                if (_routeMap.Count > 0 && _unvisitedCell > 0)
                {
                    currentCell = _routeMap.Peek();
                    _routeMap.Pop();
                }
                else
                {
                    _routeMap.Pop();
                }
            }
            else
            {
                RemoveWallBetweenNeighbours(currentCell, targetCell);
                previousCell = currentCell;
                currentCell = targetCell;

                currentCell.Visited = true;
                _unvisitedCell--;

                _routeMap.Push(previousCell);
            }
        }
    }

    private void PrintTextMap()
    {
        string map = "";
        for (int y = _heightY - 1; y >= 0; y--)
        {
            for (int x = 0; x < _widthX; x++)
            {
                if (_mazeMap[x, y].Type == EnumCellType.Wall)
                {
                    map += "░";
                }
                else
                {
                    map += "▓";
                }
            }
            map += "\n";
        }
        Debug.Log($"Current map:\n{map}");
    }

    private Cell GetRandomUnvisitedNeighbour(Cell c, int distance)
    {
        int x = c.X;
        int y = c.Y;

        Cell up = new Cell(c.X, c.Y + distance);
        Cell rt = new Cell(c.X + distance, c.Y);
        Cell dw = new Cell(c.X, c.Y - distance);
        Cell lt = new Cell(c.X - distance, c.Y);

        Cell[] potentialNeighbours = { dw, rt, up, lt };

        List<Cell> neighbours = new List<Cell>();

        //проходим по всем потенциальным соседям
        for (int i = 0; i < 4; i++)
        {
            //если не выходят за границы
            if (potentialNeighbours[i].X > 0 && potentialNeighbours[i].X < _widthX && potentialNeighbours[i].Y > 0 && potentialNeighbours[i].Y < _heightY)
            {
                Cell currentNeighbour = _mazeMap[potentialNeighbours[i].X, potentialNeighbours[i].Y];
                if (currentNeighbour.Type != EnumCellType.Wall && currentNeighbour.Visited == false)
                {
                    neighbours.Add(currentNeighbour);
                }
            }
        }
        if (neighbours.Count > 0)
        {
            //Debug.Log($"Cell [{c.X},{c.Y}]: unvisited neighbours: {neighbours.Count}");
            return neighbours[UnityEngine.Random.Range(0, neighbours.Count)];
        }
        else
        {
            //Debug.Log($"Cell [{c.X},{c.Y}]: unvisited neighbours: {neighbours.Count}");
            return null;
        }

    }

    private void RemoveWallBetweenNeighbours(Cell first, Cell second)
    {
        string log = $"First cell: [{first.X},{first.Y}]\nSecond cell: [{second.X},{second.Y}]\n";

        int xDiff = second.X - first.X;
        int yDiff = second.Y - first.Y;
        int addX = (xDiff != 0) ? (xDiff / Math.Abs(xDiff)) : 0;
        int addY = (yDiff != 0) ? (yDiff / Math.Abs(yDiff)) : 0;

        Cell target = new Cell(first.X + addX, first.Y + addY);
        //Debug.Log(log + $"Wall removed: [{target.X},{target.Y}]");

        _mazeMap[target.X, target.Y].Type = EnumCellType.Floor;
        //PrintTextMap();
    }

    public List<RenderData> GetRenderData()
    {
        List<RenderData> tmp = new List<RenderData>();

        int size = 0;
        int sPos = 0;
        int ePos = 0;
        bool lineEnded = true;

        for (int y = 0; y < _heightY; y++)
        {
            for (int x = 0; x < _widthX; x++)
            {
                if (_mazeMap[x, y].Type == EnumCellType.Wall)
                {
                    if (size == 0)
                    {
                        sPos = x;
                    }
                    size++;
                    lineEnded = false;
                    ePos = x;
                }
                else
                {
                    lineEnded = true;
                }

                if (x == _widthX - 1)
                {
                    lineEnded = true;
                }

                if (lineEnded && size > 0)
                {
                    tmp.Add(new RenderData(new Vector3((sPos + ePos) / 2, y, 0), new Vector3(size, 1, 1), size, EnumCellType.Wall));
                    size = 0;
                }
            }
        }
        for (int x = 0; x < _widthX; x++)
        {
            for (int y = 0; y < _heightY; y++)
            {
                if (_mazeMap[x, y].Type == EnumCellType.Wall)
                {
                    if (size == 0)
                    {
                        sPos = y;
                    }
                    size++;
                    lineEnded = false;
                    ePos = y;
                }
                else
                {
                    lineEnded = true;
                }

                if (y == _heightY - 1)
                {
                    lineEnded = true;
                }

                if (lineEnded && size > 0)
                {
                    if (size > 1)
                        tmp.Add(new RenderData(new Vector3(x, (sPos + ePos) / 2, 0), new Vector3(1, size, 1), size, EnumCellType.Wall));
                    size = 0;
                }
            }
        }

        List<RenderData> data = new List<RenderData>();

        //foreach (RenderData line in tmp)
        //{
        //    if (line.Size > 1)
        //    {
        //        data.Add(line);
        //    }
        //}

        double random1X = UnityEngine.Random.Range(0, _widthX / 2) * 2 + 1;
        double random1Y = UnityEngine.Random.Range(0, _heightY / 2) * 2 + 1;
        tmp.Add(new RenderData(new Vector3((float)random1X, (float)random1Y, 1), new Vector3(1, 1, 1), 1, EnumCellType.Enter));

        bool gotit = false;
        do
        {
            double random2X = UnityEngine.Random.Range(0, _widthX / 2) * 2 + +1;
            double random2Y = UnityEngine.Random.Range(0, _heightY / 2) * 2 + +1;
            if (Vector2.Distance(new Vector2((float)random1X, (float)random1Y), new Vector2((float)random2X, (float)random2Y)) > (_heightY + _widthX) / 4f)
            {
                tmp.Add(new RenderData(new Vector3((float)random2X, (float)random2Y, 1), new Vector3(1, 1, 1), 1, EnumCellType.Exit));
                gotit = true;
            }

        } while (!gotit);
        return tmp;
    }
}
