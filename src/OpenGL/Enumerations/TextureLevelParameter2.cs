namespace OpenGL;

public enum TextureLevelParameter2 : ushort
{
    MagFilter = GL_TEXTURE_MAG_FILTER,
    MinFilter = GL_TEXTURE_MIN_FILTER,
    WrapS = GL_TEXTURE_WRAP_S,
    WrapT = GL_TEXTURE_WRAP_T,
    BorderColor = GL_TEXTURE_BORDER_COLOR,
    Priority = GL_TEXTURE_PRIORITY,
    Resident = GL_TEXTURE_RESIDENT
}