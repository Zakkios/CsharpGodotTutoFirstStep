using Godot;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;
using System;

public partial class ScoreManager : Node
{
    public static ScoreManager Instance;
    private const string SavePath = "user://scores.json";
    public const int MaxScores = 3;

    public List<int> Scores = new();

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadScores();
        }
        else
        {
            QueueFree();
        }
    }

    public void AddScore(int newScore)
    {
        GD.Print($"New score: {newScore}");
        Scores.Add(newScore);
        Scores = Scores.OrderByDescending(s => s).Take(MaxScores).ToList();
        SaveScores();
    }

    public void SaveScores()
    {
        try
        {
            var json = JsonSerializer.Serialize(Scores);
            File.WriteAllText(ProjectSettings.GlobalizePath(SavePath), json);
        }
        catch (System.Exception e)
        {
            GD.PushError($"Erreur lors de la sauvegarde des scores : {e.Message}");
        }
    }

    public void LoadScores()
    {
        try
        {
            string path = ProjectSettings.GlobalizePath(SavePath);
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                Scores = JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
            }
            else
            {
                Scores = new List<int>();
            }
        }
        catch (System.Exception e)
        {
            GD.PushError($"Erreur lors du chargement des scores : {e.Message}");
            Scores = new List<int>();
        }
    }
}
