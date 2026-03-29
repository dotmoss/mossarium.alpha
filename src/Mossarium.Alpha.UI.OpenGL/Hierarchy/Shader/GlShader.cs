using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlShader : IObjectHandle
{
    public GlShader(ShaderType type, ReadOnlySpan<byte> sourceCode)
    {
        ID = GL.CreateShader(type);
        GL.ShaderSource(ID, sourceCode);
        GL.CompileShader(ID);

#if DEBUG
        bool success;
        GL.GetShaderCompilationStatus(ID, &success);
        if (!success)
            throw null!;
#endif
    }

    public uint ID { get; private set; }

    public void AttachToProgram(GlProgram program)
    {
        GL.AttachShader(program.ID, ID);
    }

    public void Dispose()
    {
        GL.DeleteShader(ID);
    }
}