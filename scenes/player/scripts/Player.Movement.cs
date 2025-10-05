using System;
using Godot;

namespace Survivor;

public partial class Player
{
    private void Move(Vector2 velocity, double delta)
    {
        Position += velocity * (float)delta;
        Position = new Vector2(
            Math.Clamp(Position.X, 0, screenSize.X),
            Math.Clamp(Position.Y, 0, screenSize.Y)
        );
    }
}
