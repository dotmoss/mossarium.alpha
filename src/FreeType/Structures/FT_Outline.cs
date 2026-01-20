public unsafe struct FT_Outline
{
    public ushort n_contours;
    public ushort n_points;

    public FT_Vector* points;
    public char* tags;
    public ushort* contours;

    public int flags;
}