using System;
using System.IO;
using System.Linq;

namespace policechase.Core
{
    public static class HighScoreManager
    {
        private static string GetFilePath()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return Path.Combine(desktopPath, "policechase_scores.txt");
        }

        public static int LoadHighScore()
        {
            try
            {
                string path = GetFilePath();
                if (File.Exists(path))
                {
                    var lines = File.ReadAllLines(path);
                    int maxScore = 0;
                    foreach (var line in lines)
                    {
                        // Format: "Date Time | Score: X"
                        if (line.Contains("Score: "))
                        {
                            string scorePart = line.Split(new[] { "Score: " }, StringSplitOptions.None).Last();
                            if (int.TryParse(scorePart, out int score))
                            {
                                if (score > maxScore) maxScore = score;
                            }
                        }
                    }
                    return maxScore;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading high score: " + ex.Message);
            }
            return 0;
        }

        public static void RecordScore(int score)
        {
            try
            {
                string path = GetFilePath();
                string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Score: {score}";
                File.AppendAllLines(path, new[] { entry });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error recording score: " + ex.Message);
            }
        }
    }
}
