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
        => self.Write(&data, sizeof(TData), 0);

    public static void Write<TBuffer, TData>(this TBuffer self, ReadOnlySpan<TData> data, int offset = 0)
        where TBuffer : IBufferHandle
        where TData : unmanaged
    {
        fixed (TData* dataPointer = data)
            self.Write(dataPointer, sizeof(TData) * data.Length, offset);
    }

    public static void Write<TBuffer>(this TBuffer self, void* data, int length, int offset = 0)
        where TBuffer : IBufferHandle
    {
        self.Bind();
        GL.BufferSubData(self.Type, offset, length, data);
    }
}