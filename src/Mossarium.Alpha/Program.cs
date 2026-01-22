using DebugProfiler;
using Mossarium.Alpha.UI.Managers;
using Mossarium.Alpha.Windows;

namespace Mossarium.Alpha;

// TODO
// Implement InstanceManager to allow multiple mossarium windows
// Implement AtlasPacker to pack font glyphs
// Implement OpenGLCache to cache shaders bytecode
// Switch to buffer ranges in uniform buffers
unsafe class Program
{
    static void Main()
    {
        Profiler.Register<ProfileStage>("Application");
        Profiler.Push(ProfileStage.UIInitialization);

        var window = new ApplicationWindow();

        WindowManager.InitializeWindow(window);
        window.Visible = true;

        Profiler.Pop();
        Profiler.ShowProfiledData();

        WindowManager.TransferThreadControlToUI();        
    }
}

// DEFINES
// Define_Enable_Debug_Profiler
// Define_UI_Unlimited_FPS