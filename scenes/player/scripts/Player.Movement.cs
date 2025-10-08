using System;
using Godot;

namespace Survivor;

public partial class Player
{
    // private void Move(Vector2 velocity, double delta)
    // {
    //     Position += velocity * (float)delta;
    //     MoveAndSlide();
    //     Position = new Vector2(
    //         Math.Clamp(Position.X, 0, screenSize.X),
    //         Math.Clamp(Position.Y, 0, screenSize.Y)
    //     );
    // }

    private void Move(Vector2 velocity)
    {
        Velocity = velocity;  // ← Définis la vélocité du CharacterBody2D
        MoveAndSlide();       // ← Applique le mouvement avec collisions

        // Clamp pour rester dans l'écran
        Position = new Vector2(
            Mathf.Clamp(Position.X, 0, screenSize.X),
            Mathf.Clamp(Position.Y, 0, screenSize.Y)
        );
    }
}
