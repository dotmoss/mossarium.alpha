using Mossarium.Alpha.UI.Windowing.Structures;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public struct GlslWindowData
{
    private SizeF4 size;
    private SizeF4 sizeTransformer;

    public SizeU2 Size
    { 
        set
        {
            size = new SizeF4(value.Width, value.Height);
            sizeTransformer = new SizeF4(131072f / value.Width, 131072f / value.Height);
        }
    }
}