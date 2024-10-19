using WinBiometricDotNet;

namespace Biometrics;
public record FingerPrint
    {
    public string Id { get; set; }
    public FingerPosition Position { get; set; }
    public required BiometricIdentity Identity { get; set; }
    public required Action AssignedFunction { get; set; }
}
