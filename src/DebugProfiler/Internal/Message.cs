namespace DebugProfiler.Internal;

class Message
{
    public Message(string text)
    {
        Text = text;
        RegisteredDate = DateTime.Now;
    }

    public readonly string Text;
    public readonly DateTime RegisteredDate;

    public string Serialize() => $"[{RegisteredDate:O}] {Text}";
}