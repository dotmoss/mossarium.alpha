using static OpenGL.Enums;

namespace OpenGL;

public unsafe class GLEX : GL
{
    GLEX() : base() { }

    public static void InititalizeContextFunctions()
    {
        wglCreateContextAttribsARB = (delegate* unmanaged[SuppressGCTransition]<nint, nint, int*, nint>)GetProcAddress("wglCreateContextAttribsARB"u8);
        wglChoosePixelFormatARB = (delegate* unmanaged[SuppressGCTransition]<nint, int*, float*, uint, int*, uint*, bool>)GetProcAddress("wglChoosePixelFormatARB"u8);
        glCreateShader = (delegate* unmanaged[SuppressGCTransition]<ShaderType, uint>)GetProcAddress("glCreateShader"u8);
        glShaderSource = (delegate* unmanaged[SuppressGCTransition]<uint, int, byte**, int*, void>)GetProcAddress("glShaderSource"u8);
        glCompileShader = (delegate* unmanaged[SuppressGCTransition]<uint, void>)GetProcAddress("glCompileShader"u8);
        glGenVertexArrays = (delegate* unmanaged[SuppressGCTransition]<uint, uint*, void>)GetProcAddress("glGenVertexArrays"u8);
        glGenBuffers = (delegate* unmanaged[SuppressGCTransition]<uint, uint*, void>)GetProcAddress("glGenBuffers"u8);
        glBindVertexArray = (delegate* unmanaged[SuppressGCTransition]<uint, void>)GetProcAddress("glBindVertexArray"u8);
        glBindBuffer = (delegate* unmanaged[SuppressGCTransition]<BufferType, uint, void>)GetProcAddress("glBindBuffer"u8);
        glBufferData = (delegate* unmanaged[SuppressGCTransition]<BufferType, int, void*, BufferUsage, void>)GetProcAddress("glBufferData"u8);
        glBufferSubData = (delegate* unmanaged[SuppressGCTransition]<BufferType, int, int, void*, void>)GetProcAddress("glBufferSubData"u8);
        glVertexAttribPointer = (delegate* unmanaged[SuppressGCTransition]<uint, int, DataType, bool, int, void*, void>)GetProcAddress("glVertexAttribPointer"u8);
        glEnableVertexAttribArray = (delegate* unmanaged[SuppressGCTransition]<uint, void>)GetProcAddress("glEnableVertexAttribArray"u8);
        glUseProgram = (delegate* unmanaged[SuppressGCTransition]<uint, void>)GetProcAddress("glUseProgram"u8);
        glCreateProgram = (delegate* unmanaged[SuppressGCTransition]<uint>)GetProcAddress("glCreateProgram"u8);
        glAttachShader = (delegate* unmanaged[SuppressGCTransition]<uint, uint, void>)GetProcAddress("glAttachShader"u8);
        glLinkProgram = (delegate* unmanaged[SuppressGCTransition]<uint, void>)GetProcAddress("glLinkProgram"u8);
        glGetShaderiv = (delegate* unmanaged[SuppressGCTransition]<uint, ShaderStatusName, int*, void>)GetProcAddress("glGetShaderiv"u8);
        glGetProgramiv = (delegate* unmanaged[SuppressGCTransition]<uint, ProgramStatusName, int*, void>)GetProcAddress("glGetProgramiv"u8);
        glBindBufferBase = (delegate* unmanaged[SuppressGCTransition]<BufferType, uint, uint, void>)GetProcAddress("glBindBufferBase"u8);
        glDeleteShader = (delegate* unmanaged[SuppressGCTransition]<uint, void>)GetProcAddress("glDeleteShader"u8);

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

    static delegate* unmanaged[SuppressGCTransition]<nint, nint, int*, nint> wglCreateContextAttribsARB;
    static delegate* unmanaged[SuppressGCTransition]<nint, int*, float*, uint, int*, uint*, bool> wglChoosePixelFormatARB;
    static delegate* unmanaged[SuppressGCTransition]<ShaderType, uint> glCreateShader;
    static delegate* unmanaged[SuppressGCTransition]<uint, int, byte**, int*, void> glShaderSource;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glCompileShader;
    static delegate* unmanaged[SuppressGCTransition]<uint, uint*, void> glGenVertexArrays;
    static delegate* unmanaged[SuppressGCTransition]<uint, uint*, void> glGenBuffers;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glBindVertexArray;
    static delegate* unmanaged[SuppressGCTransition]<BufferType, uint, void> glBindBuffer;
    static delegate* unmanaged[SuppressGCTransition]<BufferType, int, void*, BufferUsage, void> glBufferData;
    static delegate* unmanaged[SuppressGCTransition]<BufferType, int, int, void*, void> glBufferSubData;
    static delegate* unmanaged[SuppressGCTransition]<uint, int, DataType, bool, int, void*, void> glVertexAttribPointer;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glEnableVertexAttribArray;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glUseProgram;
    static delegate* unmanaged[SuppressGCTransition]<uint> glCreateProgram;
    static delegate* unmanaged[SuppressGCTransition]<uint, uint, void> glAttachShader;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glLinkProgram;
    static delegate* unmanaged[SuppressGCTransition]<uint, ShaderStatusName, int*, void> glGetShaderiv;
    static delegate* unmanaged[SuppressGCTransition]<uint, ProgramStatusName, int*, void> glGetProgramiv;
    static delegate* unmanaged[SuppressGCTransition]<BufferType, uint, uint, void> glBindBufferBase;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glDeleteShader;

    public static void CompileShader(uint shader) => glCompileShader(shader);

    public static void ShaderSource(uint shader, ReadOnlySpan<byte> source)
    { 
        fixed (byte* sourceBytes = source)
            glShaderSource(shader, 1, &sourceBytes, null);
    }

    public static uint CreateShader(ShaderType type) => glCreateShader(type);

    public static nint CreateContextAttribsARB(nint hdc, nint shareContext, int* attributeList) 
        => wglCreateContextAttribsARB(hdc, shareContext, attributeList);

    public static bool ChoosePixelFormatARB(nint hdc, int* iAttributeList, float* fAttributeList, uint maxFormats, int* formats, uint* numFormats)
        => wglChoosePixelFormatARB(hdc, iAttributeList, fAttributeList, maxFormats, formats, numFormats);

    public static void GenerateVertexArrays(uint count, uint* array)
        => glGenVertexArrays(count, array);

    public static void GenerateBuffers(uint count, uint* array)
        => glGenBuffers(count, array);

    public static void BindVertexArray(uint array)
        => glBindVertexArray(array);

    public static void BindBuffer(BufferType targetType, uint array)
        => glBindBuffer(targetType, array);

    public static void BufferData(BufferType targetType, int size, void* data, BufferUsage usage)
        => glBufferData(targetType, size, data, usage);

    public static void BufferData<T>(BufferType targetType, T[] array, BufferUsage usage) 
        where T : unmanaged
    {
        fixed (T* pointer = array)
            glBufferData(targetType, array.Length * sizeof(T), pointer, usage);
    }

    public static void BufferSubData(BufferType targetType, int offset, int size, void* data)
        => glBufferSubData(targetType, offset, size, data);

    public static void VertexAttribPointer(uint index, int size, DataType type, bool normalized, int stride, void* pointer)
        => glVertexAttribPointer(index, size, type, normalized, stride, pointer);

    public static void EnableVertexAttribArray(uint index)
        => glEnableVertexAttribArray(index);

    public static void UseProgram(uint program)
        => glUseProgram(program);

    public static uint CreateProgram()
        => glCreateProgram();

    public static void AttachShader(uint program, uint shader)
        => glAttachShader(program, shader);

    public static void LinkProgram(uint program)
        => glLinkProgram(program);

    public static void GetShaderiv(uint shader, ShaderStatusName name, int* parameters)
        => glGetShaderiv(shader, name, parameters);

    public static void GetProgramiv(uint shader, ProgramStatusName name, int* parameters)
        => glGetProgramiv(shader, name, parameters);

    public static void BindBufferBase(BufferType target, uint baseIndex, uint bufferIndex)
        => glBindBufferBase(target, baseIndex, bufferIndex);

    public static void DeleteShader(uint shader)
        => glDeleteShader(shader);
}