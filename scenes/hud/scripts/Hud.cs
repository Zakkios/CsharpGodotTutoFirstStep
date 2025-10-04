using Godot;
using System.Threading.Tasks;

public partial class Hud : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	private Label scoreLabel;
	private Label messageLabel;
	private Timer messageTimer;
	private Button startButton;

	public override void _Ready()
	{
		scoreLabel = GetNode<Label>("ScoreLabel");
		messageLabel = GetNode<Label>("MessageLabel");
		messageTimer = GetNode<Timer>("MessageTimer");
		startButton = GetNode<Button>("StartButton");

		startButton.Pressed += OnStartButtonPressed;
		messageTimer.Timeout += () => messageLabel.Hide();

		messageLabel.Text = "Esquive les monstres !";
		scoreLabel.Text = "Score: 0";
	}

	public void ShowMessage(string message)
	{
		messageLabel.Text = message;
		messageLabel.Show();
		messageTimer.Start();
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
