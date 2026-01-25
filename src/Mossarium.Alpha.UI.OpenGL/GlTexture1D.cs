using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlTexture1D
{
    public GlTexture1D() => throw null!;

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

    public void Allocate(int width)
    {
        Bind();
        GL.TextureImage(Texture1DTarget.Texture, 0, InternalFormat.RGBA8, width, 0, ImageFormat.RGBA, ImageType.UByte, null);
    }

    public void Write(void* pixels, int offset, int width)
    {
        GL.TextureSubImage(Texture1DTarget.Texture, 0, offset, width, ImageFormat.RGBA, ImageType.UByte, pixels);
    }

    void SetupParameters()
    {
        const int GL_NEAREST = 0x2600;

        GL.TextureParameter(TextureTarget.Texture1D, TextureLevelParameter3.MinFilter, GL_NEAREST);
        GL.TextureParameter(TextureTarget.Texture1D, TextureLevelParameter3.MagFilter, GL_NEAREST);
    }

    public static GlTexture1D Create()
    {
        GlTexture1D texture;
        GL.GenerateTextures(1, (uint*)&texture);
        texture.SetupParameters();

        return texture;
    }

    public static void CreateMultiple(
        GlTexture1D* t1,
        GlTexture1D* t2)
    {
        GenerateMultiple([(nint)t1, (nint)t2]);
    }

    static void GenerateMultiple(ReadOnlySpan<nint> textures)
    {
        var count = textures.Length;
        var ids = stackalloc uint[count];
        GL.GenerateTextures((uint)count, ids);
        fixed (nint* texturesPointer = textures)
        {
            for (var index = 0; index < count; index++)
            {
                var texture = (GlTexture1D*)texturesPointer[index];
                texture->ID = ids[index];
                texture->SetupParameters();
            }
        }
    }

    public static void DisposeMultiple(ReadOnlySpan<GlTexture1D> textures)
    {
        var count = textures.Length;
        var ids = stackalloc uint[count];
        fixed (GlTexture1D* texturesPointer = textures)
        {
            for (var index = 0; index < count; index++)
            {
                ids[index] = texturesPointer[index].ID;
            }
        }
        GL.DeleteTextures((uint)count, ids);
    }
}