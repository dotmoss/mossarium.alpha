using DebugProfiler;
using System.Runtime.InteropServices;
using WindowsOS;

namespace OpenGL;

#pragma warning disable CS0649
public unsafe static partial class GL
{
    static GL()
    {
        if (Kernel32.LoadLibrary("opengl32"u8) == 0)
            throw null!;
    }

    const string gl = "opengl32";

    public static void InititalizeContextFunctions()
    {
        Profiler.Register<ProfileStage>("OpenGL");
        Profiler.Push(ProfileStage.InititalizeContextFunctions);

        var names =
            "glCreateProgram\0wglChoosePixelFormatARB\0glCreateShader\0glShaderSource\0glCompileShader\0glGenVertexArrays\0"u8 +
            "glGenBuffers\0glDeleteBuffers\0glBindVertexArray\0glDeleteVertexArrays\0glBindBuffer\0glBufferData\0glBufferSubData\0"u8 +
            "glVertexAttribPointer\0glVertexAttribIPointer\0glVertexAttribDivisor\0glEnableVertexAttribArray\0glUseProgram\0wglCreateContextAttribsARB\0"u8 +
            "glAttachShader\0glLinkProgram\0glGetShaderiv\0glGetProgramiv\0glBindBufferBase\0glDeleteShader\0glDeleteProgram\0"u8 +
            "glPushDebugGroup\0glPopDebugGroup\0glActiveTexture"u8;
        fixed (byte* namesPointer = names)
        {
            var pointer = namesPointer;
            var pointerEnd = pointer + names.Length - 1;

            fixed (void* firstFunctionPointer = &glCreateProgram)
            {
                var functionPointer = (nint*)firstFunctionPointer;
                do
                {
                    var function = GL.GetProcAddress(pointer);
#if DEBUG
                    if (function == 0)
                        throw null!;
#endif
                    *functionPointer++ = function;
                    pointer += new ReadOnlySpan<byte>(pointer, short.MaxValue).IndexOf((byte)0) + 1;
                }
                while (pointer < pointerEnd);
            }
        }

        Profiler.Pop();
    }

    static delegate* unmanaged[SuppressGCTransition]<uint> glCreateProgram;
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
    static delegate* unmanaged[SuppressGCTransition]<nint, nint, int*, nint> wglCreateContextAttribsARB;
    static delegate* unmanaged[SuppressGCTransition]<uint, uint, void> glAttachShader;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glLinkProgram;
    static delegate* unmanaged[SuppressGCTransition]<uint, ShaderStatusName, int*, void> glGetShaderiv;
    static delegate* unmanaged[SuppressGCTransition]<uint, ProgramStatusName, int*, void> glGetProgramiv;
    static delegate* unmanaged[SuppressGCTransition]<BufferType, uint, uint, void> glBindBufferBase;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glDeleteShader;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glDeleteProgram;
    static delegate* unmanaged[SuppressGCTransition]<DebugSource, uint, int, byte*, void> glPushDebugGroup;
    static delegate* unmanaged[SuppressGCTransition]<void> glPopDebugGroup;
    static delegate* unmanaged[SuppressGCTransition]<uint, void> glActiveTexture;

    public static void CompileShader(uint shader)
        => glCompileShader(shader);

    public static void ShaderSource(uint shader, ReadOnlySpan<byte> source)
    {
        fixed (byte* sourceBytes = source)
            glShaderSource(shader, 1, &sourceBytes, null);
    }

    public static uint CreateShader(ShaderType type) 
        => glCreateShader(type);

    public static nint CreateContextAttribsARB(nint hdc, nint shareContext, int* attributeList)
        => wglCreateContextAttribsARB(hdc, shareContext, attributeList);

    public static bool ChoosePixelFormatARB(nint hdc, int* iAttributeList, float* fAttributeList, uint maxFormats, int* formats, uint* numFormats)
        => wglChoosePixelFormatARB(hdc, iAttributeList, fAttributeList, maxFormats, formats, numFormats);

    public static void GenerateVertexArrays(uint count, uint* ids)
        => glGenVertexArrays(count, ids);

    public static void GenerateBuffers(uint count, uint* ids)
        => glGenBuffers(count, ids);

    public static void DeleteBuffers(uint count, uint* ids)
        => glDeleteBuffers(count, ids);

    public static void BindVertexArray(uint arrayId)
        => glBindVertexArray(arrayId);

    public static void DeleteVertexArray(uint count, uint* ids)
        => glDeleteVertexArray(count, ids);

    public static void BindBuffer(BufferType targetType, uint bufferId)
        => glBindBuffer(targetType, bufferId);

    public static void BufferData(BufferType targetType, int size, void* data, BufferUsage usage)
        => glBufferData(targetType, size, data, usage);

