namespace WindowsOS.Utils;

public unsafe struct HighAccuracyWaitableTimer : IDisposable
{
    public HighAccuracyWaitableTimer()
    {
        Handle = Kernel32.CreateWaitableTimerW(default/*2: unsupported*/, true, null);
    }

    public readonly nint Handle;

    public void ResetTimer(long nanoseconds)
    {
        var negativeNanoseconds = -nanoseconds;
        Kernel32.SetWaitableTimer(Handle, &negativeNanoseconds, 0, default, default, false);
    }

    public void Wait(long nanoseconds)
    {
        ResetTimer(nanoseconds);
        Kernel32.WaitForSingleObjectEx(Handle, true);
    }

    public void Dispose()
    {
        Kernel32.CloseHandle(Handle);
    }
}