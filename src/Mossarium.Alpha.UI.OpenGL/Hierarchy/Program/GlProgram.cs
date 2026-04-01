using OpenGL;
using OpenGL.Enumerations;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlProgram : IObjectHandle
{
    public GlProgram(void* binary, int binaryLength, uint binaryFormat)
    {
        ID = GL.CreateProgram();

        GL.ProgramBinary(ID, binaryFormat, binary, binaryLength);

#if DEBUG
        bool isSuccess;
        GL.GetProgramLinkStatus(ID, &isSuccess);
        if (!isSuccess)
            throw null!;
#endif
    }

    public GlProgram(params ReadOnlySpan<GlShader> shaders)
    {
        ID = GL.CreateProgram();
        for (var shaderIndex = 0; shaderIndex < shaders.Length; shaderIndex++)
            shaders[shaderIndex].AttachToProgram(this);

        GL.ProgramParameteri(ID, ProgramParameterName.BinaryRetrievableHint, true);
        GL.LinkProgram(ID);

#if DEBUG
        bool isSuccess;
        GL.GetProgramLinkStatus(ID, &isSuccess);
        if (!isSuccess)
            throw null!;
#endif
    }

    public uint ID { get; private set; }

    public int ReadBinary(void* buffer, int bufferLength, uint* binaryFormat)
    {
#if DEBUG
        bool isSuccess;
        GL.GetProgramBinaryRetrievableHint(ID, &isSuccess);
        if (!isSuccess)
            throw null!;
#endif

        int length;
        GL.GetProgramBinaryLength(ID, &length);
#if DEBUG
        if (length < 0 || length > bufferLength)
            throw null!;
#endif

        GL.GetProgramBinary(ID, length, &length, binaryFormat, buffer);

        var a = GL.GetError();

        return length;
    }

    public void Use()
    {
        GL.UseProgram(ID);
    }

    public void Dispose()
    {
        GL.DeleteProgram(ID);
    }
}