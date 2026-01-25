namespace OpenGL;

public enum PixelType : ushort
{
    ColorIndex = GL_COLOR_INDEX,
    StencilIndex = GL_STENCIL_INDEX,
    DepthComponent = GL_DEPTH_COMPONENT,
    Red = GL_RED,
    Green = GL_GREEN,
    Blue = GL_BLUE,
    Alpha = GL_ALPHA,
    RGB = GL_RGB,
    RGBA = GL_RGBA,
    Luminance = GL_LUMINANCE,
    LuminanceAlpha = GL_LUMINANCE_ALPHA
}