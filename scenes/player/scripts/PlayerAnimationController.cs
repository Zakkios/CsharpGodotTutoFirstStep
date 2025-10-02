using Godot;

public class PlayerAnimationController
{
	private AnimatedSprite2D sprite;

	public enum AnimState { IdleDown, IdleSide, IdleUp, Walk, WalkUp }

	public PlayerAnimationController(AnimatedSprite2D spriteNode)
	{
		sprite = spriteNode;
	}

	public void PlayWalk(Vector2 dir)
	{
		if (dir.X != 0)
		{
			sprite.Play("walk_side");
			sprite.FlipH = dir.X < 0;
		}
		else if (dir.Y < 0)
		{
			sprite.Play("walk_up");
		}
		else if (dir.Y > 0)
		{
			sprite.Play("walk_down");
		}
	}

	public void PlayIdle(Vector2 lastDir)
	{
		if (lastDir.X > 0)
		{
			sprite.Play("idle_side");
			sprite.FlipH = false;
		}
		else if (lastDir.X < 0)
		{
			sprite.Play("idle_side");
			sprite.FlipH = true;
		}
		else if (lastDir.Y < 0)
		{
			sprite.Play("idle_up");
		}
		else
		{
			sprite.Play("idle_down");
		}
	}
}
