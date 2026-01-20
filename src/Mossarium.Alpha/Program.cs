using System.Runtime.InteropServices;
using static FreeType;
using static HarfBuzz;

namespace Mossarium.Alpha;

// TODO
// Implement InstanceManager to allow multiple mossarium windows
unsafe class Program
{
    static void Main()
    {
        const int FT_LOAD_RENDER = 1 << 2;

        FT_Library library;
        FT_Init_FreeType(&library);
        FT_Face face;
        FT_New_Face(library, @"C:\Users\-\AppData\Local\Microsoft\Windows\Fonts\o_Concas.ttf"u8, 0, &face);
        FT_Set_Pixel_Sizes(face, 0, 32);

        hb_font_t* hb_font = hb_ft_font_create(face, null);

        hb_buffer_t* buf = hb_buffer_create();
        hb_buffer_add_utf8(buf, "ffi=>/="u8, -1, 0, -1);
        hb_buffer_guess_segment_properties(buf);

        hb_shape(hb_font, buf, null, 0);

        uint glyph_count;
        hb_glyph_info_t* glyph_info = hb_buffer_get_glyph_infos(buf, &glyph_count);
        hb_glyph_position_t* glyph_pos = hb_buffer_get_glyph_positions(buf, &glyph_count);

        var name = stackalloc byte[64];
        for (uint i = 0; i < glyph_count; i++)
        {
            uint glyph_index = glyph_info[i].codepoint;
            FT_Load_Glyph(face, glyph_index, FT_LOAD_RENDER);

            FT_Get_Glyph_Name(face, glyph_index, name, 64);

            var managed_name = Marshal.PtrToStringAnsi((nint)name);
        }

        hb_buffer_destroy(buf);
        hb_font_destroy(hb_font);

        FT_Done_Face(face);
        FT_Done_FreeType(library);

        //new ApplicationWindow().TransferThreadControl();
    }
}