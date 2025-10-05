using Godot;

namespace Survivor;

public partial class Main
{
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

    private void UpdateMobSpawner()
    {
        if (mobSpawnLocation == null)
        {
            return;
        }

        mobSpawner = new MobSpawner(mobScene, mobSpawnLocation, MobBaseSpeed, MobSpeedPerPoint);
    }
}
