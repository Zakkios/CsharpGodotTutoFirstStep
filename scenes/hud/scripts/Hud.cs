using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace Survivor;

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
		CacheNodes();
		BindSignals();
		InitializeDefaultText();
		RefreshHighScores();
	}

	public void ShowMessage(string message)
	{
		messageLabel.Text = message;
		messageLabel.Show();
		messageTimer.Start();
	}

	public void ShowHighScores(IReadOnlyList<int> scores)
	{
		highScoresLabel.Text = HighScoreFormatter.Format(scores, ScoreManager.MaxScores);
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
		startButton.Text = "Reessayer";
		startButton.Show();
	}

	public void UpdateScore(int score)
	{
		scoreLabel.Text = $"Score: {score}";
	}

	private void CacheNodes()
	{
		scoreLabel = GetNode<Label>("ScoreLabel");
		messageLabel = GetNode<Label>("MessageLabel");
		highScoresLabel = GetNode<Label>("HighScoresLabel");
		messageTimer = GetNode<Timer>("MessageTimer");
		startButton = GetNode<Button>("StartButton");
	}

	private void BindSignals()
	{
		startButton.Pressed += OnStartButtonPressed;
		messageTimer.Timeout += HideMessage;
	}

	private void InitializeDefaultText()
	{
		messageLabel.Text = "Esquive les monstres !";
		scoreLabel.Text = "Score: 0";
	}

	private void RefreshHighScores()
	{
		IReadOnlyList<int> scores = ScoreManager.Instance?.Scores ?? Array.Empty<int>();
		ShowHighScores(scores);
	}

	private void OnStartButtonPressed()
	{
		startButton.Hide();
		messageLabel.Hide();
		EmitSignal(SignalName.StartGame);
	}

	private void HideMessage()
	{
		messageLabel.Hide();
	}
}
