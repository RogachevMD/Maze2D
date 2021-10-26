using UnityEngine;

[System.Serializable]
public class RenderData
{
    public RenderData(Vector3 position, Vector3 scale, int size, EnumCellType type)
    {
        _position = position;
        _scale = scale;
        _size = size;
        _type = type;
    }

    private Vector3 _position;
    private Vector3 _scale;
    private int _size;
    private EnumCellType _type;

    public Vector3 Position
    {
        get => _position;
    }

    public Vector3 Scale
    {
        get => _scale;
    }

    public int Size
    {
        get => _size;
    }

    public EnumCellType Type
    {
        get => _type;
    }
}
