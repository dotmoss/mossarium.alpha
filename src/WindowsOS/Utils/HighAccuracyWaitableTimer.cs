namespace WindowsOS.Utils;

public unsafe class HighAccuracyWaitableTimer : IDisposable
{
    public HighAccuracyWaitableTimer()
    {
        handle = Kernel32.CreateWaitableTimer(default/*2: unsupported*/, true, null);
    }

    nint handle;

    public void Wait(long nanoseconds)
    {
        var negativeNanoseconds = -nanoseconds;
        Kernel32.SetWaitableTimer(handle, &negativeNanoseconds, 0, default, default, false);
        Kernel32.WaitForSingleObjectEx(handle, true);
    }

    public void Dispose()
    {
        Kernel32.CloseHandle(handle);
    }
}