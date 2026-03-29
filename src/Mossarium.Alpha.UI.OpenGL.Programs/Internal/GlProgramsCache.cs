using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL.Programs.Internal;

#pragma warning disable CA2022
file struct GlProgramsCache 
{
    public const int MaxBufferLength = 30720;
}

unsafe ref struct GlProgramsCacheWriter : IDisposable
{
    public void SetFilePath(string cacheFilePath)
    {
        cacheFileStream = new FileStream(cacheFilePath, FileMode.Create, FileAccess.Write, FileShare.Read);
    }

    FileStream cacheFileStream;
    fixed byte buffer[GlProgramsCache.MaxBufferLength];

    public void Write(GlProgram program)
    {
        fixed (byte* bufferPointer = buffer)
        {
            uint binaryFormat;
            var binaryLength = program.ReadBinary(bufferPointer, GlProgramsCache.MaxBufferLength, &binaryFormat);
            cacheFileStream.Write(new ReadOnlySpan<byte>(&binaryLength, sizeof(int)));
            cacheFileStream.Write(new ReadOnlySpan<byte>(&binaryFormat, sizeof(uint)));
            cacheFileStream.Write(new ReadOnlySpan<byte>(bufferPointer, binaryLength));
        }
    }

    public void Dispose()
    {
        cacheFileStream.Dispose();
    }
}

unsafe ref struct GlProgramsCacheReader : IDisposable
{
    public void SetFilePath(string cacheFilePath)
    {
        cacheFileStream = new FileStream(cacheFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    FileStream cacheFileStream;
    fixed byte buffer[GlProgramsCache.MaxBufferLength];

    public GlProgram Read()
    {
        fixed (byte* bufferPointer = buffer)
        {
            int binaryLength;
            uint binaryFormat;
            cacheFileStream.Read(new Span<byte>(&binaryLength, sizeof(int)));
            cacheFileStream.Read(new Span<byte>(&binaryFormat, sizeof(uint)));
            cacheFileStream.Read(new Span<byte>(bufferPointer, binaryLength));

            var program = new GlProgram(bufferPointer, binaryLength, binaryFormat);
            return program;
        }
    }

    public void Dispose()
    {
        cacheFileStream.Dispose();
    }
}