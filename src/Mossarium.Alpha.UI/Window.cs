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
        GL.Enable(Cap.Blend);
        GL.BlendFunc(FactorEnum.SrcAlpha, FactorEnum.OneMinusSrcAlpha);

        GL.Clear(ClearMask.Color);
        
        GlPrograms.AP.Use();

        var ubRoundedRectangleData = new UbRoundedRectangleData
        {
            Position = (160, 120),
            Size = (150, 120),
            CornerRadius = 20,
            Color = (1, 0, 0)
        };
        GlUniformBufferRegistry.RoundedRectangleData.Write(ubRoundedRectangleData);

        GL.DrawArrays(Mode.TriangleStrip, 0, 4);

        GDI32.SwapBuffers(DeviceContextHandle);
    }

    public new void Dispose()
    {
        WindowManager.FinalizeWindow(this);
        base.Dispose();
    }

    public static new class Dispatcher
    {
        public static void OnRender(Window window) => window.OnRender();
        public static void OnRendererInitialized(Window window) => window.OnRendererInitialized();
    }
}