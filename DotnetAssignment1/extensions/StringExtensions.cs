namespace DotnetAssignment1.extensions;

public static class StringExtensions
{
    // Extension method to split a line and verify it has enough parts
    public static string[] SplitAndValidate(this string line, char delimiter, int expectedParts)
    {
        var parts = line.Split(delimiter);
        return parts.Length >= expectedParts ? parts : Array.Empty<string>();
    }
}