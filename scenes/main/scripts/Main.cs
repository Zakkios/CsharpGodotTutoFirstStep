using Godot;
using System;


public partial class Main : Node
{
	[Export]
	public PackedScene MobScene;
	public int Score = 0;

	private Player player;
	private Timer scoreTimer;
	private Timer mobTimer;
	private Timer startTimer;
	private Marker2D startPosition;

	public override void _Ready()
	{
		player = GetNode<Player>("Player");
		scoreTimer = GetNode<Timer>("ScoreTimer");
		mobTimer = GetNode<Timer>("MobTimer");
		startTimer = GetNode<Timer>("StartTimer");
		startPosition = GetNode<Marker2D>("StartPosition");
		player.Hit += GameOver;
		startTimer.Timeout += StartTimerTimeout;
		scoreTimer.Timeout += ScoreTimerTimeout;
		mobTimer.Timeout += MobTimerTimeout;

		NewGame();
	}

	public void GameOver()
	{
		scoreTimer.Stop();
		mobTimer.Stop();
	}

	public void NewGame()
	{
		Score = 0;
		player.Start(startPosition.Position);
		startTimer.Start();
	}

	private void StartTimerTimeout()
	{
		mobTimer.Start();
		scoreTimer.Start();
	}

	private void ScoreTimerTimeout()
	{
		Score++;
	}

	private void MobTimerTimeout()
	{
		var mob = (Mob)MobScene.Instantiate();
		var mobSpawnLocation = GetNode<PathFollow2D>("MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();
		var direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
		mob.Position = mobSpawnLocation.Position;
		var velocity = new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction)) * (150 + Score * 10);
		mob.LinearVelocity = velocity.Rotated(direction);
		AddChild(mob);
	}
}
