using WinBiometricDotNet;

public class BiometricService
{
    private Session _session;
    private uint _unitId;

    public void OpenSession()
    {
        _session = WinBiometric.OpenSession();
        _unitId = WinBiometric.LocateSensor(_session);
    }

    public void BeginEnroll(FingerPosition fingerPosition)
    {
        WinBiometric.BeginEnroll(_session, fingerPosition, _unitId);
    }

    public CaptureEnrollResult CaptureEnroll()
    {
        return WinBiometric.CaptureEnroll(_session);
    }

    public IEnumerable<FingerPosition> GetEnrolledFingerPositions()
    {
        return WinBiometric.EnumEnrollments(_session, _unitId);
    }

    public BiometricIdentity CommitEnroll()
    {
        return WinBiometric.CommitEnroll(_session);
    }

    public VerifyResult Verify(FingerPosition fingerPosition)
    {
        return WinBiometric.Verify(_session, fingerPosition);
    }

    public void DeleteTemplate(BiometricIdentity identity, FingerPosition fingerPosition)
    {
        WinBiometric.DeleteTemplate(_session, _unitId, identity, fingerPosition);
    }

    public IdentifyResult Identify()
    {
        return WinBiometric.Identify(_session);
    }

    public void CloseSession()
    {
        WinBiometric.CloseSession(_session);
    }
}
