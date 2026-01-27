using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlTexture1D : ITexture1D
{
    public GlTexture1D(int width)
    {
        uint id;
        GL.GenerateTextures(1, &id);
        ID = id;

        this.Bind();

        this.SetParameter(TextureParameter.MinFilter, TextureMinFilter.Nearest);
        this.SetParameter(TextureParameter.MagFilter, TextureMagFilter.Nearest);
        this.SetParameter(TextureParameter.WrapS, TextureWrapMode.ClampToEdge);
        this.SetParameter(TextureParameter.BaseLevel, 0);
        this.SetParameter(TextureParameter.MaxLevel, 0);

        this.Storage(width);
    }

    public uint ID { get; private set; }

    public TextureTarget Target => TextureTarget.Texture1D;
    public InternalFormat InternalFormat => InternalFormat.RGBA8;
}