    public static void BufferData<T>(BufferType targetType, T data, BufferUsage usage)
        where T : unmanaged
        => glBufferData(targetType, sizeof(T), &data, usage);

    public static void BufferData<T>(BufferType targetType, T* data, BufferUsage usage)
        where T : unmanaged
        => glBufferData(targetType, sizeof(T), data, usage);

    public static void BufferSubData(BufferType targetType, int offset, int size, void* data)
        => glBufferSubData(targetType, offset, size, data);

    public static void VertexAttribPointer(uint index, int size, DataType type, bool normalize, int stride, void* pointer)
        => glVertexAttribPointer(index, size, type, normalize, stride, pointer);

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

    public static void PushDebugGroup(DebugSource source, uint id, int length, byte* message)
        => glPushDebugGroup(source, id, length, message);

    public static void PopDebugGroup()
        => glPopDebugGroup();

    public static void ActiveTexture(uint texture)
        => glActiveTexture(texture);

    public static void PushDebugGroup(DebugSource source, uint id, ReadOnlySpan<byte> message)
    {
        fixed (byte* messagePointer = message)
            glPushDebugGroup(source, id, -1, messagePointer);
    }

    #region Imports
    [LibraryImport(gl, EntryPoint = "glBindTexture"), SuppressGCTransition] public static partial 
        void BindTexture(TextureParameterTarget target, uint texture);

    [LibraryImport(gl, EntryPoint = "glBlendFunc"), SuppressGCTransition] public static partial 
        void BlendFunc(FactorEnum sourceFactor, FactorEnum destinationFactor);

    [LibraryImport(gl, EntryPoint = "glClear"), SuppressGCTransition] public static partial 
        void Clear(ClearMask mask);

    [LibraryImport(gl, EntryPoint = "glClearColor"), SuppressGCTransition] public static partial 
        void ClearColor(float red, float green, float blue, float alpha);

    [LibraryImport(gl, EntryPoint = "glClearDepth"), SuppressGCTransition] public static partial 
        void ClearDepth(double depth);

    [LibraryImport(gl, EntryPoint = "glClearStencil"), SuppressGCTransition] public static partial 
        void ClearStencil(int stencil);

    [LibraryImport(gl, EntryPoint = "glColorMask"), SuppressGCTransition] public static partial 
        void ColorMask(byte red, byte green, byte blue, byte alpha);

    [LibraryImport(gl, EntryPoint = "glCopyTexImage1D"), SuppressGCTransition] public static partial 
        void CopyTextureImage1D(TextureParameterTarget target, int level, InternalFormat internalFormat, int x, int y, int width, int border);

    [LibraryImport(gl, EntryPoint = "glCopyTexImage2D"), SuppressGCTransition] public static partial 
        void CopyTextureImage2D(TextureParameterTarget target, int level, InternalFormat internalFormat, int x, int y, int width, int height, int border);

    [LibraryImport(gl, EntryPoint = "glCopyTexSubImage1D"), SuppressGCTransition] public static partial 
        void CopyTextureSubImage1D(TextureParameterTarget target, int level, int xoffset, int x, int y, int width);

    [LibraryImport(gl, EntryPoint = "glCopyTexSubImage2D"), SuppressGCTransition] public static partial 
        void CopyTextureSubImage2D(TextureParameterTarget target, int level, int xoffset, int yoffset, int x, int y, int width, int height);

    [LibraryImport(gl, EntryPoint = "glCullFace"), SuppressGCTransition] public static partial 
        void CullFace(FaceEnum mode);

    [LibraryImport(gl, EntryPoint = "glDeleteTextures"), SuppressGCTransition] public static partial 
        void DeleteTextures(uint count, uint* textureIds);

    [LibraryImport(gl, EntryPoint = "glDepthFunc"), SuppressGCTransition] public static partial 
        void DepthFunc(TestFunction testFunction);

    [LibraryImport(gl, EntryPoint = "glDepthMask"), SuppressGCTransition] public static partial 
        void DepthMask(byte flag);

    [LibraryImport(gl, EntryPoint = "glDepthRange"), SuppressGCTransition] public static partial 
        void DepthRange(double zNear, double zFar);

    [LibraryImport(gl, EntryPoint = "glDisable"), SuppressGCTransition] public static partial 
        void Disable(StateCap cap);

    [LibraryImport(gl, EntryPoint = "glDrawArrays"), SuppressGCTransition] public static partial 
        void DrawArrays(DrawMode mode, int first, int count);

    [LibraryImport(gl, EntryPoint = "glDrawBuffer"), SuppressGCTransition] public static partial 
        void DrawBuffer(DrawMode mode);

    [LibraryImport(gl, EntryPoint = "glDrawElements"), SuppressGCTransition] public static partial 
        void DrawElements(DrawMode mode, int count, ElementIndexType type, nint offset);

