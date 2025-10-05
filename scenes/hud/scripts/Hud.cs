using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Hud : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	private Label scoreLabel;
	private Label messageLabel;
	private Label highScoresLabel;
	private Timer messageTimer;
	private Button startButton;

	public override void _Ready()
	{
		scoreLabel = GetNode<Label>("ScoreLabel");
		messageLabel = GetNode<Label>("MessageLabel");
		highScoresLabel = GetNode<Label>("HighScoresLabel");
		messageTimer = GetNode<Timer>("MessageTimer");
		startButton = GetNode<Button>("StartButton");

		startButton.Pressed += OnStartButtonPressed;
		messageTimer.Timeout += () => messageLabel.Hide();

		messageLabel.Text = "Esquive les monstres !";
		scoreLabel.Text = "Score: 0";
		ShowHighScores(ScoreManager.Instance.Scores);
	}

	public void ShowMessage(string message)
	{
		messageLabel.Text = message;
		messageLabel.Show();
		messageTimer.Start();
	}

	public void ShowHighScores(List<int> scores)
	{
		string text = "🏆 Records :\n";
		if (scores.Count == 0)
		{
			text += "Aucun score";
			highScoresLabel.Text = text;
			return;
		}
		for (int i = 0; i < scores.Count && i < ScoreManager.MaxScores; i++)
		{
			switch (i)
			{
				case 0:
					text += "🥇 ";
					break;
				case 1:
					text += "🥈 ";
					break;
				case 2:
					text += "🥉 ";
					break;
				default:
					text += $"{i + 1}. ";
					break;
			}
			text += $"{scores[i]}\n";
		}
		highScoresLabel.Text = text;
		highScoresLabel.Show();
	}

	public void HideHighScores()
	{
		highScoresLabel.Hide();
	}

	public async Task ShowGameOver()
	{
		if (!messageTimer.IsStopped())
		{
			messageTimer.Stop();
		}
		messageLabel.Text = "Game over !\nScore : " + scoreLabel.Text.Split(": ")[1];
		messageLabel.Show();

		await ToSignal(GetTree().CreateTimer(2.0), SceneTreeTimer.SignalName.Timeout);
		startButton.Text = "Réessayer";
		startButton.Show();
	}

	public void UpdateScore(int score)
	{
		scoreLabel.Text = $"Score: {score}";
	}

	private void OnStartButtonPressed()
	{
		startButton.Hide();
		messageLabel.Hide();
		EmitSignal(SignalName.StartGame);
	}
}
