using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

#pragma warning disable CS0649
public unsafe struct GlUniformBuffer : IBufferHandle
{
    public GlUniformBuffer(uint bufferId, uint bindingId)
    {
        Initialize(bufferId, bindingId);
    }

    public GlUniformBuffer(uint bindingId)
    {
        uint bufferId;
        GL.GenerateBuffers(1, &bufferId);
        Initialize(bufferId, bindingId);
    }

    public uint ID { get; private set; }
    public BufferType Type => BufferType.Uniform;

    void Initialize(uint bufferId, uint bindingId)
    {
        ID = bufferId;
        GL.BindBuffer(BufferType.Uniform, bufferId);
        GL.BindBufferBase(BufferType.Uniform, bindingId, bufferId);
    }
}

public unsafe struct GlUniformBuffer<TData> : INonFunctionalBufferHandle
    where TData : unmanaged
{
    public GlUniformBuffer(uint bufferId, uint bindingId)
    {
        Initialize(bufferId, bindingId);
    }

    public GlUniformBuffer(uint bindingId)
    {
        uint bufferId;
        GL.GenerateBuffers(1, &bufferId);
        Initialize(bufferId, bindingId);
    }

    public uint ID { get; private set; }
    public BufferType Type => BufferType.Uniform;

    void Initialize(uint bufferId, uint bindingId)
    {
        ID = bufferId;
        GL.BindBuffer(BufferType.Uniform, bufferId);
        GL.BindBufferBase(BufferType.Uniform, bindingId, bufferId);
        GL.BufferData(BufferType.Uniform, sizeof(TData), null, BufferUsage.DynamicDraw);
    }

    public void Write(TData data)
    {
        this.Bind();
        GL.BufferSubData(BufferType.Uniform, 0, sizeof(TData), &data);
    }

    public void Write(TData* data)
    {
        this.Bind();
        GL.BufferSubData(BufferType.Uniform, 0, sizeof(TData), data);
    }
}