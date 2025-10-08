using System;
using Godot;

namespace Survivor;

public partial class Player
{
    private void Move(Vector2 velocity)
    {
        Velocity = velocity;
        MoveAndSlide();

        Position = new Vector2(
            Mathf.Clamp(Position.X, 0, screenSize.X),
            Mathf.Clamp(Position.Y, 0, screenSize.Y)
        );
    }
}
