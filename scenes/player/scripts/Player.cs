using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed = 400;

	[Signal]
	public delegate void HitEventHandler();

	private Vector2 screenSize;
	private Vector2 lastDirection = Vector2.Down;

	private PlayerAnimationController animationController;
	private CollisionShape2D collision;

	public override void _Ready()
	{
		Hide();
		screenSize = GetViewportRect().Size;
		var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		animationController = new PlayerAnimationController(sprite);
		BodyEntered += OnBodyEntered;
	}

	public override void _Process(double delta)
	{
		Vector2 dir = PlayerInput.GetDirection();
		Vector2 velocity = dir * Speed;

		if (dir != Vector2.Zero)
		{
			lastDirection = dir;
			animationController.PlayWalk(dir);
		}
		else
		{
			animationController.PlayIdle(lastDirection);
		}

		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Math.Clamp(Position.X, 0, screenSize.X),
			y: Math.Clamp(Position.Y, 0, screenSize.Y)
		);
	}

	private void OnBodyEntered(Node2D body)
	{
		Hide();
		EmitSignal(SignalName.Hit);
		collision.Disabled = true;
	}

	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		collision.Disabled = false;
	}
}
