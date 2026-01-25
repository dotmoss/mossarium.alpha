namespace OpenGL;

public enum ReadBufferType : ushort
{
    None = GL_NONE,
    FrontLeft = GL_FRONT_LEFT,
    FrontRight = GL_FRONT_RIGHT,
    BackLeft = GL_BACK_LEFT,
    BackRight = GL_BACK_RIGHT,
    Front = GL_FRONT,
    Back = GL_BACK,
    Left = GL_LEFT,
    Right = GL_RIGHT,
    FrontAndBack = GL_FRONT_AND_BACK,
    Aux0 = GL_AUX0,
    Aux1 = GL_AUX1,
    Aux2 = GL_AUX2,
    Aux3 = GL_AUX3
}