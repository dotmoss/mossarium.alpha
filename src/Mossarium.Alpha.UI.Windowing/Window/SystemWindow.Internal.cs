using System.Diagnostics;
using WindowsOS;

namespace Mossarium.Alpha.UI.Windowing;

unsafe partial class SystemWindow
{
	class SystemWindowInternal : IDisposable
	{
        SystemWindowInternal(nint handle)
        {
            Handle = handle;
            HDC = User32.GetDC(handle);
        }

        public nint Handle { get; init; }
        public nint HDC { get; init; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Title { get => User32.GetWindowTitle(Handle); set => User32.SetWindowTitle(Handle, value); }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public (int X, int Y, int Width, int Height) Rectangle
        {
            get => User32.GetWindowRectangle(Handle);
            set => User32.SetWindowRectangle(Handle, value.X, value.Y, value.Width, value.Height);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public (int X, int Y) Location
        {
            get
            {
                var rectangle = Rectangle;
                return (rectangle.X, rectangle.Y);
            }
            set
            {
                var rectangle = Rectangle;
                User32.SetWindowRectangle(Handle, value.X, value.Y, value.X + rectangle.Width, value.Y + rectangle.Height);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public (int Width, int Height) Size
        {
            get
            {
                var rectangle = Rectangle;
                return (rectangle.Width, rectangle.Height);
            }
            set
            {
                var rectangle = Rectangle;
                User32.SetWindowRectangle(Handle, rectangle.X, rectangle.Y, value.Width, value.Height);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public WindowStyles Style { get => User32.GetWindowStyle(Handle); set => User32.SetWindowStyle(Handle, value); }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public WindowExStyles ExStyle { get => User32.GetWindowExStyle(Handle); set => User32.SetWindowExStyle(Handle, value); }

        public bool PeekMessage(Message* message) => User32.PeekMessage(message, Handle, 0, 0, removeMessage: 1) != 0;

        public bool GetMessage(Message* message) => User32.GetMessage(message, Handle, 0, 0) != 0;

        public bool TranslateMessage(Message* message) => User32.TranslateMessage(message) != 0;

        public bool DispatchMessage(Message* message) => User32.DispatchMessage(message) != 0;

        public void Dispose()
        {
            User32.ReleaseDC(Handle, HDC);
            User32.DestroyWindow(Handle);
        }

        public static SystemWindowInternal Create(
            string title,
            (int X, int Y) location,
            (int Width, int Height) size)
        {
            var handle = User32.CreateWindow(0, 32770, title, 0, location.X, location.Y, size.Width, size.Height, 0, 0, 0/*instance*/, 0);
            var window = FromHandle(handle);

            return window;
        }

        public static SystemWindowInternal FromHandle(nint handle) => new SystemWindowInternal(handle);
    }
}