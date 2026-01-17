using static OpenGL.Enums;
using GL = OpenGL.GLEX;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlslShader : IDisposable
{
    public GlslShader(ShaderType type, ReadOnlySpan<byte> sourceCode)
    {
        var shaderId = GL.CreateShader(type);
        GL.ShaderSource(shaderId, sourceCode);
        GL.CompileShader(shaderId);

#if DEBUG
        bool success;
        GL.GetShaderCompilationStatus(shaderId, &success);
        if (!success)
            throw null!;
#endif

        ID = shaderId;
    }

    public uint ID { get; init; }

    public void Dispose()
    {
        GL.DeleteShader(ID);
    }
}