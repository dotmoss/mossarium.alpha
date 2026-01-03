using System.Runtime.InteropServices;

namespace WindowsOS;

public unsafe static class User32
{
    const string user = "user32";

    #region DllImports
    
    [DllImport(user), SuppressGCTransition] public static extern 
        int GetWindowLongW(nint hWnd, int nIndex);

    [DllImport(user), SuppressGCTransition] public static extern 
        int SetWindowLongW(nint hWnd, int nIndex, int dwNewLong);

    [DllImport(user), SuppressGCTransition] public static extern 
        long GetWindowLongPtrW(nint hWnd, int nIndex);

    [DllImport(user), SuppressGCTransition] public static extern 
        long SetWindowLongPtrW(nint hWnd, int nIndex, long dwNewLong);

    [DllImport(user), SuppressGCTransition] public static extern 
        nint CreateWindowExW(int dwExStyle, char* lpClassName, char* lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, nint hWndParent, nint hMenu, nint hInstance, nint lpParam);

    [DllImport(user), SuppressGCTransition] public static extern 
        int GetWindowTextW(nint hWnd, char* lpString, int nMaxCount);

    [DllImport(user), SuppressGCTransition] public static extern 
        int SetWindowTextW(nint hWnd, char* lpString);

    [DllImport(user), SuppressGCTransition] public static extern 
        int SetWindowPos(nint hWnd, nint hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport(user), SuppressGCTransition] public static extern 
        int GetWindowRect(nint hWnd, int* pRect);

    [DllImport(user)] public static extern 
        long CallWindowProcW(void* wndFunc, nint hWnd, uint msg, ulong wParam, ulong lParam);

    [DllImport(user)] public static extern 
        long DefWindowProcW(nint hWnd, uint msg, ulong wParam, ulong lParam);

    [DllImport(user), SuppressGCTransition] public static extern
        nint GetDC(nint hWnd);

    [DllImport(user), SuppressGCTransition] public static extern
        int ReleaseDC(nint hWnd, nint hDC);

    [DllImport(user), SuppressGCTransition] public static extern
        int DestroyWindow(nint hWnd);

