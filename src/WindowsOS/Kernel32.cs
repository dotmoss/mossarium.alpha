using System.Runtime.InteropServices;

namespace WindowsOS;

public unsafe partial class Kernel32
{
    const string kernel = "kernel32";

    #region DllImports
    [LibraryImport(kernel), SuppressGCTransition] public static partial
        nint GetModuleHandleA(byte* name);

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        nint GetProcAddress(nint module, byte* name);

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        nint LoadLibraryA(byte* dllPath);

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        int GetCurrentThreadId();

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        nint CreateWaitableTimerW(
            nint lpTimerAttributes,
            [MarshalAs(UnmanagedType.Bool)]
            bool bManualReset,
            char* lpTimerName
        );

    [return: MarshalAs(UnmanagedType.Bool)]
    [LibraryImport(kernel), SuppressGCTransition] public static partial
        bool SetWaitableTimer(
            nint hTimer,
            long* pDueTime,
            int lPeriod,
            nint pfnCompletionRoutine,
            nint lpArgToCompletionRoutine,
            [MarshalAs(UnmanagedType.Bool)]
            bool fResume
        );

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        int CloseHandle(nint hObject);

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        uint WaitForSingleObject(nint hHandle, uint dwMilliseconds);

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        uint WaitForSingleObjectEx(
            nint hHandle, 
            uint dwMilliseconds,
            [MarshalAs(UnmanagedType.Bool)]
            bool bAlertable
        );

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        int WaitForMultipleObjects(
            int nCount, 
            nint* lpHandles, 
            [MarshalAs(UnmanagedType.Bool)] 
            bool bWaitAll, 
            uint dwMilliseconds
        );

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        int WaitForMultipleObjectsEx(
            int nCount, 
            nint* lpHandles, 
            [MarshalAs(UnmanagedType.Bool)] 
            bool bWaitAll, 
            uint dwMilliseconds, 
            [MarshalAs(UnmanagedType.Bool)] 
            bool bAlertable
        );

    [LibraryImport(kernel), SuppressGCTransition] public static partial
        nint CreateEventA(
            nint lpEventAttributes, 
            [MarshalAs(UnmanagedType.Bool)] 
            bool bManualReset, 
            [MarshalAs(UnmanagedType.Bool)] 
            bool bInitialState, 
            byte* lpName
        );

    [LibraryImport(kernel), SuppressGCTransition] public static partial
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

    public static uint WaitForSingleObjectEx(nint handle, [MarshalAs(UnmanagedType.Bool)] bool alertable) => WaitForSingleObjectEx(handle, 0xFFFFFFFF, alertable);
}