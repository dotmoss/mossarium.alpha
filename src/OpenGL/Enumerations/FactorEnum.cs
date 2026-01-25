namespace OpenGL;

public enum FactorEnum : ushort
{
    Zero = GL_ZERO,
    One = GL_ONE,
    SrcColor = GL_SRC_COLOR,
    OneMinusSrcColor = GL_ONE_MINUS_SRC_COLOR,
    SrcAlpha = GL_SRC_ALPHA,
    OneMinusSrcAlpha = GL_ONE_MINUS_SRC_ALPHA,
    DstAlpha = GL_DST_ALPHA,
    OneMinusDstAlpha = GL_ONE_MINUS_DST_ALPHA,
    DstColor = GL_DST_COLOR,
    OneMinusDstColor = GL_ONE_MINUS_DST_COLOR,
    SrcAlphaSaturate = GL_SRC_ALPHA_SATURATE,
    ConstColor = GL_CONSTANT_COLOR,
    OneMinusConstColor = GL_ONE_MINUS_CONSTANT_COLOR,
    ConstAlpha = GL_CONSTANT_ALPHA,
    OneMinusConstAlpha = GL_ONE_MINUS_CONSTANT_ALPHA
}