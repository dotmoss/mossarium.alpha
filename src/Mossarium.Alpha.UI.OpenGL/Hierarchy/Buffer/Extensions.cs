using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe static class IBufferHandleExtension
{
    public static void Bind<TBuffer>(this TBuffer self)
        where TBuffer : INonFunctionalBufferHandle
        => GL.BindBuffer(self.Type, self.ID);

    public static void Allocate<TBuffer>(this TBuffer self, int length)
        where TBuffer : IBufferHandle
        => self.Allocate(null, length);

    public static void Allocate<TBuffer, TData>(this TBuffer self, TData data)
        where TBuffer : IBufferHandle
        where TData : unmanaged
        => self.Allocate(&data, sizeof(TData));

    public static void Allocate<TBuffer, TData>(this TBuffer self, ReadOnlySpan<TData> data)
        where TBuffer : IBufferHandle
        where TData : unmanaged
    {
        fixed (TData* dataPointer = data)
            self.Allocate(dataPointer, data.Length * sizeof(TData));
    }

    public static void Allocate<TBuffer>(this TBuffer self, void* data, int length)
        where TBuffer : IBufferHandle
    {
        self.Bind();
        GL.BufferData(self.Type, length, data, BufferUsage.DynamicDraw);
    }

    public static void Write<TBuffer, TData>(this TBuffer self, TData data)
        where TBuffer : IBufferHandle
        where TData : unmanaged
        => self.Write(&data, 0, sizeof(TData));

    public static void Write<TBuffer, TData>(this TBuffer self, ReadOnlySpan<TData> data)
        where TBuffer : IBufferHandle
        where TData : unmanaged
    {
        fixed (TData* dataPointer = data)
            self.Write(dataPointer, 0, sizeof(TData) * data.Length);
    }

    public static void Write<TBuffer>(this TBuffer self, void* data, int offset, int length)
        where TBuffer : IBufferHandle
    {
        self.Bind();
        GL.BufferSubData(self.Type, offset, length, data);
    }

    public static void Read<TBuffer, TData>(this TBuffer self, TData data)
        where TBuffer : IBufferHandle
        where TData : unmanaged
        => self.Read(&data, 0, sizeof(TData));

    public static void Read<TBuffer, TData>(this TBuffer self, ReadOnlySpan<TData> data, int offset)
        where TBuffer : IBufferHandle
        where TData : unmanaged
    {
        fixed (TData* dataPointer = data)
            self.Read(dataPointer, offset, sizeof(TData) * data.Length);
    }

    public static void Read<TBuffer>(this TBuffer self, void* data, int offset, int length)
        where TBuffer : IBufferHandle
    {
        self.Bind();
        GL.GetBufferSubData(self.Type, offset, length, data);
    }
}