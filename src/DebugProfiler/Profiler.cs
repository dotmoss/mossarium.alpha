using System.Diagnostics;
using DebugProfiler.Internal;

namespace DebugProfiler;

public static class Profiler
{
#if Enable_Debug_Profiler
    public const bool IsEnabled = true;

    public static void Register<T>(string name)
        where T : struct, Enum => FrameCollection<T>.Register(name);

    public static void Push<T>(T stage)
        where T : struct, Enum => FrameCollection<T>.Push(stage);

    public static void Pop() => FrameCollection.Pop();

    public static void Log(string message) => MessageCollection.Register(message);

    public static void ShowProfiledData()
    {
        var serializedFrames = FramesSerializer.Serialize();
        var serializedMessages = MessagesSerializer.Serialize();
        var serialized = serializedFrames + "\n" + serializedMessages;
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, serialized);
        Process.Start("notepad.exe", tempFile);
    }
#else
    public const bool IsEnabled = false;

    public static void Register<T>(string name)
        where T : struct, Enum { }

    public static void Push<T>(T stage)
        where T : struct, Enum { }

    public static void Pop() { }

    public static void Log(string message) { }

    public static void ShowProfiledData() { }
#endif
}