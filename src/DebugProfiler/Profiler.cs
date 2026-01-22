using DebugProfiler.Internal;
using System.Diagnostics;

namespace DebugProfiler;

public static class Profiler
{
#if Enable_Debug_Profiler
    public static void Register<T>(string name)
        where T : struct, Enum => FrameCollection<T>.Register(name);

    public static void Push<T>(T stage)
        where T : struct, Enum => FrameCollection<T>.Push(stage);

    public static void Pop() => FrameCollection.Pop();

    public static void ShowProfiledData()
    {
        var serialized = FramesSerializer.Serialize();
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, serialized);
        Process.Start("notepad.exe", tempFile);
    }
#else
    public static void Register<T>(string name)
        where T : struct, Enum { }

    public static void Push<T>(T stage)
        where T : struct, Enum { }

    public static void Pop() { }

    public static void ShowProfiledData() { }
#endif
}