using Godot;

public static class PlayerInput
{
	public static Vector2 GetDirection()
	{
		return Input.GetVector("move_left", "move_right", "move_up", "move_down");
	}
}
