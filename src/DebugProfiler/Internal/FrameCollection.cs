using System.Diagnostics.CodeAnalysis;

namespace DebugProfiler.Internal;

internal class FrameCollection
{
    protected FrameCollection() { }

    protected static int CollectionCounter;

    public static readonly List<string> CollectionNames = new List<string>();
    public static readonly List<Type> CollectionTypes = new List<Type>();

    [AllowNull] 
    public static Frame RootFrame { get; protected set; }

    [AllowNull]
    protected static Frame CurrentFrame;

    public static void Pop()
    {
        if (CurrentFrame is null)
            throw null!;

        CurrentFrame.Stopwatch.Stop();
        CurrentFrame = CurrentFrame.OriginFrame;
    }
}

internal unsafe class FrameCollection<T> : FrameCollection
    where T : struct, Enum
{
    FrameCollection() { }

    [AllowNull]
    static string collectionName;
    static int collectionIndex;

    public static void Register(string name)
    {
        collectionName = name;
        CollectionNames.Add(name);
        CollectionTypes.Add(typeof(T));
        collectionIndex = CollectionCounter++;
    }

    public static void Push(T stage)
    {
        var derivative = new Frame(CurrentFrame, collectionIndex, *(int*)&stage);
        if (CurrentFrame is null)
        {
            if (RootFrame is not null)
                throw null!;

             RootFrame = derivative;
        }
        else
        {
            CurrentFrame.Derivatives.Add(derivative);
        }

        CurrentFrame = derivative;
        derivative.Stopwatch.Start();
    }
}