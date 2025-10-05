using Godot;

namespace Survivor;

public partial class Player
{
    private void UpdateAnimation(Vector2 direction)
    {
        if (direction == Vector2.Zero)
        {
            animationController.PlayIdle(lastDirection);
            return;
        }

        lastDirection = direction;
        animationController.PlayWalk(direction);
    }
}
