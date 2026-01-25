namespace OpenGL;

public enum TextureLevelParameter : ushort
{
    Width = GL_TEXTURE_WIDTH,
    Height = GL_TEXTURE_HEIGHT,
    InternalFormat = GL_TEXTURE_INTERNAL_FORMAT,
    Border = GL_TEXTURE_BORDER,
    RedSize = GL_TEXTURE_RED_SIZE,
    GreenSize = GL_TEXTURE_GREEN_SIZE,
    BlueSize = GL_TEXTURE_BLUE_SIZE,
    AlphaSize = GL_TEXTURE_ALPHA_SIZE,
    LuminanceSize = GL_TEXTURE_LUMINANCE_SIZE,
    IntensitySize = GL_TEXTURE_INTENSITY_SIZE,
    Components = GL_TEXTURE_COMPONENTS
}