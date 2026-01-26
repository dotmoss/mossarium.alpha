using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlTexture1D : IObjectHandle
{
    public GlTexture1D(int width)
    {
        const int GL_NEAREST = 0x2600;
        Allocate(width);

        GL.TextureParameter(TextureTarget.Texture1D, TextureLevelParameter3.MinFilter, GL_NEAREST);
        GL.TextureParameter(TextureTarget.Texture1D, TextureLevelParameter3.MagFilter, GL_NEAREST);
    }

    public GlTexture1D(int width, int height) : this(width * height) { }

    public uint ID { get; private set; }

    public void Bind()
    {
        GL.BindTexture(TextureParameterTarget.Texture1D, ID);
    }

    public void Active(uint index)
    {
        const uint GL_TEXTURE0 = 0x84C0;

        GL.ActiveTexture(GL_TEXTURE0 + index);
        Bind();
    }

    void Allocate(int width)
    {
        Bind();
        GL.TextureImage(Texture1DTarget.Texture, 0, InternalFormat.RGBA8, width, 0, ImageFormat.RGBA, ImageType.UByte, null);
    }

    public void Reallocate(int width)
    {
        Allocate(width);
    }

    public void Write(void* pixels, int offset, int width)
    {
        GL.TextureSubImage(Texture1DTarget.Texture, 0, offset, width, ImageFormat.RGBA, ImageType.UByte, pixels);
    }

    public void Dispose()
    {
        var id = ID;
        GL.DeleteTextures(1, &id);
    }
}