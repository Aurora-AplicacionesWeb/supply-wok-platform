namespace Aurora.SupplyWok.Platform.Shared.Infrastructure.Configuration;

public static class DotEnvLoader
{
    public static void Load(IEnumerable<string> candidateDirectories, string? environmentName)
    {
        foreach (var directory in candidateDirectories.Where(Directory.Exists).Distinct(StringComparer.OrdinalIgnoreCase))
        {
            LoadFile(Path.Combine(directory, ".env"));

            if (!string.IsNullOrWhiteSpace(environmentName))
                LoadFile(Path.Combine(directory, $".env.{environmentName.ToLowerInvariant()}"));
        }
    }

    private static void LoadFile(string path)
    {
        if (!File.Exists(path)) return;

        foreach (var rawLine in File.ReadAllLines(path))
        {
            var line = rawLine.Trim();
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#')) continue;

            var separatorIndex = line.IndexOf('=');
            if (separatorIndex <= 0) continue;

            var key = line[..separatorIndex].Trim();
            if (string.IsNullOrWhiteSpace(key) || !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key)))
                continue;

            var value = line[(separatorIndex + 1)..].Trim();
            if ((value.StartsWith('"') && value.EndsWith('"')) || (value.StartsWith('\'') && value.EndsWith('\'')))
                value = value[1..^1];

            Environment.SetEnvironmentVariable(key, value);
        }
    }
}
