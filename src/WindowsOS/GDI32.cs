using System.Runtime.InteropServices;

namespace WindowsOS;

public static unsafe class GDI32
{
    const string gdi = "gdi32";

    #region DllImports
    [DllImport(gdi), SuppressGCTransition] public static extern
        nint GetStockObject(StockObjects fnObject);

    [DllImport(gdi), SuppressGCTransition] public static extern
        int ChoosePixelFormat(nint hdc, PixelFormatDescriptor* ppfd);

    [DllImport(gdi), SuppressGCTransition] public static extern
        int DescribePixelFormat(nint hdc, int pixelFormat, uint bytes, PixelFormatDescriptor* ppfd);

    [DllImport(gdi), SuppressGCTransition] public static extern
        bool SetPixelFormat(nint hdc, int format, PixelFormatDescriptor* ppfd);

    [DllImport(gdi), SuppressGCTransition] public static extern
        bool SwapBuffers(nint hdc);
    #endregion
}