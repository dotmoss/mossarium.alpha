using System.Runtime.InteropServices;

public unsafe static class HarfBuzz
{
    const string harfbuzz = "harfbuzz";

    #region DllImports
    [DllImport(harfbuzz), SuppressGCTransition] public static extern
        hb_font_t* hb_ft_font_create(FT_Face ft_face, void* destroy);

    [DllImport(harfbuzz), SuppressGCTransition] public static extern
        hb_buffer_t* hb_buffer_create();
    
    [DllImport(harfbuzz), SuppressGCTransition] public static extern
        void hb_buffer_add_utf8(hb_buffer_t* buffer, byte* text, int text_length, uint item_offset, int item_length);

    [DllImport(harfbuzz), SuppressGCTransition] public static extern
        void hb_buffer_guess_segment_properties(hb_buffer_t* buffer);

    [DllImport(harfbuzz), SuppressGCTransition] public static extern
        void hb_shape(hb_font_t* font, hb_buffer_t* buffer, hb_feature_t* features, uint num_features);

    [DllImport(harfbuzz), SuppressGCTransition] public static extern
        hb_glyph_info_t* hb_buffer_get_glyph_infos(hb_buffer_t* buffer, uint* length);

    [DllImport(harfbuzz), SuppressGCTransition] public static extern
        hb_glyph_position_t* hb_buffer_get_glyph_positions(hb_buffer_t* buffer, uint* length);

    [DllImport(harfbuzz), SuppressGCTransition] public static extern
        void hb_buffer_destroy(hb_buffer_t* buffer);

    [DllImport(harfbuzz), SuppressGCTransition] public static extern
        void hb_font_destroy(hb_font_t* font);
    #endregion

    public static void hb_buffer_add_utf8(hb_buffer_t* buffer, ReadOnlySpan<byte> text, int text_length, uint item_offset, int item_length)
    {
        fixed (byte* textPointer = text)
            hb_buffer_add_utf8(buffer, textPointer, text_length, item_offset, item_length);
    }
}