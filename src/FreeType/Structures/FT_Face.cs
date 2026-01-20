public unsafe struct FT_Face
{
    public FT_FaceRec* rec;
}

public unsafe struct FT_FaceRec
{
    public int num_faces;
    public int face_index;

    public int face_flags;
    public int style_flags;

    public int num_glyphs;

    public byte* family_name;
    public byte* style_name;

    public int num_fixed_sizes;
    public FT_Bitmap_Size* available_sizes;

    public int num_charmaps;
    public FT_CharMap* charmaps;

    public FT_Generic generic;

    public FT_BBox bbox;

    public ushort units_per_EM;
    public short ascender;
    public short descender;
    public short height;

    public short max_advance_width;
    public short max_advance_height;

    public short underline_position;
    public short underline_thickness;

    public FT_GlyphSlot glyph;
    public FT_Size size;
    public FT_CharMap charmap;

    /* private fields, internal to FreeType */
    /* ... */
}