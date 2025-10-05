using Godot;

namespace Survivor;

public partial class Main : Node
{
	private const float MobBaseSpeed = 50f;
	private const float MobSpeedPerPoint = 2f;

	private PackedScene mobScene;
	private Hud hud;
	private Player player;
	private Timer scoreTimer;
	private Timer mobTimer;
	private Timer startTimer;
	private Marker2D startPosition;
	private PathFollow2D mobSpawnLocation;
	private AudioStreamPlayer music;
	private AudioStreamPlayer gameOverSound;
	private MobSpawner mobSpawner;
	private bool isGameRunning;
	private bool isGameOver;

	public int Score { get; private set; }

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

	public override void _Ready()
	{
		CacheNodes();
		UpdateMobSpawner();
		ConnectSignals();
	}
}
