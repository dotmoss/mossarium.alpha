using static OpenGL.Enums;
using GL = OpenGL.GLEX;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlBuffer : IDisposable
{
    public uint ID { get; private set; }
    public BufferType Type { get; private set; }
    public BufferUsage Usage { get; private set; }

    public void Bind()
    {
        GL.BindBuffer(Type, ID);
    }

    public void Allocate(int length)
    {
        Allocate(null, length);
    }

    public void Allocate<T>(T data)
        where T : unmanaged
    {
        Allocate(&data, sizeof(T));
    }

    public void Allocate<T>(ReadOnlySpan<T> data)
        where T : unmanaged
    {
        fixed (T* dataPointer = data)
            Allocate(dataPointer, data.Length * sizeof(T));
    }

    public void Allocate(void* data, int length)
    {
        Bind();
        GL.BufferData(Type, length, data, Usage);
    }

    public void Write<T>(T data)
        where T : unmanaged
    {
        Write(&data, sizeof(T));
    }

    public void Write<T>(ReadOnlySpan<T> data)
        where T : unmanaged
    {
        fixed (T* dataPointer = data)
        {
            Write(dataPointer, data.Length * sizeof(T));
        }
    }

    public void Write(void* data, int length)
    {
        Bind();
        GL.BufferSubData(Type, 0, length, data);
    }

    public void Write<T>(ReadOnlySpan<T> data, int offset)
        where T : unmanaged
    {
        Bind();
        fixed (T* dataPointer = data)
        {
            GL.BufferSubData(Type, offset, data.Length, dataPointer);
        }
    }

    public void Write(void* data, int length, int offset)
    {
        Bind();
        GL.BufferSubData(Type, offset, length, data);
    }

    public void BindToUniformBase(uint baseIndex)
    {
        Bind();
        GL.BindBufferBase(Type, baseIndex, ID);
    }

    public void Dispose()
    {
        var id = ID;
        GL.DeleteBuffers(1, &id);
    }

    public static GlBuffer From(uint id, BufferType type, BufferUsage usage)
    {
        var buffer = new GlBuffer
        {
            ID = id,
            Type = type,
            Usage = usage
        };

        return buffer;
    }

    public static GlBuffer Create(BufferType type, BufferUsage usage)
    {
        GlBuffer buffer;
        GL.GenerateBuffers(1, (uint*)&buffer);
        (buffer.Type, buffer.Usage) = (type, usage);
        return buffer;
    }

    public static void CreateMultiple(
        GlBuffer* b1, BufferType t1, BufferUsage u1, 
        GlBuffer* b2, BufferType t2, BufferUsage u2)
    {
        GenerateMultiple([(nint)b1, (nint)b2]);
        (b1->Type, b1->Usage) = (t1, u1);
        (b2->Type, b2->Usage) = (t2, u2);
    }

    public static void CreateMultiple(
        GlBuffer* b1, BufferType t1, BufferUsage u1,
        GlBuffer* b2, BufferType t2, BufferUsage u2,
        GlBuffer* b3, BufferType t3, BufferUsage u3)
    {
        GenerateMultiple([(nint)b1, (nint)b2, (nint)b3]);
        (b1->Type, b1->Usage) = (t1, u1);
        (b2->Type, b2->Usage) = (t2, u2);
        (b3->Type, b3->Usage) = (t3, u3);
    }

    public static void CreateMultiple(
        GlBuffer* b1, BufferType t1, BufferUsage u1,
        GlBuffer* b2, BufferType t2, BufferUsage u2,
        GlBuffer* b3, BufferType t3, BufferUsage u3,
        GlBuffer* b4, BufferType t4, BufferUsage u4)
    {
        GenerateMultiple([(nint)b1, (nint)b2, (nint)b3, (nint)b4]);
        (b1->Type, b1->Usage) = (t1, u1);
        (b2->Type, b2->Usage) = (t2, u2);
        (b3->Type, b3->Usage) = (t3, u3);
        (b4->Type, b4->Usage) = (t4, u4);
    }

    public static void CreateMultiple(
        GlBuffer* b1, BufferType t1, BufferUsage u1,
        GlBuffer* b2, BufferType t2, BufferUsage u2,
        GlBuffer* b3, BufferType t3, BufferUsage u3,
        GlBuffer* b4, BufferType t4, BufferUsage u4,
        GlBuffer* b5, BufferType t5, BufferUsage u5)
    {
        GenerateMultiple([(nint)b1, (nint)b2, (nint)b3, (nint)b4, (nint)b5]);
        (b1->Type, b1->Usage) = (t1, u1);
        (b2->Type, b2->Usage) = (t2, u2);
        (b3->Type, b3->Usage) = (t3, u3);
        (b4->Type, b4->Usage) = (t4, u4);
        (b5->Type, b5->Usage) = (t5, u5);
    }

    static void GenerateMultiple(ReadOnlySpan<nint> buffers)
    {
        var count = buffers.Length;
        var ids = stackalloc uint[buffers.Length];
        GL.GenerateBuffers((uint)count, ids);
        fixed (nint* buffersPointer = buffers)
        {
            for (var index = 0; index < count; index++)
            {
                ((GlBuffer*)buffersPointer[index])->ID = ids[index];
            }
        }
    }

    public static void DisposeMultiple(ReadOnlySpan<GlBuffer> buffers)
    {
        var count = buffers.Length;
        var ids = stackalloc uint[buffers.Length];
        fixed (GlBuffer* buffersPointer = buffers)
        {
            for (var index = 0; index < count; index++)
            {
                ids[index] = buffersPointer[index].ID;
            }
        }
        GL.GenerateBuffers((uint)count, ids);
    }
}