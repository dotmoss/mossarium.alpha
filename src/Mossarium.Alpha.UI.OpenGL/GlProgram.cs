using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlProgram : IDisposable
{
    public GlProgram(params ReadOnlySpan<GlShader> shaders)
    {
        var programId = GL.CreateProgram();
        for (var shaderIndex = 0; shaderIndex < shaders.Length; shaderIndex++)
            GL.AttachShader(programId, shaders[shaderIndex].ID);
        
        GL.LinkProgram(programId);

#if DEBUG
        bool isSuccess;
        GL.GetProgramLinkStatus(programId, &isSuccess);
        if (!isSuccess)
            throw null!;
#endif

        ID = programId;
    }

    public uint ID { get; private set; }

    public void Use()
    {
        GL.UseProgram(ID);
    }

    public void Dispose()
    {
        GL.DeleteProgram(ID);
    }
}