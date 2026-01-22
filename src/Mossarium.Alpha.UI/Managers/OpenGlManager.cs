using Mossarium.Alpha.UI.OpenGL;
using OpenGL;
using static OpenGL.Enums;

namespace Mossarium.Alpha.UI.Managers;

public unsafe static class OpenGlManager
{
    static bool isInitialContextInitialized;
    public static void InitializeWindowContext(Window window)
    {
        if (isInitialContextInitialized)
        {
            WindowGlContext.InitializeSlaveContext(window.DeviceContextHandle);
        }
        else
        {
            isInitialContextInitialized = true;
            WindowGlContext.InitializeMasterContext(window.DeviceContextHandle);
            GlUniformBufferRegistry.Initialize();
            GlPrograms.Initialize();
        }
    }

    static nint currentDeviceContextHandle;
    public static void MakeWindowCurrent(Window window)
    {
        var deviceContextHandle = window.DeviceContextHandle;
        if (deviceContextHandle == currentDeviceContextHandle)
            return;

        currentDeviceContextHandle = deviceContextHandle;
        GL.MakeCurrent(deviceContextHandle, WindowGlContext.ContextHandle);
    }
}