using System.Text.Json;
using Biometrics;
using Spectre.Console;
using Spectre.Console.Json;
using WinBiometricDotNet;

var app = new Application();
app.Run();
//var session = WinBiometric.OpenSession();
//AnsiConsole.Markup("[green]Before starting please use the biometric sensor:[/] ");
//var unitId = WinBiometric.LocateSensor(session);

//var possibleFingerPositions = new[]
//{
//    FingerPosition.LeftLittle,
//    FingerPosition.LeftRing,
//    FingerPosition.LeftMiddle,
//    FingerPosition.LeftIndex,
//    FingerPosition.LeftThumb,
//    FingerPosition.RightThumb,
//    FingerPosition.RightIndex,
//    FingerPosition.RightMiddle,
//    FingerPosition.RightRing,
//    FingerPosition.RightLittle
//};
//var fingerPrints = new List<FingerPrint>();

//while (true)
//{
//    DisplayFingerPrints(fingerPrints);

//    var menuSelection = AnsiConsole.Prompt(
//        new SelectionPrompt<string>()
//            .Title("Choose an [green]option[/]:")
//            .PageSize(10)
//            .AddChoices(
//                new[]
//                {
//                    "1 - Add Fingerprint",
//                    "2 - Manage Fingerprints",
//                    "3 - Execute Fingerprint function",
//                    "4 - Save fingerprints to file",
//                    "5 - Load fingerprints from file",
//                    "6 - Exit"
//                }
//            )
//    );

//    // Handle selection
//    switch (menuSelection)
//    {
//        case "1 - Add Fingerprint":
//            AddFingerPrint();
//            break;
//        case "2 - Manage Fingerprints":
//            ManageFingerPrints();
//            break;
//        case "3 - Execute Fingerprint function":
//            ExecuteFingerPrintFunction();
//            break;
//        case "4 - Save fingerprints to file":
//            SaveFingerPrintsToFile();
//            break;
//        case "5 - Load fingerprints from file":
//            LoadFingerPrintsFromFile();
//            break;
//        case "6 - Exit":
//            Exit();
//            return;
//        default:
//            AnsiConsole.MarkupLine("[red]Invalid option![/]");
//            break;
//    }

//    AnsiConsole.MarkupLine("\nPress any key to go back to the menu...");

//    Console.ReadKey();
//    Console.Clear();
//}

//static void DisplayFingerPrints(List<FingerPrint> fingerPrints)
//{
//    foreach (var fingerPrint in fingerPrints)
//    {
//        var fingerPrintIdentitySerialized = JsonSerializer.Serialize(fingerPrint.Identity);
//        var jsonText = new JsonText(fingerPrintIdentitySerialized);
//        AnsiConsole.Write(
//            new Panel(jsonText)
//                .Header(
//                    $"{fingerPrint.Id} - {fingerPrint.Position.ToString()} - {fingerPrint.AssignedFunction.GetType()}"
//                )
//                .Collapse()
//                .RoundedBorder()
//                .BorderColor(Color.Yellow)
//        );
//    }
//}

//void AddFingerPrint()
//{
//    AnsiConsole.Markup("[red]Starting[/] new fingerprint addition process:");
//    var fingerPrintId = AnsiConsole.Prompt(
//        new TextPrompt<string>("Enter [green]fingerprint ID you want to use[/]?").PromptStyle("red")
//    );

//    var fingerPosition = AnsiConsole.Prompt(
//        new SelectionPrompt<FingerPosition>()
//            .Title("Select which [green]finger you will use[/]:")
//            .AddChoices(possibleFingerPositions.Except(fingerPrints.Select(f => f.Position)))
//    );

//    var functionToAssign = SelectFunctionToAssign();

//    AnsiConsole.Markup($"Please scan your {fingerPosition.ToString()}");
//    WinBiometric.BeginEnroll(session, fingerPosition, unitId);

//    var captureEnrollResult = WinBiometric.CaptureEnroll(session);
//    while (captureEnrollResult.IsRequiredMoreData || captureEnrollResult.RejectDetail != default)
//    {
//        AnsiConsole.Markup($"Please scan your {fingerPosition.ToString()} again for more data");
//        captureEnrollResult = WinBiometric.CaptureEnroll(session);
//    }

//    var biometricIdentity = WinBiometric.CommitEnroll(session);
//    fingerPrints.Add(
//        new FingerPrint()
//        {
//            Id = fingerPrintId,
//            Position = fingerPosition,
//            Identity = biometricIdentity,
//            AssignedFunction = functionToAssign,
//        }
//    );
//    AnsiConsole.Markup($"{fingerPosition.ToString()} saved successfully.");
//}

