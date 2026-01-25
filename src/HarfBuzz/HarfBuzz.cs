using System.Runtime.InteropServices;

public unsafe static partial class HarfBuzz
{
    const string harfbuzz = "harfbuzz";

    #region Imports
    [LibraryImport(harfbuzz), SuppressGCTransition] public static partial
        hb_font_t* hb_ft_font_create(FT_Face ft_face, void* destroy);

    [LibraryImport(harfbuzz), SuppressGCTransition] public static partial
        hb_buffer_t* hb_buffer_create();

    [LibraryImport(harfbuzz), SuppressGCTransition] public static partial
        void hb_buffer_add_utf8(hb_buffer_t* buffer, byte* text, int text_length, uint item_offset, int item_length);

    [LibraryImport(harfbuzz), SuppressGCTransition] public static partial
        void hb_buffer_guess_segment_properties(hb_buffer_t* buffer);

    [LibraryImport(harfbuzz), SuppressGCTransition] public static partial
        void hb_shape(hb_font_t* font, hb_buffer_t* buffer, hb_feature_t* features, uint num_features);

    [LibraryImport(harfbuzz), SuppressGCTransition] public static partial
        hb_glyph_info_t* hb_buffer_get_glyph_infos(hb_buffer_t* buffer, uint* length);

    [LibraryImport(harfbuzz), SuppressGCTransition] public static partial
        hb_glyph_position_t* hb_buffer_get_glyph_positions(hb_buffer_t* buffer, uint* length);

    [LibraryImport(harfbuzz), SuppressGCTransition] public static partial
        void hb_buffer_destroy(hb_buffer_t* buffer);

    [LibraryImport(harfbuzz), SuppressGCTransition] public static partial
        void hb_font_destroy(hb_font_t* font);
    #endregion

    public static void hb_buffer_add_utf8(hb_buffer_t* buffer, ReadOnlySpan<byte> text, int text_length, uint item_offset, int item_length)
    {
        fixed (byte* textPointer = text)
            hb_buffer_add_utf8(buffer, textPointer, text_length, item_offset, item_length);
    }
}