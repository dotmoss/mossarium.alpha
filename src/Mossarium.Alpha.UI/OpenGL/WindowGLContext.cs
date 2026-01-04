using OpenGL;
using WindowsOS;
using static WindowsOS.PixelFormatDescriptor;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe class WindowGLContext : IDisposable
{
    public WindowGLContext(nint handleToDeviceContext)
    {
        this.handleToDeviceContext = handleToDeviceContext;
        WindowContextHandle = InitializeWindowContext();
    }

    public readonly nint WindowContextHandle;
    readonly nint handleToDeviceContext;

    nint InitializeWindowContext()
    {
        if (!isOpenGLContextInitialized)
        {
            isOpenGLContextInitialized = true;

            var pixelFormatDescriptor = new PixelFormatDescriptor
            {
                Size = (short)sizeof(PixelFormatDescriptor),
                Version = 1,
                PixelType = PixelTypeEnum.RGBA,
                Flags = FlagsEnum.DrawToWindow | FlagsEnum.SupportOpenGL | FlagsEnum.DoubleBuffer,
                ColorBits = 24,
                AlphaBits = 0,
                LayerType = LayerTypeEnum.MainPlane,
                DepthBits = 0,
                StencilBits = 0
            };
            var pixelFormat = GDI32.ChoosePixelFormat(handleToDeviceContext, &pixelFormatDescriptor);
            if (pixelFormat == 0)
                throw null!;

            if (!GDI32.SetPixelFormat(handleToDeviceContext, pixelFormat, &pixelFormatDescriptor))
                throw null!;

            var tempContext = GL.CreateContext(handleToDeviceContext);
            if (tempContext == 0)
                throw null!;

            if (!GL.MakeCurrent(handleToDeviceContext, tempContext))
                throw null!;

            GLEX.InititalizeNewVersionFunctions();

            if (!GL.MakeCurrent(handleToDeviceContext, 0))
                throw null!;

            if (!GL.DeleteContext(tempContext))
                throw null!;
        }

        var iAttributes = stackalloc int[]
        {
            DRAW_TO_WINDOW_ARB, 1,
            SUPPORT_OPENGL_ARB, 1,
            DOUBLE_BUFFER_ARB, 1,
            ACCELERATION_ARB, FULL_ACCELERATION_ARB,
            PIXEL_TYPE_ARB, TYPE_RGBA_ARB,
            COLOR_BITS_ARB, 24,
            DEPTH_BITS_ARB, 0,
            STENCIL_BITS_ARB, 1,
            0
        };
        var fAttributes = stackalloc float[0];
        int formats;
        uint numFormats;
        if (!GLEX.ChoosePixelFormatARB(handleToDeviceContext, iAttributes, fAttributes, 1, &formats, &numFormats))
            throw null!;

        var gl33Attributes = stackalloc int[]
        {
            CONTEXT_MAJOR_VERSION_ARB, 3,
            CONTEXT_MINOR_VERSION_ARB, 3,
            CONTEXT_PROFILE_MASK_ARB,  CONTEXT_CORE_PROFILE_BIT_ARB,
            0
        };
        var context = GLEX.CreateContextAttribsARB(handleToDeviceContext, 0, gl33Attributes);
        if (context == 0)
            throw null!;

        if (!GL.MakeCurrent(handleToDeviceContext, context))
            throw null!;

        return context;
    }

    public void Dispose()
    {
        if (!GL.MakeCurrent(handleToDeviceContext, 0))
            throw null!;

        if (!GL.DeleteContext(WindowContextHandle))
            throw null!;
    }

    static bool isOpenGLContextInitialized;

    const int
        DRAW_TO_WINDOW_ARB = 0x2001,
        ACCELERATION_ARB = 0x2003,
        SUPPORT_OPENGL_ARB = 0x2010,
        DOUBLE_BUFFER_ARB = 0x2011,
        PIXEL_TYPE_ARB = 0x2013,
        COLOR_BITS_ARB = 0x2014,
        DEPTH_BITS_ARB = 0x2022,
        STENCIL_BITS_ARB = 0x2023,
        FULL_ACCELERATION_ARB = 0x2027,
        TYPE_RGBA_ARB = 0x202B;

    const int
        CONTEXT_MAJOR_VERSION_ARB = 0x2091,
        CONTEXT_MINOR_VERSION_ARB = 0x2092,
        CONTEXT_PROFILE_MASK_ARB = 0x9126,
        CONTEXT_CORE_PROFILE_BIT_ARB = 1;
}