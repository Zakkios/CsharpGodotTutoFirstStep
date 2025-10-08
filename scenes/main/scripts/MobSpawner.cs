using Godot;
using System;

namespace Survivor;

public sealed class MobSpawner
{
    private readonly PackedScene mobScene;
    private readonly PathFollow2D spawnLocation;
    private readonly float baseSpeed;
    private readonly float speedPerPoint;

    public MobSpawner(PackedScene mobScene, PathFollow2D spawnLocation, float baseSpeed, float speedPerPoint)
    {
        this.mobScene = mobScene;
        this.spawnLocation = spawnLocation ?? throw new ArgumentNullException(nameof(spawnLocation));
        this.baseSpeed = baseSpeed;
        this.speedPerPoint = speedPerPoint;
    }

    public Mob Spawn(int score)
    {
        if (mobScene == null)
        {
            GD.PushWarning("MobScene is not assigned.");
            return null;
        }

        spawnLocation.ProgressRatio = GD.Randf();

        Mob mob = mobScene.Instantiate<Mob>();
        mob.Position = spawnLocation.GlobalPosition;

        float direction = spawnLocation.Rotation + Mathf.Pi / 2f;
        Vector2 velocity = new Vector2(Mathf.Cos(direction), Mathf.Sin(direction));
        mob.Velocity = velocity * (baseSpeed + score * speedPerPoint);

        return mob;
    }
}
