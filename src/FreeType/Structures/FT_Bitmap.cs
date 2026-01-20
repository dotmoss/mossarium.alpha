public unsafe struct FT_Bitmap
{
    public uint rows;
    public uint width;
    public int pitch;
    public char* buffer;
    public ushort num_grays;
    public char pixel_mode;
    public char palette_mode;
    public void* palette;
}