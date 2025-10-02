using Godot;

public partial class Mob : RigidBody2D
{
	private AnimatedSprite2D sprite;
	private VisibleOnScreenNotifier2D notifier;

	public override void _Ready()
	{
		CacheNodes();
		PlayRandomAnimation();
		notifier.ScreenExited += OnScreenExited;
	}

	private void CacheNodes()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
	}

	private void PlayRandomAnimation()
	{
		var animationNames = sprite.SpriteFrames.GetAnimationNames();
		if (animationNames.Length == 0)
		{
			return;
		}

		sprite.Play(animationNames[GD.Randi() % animationNames.Length]);
	}

	private void OnScreenExited()
	{
		QueueFree();
	}
}
