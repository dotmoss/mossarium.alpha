public unsafe struct FT_Size
{
    public FT_SizeRec* rec;
}

public unsafe struct FT_SizeRec
{
    public FT_Face face;
    public FT_Generic generic;
    public FT_Size_Metrics metrics;
    public FT_Size_Internal @internal;
}