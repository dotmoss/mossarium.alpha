namespace Mossarium.Alpha.UI.OpenGL;

public struct GlslWindowData
{
    private (float X, float Y) sizeTransformer;

    public (ushort Width, ushort Height) Size { set => sizeTransformer = (131072f / value.Width, 131072f / value.Height); }
}