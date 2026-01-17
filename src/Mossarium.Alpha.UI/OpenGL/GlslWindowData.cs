using Mossarium.Alpha.UI.Windowing.Structures;

namespace Mossarium.Alpha.UI.OpenGL;

public struct GlslWindowData
{
    private SizeF4 sizeTransformer;

    public SizeU2 Size { set => sizeTransformer = new SizeF4(131072f / value.Width, 131072f / value.Height); }
}