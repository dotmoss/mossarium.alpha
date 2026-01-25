namespace OpenGL;

public enum ParameterPixelStore : ushort
{
    SwapBytes = GL_PACK_SWAP_BYTES,
    LSBFirst = GL_PACK_LSB_FIRST,
    PackLength = GL_PACK_ROW_LENGTH,
    SkipPixels = GL_PACK_SKIP_PIXELS,
    SkipRows = GL_PACK_SKIP_PIXELS,
    Alignment = GL_PACK_ALIGNMENT
}