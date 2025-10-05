using System.Collections.Generic;
using System.Text;

namespace Survivor;

public static class HighScoreFormatter
{
    private static readonly string[] RankLabels = { "🥇", "🥈", "🥉" };

    public static string Format(IReadOnlyList<int> scores, int maxScores)
    {
        var builder = new StringBuilder();
        builder.Append("Records :\r");

        if (scores.Count == 0)
        {
            builder.Append("Aucun score");
            return builder.ToString();
        }

        for (int i = 0; i < scores.Count && i < maxScores; i++)
        {
            string prefix = i < RankLabels.Length ? RankLabels[i] : $"{i + 1}e";
            builder.Append($"{prefix} : {scores[i]}\r");
        }

        return builder.ToString().TrimEnd('\r');
    }
}
