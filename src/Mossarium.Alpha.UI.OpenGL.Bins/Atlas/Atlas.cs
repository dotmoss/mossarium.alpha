using DebugProfiler;
using Mossarium.Alpha.UI.OpenGL.Bins.Contracts;
using Mossarium.Alpha.UI.OpenGL.Bins.Internal;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe partial struct Atlas<TBuffer>
    where TBuffer : unmanaged, IGlBufferContract
{
    TBuffer gpuBuffer;
    uint gpuBufferLength;
    BlockList<BinTexture> textures;
    BinTextureCollection textureCollection;

    void Initialize(TBuffer buffer)
    {
        const int InitialBuffer = 2048 * 2048 * 4;

        gpuBuffer = buffer;
        gpuBufferLength = InitialBuffer;
        gpuBuffer.Allocate(gpuBufferLength);
        textures = new BlockList<BinTexture>(16);
        textureCollection = new BinTextureCollection(textures, gpuBufferLength);
    }

    public static Atlas<TBuffer>* Create(TBuffer buffer)
    {
        Profiler.Push(ProfileStage.AtlasInitialization);
        var atlas = (Atlas<TBuffer>*)NativeMemory.Alloc((nuint)sizeof(Atlas<TBuffer>));
        atlas->Initialize(buffer);
        Profiler.Pop();

        return atlas;
    }

    static Atlas()
    {
        Profiler.Register<ProfileStage>("OpenGL.Bins");
    }
}