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
		// Crée un mob à partir de la scène préfabriquée
		var mob = (Mob)MobScene.Instantiate();

		// Choisit un point aléatoire sur le chemin
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Calcule la direction de sortie (rotation du PathFollow2D + 90°)
		var direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Place le mob à cet endroit
		mob.Position = mobSpawnLocation.GlobalPosition;

		// Donne une direction au mob
		var velocity = new Vector2((float)Math.Cos(direction), (float)Math.Sin(direction));

		// Multiplie par une vitesse aléatoire, dépendant du score
		velocity *= (150 + Score * 10);

		// Applique la vélocité au mob
		mob.LinearVelocity = velocity;

		// Ajoute le mob dans la scène
		AddChild(mob);
	}

}
