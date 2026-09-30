namespace EduVibe.Tests.Helpers;

public class LogAnalyzer
{
    public bool IsValidLogFileName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) {
            throw new ArgumentException("No filename provided!");
        }
        if (!fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }
        return true;
    }
}