namespace DebugProfiler.Internal;

static class MessageCollection
{
    public static readonly List<Message> Messages = new List<Message>();

    public static void Register(string messageText)
    {
        var message = new Message(messageText);
        Messages.Add(message);
    }
}