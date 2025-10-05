using Godot;

namespace Survivor;

public partial class Player
{
    private void CacheNodes()
    {
        AnimatedSprite2D sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        collision = GetNode<CollisionShape2D>("CollisionShape2D");
        animationController = new PlayerAnimationController(sprite);
    }

    private void OnBodyEntered(Node2D body)
    {
        Hide();
        EmitSignal(SignalName.Hit);
        collision.SetDeferred("disabled", true);
    }
}
