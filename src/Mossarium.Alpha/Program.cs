using System.Runtime.InteropServices;
using static FreeType;

namespace Mossarium.Alpha;

// TODO
// Implement InstanceManager to allow multiple mossarium windows
unsafe class Program
{
    static void Main()
    {
        FT_Library library;
        FT_Init_FreeType(&library);
        FT_Face face;
        FT_New_Face(library, @"C:\Users\-\AppData\Local\Microsoft\Windows\Fonts\o_Concas.ttf"u8, 0, &face);
        FT_Set_Pixel_Sizes(face, 0, 32);

        byte* glyph_name = stackalloc byte[64];
        for (uint gindex = 0; gindex < face.rec->num_glyphs; gindex++)
        {
            const int FT_LOAD_NO_SCALE = 1 << 0;

            if (FT_Load_Glyph(face, gindex, FT_LOAD_NO_SCALE) is not FT_Error.NoError)
                continue;

            if (FT_HAS_GLYPH_NAMES(face))
            {
                FT_Get_Glyph_Name(face, gindex, glyph_name, 64);
            }
            else
            {
                "no-name"u8.CopyTo(new Span<byte>(glyph_name, 64));
            }

            var name = Marshal.PtrToStringAnsi((nint)glyph_name);
            Console.WriteLine($"ID: {gindex} Name: {name}");
        }

        _ = 3;

        FT_Done_Face(face);
        FT_Done_FreeType(library);

        //new ApplicationWindow().TransferThreadControl();
    }
}