using Mossarium.Alpha.UI.OpenGL.Bins.Contracts;
using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

#pragma warning disable CS0649
public unsafe struct GlTextureBuffer : IBufferHandle, IGlBufferContract
{
    public GlTextureBuffer()
    {
        uint id;
        GL.GenerateBuffers(1, &id);
        ID = id;
    }

    public uint ID { get; private set; }
    public BufferType Type => BufferType.Texture;
    
    public TTexture DefineTexture<TTexture>()
        where TTexture : ITexture1D, new()
    {
        var texture = new TTexture();
        texture.Bind();

        GL.TextureBuffer(TextureBufferTarget.TextureBuffer, texture.InternalFormat, ID);

        return texture;
    }

    public GlBufferTextureRgb8 DefineTextureRgb8()
        => DefineTexture<GlBufferTextureRgb8>();

    public GlBufferTextureGrayScale DefineTextureGrayScale()
        => DefineTexture<GlBufferTextureGrayScale>();

    void IGlBufferContract.Allocate(uint byteCount)
    {
        IBufferHandleExtension.Allocate(this, (int)byteCount);
    }

    void IGlBufferContract.Read(void* destination, uint offset, uint length)
    {
        IBufferHandleExtension.Read(this, destination, (int)offset, (int)length);
    }

    void IGlBufferContract.Write(void* source, uint offset, uint length)
    {
        IBufferHandleExtension.Write(this, source, (int)offset, (int)length);
    }
}