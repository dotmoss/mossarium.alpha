using DebugProfiler;
using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing.Structures;
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
            SetupInitialStates();
            Profiler.Pop();
        }
    }

    static void SetupInitialStates()
    {
        GL.Disable(StateCap.DepthTest);
        GL.Disable(StateCap.ScissorTest);
        GL.Disable(StateCap.StencilTest);

        GL.Enable(StateCap.Blend);
        GL.BlendFunc(FactorEnum.SrcAlpha, FactorEnum.OneMinusSrcAlpha);

        var clearColor = new Rgb(31, 31, 31);
        GL.ClearColor(1f / 256 * clearColor.R, 1f / 256 * clearColor.G, 1f / 256 * clearColor.B, 1f);
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