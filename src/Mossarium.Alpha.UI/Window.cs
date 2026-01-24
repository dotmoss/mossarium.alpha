using DebugProfiler;
using Mossarium.Alpha.UI.Managers;
using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing;
using Mossarium.Alpha.UI.Windowing.Structures;
using OpenGL;
using WindowsOS;
using static OpenGL.Enums;

namespace Mossarium.Alpha.UI;

public unsafe class Window : SystemWindow
{
    public Window(string title, LocationI4 location, SizeI4 size, WindowInitialAttributes attributes = WindowInitialAttributes.None)
        : base(title, location, size, attributes)
    {

    }

    public WindowManager.PrivateWindowManagerData PrivateWindowManagerData;

    protected override void OnMouseLeftDown()
    {
        SendMessage(WindowMessage.NcLButtonDown, 0x02, 0x00);
    }

    protected virtual void OnRendererInitialized()
    {
        vertexArray = GlVertexArray<GlVertex, ushort>.Create();
    }

    GlVertexArray<GlVertex, ushort> vertexArray;
    protected virtual void OnRender()
    {
        OpenGlManager.MakeWindowCurrent(this);

        var ubWindowData = new UbWindowData
        {
            Size = ((ushort)Size.Width, (ushort)Size.Height)
        };
        GlUniformBufferRegistry.WindowData.Write(ubWindowData);

        vertexArray.Bind();

        GL.ClearColor(1f / 256 * 31, 1f / 256 * 31, 1f / 256 * 31, 1f);
        GL.Clear(ClearMask.Color);

        GlPrograms.TransparentWindowCorners.Use();

        GL.DrawArrays(Mode.Triangles, 0, 12);

        GDI32.SwapBuffers(DeviceContextHandle);
    }

    public new void Dispose()
    {
        WindowManager.FinalizeWindow(this);
        base.Dispose();
    }
    
    static Window()
    {
        Profiler.Register<ProfileStage>("UserInterface");
    }

    public static new class Dispatcher
    {
        public static void OnRender(Window window) => window.OnRender();
        public static void OnRendererInitialized(Window window) => window.OnRendererInitialized();
    }
}