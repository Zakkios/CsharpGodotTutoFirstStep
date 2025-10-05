using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Godot;

#nullable enable

namespace Survivor;

public sealed class ScoreRepository
{
    private readonly string absolutePath;

    public ScoreRepository(string relativePath)
    {
        absolutePath = ProjectSettings.GlobalizePath(relativePath);
    }

    public bool TryLoad(out List<int> scores, out string? error)
    {
        try
        {
            if (!File.Exists(absolutePath))
            {
                scores = new List<int>();
                error = null;
                return true;
            }

            string json = File.ReadAllText(absolutePath);
            scores = JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
            error = null;
            return true;
        }
        catch (Exception exception)
        {
            scores = new List<int>();
            error = exception.Message;
            return false;
        }
    }

    public bool TrySave(IEnumerable<int> scores, out string? error)
    {
        try
        {
            string json = JsonSerializer.Serialize(scores);
            File.WriteAllText(absolutePath, json);
            error = null;
            return true;
        }
        catch (Exception exception)
        {
            error = exception.Message;
            return false;
        }
    }
}
