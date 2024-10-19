using Newtonsoft.Json;
using WinBiometricDotNet;

namespace Biometrics;

public class FingerPrint
{
    public FingerPrint() { }

    public required string Id { get; set; }
    public FingerPosition Position { get; set; }

    [JsonConverter(typeof(BiometricIdentityConverter))]
    public required BiometricIdentity Identity { get; set; }

    [JsonIgnore]
    public Action AssignedFunction { get; set; } = new Action(() => { });
    public required string AssignedFunctionName { get; set; }
}
