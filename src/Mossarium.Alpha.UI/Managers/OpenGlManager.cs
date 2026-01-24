using DebugProfiler;
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
            SetupInitialStates();
            Profiler.Pop();
        }
    }

    static void SetupInitialStates()
    {
        GL.Disable(Cap.DepthTest);
        GL.Disable(Cap.ScissorTest);
        GL.Disable(Cap.StencilTest);

        GL.Enable(Cap.Blend);
        GL.BlendFunc(FactorEnum.SrcAlpha, FactorEnum.OneMinusSrcAlpha);
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