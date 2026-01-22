using Mossarium.Alpha.UI.Windowing.Structures;
using WindowsOS;

namespace Mossarium.Alpha.UI.Windowing;

public unsafe struct NativeWindow : IDisposable
{
    NativeWindow(nint handle)
    {
        Handle = handle;
        HDC = User32.GetDC(handle);
    }

    public readonly nint Handle;
    public readonly nint HDC;

    public string Title { get => User32.GetWindowTitle(Handle); set => User32.SetWindowTitle(Handle, value); }

    public Win32Rectangle Rectangle
    {
        get
        {
            var rectangle = User32.GetWindowRectangle(Handle);
            return new Win32Rectangle(rectangle.X, rectangle.Y, rectangle.X2, rectangle.Y2);
        }
        set => User32.SetWindowRectangle(Handle, value.X, value.Y, value.Width, value.Height);
    }

    public LocationI4 Location
    {
        get
        {
            var rectangle = Rectangle;
            return new LocationI4(rectangle.X, rectangle.Y);
        }
        set
        {
            var rectangle = Rectangle;
            var (width, height) = (rectangle.Width, rectangle.Height);
            User32.SetWindowRectangle(Handle, value.X, value.Y, width, height);
        }
    }

    public SizeI4 Size
    {
        get
        {
            var rectangle = Rectangle;
            return new SizeI4(rectangle.Width, rectangle.Height);
        }
        set
        {
            var rectangle = Rectangle;
            User32.SetWindowRectangle(Handle, rectangle.X, rectangle.Y, value.Width, value.Height);
        }
    }

    public WindowStyles Style { get => User32.GetWindowStyle(Handle); set => User32.SetWindowStyle(Handle, value); }

    public WindowExStyles ExStyle { get => User32.GetWindowExStyle(Handle); set => User32.SetWindowExStyle(Handle, value); }

    public void Dispose()
    {
        User32.ReleaseDC(Handle, HDC);
        User32.DestroyWindow(Handle);
    }

    public static NativeWindow Create(
        string title,
        LocationI4 location,
        SizeI4 size)
    {
        var handle = User32.CreateWindow(0, 32770, title, 0, location.X, location.Y, size.Width, size.Height, 0, 0, 0/*instance*/, 0);
        var window = FromHandle(handle);

        return window;
    }

    public static NativeWindow FromHandle(nint handle) => new NativeWindow(handle);
}