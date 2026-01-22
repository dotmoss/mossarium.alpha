using DebugProfiler;
using Mossarium.Alpha.UI.OpenGL;
using OpenGL;

namespace Mossarium.Alpha.UI.Managers;

public unsafe static class OpenGlManager
{
    static bool isInitialContextInitialized;
    public static void InitializeWindowContext(Window window)
    {
        if (isInitialContextInitialized)
        {
            Profiler.Push(ProfileStage.WindowGlContextInitialization);
            WindowGlContext.InitializeContext(window.DeviceContextHandle);
            Profiler.Pop();
        }
        else
        {
            isInitialContextInitialized = true;

            Profiler.Push(ProfileStage.FirstWindowGlContextInitialization);
            WindowGlContext.InitializeContext(window.DeviceContextHandle);
            GlUniformBufferRegistry.Initialize();
            GlPrograms.Initialize();
            Profiler.Pop();
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