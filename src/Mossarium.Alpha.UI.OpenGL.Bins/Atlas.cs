using DebugProfiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe struct Atlas : IDisposable
{
    public const int AtlasWidth = 2048;
    public const int AtlasHeight = 2048;
    public const int PixelBytes = 4;
    public const int PixelBits = 24;
    public const int AtlasByteSize = AtlasWidth * AtlasHeight * PixelBytes;

    public void Dispose()
    {
        NativeMemory.AlignedFree(Unsafe.AsPointer(ref this));
    }

    public static Atlas* Create()
    {
        Profiler.Push(ProfileStage.Atlas16KbAllocation);
        var atlas = (Atlas*)NativeMemory.AlignedAlloc(AtlasByteSize, AtlasByteSize);
        Profiler.Pop();

        return atlas;
    }

    static Atlas()
    {
        Profiler.Register<ProfileStage>("OpenGL.Bins");
    }
}