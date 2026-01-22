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

    public static void DecodeLocation(ulong lParam, out int x, out int y) => (x, y) = ((int)(lParam & 0xFFFF), (int)(lParam >> 16));

    public static void DecodeSize(ulong lParam, out int width, out int height) => (width, height) = ((int)(lParam & 0xFFFF), (int)(lParam >> 16));

    public static void DecodeDownKey(ulong wParam, ulong lParam, out DownKey key) => key = new DownKey(wParam, lParam);

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

        public bool IsExtended => (state & KeyDownState.Extended) > 0;
        public bool IsDlgMode => (state & KeyDownState.DialogMode) > 0;
        public bool IsMenuMode => (state & KeyDownState.MenuMode) > 0;
        public bool IsAltDown => (state & KeyDownState.AltDown) > 0;
        public bool IsRepeat => (state & KeyDownState.Repeat) > 0;
        public bool IsUp => (state & KeyDownState.Up) > 0;

        enum KeyDownState
        {
            Extended = 0x0100,
            DialogMode = 0x0800,
            MenuMode = 0x1000,
            AltDown = 0x2000,
            Repeat = 0x4000,
            Up = 0x8000
        }
    }
};