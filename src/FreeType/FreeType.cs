using System.Runtime.InteropServices;

public unsafe static class FreeType
{
    const string freetype = "freetype";

    #region DllImports
    [DllImport(freetype), SuppressGCTransition] public static extern
        FT_Error FT_Init_FreeType(FT_Library* library);

    [DllImport(freetype), SuppressGCTransition] public static extern
        FT_Error FT_New_Face(FT_Library library, byte* pathname, uint face_index, FT_Face* aface);

    [DllImport(freetype), SuppressGCTransition] public static extern
        FT_Error FT_Set_Pixel_Sizes(FT_Face face, uint pixel_width, uint pixel_height);

    [DllImport(freetype), SuppressGCTransition] public static extern
        FT_Error FT_Load_Glyph(FT_Face face, uint glyph_index, int loadFlags);

    [DllImport(freetype), SuppressGCTransition] public static extern
        FT_Error FT_Get_Glyph_Name(FT_Face face, uint glyph_index, void* buffer, uint buffer_max);

    [DllImport(freetype), SuppressGCTransition] public static extern
        FT_Error FT_Done_Face(FT_Face face);

    [DllImport(freetype), SuppressGCTransition] public static extern
        FT_Error FT_Done_FreeType(FT_Library library);
    #endregion

    public static FT_Error FT_New_Face(FT_Library library, ReadOnlySpan<byte> pathname, uint face_index, FT_Face* aface)
    {
        fixed (byte* pathnamePointer = pathname)
            return FT_New_Face(library, pathnamePointer, face_index, aface);
    }

    public static FT_Error FT_Get_Glyph_Name(FT_Face face, uint glyph_index, Span<byte> buffer)
    {
        fixed (byte* bufferPointer = buffer)
            return FT_Get_Glyph_Name(face, glyph_index, bufferPointer, (uint)buffer.Length);
    }
    
    const int FT_FACE_FLAG_GLYPH_NAMES = 1 << 9;
    public static bool FT_HAS_GLYPH_NAMES(FT_Face face) => (face.rec->face_flags & FT_FACE_FLAG_GLYPH_NAMES) != 0;
}