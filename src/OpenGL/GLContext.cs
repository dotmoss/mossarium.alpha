using static OpenGL.Enums;

namespace OpenGL;

public unsafe static class GLContext
{
    public static void Inititalize()
    {
        wglCreateContextAttribsARB = (delegate* unmanaged<nint, nint, int*, nint>)GetProcAddress("wglCreateContextAttribsARB"u8);
        wglChoosePixelFormatARB = (delegate* unmanaged<nint, int*, float*, uint, int*, uint*, bool>)GetProcAddress("wglChoosePixelFormatARB"u8);
        glCreateShader = (delegate* unmanaged<ShaderType, uint>)GetProcAddress("glCreateShader"u8);
        glShaderSource = (delegate* unmanaged<uint, int, byte*, int*, void>)GetProcAddress("glShaderSource"u8);
        glCompileShader = (delegate* unmanaged<uint, void>)GetProcAddress("glCompileShader"u8);

        nint GetProcAddress(ReadOnlySpan<byte> name)
        {
            fixed (byte* nameBytes = name)
            {
                var processAddress = GL.GetProcAddress(nameBytes);
                if (processAddress == 0)
                    throw null!;

                return processAddress;
            }
        }
    }

    static delegate* unmanaged<nint, nint, int*, nint> wglCreateContextAttribsARB;
    static delegate* unmanaged<nint, int*, float*, uint, int*, uint*, bool> wglChoosePixelFormatARB;
    static delegate* unmanaged<ShaderType, uint> glCreateShader;
    static delegate* unmanaged<uint, int, byte*, int*, void> glShaderSource;
    static delegate* unmanaged<uint, void> glCompileShader;

    public static void CompileShader(uint shader) => glCompileShader(shader);

    public static void ShaderSource(uint shader, ReadOnlySpan<byte> source)
    { 
        var length = source.Length;
        fixed (byte* sourceBytes = source)
            glShaderSource(shader, 1, sourceBytes, &length);
    }

    public static uint CreateShader(ShaderType type) => glCreateShader(type);

    public static nint CreateContextAttribsARB(nint hdc, nint shareContext, int* attributeList) 
        => wglCreateContextAttribsARB(hdc, shareContext, attributeList);

    public static bool ChoosePixelFormatARB(nint hdc, int* iAttributeList, float* fAttributeList, uint maxFormats, int* formats, uint* numFormats)
        => wglChoosePixelFormatARB(hdc, iAttributeList, fAttributeList, maxFormats, formats, numFormats);
}