using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlBufferTextureGrayScale : ITexture1D
{
    public GlBufferTextureGrayScale(int width)
    {
        uint id;
        GL.GenerateTextures(1, &id);
        ID = id;
    }

    public uint ID { get; private set; }

    public TextureTarget Target => TextureTarget.TextureBuffer;
    public InternalFormat InternalFormat => InternalFormat.R8;
}