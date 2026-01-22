using System.Runtime.InteropServices;

namespace WindowsOS;

public unsafe class Kernel32
{
    const string kernel = "kernel32";

    #region DllImports
    [DllImport(kernel), SuppressGCTransition] public static extern
        nint GetModuleHandleA(byte* name);

    [DllImport(kernel), SuppressGCTransition] public static extern
        nint GetProcAddress(nint module, byte* name);

    [DllImport(kernel), SuppressGCTransition] public static extern
        nint LoadLibraryA(byte* dllPath);

    [DllImport(kernel), SuppressGCTransition] public static extern
        int GetCurrentThreadId();

    [DllImport(kernel), SuppressGCTransition] public static extern 
        nint CreateWaitableTimer(
            nint lpTimerAttributes,
            bool bManualReset,
            char* lpTimerName);

    [DllImport(kernel), SuppressGCTransition] public static extern 
        bool SetWaitableTimer(
            nint hTimer,
            long* pDueTime,
            int lPeriod,
            nint pfnCompletionRoutine,
            nint lpArgToCompletionRoutine,
            bool fResume);

    [DllImport(kernel), SuppressGCTransition] public static extern 
        int CloseHandle(nint hObject);

    [DllImport(kernel), SuppressGCTransition] public static extern 
        uint WaitForSingleObject(nint hHandle, uint dwMilliseconds);

    [DllImport(kernel), SuppressGCTransition] public static extern
        uint WaitForSingleObjectEx(nint hHandle, uint dwMilliseconds, bool bAlertable);

    [DllImport(kernel), SuppressGCTransition] public static extern
        int WaitForMultipleObjects(int nCount, nint* lpHandles, bool bWaitAll, uint dwMilliseconds);

    [DllImport(kernel), SuppressGCTransition] public static extern
        int WaitForMultipleObjectsEx(int nCount, nint* lpHandles, bool bWaitAll, uint dwMilliseconds, bool bAlertable);

    [DllImport(kernel), SuppressGCTransition] public static extern 
        nint CreateEventA(nint lpEventAttributes, bool bManualReset, bool bInitialState, byte* lpName);

    [DllImport(kernel), SuppressGCTransition] public static extern 
        int SetEvent(nint hEvent);
    #endregion

    public static nint GetModuleHandle(ReadOnlySpan<byte> name)
    {
        fixed (byte* nameBytes = name)
            return GetModuleHandleA(nameBytes);
    }

    public static nint LoadLibrary(ReadOnlySpan<byte> dllPath)
    {
        fixed (byte* dllPathBytes = dllPath)
            return LoadLibraryA(dllPathBytes);
    }

    public static uint WaitForSingleObject(nint handle) => WaitForSingleObject(handle, 0xFFFFFFFF);

    public static uint WaitForSingleObjectEx(nint handle, bool alertable) => WaitForSingleObjectEx(handle, 0xFFFFFFFF, alertable);
}