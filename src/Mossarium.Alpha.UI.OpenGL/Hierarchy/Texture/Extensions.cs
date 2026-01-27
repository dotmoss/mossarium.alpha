using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe static class ITextureExtensions
{
    public static void Bind<TTexture>(this TTexture self)
        where TTexture : ITexture
        => GL.BindTexture((TextureParameterTarget)self.Target, self.ID);

    public static void Active<TTexture>(this TTexture self, uint index)
        where TTexture : ITexture
    {
        const uint GL_TEXTURE0 = 0x84C0;

        GL.ActiveTexture(GL_TEXTURE0 + index);
        self.Bind();
    }

    public static void Write<TTexture>(this TTexture self, void* pixels, int offset, int width)
        where TTexture : ITexture
    {
        self.Bind();
        GL.TextureSubImage(Texture1DTarget.Texture, 0, offset, width, ImageFormat.RGBA, ImageType.UByte, pixels);
    }

    public static void Storage<TTexture>(this TTexture self, int width)
        where TTexture : ITexture1D
        => GL.TextureStorage1D(Texture1DTarget.Texture, 1, self.InternalFormat, width);

    public static void Storage<TTexture>(this TTexture self, int width, int height)
        where TTexture : ITexture2D
        => GL.TextureStorage2D(Texture2DTarget.Texture, 1, self.InternalFormat, width, height);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, int value)
        where TTexture : ITexture1D
        => GL.TextureParameter(TextureTarget.Texture1D, parameter, value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, DepthFunction value)
        where TTexture : ITexture1D
        => self.SetParameter(parameter, (int)value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, TextureCompareMode value)
        where TTexture : ITexture1D
        => self.SetParameter(parameter, (int)value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, TextureMagFilter value)
        where TTexture : ITexture1D
        => self.SetParameter(parameter, (int)value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, TextureMinFilter value)
        where TTexture : ITexture1D
        => self.SetParameter(parameter, (int)value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, TextureWrapMode value)
        where TTexture : ITexture1D
        => self.SetParameter(parameter, (int)value);
}

public unsafe static class ITextureExtensions2
{
    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, int value)
        where TTexture : ITexture2D
        => GL.TextureParameter(TextureTarget.Texture2D, parameter, value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, DepthFunction value)
        where TTexture : ITexture2D
        => self.SetParameter(parameter, (int)value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, TextureCompareMode value)
        where TTexture : ITexture2D
        => self.SetParameter(parameter, (int)value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, TextureMagFilter value)
        where TTexture : ITexture2D
        => self.SetParameter(parameter, (int)value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, TextureMinFilter value)
        where TTexture : ITexture2D
        => self.SetParameter(parameter, (int)value);

    public static void SetParameter<TTexture>(this TTexture self, TextureParameter parameter, TextureWrapMode value)
        where TTexture : ITexture2D
        => self.SetParameter(parameter, (int)value);
}