namespace OpenGL;

public enum ErrorStatus : ushort
{
    NoError = GL_NO_ERROR,
    InvalidEnum = GL_INVALID_ENUM,
    InvalidValue = GL_INVALID_VALUE,
    InvalidOperation = GL_INVALID_OPERATION,
    StackOverflow = GL_STACK_OVERFLOW,
    StackUnderflow = GL_STACK_UNDERFLOW,
    OutOfMemory = GL_OUT_OF_MEMORY
}
