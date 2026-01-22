using Mossarium.Alpha.UI.OpenGL;
using static OpenGL.Enums;
using GL = OpenGL.GLEX;

namespace Mossarium.Alpha.UI.Managers;

public static class OpenGlManager
{
    static bool isInitialized;
    public static void NotifyNewWindow(Window window)
    {
        if (isInitialized)
            return;
        isInitialized = true;

        WindowGlContext.Initialize(window.DeviceContextHandle);
        GlUniformBufferRegistry.Initialize();
        GlslImpl.Initialize();
    }


    static nint activeHdc;
    public static void SetActiveContext(Window window)
    {
        var hdc = window.DeviceContextHandle;
        if (hdc == activeHdc)
            return;

        activeHdc = hdc;
        GL.MakeCurrent(hdc, WindowGlContext.Handle);
    }
}