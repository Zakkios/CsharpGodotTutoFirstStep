using Godot;

public partial class Main : Node
{
	private const float MobBaseSpeed = 50f;
	private const float MobSpeedPerPoint = 2f;

	private PackedScene mobScene;

	[Export]
	public PackedScene MobScene
	{
		get => mobScene;
		set
		{
			mobScene = value;
			UpdateMobSpawner();
		}
	}

	public int Score { get; private set; }

	private Hud hud;
	private Player player;
	private Timer scoreTimer;
	private bool isGameRunning;
	private Timer mobTimer;
	private Timer startTimer;
	private Marker2D startPosition;
	private PathFollow2D mobSpawnLocation;
	private AudioStreamPlayer music;
	private AudioStreamPlayer gameOverSound;
	private MobSpawner mobSpawner;

	public override void _Ready()
	{
		CacheNodes();
		UpdateMobSpawner();
		ConnectSignals();
	}

	private void CacheNodes()
	{
		hud = GetNode<Hud>("HUD");
		player = GetNode<Player>("Player");
		scoreTimer = GetNode<Timer>("ScoreTimer");
		mobTimer = GetNode<Timer>("MobTimer");
		startTimer = GetNode<Timer>("StartTimer");
		startPosition = GetNode<Marker2D>("StartPosition");
		mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		music = GetNode<AudioStreamPlayer>("Music");
		gameOverSound = GetNode<AudioStreamPlayer>("GameOverSound");
	}

	private void ConnectSignals()
	{
		hud.StartGame += NewGame;
		player.Hit += GameOver;
		startTimer.Timeout += OnStartTimerTimeout;
		scoreTimer.Timeout += OnScoreTimerTimeout;
		mobTimer.Timeout += OnMobTimerTimeout;
	}

	public void NewGame()
	{
		isGameRunning = true;
		music.Play();
		ResetScore();
		player.Start(startPosition.Position);
		startTimer.Start();
		hud.UpdateScore(Score);
		hud.ShowMessage("Prêt ?");
		GetTree().CreateTimer(1.5).Timeout += () =>
		{
			if (isGameRunning)
			{
				hud.ShowMessage("C'est parti !");
			}
		};
	}

	public async void GameOver()
	{
		isGameRunning = false;
		music.Stop();
		gameOverSound.Play();
		scoreTimer.Stop();
		mobTimer.Stop();
		await hud.ShowGameOver();
	}

	private void ResetScore()
	{
		Score = 0;
	}

	private void UpdateMobSpawner()
	{
		if (mobSpawnLocation == null)
		{
			return;
		}

		mobSpawner = new MobSpawner(mobScene, mobSpawnLocation, MobBaseSpeed, MobSpeedPerPoint);
	}

	private void OnStartTimerTimeout()
	{
		mobTimer.Start();
		scoreTimer.Start();
	}

	private void OnScoreTimerTimeout()
	{
		Score++;
		hud.UpdateScore(Score);
	}

	private void OnMobTimerTimeout()
	{
		SpawnMob();
	}

	private void SpawnMob()
	{
		if (mobSpawner == null)
		{
			GD.PushWarning("MobSpawner is not initialised.");
			return;
		}

		var mob = mobSpawner.Spawn(Score);
		if (mob == null)
		{
			return;
		}

		AddChild(mob);
	}
}
