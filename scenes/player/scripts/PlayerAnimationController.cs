using Godot;
using System;

namespace Survivor;

public sealed class PlayerAnimationController
{
	private readonly AnimatedSprite2D sprite;

	public PlayerAnimationController(AnimatedSprite2D spriteNode)
	{
		sprite = spriteNode ?? throw new ArgumentNullException(nameof(spriteNode));
	}

	public void PlayWalk(Vector2 direction)
	{
		if (direction.X != 0)
		{
			sprite.Play("walk_side");
			sprite.FlipH = direction.X < 0;
			return;
		}

		sprite.Play(direction.Y < 0 ? "walk_up" : "walk_down");
	}

	public void PlayIdle(Vector2 lastDirection)
	{
		if (lastDirection.X != 0)
		{
			sprite.Play("idle_side");
			sprite.FlipH = lastDirection.X < 0;
			return;
		}

		sprite.Play(lastDirection.Y < 0 ? "idle_up" : "idle_down");
	}
}
