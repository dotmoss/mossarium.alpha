using System.Diagnostics;

namespace DebugProfiler.Internal;

internal unsafe class Frame
{
    public Frame(Frame? originFrame, int collectionIndex, int stage)
    {
        CollectionIndex = collectionIndex;
        Stage = stage;
        OriginFrame = originFrame;
        Stopwatch = new Stopwatch();
        Derivatives = new List<Frame>();
    }

    public readonly int CollectionIndex;
    public readonly int Stage;
    public readonly Frame? OriginFrame;
    public readonly Stopwatch Stopwatch;
    public readonly List<Frame> Derivatives;
}