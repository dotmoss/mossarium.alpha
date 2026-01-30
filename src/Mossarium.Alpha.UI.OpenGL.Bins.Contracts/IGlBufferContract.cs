namespace Mossarium.Alpha.UI.OpenGL.Bins.Contracts;

public unsafe interface IGlBufferContract
{
    void Allocate(uint byteCount);
    void Read(void* destination, uint offset, uint length);
    void Write(void* source, uint offset, uint length);
}