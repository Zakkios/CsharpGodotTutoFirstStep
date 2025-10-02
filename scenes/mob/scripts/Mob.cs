using Godot;
using System;

public partial class Mob : RigidBody2D
{
	private AnimatedSprite2D sprite;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var mobTypes = sprite.SpriteFrames.GetAnimationNames();
		sprite.Play(mobTypes[GD.Randi() % mobTypes.Length]);

		var notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
		notifier.ScreenEntered += OnScreenExited;
	}

	private void OnScreenExited()
	{
		QueueFree();
	}

	public override void _Process(double delta)
	{

	}
}