//static Action SelectFunctionToAssign()
//{
//    var functionSelection = AnsiConsole.Prompt(
//        new SelectionPrompt<string>()
//            .Title("Choose a [green]function[/] to execute:")
//            .AddChoices(new[] { "1 - Display Date", "2 - Say Hello", "3 - Display Art" })
//    );

//    switch (functionSelection)
//    {
//        case "1 - Display Date":
//            return DisplayDate;
//        case "2 - Say Hello":
//            return SayHello;
//        case "3 - Display Art":
//            return DisplayArt;
//        default:
//            return SayHello;
//    }
//}

//void ManageFingerPrints()
//{
//    var fingerPrintId = AnsiConsole.Prompt(
//        new SelectionPrompt<string>()
//            .Title("Choose a [green]fingerprint[/] to edit:")
//            .PageSize(10)
//            .AddChoices(fingerPrints.Select(f => f.Id))
//    );
//    var selectedFingerPrint = fingerPrints.First(f => f.Id == fingerPrintId);

//    var actionSelection = AnsiConsole.Prompt(
//        new SelectionPrompt<string>()
//            .Title("Choose the [green] action[/]:")
//            .AddChoices(new[] { "1 - Assign Function", "2 - Verify", "3 - Delete" })
//    );

//    switch (actionSelection)
//    {
//        case "1 - Assign Function":
//            var function = SelectFunctionToAssign();
//            selectedFingerPrint.AssignedFunction = function;
//            break;
//        case "2 - Verify":
//            AnsiConsole.MarkupLine($"Please scan {selectedFingerPrint.Position} to verify");
//            var result = WinBiometric.Verify(session, selectedFingerPrint.Position);
//            if (result.IsMatch)
//            {
//                AnsiConsole.MarkupLine("[green]Verification successful![/]");
//            }
//            else
//            {
//                AnsiConsole.MarkupLine(
//                    $"[red] Verification unsuccessful[/]. Reject reason: {result.RejectDetail.ToString()}"
//                );
//            }
//            break;
//        case "3 - Delete":
//            AnsiConsole.MarkupLine("[red]Please identify yourself![/]");
//            WinBiometric.DeleteTemplate(
//                session,
//                unitId,
//                selectedFingerPrint.Identity,
//                selectedFingerPrint.Position
//            );

//            break;
//    }
//}

//void SaveFingerPrintsToFile()
//{
//    AnsiConsole.MarkupLine("Scan a finger and the function will be executed!");
//}

//void LoadFingerPrintsFromFile()
//{
//    AnsiConsole.MarkupLine("Scan a finger and the function will be executed!");
//}

//void ExecuteFingerPrintFunction()
//{
//    AnsiConsole.MarkupLine("Scan a finger and the function will be executed!");

//    IdentifyResult? identifyResult;

//    while (true)
//    {
//        try
//        {
//            identifyResult = WinBiometric.Identify(session);
//            break;
//        }
//        catch (WinBiometricException ex)
//        {
//            AnsiConsole.Markup($"{ex.Message}. Try again!");
//        }
//    }

//    var identifiedFingerPrint = fingerPrints.First(f =>
//        f.Position == identifyResult.FingerPosition
//    );

//    identifiedFingerPrint.AssignedFunction.Invoke();
//}

//void Exit()
//{
//    AnsiConsole.MarkupLine("[red]Initiating fingerprint removal process![/]");

//    foreach (var fingerPrint in fingerPrints)
//    {
//        WinBiometric.DeleteTemplate(session, unitId, fingerPrint.Identity, fingerPrint.Position);
//    }

//    AnsiConsole.MarkupLine("[red]Exiting the application... Goodbye![/]");
//    WinBiometric.CloseSession(session);
//}

//static void DisplayDate()
//{
//    AnsiConsole.MarkupLine($"[blue]Today's date is {DateTime.Now:yyyy-MM-dd}[/]");
//}

//static void SayHello()
//{
//    AnsiConsole.MarkupLine("[green]Hello, welcome![/]");
//}

//static void DisplayArt()
//{
//    int pyramidHeight = 5;
//    AnsiConsole.MarkupLine(string.Empty);
//    for (int i = 1; i <= pyramidHeight; i++)
//    {
//        for (int j = pyramidHeight - i; j > 0; j--)
//        {
//            AnsiConsole.Markup(" ");
//        }

//        for (int k = 0; k < (2 * i - 1); k++)
//        {
//            AnsiConsole.Markup("*");
//        }

//        AnsiConsole.MarkupLine(string.Empty);
//    }
//}
