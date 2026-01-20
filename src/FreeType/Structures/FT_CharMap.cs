public unsafe struct FT_CharMap
{
    public FT_CharMapRec* rec;
}

public unsafe struct FT_CharMapRec
{
    public FT_Face face;
    public FT_Encoding encoding;
    public ushort platform_id;
    public ushort encoding_id;
}