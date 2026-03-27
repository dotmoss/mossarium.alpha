using System.Text;

namespace DebugProfiler.Internal;

static class MessagesSerializer
{
    public static string Serialize()
    {
        var builder = new StringBuilder();

        foreach (var message in MessageCollection.Messages)
        {
            builder.AppendLine(message.Serialize());
        }

        return builder.ToString();
    }
}