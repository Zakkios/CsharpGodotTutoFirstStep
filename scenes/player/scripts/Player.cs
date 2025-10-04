using Godot;
using System;

public partial class Player : Area2D
{
	[Export(PropertyHint.Range, "50,300")]
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
		CacheNodes();
		BodyEntered += OnBodyEntered;
	}

	public override void _Process(double delta)
	{
		Vector2 direction = PlayerInput.GetDirection();
		if (direction != Vector2.Zero)
			direction = direction.Normalized();

		UpdateAnimation(direction);
		Move(direction * Speed, delta);
	}

	private void CacheNodes()
	{
		var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		animationController = new PlayerAnimationController(sprite);
	}

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

	private void Move(Vector2 velocity, double delta)
	{
		Position += velocity * (float)delta;

		Position = new Vector2(
			Math.Clamp(Position.X, 0, screenSize.X),
			Math.Clamp(Position.Y, 0, screenSize.Y)
		);
	}

	private void OnBodyEntered(Node2D body)
	{
		Hide();
		EmitSignal(SignalName.Hit);
		collision.SetDeferred("disabled", true);
	}

	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		collision.Disabled = false;
	}
}
