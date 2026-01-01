using Mossarium.Alpha.UI;
using WindowsOS;

namespace Mossarium.Alpha.Windows;

public class ApplicationWindow : Window
{
    static readonly (int Width, int Height) WindowSize = (480, 240);

    public ApplicationWindow() : 
        base(
            title: "Mossarium (alpha)", 
            location: GetWindowLocation(), 
            size: WindowSize
        )
    {
        InitializeWindow();
    }

    static (int X, int Y) GetWindowLocation()
    {
        var screenSize = GetScreenSize();
        var windowSize = WindowSize;

        return ((screenSize.Width - windowSize.Width) / 2, (screenSize.Height - windowSize.Height) / 2);
    }

    static (int Width, int Height) GetScreenSize() => (User32.GetScreenWidth(), User32.GetScreenHeight());
}