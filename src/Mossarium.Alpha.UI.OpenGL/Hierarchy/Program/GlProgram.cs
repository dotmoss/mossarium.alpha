using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlProgram : IObjectHandle
{
    public GlProgram(params ReadOnlySpan<GlShader> shaders)
    {
        ID = GL.CreateProgram();
        for (var shaderIndex = 0; shaderIndex < shaders.Length; shaderIndex++)
            shaders[shaderIndex].AttachToProgram(this);
        
        GL.LinkProgram(ID);

#if DEBUG
        bool isSuccess;
        GL.GetProgramLinkStatus(ID, &isSuccess);
        if (!isSuccess)
            throw null!;
#endif
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