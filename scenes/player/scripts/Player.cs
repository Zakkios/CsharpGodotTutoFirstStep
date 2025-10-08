using Godot;

namespace Survivor;

public partial class Player : CharacterBody2D
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
	}

	public override void _Process(double delta)
	{
		Vector2 direction = PlayerInput.GetDirection();
		if (direction != Vector2.Zero)
		{
			direction = direction.Normalized();
		}

		UpdateAnimation(direction);
		Move(direction * Speed);  // ← Move() appellera MoveAndSlide()
	}

	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		collision.Disabled = false;
	}
}
