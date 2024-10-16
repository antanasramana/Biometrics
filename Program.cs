// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using WinBiometricDotNet;

var session = WinBiometric.OpenSession();

Console.WriteLine("Please use the biometric scanner to locate sensor");
var unitId = WinBiometric.LocateSensor(session);

while (true)
{
    try
    {
        var identifyResult = WinBiometric.Identify(session);
        Console.WriteLine(JsonSerializer.Serialize(identifyResult));
        break;
    }
    catch (WinBiometricException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

WinBiometric.CloseSession(session);


Console.WriteLine("Please scan your Left Middle Finger");
WinBiometric.BeginEnroll(session, FingerPosition.LeftMiddle, unitId);

var captureEnrollResult = WinBiometric.CaptureEnroll(session);
while (captureEnrollResult.IsRequiredMoreData || captureEnrollResult.RejectDetail != default)
{
    Console.WriteLine("Please scan your Left Middle Finger again for more data");
    captureEnrollResult = WinBiometric.CaptureEnroll(session);
}

var biometricIdentity = WinBiometric.CommitEnroll(session);
Console.WriteLine("Left Middle Finger enrolled successfully");
Console.WriteLine(JsonSerializer.Serialize(biometricIdentity));

Console.WriteLine("Scan to identify");

