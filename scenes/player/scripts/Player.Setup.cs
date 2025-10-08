using Godot;

namespace Survivor;

public partial class Player
{
    private void CacheNodes()
    {
        AnimatedSprite2D sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        collision = GetNode<CollisionShape2D>("CollisionShape2D");
        Area2D hitBox = GetNode<Area2D>("Hitbox");
        animationController = new PlayerAnimationController(sprite);

        hitBox.BodyEntered += OnEnemyEntered;
    }

    private void OnEnemyEntered(Node2D body)
    {
        if (body.IsInGroup("Enemy"))
        {
            Hide();
            EmitSignal(SignalName.Hit);
            collision.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        }
    }
}
