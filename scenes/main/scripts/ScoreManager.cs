using System.Collections.Generic;
using System.Linq;
using Godot;

#nullable enable

namespace Survivor;

public partial class ScoreManager : Node
{
    public static ScoreManager Instance = null!;
    private const string SavePath = "user://scores.json";
    public const int MaxScores = 3;

    private readonly List<int> scores = new();
    private ScoreRepository repository = null!;

    public IReadOnlyList<int> Scores => scores;

    public override void _Ready()
    {
        if (Instance != null)
        {
            QueueFree();
            return;
        }

        Instance = this;
        repository = new ScoreRepository(SavePath);
        LoadScores();
    }

    public void AddScore(int newScore)
    {
        GD.Print($"New score: {newScore}");
        scores.Add(newScore);
        scores.Sort((a, b) => b.CompareTo(a));

        if (scores.Count > MaxScores)
        {
            scores.RemoveRange(MaxScores, scores.Count - MaxScores);
        }

        SaveScores();
    }

    private void SaveScores()
    {
        if (repository.TrySave(scores, out string? error))
        {
            return;
        }

        if (!string.IsNullOrEmpty(error))
        {
            GD.PushError($"Erreur lors de la sauvegarde des scores : {error}");
        }
    }

    private void LoadScores()
    {
        if (repository.TryLoad(out List<int> loadedScores, out string? error))
        {
            scores.Clear();
            scores.AddRange(loadedScores.OrderByDescending(score => score).Take(MaxScores));
            return;
        }

        scores.Clear();
        if (!string.IsNullOrEmpty(error))
        {
            GD.PushError($"Erreur lors du chargement des scores : {error}");
        }
    }
}
