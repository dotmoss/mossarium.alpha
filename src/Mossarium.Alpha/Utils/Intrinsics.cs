using System.Runtime.Intrinsics.X86;
using WindowsOS;

namespace Mossarium.Alpha.Utils;

public static class Intrinsics
{
    public static void Verify()
    {
        if (!Avx2.X64.IsSupported)
        {
            User32.MessageBox("AVX2 is not supported.");
        }

        if (!Bmi2.X64.IsSupported)
        {
            User32.MessageBox("BMI2 is not supported.");
        }
    }
}