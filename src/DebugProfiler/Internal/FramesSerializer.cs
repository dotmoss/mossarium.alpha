using System.Text;

namespace DebugProfiler.Internal;

internal static class FramesSerializer
{
    public static string Serialize()
    {
        var rootFrame = FrameCollection.RootFrame;
        var builder = new StringBuilder();

        Serialize(builder, rootFrame, 0);

        return builder.ToString();
    }

    static void Serialize(StringBuilder builder, Frame frame, int depth)
    {
        var elapsedTime = frame.Stopwatch.Elapsed.TotalMilliseconds;
        var derivativesTime = 0d;
        foreach (var derivativeFrame in frame.Derivatives)
        {
            var derivativeElapsedTime = derivativeFrame.Stopwatch.Elapsed.TotalMilliseconds;
            derivativesTime += derivativeElapsedTime;
        }

        var tabulation = new string(' ', depth * 2);
        var collectionName = FrameCollection.CollectionNames[frame.CollectionIndex];
        var stageName = Enum.GetName(FrameCollection.CollectionTypes[frame.CollectionIndex], frame.Stage);
        var name = $"{collectionName}->{stageName}";
        var time = Math.Round(elapsedTime, 2).ToString();
        if (derivativesTime != 0)
            time += $" [{Math.Round(elapsedTime - derivativesTime, 2)}]";

        builder.AppendLine($"{tabulation}{name} {time}");

        foreach (var derivativeFrame in frame.Derivatives)
        {
            Serialize(builder, derivativeFrame, depth + 1);
        }
    }
}