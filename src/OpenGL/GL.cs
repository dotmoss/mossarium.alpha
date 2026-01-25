using DebugProfiler;
using System.Runtime.InteropServices;
using WindowsOS;
using static OpenGL.Enums;

namespace OpenGL;

public unsafe static class GL
{
    public static void InititalizeContextFunctions()
    {
        Profiler.Register<ProfileStage>("OpenGL");
        Profiler.Push(ProfileStage.InititalizeContextFunctions);

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
        glVertexAttribIPointer = (delegate* unmanaged[SuppressGCTransition]<uint, int, DataType, int, void*, void>)GetProcAddress("glVertexAttribIPointer"u8);
        glVertexAttribDivisor = (delegate* unmanaged[SuppressGCTransition]<uint, uint, void>)GetProcAddress("glVertexAttribDivisor"u8);
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
        glUniform1i = (delegate* unmanaged[SuppressGCTransition]<int, int, void>)GetProcAddress("glUniform1i"u8);
        glUniform2f = (delegate* unmanaged[SuppressGCTransition]<int, float, float, void>)GetProcAddress("glUniform2f"u8);
        glUniform3f = (delegate* unmanaged[SuppressGCTransition]<int, float, float, float, void>)GetProcAddress("glUniform3f"u8);
        glUniform4f = (delegate* unmanaged[SuppressGCTransition]<int, float, float, float, float, void>)GetProcAddress("glUniform4f"u8);
        glPushDebugGroup = (delegate* unmanaged[SuppressGCTransition]<DebugSource, uint, int, byte*, void>)GetProcAddress("glPushDebugGroup"u8);
        glPopDebugGroup = (delegate* unmanaged[SuppressGCTransition]<void>)GetProcAddress("glPopDebugGroup"u8);
        glActiveTexture = (delegate* unmanaged[SuppressGCTransition]<uint, void>)GetProcAddress("glActiveTexture"u8);

        Profiler.Pop();

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
    static delegate* unmanaged[SuppressGCTransition]<uint, int, DataType, int, void*, void> glVertexAttribIPointer;
    static delegate* unmanaged[SuppressGCTransition]<uint, uint, void> glVertexAttribDivisor;
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
    static delegate* unmanaged[SuppressGCTransition]<int, int, void> glUniform1i;
    static delegate* unmanaged[SuppressGCTransition]<int, float, float, void> glUniform2f;
    static delegate* unmanaged[SuppressGCTransition]<int, float, float, float, void> glUniform3f;
    static delegate* unmanaged[SuppressGCTransition]<int, float, float, float, float, void> glUniform4f;
    static delegate* unmanaged[SuppressGCTransition]<DebugSource, uint, int, byte*, void> glPushDebugGroup;
    static delegate* unmanaged[SuppressGCTransition]<void> glPopDebugGroup;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glActiveTexture;

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

    public static void VertexAttribIPointer(uint index, int size, DataType type, int stride, void* pointer)
        => glVertexAttribIPointer(index, size, type, stride, pointer);

    public static void VertexAttribDivisor(uint index, uint divisor)
        => glVertexAttribDivisor(index, divisor);

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

    public static void Uniform(int index, int value)
        => glUniform1i(index, value);

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

    public static void ActiveTexture(uint texture)
        => glActiveTexture(texture);

    public static void PushDebugGroup(DebugSource source, uint id, ReadOnlySpan<byte> message)
    {
        fixed (byte* messagePointer = message)
            PushDebugGroup(source, id, -1, messagePointer);
    }

    [DllImport(gl, EntryPoint = "glBindTexture"), SuppressGCTransition]
    public static extern void BindTexture(TexPTarget target, uint texture);

    [DllImport(gl, EntryPoint = "glBlendFunc"), SuppressGCTransition]
    public static extern void BlendFunc(FactorEnum sfactor, FactorEnum dfactor);

    [DllImport(gl, EntryPoint = "glClear"), SuppressGCTransition]
    public static extern void Clear(ClearMask mask);

    [DllImport(gl, EntryPoint = "glClearColor"), SuppressGCTransition]
    public static extern void ClearColor(float red, float green, float blue, float alpha);

    [DllImport(gl, EntryPoint = "glClearDepth"), SuppressGCTransition]
    public static extern void ClearDepth(double depth);

    [DllImport(gl, EntryPoint = "glClearStencil"), SuppressGCTransition]
    public static extern void ClearStencil(int s);

    [DllImport(gl, EntryPoint = "glColorMask"), SuppressGCTransition]
    public static extern void ColorMask(byte red, byte green, byte blue, byte alpha);

    [DllImport(gl, EntryPoint = "glCopyTexImage1D"), SuppressGCTransition]
    public static extern void CopyTexImage1D(TexPTarget target, int level, InternalFormat internalFormat, int x, int y, int width, int border);

    [DllImport(gl, EntryPoint = "glCopyTexImage2D"), SuppressGCTransition]
    public static extern void CopyTexImage2D(TexPTarget target, int level, InternalFormat internalFormat, int x, int y, int width, int height, int border);

    [DllImport(gl, EntryPoint = "glCopyTexSubImage1D"), SuppressGCTransition]
    public static extern void CopyTexSubImage1D(TexPTarget target, int level, int xoffset, int x, int y, int width);

    [DllImport(gl, EntryPoint = "glCopyTexSubImage2D"), SuppressGCTransition]
    public static extern void CopyTexSubImage2D(TexPTarget target, int level, int xoffset, int yoffset, int x, int y, int width, int height);

    [DllImport(gl, EntryPoint = "glCullFace"), SuppressGCTransition]
    public static extern void CullFace(FaceEnum mode);

    [DllImport(gl, EntryPoint = "glDeleteTextures"), SuppressGCTransition]
    public static extern void DeleteTextures(uint n, uint* textures);

    [DllImport(gl, EntryPoint = "glDepthFunc"), SuppressGCTransition]
    public static extern void DepthFunc(Func func);

    [DllImport(gl, EntryPoint = "glDepthMask"), SuppressGCTransition]
    public static extern void DepthMask(byte flag);

    [DllImport(gl, EntryPoint = "glDepthRange"), SuppressGCTransition]
    public static extern void DepthRange(double zNear, double zFar);

    [DllImport(gl, EntryPoint = "glDisable"), SuppressGCTransition]
    public static extern void Disable(Cap cap);

    [DllImport(gl, EntryPoint = "glDrawArrays"), SuppressGCTransition]
    public static extern void DrawArrays(Mode mode, int first, int count);

    [DllImport(gl, EntryPoint = "glDrawBuffer"), SuppressGCTransition]
    public static extern void DrawBuffer(Mode mode);

    [DllImport(gl, EntryPoint = "glDrawElements"), SuppressGCTransition]
    public static extern void DrawElements(Mode mode, int count, BUType type, nint offset);

    [DllImport(gl, EntryPoint = "glEdgeFlag"), SuppressGCTransition]
    public static extern void EdgeFlag(byte flag);

    [DllImport(gl, EntryPoint = "glEnable"), SuppressGCTransition]
    public static extern void Enable(Cap cap);

    [DllImport(gl, EntryPoint = "glFinish"), SuppressGCTransition]
    public static extern void Finish();

    [DllImport(gl, EntryPoint = "glFlush"), SuppressGCTransition]
    public static extern void Flush();

    [DllImport(gl, EntryPoint = "glFrontFace"), SuppressGCTransition]
    public static extern void FrontFace(FaceMode mode);

    [DllImport(gl, EntryPoint = "glGenTextures"), SuppressGCTransition]
    public static extern void GenTextures(uint n, uint* textures);

    [DllImport(gl, EntryPoint = "glGetBooleanv"), SuppressGCTransition]
    public static extern void GetBoolean(PName pname, byte* @params);

    [DllImport(gl, EntryPoint = "glGetDoublev"), SuppressGCTransition]
    public static extern void GetDouble(PName pname, double* @params);

    [DllImport(gl, EntryPoint = "glGetError"), SuppressGCTransition]
    public static extern Status GetError();

    [DllImport(gl, EntryPoint = "glGetFloatv"), SuppressGCTransition]
    public static extern void GetFloat(PName pname, float* @params);

    [DllImport(gl, EntryPoint = "glGetIntegerv"), SuppressGCTransition]
    public static extern void GetInteger(PName pname, int* @params);

    [DllImport(gl, EntryPoint = "glGetString"), SuppressGCTransition]
    public static extern byte* GetString(StringName name);

    [DllImport(gl, EntryPoint = "glGetTexImage"), SuppressGCTransition]
    public static extern void GetTexImage(TexTarget target, int level, ImagePixelType format, BType type, nint pixels);

    [DllImport(gl, EntryPoint = "glGetTexLevelParameterfv"), SuppressGCTransition]
    public static extern void GetTexLevelParameter(TexPTarget target, int level, TexN pname, float* @params);

    [DllImport(gl, EntryPoint = "glGetTexLevelParameteriv"), SuppressGCTransition]
    public static extern void GetTexLevelParameter(TexPTarget target, int level, TexN pname, int* @params);

    [DllImport(gl, EntryPoint = "glGetTexParameterfv"), SuppressGCTransition]
    public static extern void GetTexParameter(TexTarget target, TexNV pname, float* @params);

    [DllImport(gl, EntryPoint = "glGetTexParameteriv"), SuppressGCTransition]
    public static extern void GetTexParameter(TexTarget target, TexNV pname, int* @params);

    [DllImport(gl, EntryPoint = "glHint"), SuppressGCTransition]
    public static extern void Hint(Hint target, CalcType mode);

    [DllImport(gl, EntryPoint = "glIsEnabled"), SuppressGCTransition]
    public static extern bool IsEnabled(Cap cap);

    [DllImport(gl, EntryPoint = "glIsTexture"), SuppressGCTransition]
    public static extern byte IsTexture(uint texture);

    [DllImport(gl, EntryPoint = "glLineWidth"), SuppressGCTransition]
    public static extern void LineWidth(float width);

    [DllImport(gl, EntryPoint = "glLogicOp"), SuppressGCTransition]
    public static extern void LogicOp(OpCode opcode);

    [DllImport(gl, EntryPoint = "glPixelStoref"), SuppressGCTransition]
    public static extern void PixelStore(StoreN pname, float param);

    [DllImport(gl, EntryPoint = "glPixelStorei"), SuppressGCTransition]
    public static extern void PixelStore(StoreN pname, int param);

    [DllImport(gl, EntryPoint = "glPointSize"), SuppressGCTransition]
    public static extern void PointSize(float size);

    [DllImport(gl, EntryPoint = "glPolygonMode"), SuppressGCTransition]
    public static extern void PolygonMode(MaterialFace face, MeshType mode);

    [DllImport(gl, EntryPoint = "glPolygonOffset"), SuppressGCTransition]
    public static extern void PolygonOffset(float factor, float units);

    [DllImport(gl, EntryPoint = "glReadBuffer"), SuppressGCTransition]
    public static extern void ReadBuffer(BufType mode);

    [DllImport(gl, EntryPoint = "glReadPixels"), SuppressGCTransition]
    public static extern void ReadPixels(int x, int y, int width, int height, ImagePixelType format, ReadType type, nint pixels);

    [DllImport(gl, EntryPoint = "glScissor"), SuppressGCTransition]
    public static extern void Scissor(int x, int y, int width, int height);

    [DllImport(gl, EntryPoint = "glStencilFunc"), SuppressGCTransition]
    public static extern void StencilFunc(Func func, int @ref, uint mask);

    [DllImport(gl, EntryPoint = "glStencilMask"), SuppressGCTransition]
    public static extern void StencilMask(uint mask);

    [DllImport(gl, EntryPoint = "glStencilOp"), SuppressGCTransition]
    public static extern void StencilOp(FailType fail, FailType zfail, FailType zpass);

    [DllImport(gl, EntryPoint = "glTexImage1D"), SuppressGCTransition]
    public static extern void TexImage(Tex1DTarget target, int level, InternalFormat internalformat, int width, int border, ImageFormat format, ImageType type, void* pixels);

    [DllImport(gl, EntryPoint = "glTexImage2D"), SuppressGCTransition]
    public static extern void TexImage(Tex2DTarget target, int level, InternalFormat internalformat, int width, int height, int border, ImageFormat format, ImageType type, void* pixels);

    [DllImport(gl, EntryPoint = "glTexParameterf"), SuppressGCTransition]
    public static extern void TexParameter(TexTarget target, TexNV2 pname, float param);

    [DllImport(gl, EntryPoint = "glTexParameterfv"), SuppressGCTransition]
    public static extern void TexParameter(TexTarget target, TexNV2 pname, float* @params);

    [DllImport(gl, EntryPoint = "glTexParameteri"), SuppressGCTransition]
    public static extern void TexParameter(TexTarget target, TexNV2 pname, int param);

    [DllImport(gl, EntryPoint = "glTexParameteriv"), SuppressGCTransition]
    public static extern void TexParameter(TexTarget target, TexNV2 pname, int* @params);

    [DllImport(gl, EntryPoint = "glTexSubImage1D"), SuppressGCTransition]
    public static extern void TexSubImage(Tex1DTarget target, int level, int xoffset, int width, ImageFormat format, ImageType type, void* pixels);

    [DllImport(gl, EntryPoint = "glTexSubImage2D"), SuppressGCTransition]
    public static extern void TexSubImage(Tex2DTarget target, int level, int xoffset, int yoffset, int width, int height, ImageFormat format, ImageType type, void* pixels);

    [DllImport(gl, EntryPoint = "glViewport"), SuppressGCTransition]
    public static extern void Viewport(int x, int y, int width, int height);

    [DllImport(gl, EntryPoint = "wglCopyContext"), SuppressGCTransition]
    public static extern bool CopyContext(nint source, nint dest, uint mask);

    [DllImport(gl, EntryPoint = "wglCreateContext"), SuppressGCTransition]
    public static extern nint CreateContext(nint hdc);

    [DllImport(gl, EntryPoint = "wglCreateLayerContext"), SuppressGCTransition]
    public static extern nint CreateLayerContext(nint hdc, int layerPlane);

    [DllImport(gl, EntryPoint = "wglDeleteContext"), SuppressGCTransition]
    public static extern bool DeleteContext(nint context);

    [DllImport(gl, EntryPoint = "wglGetCurrentContext"), SuppressGCTransition]
    public static extern nint GetCurrentContext();

    [DllImport(gl, EntryPoint = "wglGetCurrentDC"), SuppressGCTransition]
    public static extern nint GetCurrentDC();

    [DllImport(gl, EntryPoint = "wglGetProcAddress"), SuppressGCTransition]
    public static extern nint GetProcAddress(byte* name);

    [DllImport(gl, EntryPoint = "wglMakeCurrent"), SuppressGCTransition]
    public static extern bool MakeCurrent(nint hdc, nint context);

    [DllImport(gl, EntryPoint = "wglShareLists"), SuppressGCTransition]
    public static extern bool ShareLists(nint context1, nint context2);

    [DllImport(gl, EntryPoint = "wglSwapBuffers"), SuppressGCTransition]
    public static extern bool SwapBuffers(nint hdc);

    static GL()
    {
        if (Kernel32.LoadLibrary("opengl32"u8) == 0)
            throw null!;
    }

    const string gl = "opengl32";
}