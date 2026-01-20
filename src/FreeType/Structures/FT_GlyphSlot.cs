public unsafe struct FT_GlyphSlot
{
    public FT_GlyphSlotRec* rec;
}

public unsafe struct FT_GlyphSlotRec
{
    public FT_Library library;
    public FT_Face face;
    public FT_GlyphSlot next;
    public uint glyph_index;
    public FT_Generic generic;

    public FT_Glyph_Metrics metrics;
    public int linearHoriAdvance;
    public int linearVertAdvance;
    public FT_Vector advance;

    public FT_Glyph_Format format;

    public FT_Bitmap bitmap;
    public int bitmap_left;
    public int bitmap_top;

    public FT_Outline outline;

    public uint num_subglyphs;
    public FT_SubGlyph subglyphs;

    public void* control_data;
    public long control_len;

    public int lsb_delta;
    public int rsb_delta;

    public void* other;

    public FT_Slot_Internal @internal;
}