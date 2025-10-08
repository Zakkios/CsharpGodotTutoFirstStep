using Godot;

namespace Survivor;

public partial class Mob : CharacterBody2D
{
	private AnimatedSprite2D sprite;
	private VisibleOnScreenNotifier2D notifier;

	public override void _Ready()
	{
		CacheNodes();
		PlayRandomAnimation();
		notifier.ScreenExited += OnScreenExited;
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	private void CacheNodes()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
	}

	private void PlayRandomAnimation()
	{
		string[] animationNames = sprite.SpriteFrames.GetAnimationNames();
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
