using System.Text;

namespace LearningOpenTK.Common;

public class Logger
{
    public static readonly Logger Instance = new();

    public void Log(params object[] obj)
    {
        Print("LOG",ConsoleColor.Cyan,obj);
    }
    
    public void Warning(params object[] obj)
    {
        Print("WARNING",ConsoleColor.Yellow,obj);
    }

    public void Error(params object[] obj)
    {
        Print("ERROR",ConsoleColor.Red,obj);
    }

    public void Clear()
    {
        Console.Clear();
    }
    
    private static void Print(string prefix, ConsoleColor color,params object[] obj)
    {
        Console.ResetColor();
        Console.ForegroundColor = color;
        var builder = new StringBuilder();
        builder.Append('[');
        builder.Append(prefix);
        builder.Append(']');
        builder.Append(' ');
        builder.Append(string.Join("",obj));
        Console.WriteLine(builder.ToString());
        Console.ResetColor();
    }
    
}