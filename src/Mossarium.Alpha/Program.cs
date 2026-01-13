using Mossarium.Alpha.Windows;

namespace Mossarium.Alpha;

// TODO
// Implement InstanceManager to allow multiple mossarium windows
class Program
{
    static void Main()
    {
        new ApplicationWindow().TransferThreadControl();
    }
}