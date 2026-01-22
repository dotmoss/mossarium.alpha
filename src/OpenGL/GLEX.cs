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
        glDeleteBuffers = (delegate* unmanaged[SuppressGCTransition]<uint, uint*, void>)GetProcAddress("glDeleteBuffers"u8);
        glBindVertexArray = (delegate* unmanaged[SuppressGCTransition]<uint, void>)GetProcAddress("glBindVertexArray"u8);
        glDeleteVertexArray = (delegate* unmanaged[SuppressGCTransition]<uint, uint*, void>)GetProcAddress("glDeleteVertexArrays"u8);
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
        glDeleteProgram = (delegate* unmanaged[SuppressGCTransition]<uint, void>)GetProcAddress("glDeleteProgram"u8);
        glUniform1f = (delegate* unmanaged[SuppressGCTransition]<int, float, void>)GetProcAddress("glUniform1f"u8);
        glUniform2f = (delegate* unmanaged[SuppressGCTransition]<int, float, float, void>)GetProcAddress("glUniform2f"u8);
        glUniform3f = (delegate* unmanaged[SuppressGCTransition]<int, float, float, float, void>)GetProcAddress("glUniform3f"u8);
        glUniform4f = (delegate* unmanaged[SuppressGCTransition]<int, float, float, float, float, void>)GetProcAddress("glUniform4f"u8);
        glPushDebugGroup = (delegate* unmanaged[SuppressGCTransition]<DebugSource, uint, int, byte*, void>)GetProcAddress("glPushDebugGroup"u8);
        glPopDebugGroup = (delegate* unmanaged[SuppressGCTransition]<void>)GetProcAddress("glPopDebugGroup"u8);

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
    static delegate* unmanaged[SuppressGCTransition]<uint, uint*, void> glDeleteBuffers;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glBindVertexArray;
    static delegate* unmanaged[SuppressGCTransition]<uint, uint*, void> glDeleteVertexArray;
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
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glDeleteProgram;
    static delegate* unmanaged[SuppressGCTransition]<int, float, void> glUniform1f;
    static delegate* unmanaged[SuppressGCTransition]<int, float, float, void> glUniform2f;
    static delegate* unmanaged[SuppressGCTransition]<int, float, float, float, void> glUniform3f;
    static delegate* unmanaged[SuppressGCTransition]<int, float, float, float, float, void> glUniform4f;
    static delegate* unmanaged[SuppressGCTransition]<DebugSource, uint, int, byte*, void> glPushDebugGroup;
    static delegate* unmanaged[SuppressGCTransition]<void> glPopDebugGroup;

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

    public static void GenerateBuffers(uint count, uint* arrays)
        => glGenBuffers(count, arrays);

    public static void DeleteBuffers(uint count, uint* array)
        => glDeleteBuffers(count, array);

    public static void BindVertexArray(uint array)
        => glBindVertexArray(array);

    public static void DeleteVertexArray(uint count, uint* arrays)
        => glDeleteVertexArray(count, arrays);

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

    public static void BufferData<T>(BufferType targetType, T data, BufferUsage usage)
        where T : unmanaged
    {
        glBufferData(targetType, sizeof(T), &data, usage);
    }

    public static void BufferData<T>(BufferType targetType, T* data, BufferUsage usage)
        where T : unmanaged
    {
        glBufferData(targetType, sizeof(T), data, usage);
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

    public static void GetShaderCompilationStatus(uint shader, bool* isSuccess)
        => glGetShaderiv(shader, ShaderStatusName.CompileStatus, (int*)isSuccess);

    public static void GetProgramiv(uint program, ProgramStatusName name, int* parameters)
        => glGetProgramiv(program, name, parameters);

    public static void GetProgramLinkStatus(uint program, bool* isSuccess)
        => glGetProgramiv(program, ProgramStatusName.LinkStatus, (int*)isSuccess);

    public static void BindBufferBase(BufferType target, uint baseIndex, uint bufferIndex)
        => glBindBufferBase(target, baseIndex, bufferIndex);

    public static void DeleteShader(uint shader)
        => glDeleteShader(shader);

    public static void DeleteProgram(uint program)
        => glDeleteProgram(program);

    public static void Uniform(int index, float value)
        => glUniform1f(index, value);

    public static void Uniform(int index, float value1, float value2)
        => glUniform2f(index, value1, value2);

    public static void Uniform(int index, float value1, float value2, float value3)
        => glUniform3f(index, value1, value2, value3);

    public static void Uniform(int index, float value1, float value2, float value3, float value4)
        => glUniform4f(index, value1, value2, value3, value4);

    public static void PushDebugGroup(DebugSource source, uint id, int length, byte* message)
        => glPushDebugGroup(source, id, length, message);

    public static void PopDebugGroup()
        => glPopDebugGroup();

    public static void PushDebugGroup(DebugSource source, uint id, ReadOnlySpan<byte> message)
    {
        fixed (byte* messagePointer = message)
            PushDebugGroup(source, id, -1, messagePointer);
    }
}