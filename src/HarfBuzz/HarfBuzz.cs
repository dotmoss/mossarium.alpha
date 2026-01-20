using System.Runtime.InteropServices;

public unsafe static class HarfBuzz
{
    const string harfbuzz = "libharfbuzz-0";

    #region DllImports
    [DllImport(harfbuzz), SuppressGCTransition]
    public static extern
        uint FT_Init_FreeType(void* library);
    #endregion
}