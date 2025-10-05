using Godot;

namespace Survivor;

public partial class Main
{
    public void NewGame()
    {
        isGameRunning = true;
        isGameOver = false;
        music.Play();
        ResetScore();
        player.Start(startPosition.Position);
        startTimer.Start();
        hud.HideHighScores();
        hud.UpdateScore(Score);
        hud.ShowMessage("Pret ?");

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
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        isGameRunning = false;
        music.Stop();
        gameOverSound.Play();
        scoreTimer.Stop();
        mobTimer.Stop();
        await hud.ShowGameOver();
        ScoreManager.Instance.AddScore(Score);
        hud.ShowHighScores(ScoreManager.Instance.Scores);
    }

    private void ResetScore()
    {
        Score = 0;
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

        Mob mob = mobSpawner.Spawn(Score);
        if (mob == null)
        {
            return;
        }

        AddChild(mob);
    }
}