    [LibraryImport(gl, EntryPoint = "glEdgeFlag"), SuppressGCTransition] public static partial 
        void EdgeFlag(byte flag);

    [LibraryImport(gl, EntryPoint = "glEnable"), SuppressGCTransition] public static partial 
        void Enable(StateCap cap);

    [LibraryImport(gl, EntryPoint = "glFinish"), SuppressGCTransition] public static partial 
        void Finish();

    [LibraryImport(gl, EntryPoint = "glFlush"), SuppressGCTransition] public static partial 
        void Flush();

    [LibraryImport(gl, EntryPoint = "glFrontFace"), SuppressGCTransition] public static partial 
        void FrontFace(FaceMode mode);

    [LibraryImport(gl, EntryPoint = "glGenTextures"), SuppressGCTransition] public static partial 
        void GenerateTextures(uint count, uint* textures);

    [LibraryImport(gl, EntryPoint = "glGetBooleanv"), SuppressGCTransition] public static partial 
        void GetBoolean(ParameterName pname, byte* parameters);

    [LibraryImport(gl, EntryPoint = "glGetDoublev"), SuppressGCTransition] public static partial 
        void GetDouble(ParameterName pname, double* parameters);

    [LibraryImport(gl, EntryPoint = "glGetError"), SuppressGCTransition] public static partial 
        ErrorStatus GetError();

    [LibraryImport(gl, EntryPoint = "glGetFloatv"), SuppressGCTransition] public static partial 
        void GetFloat(ParameterName pname, float* parameters);

    [LibraryImport(gl, EntryPoint = "glGetIntegerv"), SuppressGCTransition] public static partial 
        void GetInteger(ParameterName pname, int* parameters);

    [LibraryImport(gl, EntryPoint = "glGetString"), SuppressGCTransition] public static partial 
        byte* GetString(StringName name);

    [LibraryImport(gl, EntryPoint = "glGetTexImage"), SuppressGCTransition] public static partial 
        void GetTextureImage(TextureTarget target, int level, ImagePixelType format, ImageDataType type, nint pixels);

    [LibraryImport(gl, EntryPoint = "glGetTexLevelParameterfv"), SuppressGCTransition] public static partial 
        void GetTextureLevelParameter(TextureParameterTarget target, int level, TextureLevelParameter parameterName, float* parameters);

    [LibraryImport(gl, EntryPoint = "glGetTexLevelParameteriv"), SuppressGCTransition] public static partial 
        void GetTextureLevelParameter(TextureParameterTarget target, int level, TextureLevelParameter parameterName, int* parameters);

    [LibraryImport(gl, EntryPoint = "glGetTexParameterfv"), SuppressGCTransition] public static partial 
        void GetTextureParameter(TextureTarget target, TextureLevelParameter2 parameterName, float* parameters);

    [LibraryImport(gl, EntryPoint = "glGetTexParameteriv"), SuppressGCTransition] public static partial 
        void GetTextureParameter(TextureTarget target, TextureLevelParameter2 parameterName, int* parameters);

    [LibraryImport(gl, EntryPoint = "glHint"), SuppressGCTransition] public static partial 
        void Hint(GLHint target, CalcType mode);

    [return: MarshalAs(UnmanagedType.Bool)]
    [LibraryImport(gl, EntryPoint = "glIsEnabled"), SuppressGCTransition] public static partial 
        bool IsEnabled(StateCap cap);

    [LibraryImport(gl, EntryPoint = "glIsTexture"), SuppressGCTransition] public static partial 
        byte IsTexture(uint texture);

    [LibraryImport(gl, EntryPoint = "glLineWidth"), SuppressGCTransition] public static partial 
        void LineWidth(float width);

    [LibraryImport(gl, EntryPoint = "glLogicOp"), SuppressGCTransition] public static partial 
        void LogicOp(LogicOpCode opcode);

    [LibraryImport(gl, EntryPoint = "glPixelStoref"), SuppressGCTransition] public static partial 
        void PixelStore(ParameterPixelStore pname, float parameter);

    [LibraryImport(gl, EntryPoint = "glPixelStorei"), SuppressGCTransition] public static partial 
        void PixelStore(ParameterPixelStore pname, int parameter);

    [LibraryImport(gl, EntryPoint = "glPointSize"), SuppressGCTransition] public static partial 
        void PointSize(float size);

    [LibraryImport(gl, EntryPoint = "glPolygonMode"), SuppressGCTransition] public static partial 
        void PolygonMode(MaterialFace face, MeshType mode);

    [LibraryImport(gl, EntryPoint = "glPolygonOffset"), SuppressGCTransition] public static partial 
        void PolygonOffset(float factor, float units);

    [LibraryImport(gl, EntryPoint = "glReadBuffer"), SuppressGCTransition] public static partial 
        void ReadBuffer(ReadBufferType mode);

