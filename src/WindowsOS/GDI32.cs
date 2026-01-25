using System.Runtime.InteropServices;

namespace WindowsOS;

public static unsafe partial class GDI32
{
    const string gdi = "gdi32";

    #region Imports
    [LibraryImport(gdi), SuppressGCTransition] public static partial
        nint GetStockObject(StockObjects fnObject);

    [LibraryImport(gdi), SuppressGCTransition] public static partial
        int ChoosePixelFormat(nint hdc, PixelFormatDescriptor* ppfd);

    [LibraryImport(gdi), SuppressGCTransition] public static partial
        int DescribePixelFormat(nint hdc, int pixelFormat, uint bytes, PixelFormatDescriptor* ppfd);

    [return: MarshalAs(UnmanagedType.Bool)]
    [LibraryImport(gdi), SuppressGCTransition] public static partial
        bool SetPixelFormat(nint hdc, int format, PixelFormatDescriptor* ppfd);

    [return: MarshalAs(UnmanagedType.Bool)]
    [LibraryImport(gdi), SuppressGCTransition] public static partial
        bool SwapBuffers(nint hdc);
    #endregion
}