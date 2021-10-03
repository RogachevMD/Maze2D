

using UnityEngine;

public class Cell
{
    private int _x;
    private int _y;
    public int X { get => _x; }
    public int Y { get => _y; }
    public bool Visited = false;
    public EnumCellType Type;

    public Cell(int x, int y, EnumCellType type = EnumCellType.Wall)
    {
        this._x = x;
        this._y = y;
        this.Type = type;
    }
    public void PrintStats()
    {
        Debug.Log($"Position: [{X},{Y}]\nType: [{Type}]\nIsVisited: [{Visited}]");
    }
}