    [LibraryImport(gl, EntryPoint = "glReadPixels"), SuppressGCTransition] public static partial 
        void ReadPixels(int x, int y, int width, int height, ImagePixelType format, ReadType type, nint pixels);

    [LibraryImport(gl, EntryPoint = "glScissor"), SuppressGCTransition] public static partial 
        void Scissor(int x, int y, int width, int height);

    [LibraryImport(gl, EntryPoint = "glStencilFunc"), SuppressGCTransition] public static partial 
        void StencilFunc(TestFunction func, int @ref, uint mask);

    [LibraryImport(gl, EntryPoint = "glStencilMask"), SuppressGCTransition] public static partial 
        void StencilMask(uint mask);

    [LibraryImport(gl, EntryPoint = "glStencilOp"), SuppressGCTransition] public static partial 
        void StencilOp(FailType fail, FailType zFail, FailType zPass);

    [LibraryImport(gl, EntryPoint = "glTexImage1D"), SuppressGCTransition] public static partial 
        void TextureImage(Texture1DTarget target, int level, InternalFormat internalformat, int width, int border, ImageFormat format, ImageType type, void* pixels);

    [LibraryImport(gl, EntryPoint = "glTexImage2D"), SuppressGCTransition] public static partial 
        void TextureImage(Texture2DTarget target, int level, InternalFormat internalformat, int width, int height, int border, ImageFormat format, ImageType type, void* pixels);

    [LibraryImport(gl, EntryPoint = "glTexParameterf"), SuppressGCTransition] public static partial 
        void TextureParameter(TextureTarget target, TextureLevelParameter3 pname, float param);

    [LibraryImport(gl, EntryPoint = "glTexParameterfv"), SuppressGCTransition] public static partial 
        void TextureParameter(TextureTarget target, TextureLevelParameter3 pname, float* @params);

    [LibraryImport(gl, EntryPoint = "glTexParameteri"), SuppressGCTransition] public static partial 
        void TextureParameter(TextureTarget target, TextureLevelParameter3 pname, int param);

    [LibraryImport(gl, EntryPoint = "glTexParameteriv"), SuppressGCTransition] public static partial 
        void TextureParameter(TextureTarget target, TextureLevelParameter3 pname, int* @params);

    [LibraryImport(gl, EntryPoint = "glTexSubImage1D"), SuppressGCTransition] public static partial 
        void TextureSubImage(Texture1DTarget target, int level, int xoffset, int width, ImageFormat format, ImageType type, void* pixels);

    [LibraryImport(gl, EntryPoint = "glTexSubImage2D"), SuppressGCTransition] public static partial 
        void TextureSubImage(Texture2DTarget target, int level, int xoffset, int yoffset, int width, int height, ImageFormat format, ImageType type, void* pixels);

    [LibraryImport(gl, EntryPoint = "glViewport"), SuppressGCTransition] public static partial 
        void Viewport(int x, int y, int width, int height);

    [return: MarshalAs(UnmanagedType.Bool)]
    [LibraryImport(gl, EntryPoint = "wglCopyContext"), SuppressGCTransition] public static partial 
        bool CopyContext(nint source, nint dest, uint mask);

    [LibraryImport(gl, EntryPoint = "wglCreateContext"), SuppressGCTransition] public static partial 
        nint CreateContext(nint hdc);

    [LibraryImport(gl, EntryPoint = "wglCreateLayerContext"), SuppressGCTransition] public static partial 
        nint CreateLayerContext(nint hdc, int layerPlane);

    [return: MarshalAs(UnmanagedType.Bool)]
    [LibraryImport(gl, EntryPoint = "wglDeleteContext"), SuppressGCTransition] public static partial 
        bool DeleteContext(nint context);

    [LibraryImport(gl, EntryPoint = "wglGetCurrentContext"), SuppressGCTransition] public static partial 
        nint GetCurrentContext();

    [LibraryImport(gl, EntryPoint = "wglGetCurrentDC"), SuppressGCTransition] public static partial 
        nint GetCurrentDC();

    [LibraryImport(gl, EntryPoint = "wglGetProcAddress"), SuppressGCTransition] public static partial 
        nint GetProcAddress(byte* name);

    [return: MarshalAs(UnmanagedType.Bool)]
    [LibraryImport(gl, EntryPoint = "wglMakeCurrent"), SuppressGCTransition] public static partial 
        bool MakeCurrent(nint hdc, nint context);

    [return: MarshalAs(UnmanagedType.Bool)]
    [LibraryImport(gl, EntryPoint = "wglShareLists"), SuppressGCTransition] public static partial 
        bool ShareLists(nint context1, nint context2);

    [return: MarshalAs(UnmanagedType.Bool)]
    [LibraryImport(gl, EntryPoint = "wglSwapBuffers"), SuppressGCTransition] public static partial 
        bool SwapBuffers(nint hdc);
    #endregion
}