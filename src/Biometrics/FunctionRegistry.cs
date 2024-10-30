using Spectre.Console;

public static class FunctionRegistry
{
    private static readonly Dictionary<string, Action> Functions = new Dictionary<string, Action>
    {
        { "DisplayDate", DisplayDate },
        { "SayHello", SayHello },
        { "DisplayArt", DisplayArt }
    };

    public static IReadOnlyDictionary<string, Action> AvailableFunctions => Functions;

    public static Action GetFunctionByName(string functionName)
    {
        return Functions.ContainsKey(functionName) ? Functions[functionName] : SayHello; // Default to 'Say Hello'
    }

    private static void DisplayDate()
    {
        AnsiConsole.MarkupLine($"[blue]Today's date is {DateTime.Now:yyyy-MM-dd}[/]");
    }

    private static void SayHello()
    {
        AnsiConsole.MarkupLine("[green]Hello, welcome![/]");
    }

    private static void DisplayArt()
    {
        int pyramidHeight = 5;
        AnsiConsole.WriteLine();
        for (int i = 1; i <= pyramidHeight; i++)
        {
            var spaces = new string(' ', pyramidHeight - i);
            var stars = new string('*', 2 * i - 1);
            AnsiConsole.MarkupLine($"{spaces}{stars}");
        }
    }
}
