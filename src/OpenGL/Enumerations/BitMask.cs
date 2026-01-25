namespace OpenGL;

public enum BitMask : int
{
    Current = GL_CURRENT_BIT,
    Point = GL_POINT_BIT,
    Line = GL_LINE_BIT,
    Polygon = GL_POLYGON_BIT,
    PolygonStipple = GL_POLYGON_STIPPLE_BIT,
    Pixel = GL_PIXEL_MODE_BIT,
    Lighting = GL_LIGHTING_BIT,
    Log = GL_FOG_BIT,
    Depth = GL_DEPTH_BUFFER_BIT,
    Accum = GL_ACCUM_BUFFER_BIT,
    Stencil = GL_STENCIL_BUFFER_BIT,
    Viewport = GL_VIEWPORT_BIT,
    Transform = GL_TRANSFORM_BIT,
    Enable = GL_ENABLE_BIT,
    Color = GL_COLOR_BUFFER_BIT,
    Hint = GL_HINT_BIT,
    Eval = GL_EVAL_BIT,
    List = GL_LIST_BIT,
    Texture = GL_TEXTURE_BIT,
    Scissor = GL_SCISSOR_BIT,
    All = GL_ALL_ATTRIB_BITS
}