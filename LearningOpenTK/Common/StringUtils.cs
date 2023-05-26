namespace LearningOpenTK.Common;

public static class StringUtils
{

    public static string GetBetween(string str, string begin, string end)
    {
        var from = str.IndexOf(begin)+begin.Length;
        var to = str.LastIndexOf(end);
        return str.Substring(from,to-from);
    }
}