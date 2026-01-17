using Mossarium.Alpha.UI.Windowing.Structures;
using Mossarium.Alpha.UI.Windows;
using WindowsOS;

namespace Mossarium.Alpha.Windows;

public class ApplicationWindow : Window
{
    static readonly SizeI4 WindowSize = (480, 240);

    public ApplicationWindow() : 
        base(
            title: "Mossarium (alpha)",
            location: GetLocationForScreenCenter(),
            size: WindowSize
        )
    {
        Layered = true;
        InitializeWindow();
    }

    static LocationI4 GetLocationForScreenCenter()
    {
        var screenSize = GetScreenSize();
        var windowSize = WindowSize;

        return new LocationI4((screenSize.Width - windowSize.Width) / 2, (screenSize.Height - windowSize.Height) / 2);
    }

    static SizeI4 GetScreenSize() => new SizeI4(User32.GetScreenWidth(), User32.GetScreenHeight());
}