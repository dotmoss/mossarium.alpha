using Mossarium.Alpha.UI.Managers;
using Mossarium.Alpha.Windows;

namespace Mossarium.Alpha;

// TODO
// Implement InstanceManager to allow multiple mossarium windows
// Implement AtlasPacker to pack font glyphs
// Implement OpenGLCache to cache shaders bytecode
unsafe class Program
{
    static void Main()
    {
        var window = new ApplicationWindow();

        WindowManager.InitializeWindow(window);
        window.Visible = true;

        WindowManager.TransferThreadControlToUI();
    }
}

// DEFINES
// Define_UI_Unlimited_FPS