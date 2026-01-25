namespace OpenGL;

public enum LogicOpCode : ushort
{
    Clear = GL_CLEAR,
    Set = GL_SET,
    Copy = GL_COPY,
    CopyInverted = GL_COPY_INVERTED,
    Noop = GL_NOOP,
    Invert = GL_INVERT,
    And = GL_AND,
    NAnd = GL_NAND,
    Or = GL_OR,
    Nor = GL_NOR,
    Xor = GL_XOR,
    Equiv = GL_EQUIV,
    AndReverse = GL_AND_REVERSE,
    AndInverted = GL_AND_INVERTED,
    OrReverse = GL_OR_REVERSE,
    OrInverted = GL_OR_INVERTED,
}