    [DllImport(user)] public static extern 
        int GetMessageW(Message* pMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

    [DllImport(user), SuppressGCTransition] public static extern 
        int TranslateMessage(Message* pMsg);

    [DllImport(user)] public static extern 
        int DispatchMessage(Message* pMsg);

    [DllImport(user)] public static extern
        int PeekMessageW(Message* pMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

    [DllImport(user), SuppressGCTransition] public static extern
        int MessageBoxA(nint hWnd, byte* lpText, byte* lpCaption, uint uType);

    [DllImport(user), SuppressGCTransition] public static extern
        int MessageBoxW(nint hWnd, char* lpText, char* lpCaption, uint uType);

    [DllImport(user), SuppressGCTransition] public static extern 
        int GetSystemMetrics(int nIndex);
    #endregion

    public static int MessageBox(nint handle, string text, string caption, uint type)
    {
        fixed (char* textChars = text, captionChars = caption)
            return MessageBoxW(handle, textChars, captionChars, type);
    }

    public static int MessageBox(string text, string caption = "") => MessageBox(0, text, caption, 0);

    public static int PeekMessage(Message* message, nint handle, uint msgFilterMin, uint msgFilterMax, uint removeMessage) => PeekMessageW(message, handle, msgFilterMin, msgFilterMax, removeMessage);

    public static int GetMessage(Message* message, nint handle, uint msgFilterMin, uint msgFilterMax) => GetMessageW(message, handle, msgFilterMin, msgFilterMax);

    public static int GetWindowLong(nint handle, WindowLongField field) => GetWindowLongW(handle, (int)field);
    public static void SetWindowLong(nint handle, WindowLongField field, int value) => SetWindowLongW(handle, (int)field, value);

    public static long GetWindowLongPtr(nint handle, WindowLongField field) => GetWindowLongPtrW(handle, (int)field);
    public static long SetWindowLongPtr(nint handle, WindowLongField field, nint value) => SetWindowLongPtrW(handle, (int)field, value);

    public static nint CreateWindow(
        WindowExStyles styleEx,
        char* className,
        char* windowName,
        WindowStyles style,
        int x,
        int y,
        int width,
        int height,
        nint hWndParent,
        nint menu,
        nint hInstance,
        nint lpParam)
        => CreateWindowExW((int)styleEx, className, windowName, (int)style, x, y, width, height, hWndParent, menu, hInstance, lpParam);

    public static nint CreateWindow(
        WindowExStyles styleEx,
        string className,
        string windowName,
        WindowStyles style,
        int x,
        int y,
        int width,
        int height,
        nint hWndParent,
        nint menu,
        nint hInstance,
        nint lpParam)
    {
        fixed (char* classNameChars = className, windowNameChars = windowName)
            return CreateWindowExW((int)styleEx, classNameChars, windowNameChars, (int)style, x, y, width, height, hWndParent, menu, hInstance, lpParam);
    }

    public static nint CreateWindow(
        WindowExStyles styleEx,
        ushort className,
        char* windowName,
        WindowStyles style,
        int x,
        int y,
        int width,
        int height,
        nint hWndParent,
        nint menu,
        nint hInstance,
        nint lpParam)
        => CreateWindowExW((int)styleEx, (char*)className, windowName, (int)style, x, y, width, height, hWndParent, menu, hInstance, lpParam);

    public static nint CreateWindow(
        WindowExStyles styleEx,
        ushort className,
        string windowName,
        WindowStyles style,
        int x,
        int y,
        int width,
        int height,
        nint hWndParent,
        nint menu,
        nint hInstance,
        nint lpParam)
    {
        fixed (char* windowNameChars = windowName)
            return CreateWindowExW((int)styleEx, (char*)className, windowNameChars, (int)style, x, y, width, height, hWndParent, menu, hInstance, lpParam);
    }

    public static string GetWindowTitle(nint handle)
    {
        const int MAX_LENGTH = 256;

        var buffer = stackalloc char[MAX_LENGTH];
        GetWindowTextW(handle, buffer, MAX_LENGTH);
        return Marshal.PtrToStringUni((nint)buffer)!;
    }

    public static void SetWindowTitle(nint handle, char* text) => SetWindowTextW(handle, text);

    public static void SetWindowTitle(nint handle, string text)
    {
        fixed (char* textChars = text)
            SetWindowTextW(handle, textChars);
    }

    public static void SetWindowPos(nint handle, SetOrderFlags zOrder, int x, int y, int width, int height, SetWindowPosFlags flags)
        => SetWindowPos(handle, (nint)zOrder, x, y, width, height, (uint)flags);

    public static void SetWindowPos(nint handle, int x, int y, int width, int height, SetWindowPosFlags flags)
        => SetWindowPos(handle, handle, x, y, width, height, (uint)flags);

    public static void SetWindowPos(nint handle, int x, int y, int width, int height)
        => SetWindowPos(handle, x, y, width, height, SetWindowPosFlags.NoZOrder);

    public static (int X, int Y, int Width, int Height) GetWindowRectangle(nint handle)
    {
        (int, int, int, int) rectangle;
        GetWindowRect(handle, (int*)&rectangle);
        return rectangle;
    }
    public static void SetWindowRectangle(nint handle, int x, int y, int width, int height) => SetWindowPos(handle, x, y, width, height);

    public static WindowStyles GetWindowStyle(nint handle) => (WindowStyles)GetWindowLong(handle, WindowLongField.Style);
    public static void SetWindowStyle(nint handle, WindowStyles style)
    {
        SetWindowLong(handle, WindowLongField.Style, (int)style);
        UpdateWindowStyle(handle);
    }

    public static WindowExStyles GetWindowExStyle(nint handle) => (WindowExStyles)GetWindowLong(handle, WindowLongField.ExStyle);

    public static void SetWindowExStyle(nint handle, WindowExStyles style)
    {
        SetWindowLong(handle, WindowLongField.ExStyle, (int)style);
        UpdateWindowStyle(handle);
    }

    public static nint SetWindowWndProcFunction(nint handle, nint pointer) => (nint)SetWindowLongPtr(handle, WindowLongField.WndProc, pointer);

    public static long CallWindowProccess(nint handle, nint function, WindowMessage message, ulong wParam, ulong lParam)
        => CallWindowProcW((void*)function, handle, (uint)message, wParam, lParam);

    public static long CallDefaultWindowProccess(nint handle, WindowMessage message, ulong wParam, ulong lParam)
        => DefWindowProcW(handle, (uint)message, wParam, lParam);

    public static void UpdateWindowStyle(nint handle)
        => SetWindowPos(handle, 0, 0, 0, 0, SetWindowPosFlags.NoSize | SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder | SetWindowPosFlags.FrameChanged);

    public static int GetScreenWidth()
    {
        const int SM_CXSCREEN = 0;
        return GetSystemMetrics(SM_CXSCREEN);
    }

    public static int GetScreenHeight()
    {
        const int SM_CYSCREEN = 1;
        return GetSystemMetrics(SM_CYSCREEN);
    }
}