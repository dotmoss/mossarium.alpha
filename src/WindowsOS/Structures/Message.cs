namespace WindowsOS;

public struct Message
{
    public nint WindowHandle;
    public WindowMessage WindowMessage;
    public long WParam;
    public long LParam;
    public int Time;
    public long Point;
    public int Private;

    public static Location DecodeLocation(ulong lParam) => new Location(lParam);

    public struct Location
    {
        public Location(ulong lParam)
        {
            X = (int)(lParam & 0xFFFF);
            Y = (int)(lParam >> 16);
        }

        public int X;
        public int Y;
    }

    public static Size DecodeSize(ulong lParam) => new Size(lParam);

    public struct Size
    {
        public Size(ulong lParam)
        {
            Width = (int)(lParam & 0xFFFF);
            Height = (int)(lParam >> 16);
        }

        public int Width; 
        public int Height;
    }

    public static DownKey DecodeDownKey(ulong wParam, ulong lParam) => new DownKey(wParam, lParam);

    public struct DownKey
    {
        public DownKey(ulong wParam, ulong lParam)
        {
            key = (Keys)wParam;
            state = (KeyDownState)(lParam >> 16 & 0xFFFF);
        }

        Keys key;
        KeyDownState state;

        public Keys Key => key;

        public bool IsExtended => (state & KeyDownState.EXTENDED) != 0;
        public bool IsDlgMode => (state & KeyDownState.DLGMODE) != 0;
        public bool IsMenuMode => (state & KeyDownState.MENUMODE) != 0;
        public bool IsAltDown => (state & KeyDownState.ALTDOWN) != 0;
        public bool IsRepeat => (state & KeyDownState.REPEAT) != 0;
        public bool IsUp => (state & KeyDownState.UP) != 0;
    }

    [Flags]
    enum KeyDownState
    {
        EXTENDED = 0x0100,
        DLGMODE = 0x0800,
        MENUMODE = 0x1000,
        ALTDOWN = 0x2000,
        REPEAT = 0x4000,
        UP = 0x8000
    }
};