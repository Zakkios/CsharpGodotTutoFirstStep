namespace Survivor;

public struct ObstacleData
{
    public string Name;
    public int X;
    public int Y;
    public int Width;
    public int Height;
    public bool HasCollision;
    public float SlowFactor;

    public ObstacleData(string name = "Obstacle", int x = 16, int y = 16, int width = 16, int height = 16, bool hasCollision = true, float slowFactor = 1.0f)
    {
        Name = name;
        X = x;
        Y = y;
        Width = width;
        Height = height;
        HasCollision = hasCollision;
        SlowFactor = slowFactor;
    }
}