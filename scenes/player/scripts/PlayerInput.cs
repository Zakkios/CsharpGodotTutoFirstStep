using Godot;

public static class PlayerInput
{
	public static Vector2 GetDirection()
	{
		Vector2 dir = Vector2.Zero;

		if (Input.IsActionPressed("move_right")) dir.X += 1;
		if (Input.IsActionPressed("move_left")) dir.X -= 1;
		if (Input.IsActionPressed("move_down")) dir.Y += 1;
		if (Input.IsActionPressed("move_up")) dir.Y -= 1;

		return dir.Normalized();
	}
}
