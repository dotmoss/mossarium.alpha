using System.Runtime.InteropServices;
using WindowsOS;
using static OpenGL.Enums;

namespace OpenGL;

public unsafe class GL
{
    protected GL() { }

    [DllImport(gl, EntryPoint = "glAccum"), SuppressGCTransition]
    public static extern void Accum(Op op, float value);

    [DllImport(gl, EntryPoint = "glAlphaFunc"), SuppressGCTransition]
    public static extern void AlphaFunc(Func func, float @ref);

    public static byte AreTexturesResident(int n, uint[] textures, byte[] residences)
    {
        fixed (uint* texturesPtr = textures)
        fixed (byte* residencesPtr = residences)
            return AreTexturesResident(n, texturesPtr, residencesPtr);
    }

    [DllImport(gl, EntryPoint = "glAreTexturesResident"), SuppressGCTransition]
    public static extern byte AreTexturesResident(int n, uint* textures, byte* residences);

    [DllImport(gl, EntryPoint = "glArrayElement"), SuppressGCTransition]
    public static extern void ArrayElement(int i);

    [DllImport(gl, EntryPoint = "glBegin"), SuppressGCTransition]
    public static extern void Begin(Mode mode);

    [DllImport(gl, EntryPoint = "glBindTexture"), SuppressGCTransition]
    public static extern void BindTexture(TexPTarget target, uint texture);

    public static void Bitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, byte[] bitmap)
    {
        fixed (byte* bitmapPtr = bitmap)
            Bitmap(width, height, xorig, yorig, xmove, ymove, bitmapPtr);
    }

    [DllImport(gl, EntryPoint = "glBitmap"), SuppressGCTransition]
    public static extern void Bitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, byte* bitmap);

    [DllImport(gl, EntryPoint = "glBlendFunc"), SuppressGCTransition]
    public static extern void BlendFunc(FactorEnum sfactor, FactorEnum dfactor);

    [DllImport(gl, EntryPoint = "glCallList"), SuppressGCTransition]
    public static extern void CallList(uint list);

    [DllImport(gl, EntryPoint = "glCallLists"), SuppressGCTransition]
    public static extern void CallLists(int n, Enums.DataType type, nint lists);

    [DllImport(gl, EntryPoint = "glClear"), SuppressGCTransition]
    public static extern void Clear(ClearMask mask);

    [DllImport(gl, EntryPoint = "glClearAccum"), SuppressGCTransition]
    public static extern void ClearAccum(float red, float green, float blue, float alpha);

    [DllImport(gl, EntryPoint = "glClearColor"), SuppressGCTransition]
    public static extern void ClearColor(float red, float green, float blue, float alpha);

    [DllImport(gl, EntryPoint = "glClearDepth"), SuppressGCTransition]
    public static extern void ClearDepth(double depth);

    [DllImport(gl, EntryPoint = "glClearIndex"), SuppressGCTransition]
    public static extern void ClearIndex(float c);

    [DllImport(gl, EntryPoint = "glClearStencil"), SuppressGCTransition]
    public static extern void ClearStencil(int s);

    public static void ClipPlane(PlaneOrdinal plane, double[] equation)
    {
        fixed (double* equationPtr = equation)
            ClipPlane(plane, equationPtr);
    }

    [DllImport(gl, EntryPoint = "glClipPlane"), SuppressGCTransition]
    public static extern void ClipPlane(PlaneOrdinal plane, double* equation);

    [DllImport(gl, EntryPoint = "glColor3b"), SuppressGCTransition]
    public static extern void Color(sbyte red, sbyte green, sbyte blue);

    [DllImport(gl, EntryPoint = "glColor3bv"), SuppressGCTransition]
    public static extern void Color3(sbyte* pointer);

    public static void Color3(sbyte[] v)
    {
        fixed (sbyte* vPtr = v)
            Color3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor3d"), SuppressGCTransition]
    public static extern void Color(double red, double green, double blue);

    [DllImport(gl, EntryPoint = "glColor3dv"), SuppressGCTransition]
    public static extern void Color3(double* pointer);

    public static void Color3(double[] v)
    {
        fixed (double* vPtr = v)
            Color3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor3f"), SuppressGCTransition]
    public static extern void Color(float red, float green, float blue);

    [DllImport(gl, EntryPoint = "glColor3fv"), SuppressGCTransition]
    public static extern void Color3(float* pointer);

    public static void Color3(float[] v)
    {
        fixed (float* vPtr = v)
            Color3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor3i"), SuppressGCTransition]
    public static extern void Color(int red, int green, int blue);

    [DllImport(gl, EntryPoint = "glColor3iv"), SuppressGCTransition]
    public static extern void Color3(int* pointer);

    public static void Color3(int[] v)
    {
        fixed (int* vPtr = v)
            Color3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor3s"), SuppressGCTransition]
    public static extern void Color(short red, short green, short blue);

    [DllImport(gl, EntryPoint = "glColor3sv"), SuppressGCTransition]
    public static extern void Color3(short* pointer);

    public static void Color3(short[] v)
    {
        fixed (short* vPtr = v)
            Color3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor3ub"), SuppressGCTransition]
    public static extern void Color(byte red, byte green, byte blue);

    [DllImport(gl, EntryPoint = "glColor3ubv"), SuppressGCTransition]
    public static extern void Color3(byte* pointer);

    public static void Color3(byte[] v)
    {
        fixed (byte* vPtr = v)
            Color3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor3ui"), SuppressGCTransition]
    public static extern void Color(uint red, uint green, uint blue);

    [DllImport(gl, EntryPoint = "glColor3uiv"), SuppressGCTransition]
    public static extern void Color3(uint* pointer);

    public static void Color3(uint[] v)
    {
        fixed (uint* vPtr = v)
            Color3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor3us"), SuppressGCTransition]
    public static extern void Color(ushort red, ushort green, ushort blue);

    [DllImport(gl, EntryPoint = "glColor3usv"), SuppressGCTransition]
    public static extern void Color3(ushort* pointer);

    public static void Color3(ushort[] v)
    {
        fixed (ushort* vPtr = v)
            Color3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor4b"), SuppressGCTransition]
    public static extern void Color(sbyte red, sbyte green, sbyte blue, sbyte alpha);

    [DllImport(gl, EntryPoint = "glColor4bv"), SuppressGCTransition]
    public static extern void Color4(sbyte* pointer);

    public static void Color4(sbyte[] v)
    {
        fixed (sbyte* vPtr = v)
            Color4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor4d"), SuppressGCTransition]
    public static extern void Color(double red, double green, double blue, double alpha);

    [DllImport(gl, EntryPoint = "glColor4dv"), SuppressGCTransition]
    public static extern void Color4(double* pointer);

    public static void Color4(double[] v)
    {
        fixed (double* vPtr = v)
            Color4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor4f"), SuppressGCTransition]
    public static extern void Color(float red, float green, float blue, float alpha);

    [DllImport(gl, EntryPoint = "glColor4fv"), SuppressGCTransition]
    public static extern void Color4(float* pointer);

    public static void Color4(float[] v)
    {
        fixed (float* vPtr = v)
            Color4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor4i"), SuppressGCTransition]
    public static extern void Color(int red, int green, int blue, int alpha);

    [DllImport(gl, EntryPoint = "glColor4iv"), SuppressGCTransition]
    public static extern void Color4(int* pointer);

    public static void Color4(int[] v)
    {
        fixed (int* vPtr = v)
            Color4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor4s"), SuppressGCTransition]
    public static extern void Color(short red, short green, short blue, short alpha);

    [DllImport(gl, EntryPoint = "glColor4sv"), SuppressGCTransition]
    public static extern void Color4(short* pointer);

    public static void Color4(short[] v)
    {
        fixed (short* vPtr = v)
            Color4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor4ub"), SuppressGCTransition]
    public static extern void Color(byte red, byte green, byte blue, byte alpha);

    [DllImport(gl, EntryPoint = "glColor4ubv"), SuppressGCTransition]
    public static extern void Color4(byte* pointer);

    public static void Color4(byte[] v)
    {
        fixed (byte* vPtr = v)
            Color4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor4ui"), SuppressGCTransition]
    public static extern void Color(uint red, uint green, uint blue, uint alpha);

    [DllImport(gl, EntryPoint = "glColor4uiv"), SuppressGCTransition]
    public static extern void Color4(uint* pointer);

    public static void Color4(uint[] v)
    {
        fixed (uint* vPtr = v)
            Color4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColor4us"), SuppressGCTransition]
    public static extern void Color(ushort red, ushort green, ushort blue, ushort alpha);

    [DllImport(gl, EntryPoint = "glColor4usv"), SuppressGCTransition]
    public static extern void Color4(ushort* pointer);

    public static void Color4(ushort[] v)
    {
        fixed (ushort* vPtr = v)
            Color4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glColorMask"), SuppressGCTransition]
    public static extern void ColorMask(byte red, byte green, byte blue, byte alpha);

    [DllImport(gl, EntryPoint = "glColorMaterial"), SuppressGCTransition]
    public static extern void ColorMaterial(FaceEnum face, MaterialParam mode);

    [DllImport(gl, EntryPoint = "glColorPointer"), SuppressGCTransition]
    public static extern void ColorPointer(int size, BType type, int stride, nint pointer);

    [DllImport(gl, EntryPoint = "glCopyPixels"), SuppressGCTransition]
    public static extern void CopyPixels(int x, int y, int width, int height, CopyType type);

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

    [DllImport(gl, EntryPoint = "glDeleteLists"), SuppressGCTransition]
    public static extern void DeleteLists(uint list, int range);

    public static void DeleteTextures(int n, uint[] textures)
    {
        fixed (uint* texturesPtr = textures)
            DeleteTextures(n, texturesPtr);
    }

    [DllImport(gl, EntryPoint = "glDeleteTextures"), SuppressGCTransition]
    public static extern void DeleteTextures(int n, uint* textures);

    [DllImport(gl, EntryPoint = "glDepthFunc"), SuppressGCTransition]
    public static extern void DepthFunc(Func func);

    [DllImport(gl, EntryPoint = "glDepthMask"), SuppressGCTransition]
    public static extern void DepthMask(byte flag);

    [DllImport(gl, EntryPoint = "glDepthRange"), SuppressGCTransition]
    public static extern void DepthRange(double zNear, double zFar);

    public static void Set(Cap cap, bool state)
    {
        if (state)
            Enable(cap);
        else Disable(cap);
    }

    [DllImport(gl, EntryPoint = "glDisable"), SuppressGCTransition]
    public static extern void Disable(Cap cap);

    [DllImport(gl, EntryPoint = "glDisableClientState"), SuppressGCTransition]
    public static extern void DisableClientState(ArrayState array);

    [DllImport(gl, EntryPoint = "glDrawArrays"), SuppressGCTransition]
    public static extern void DrawArrays(Mode mode, int first, int count);

    [DllImport(gl, EntryPoint = "glDrawBuffer"), SuppressGCTransition]
    public static extern void DrawBuffer(Mode mode);

    [DllImport(gl, EntryPoint = "glDrawElements"), SuppressGCTransition]
    public static extern void DrawElements(Mode mode, int count, BUType type, nint offset);

    [DllImport(gl, EntryPoint = "glDrawPixels"), SuppressGCTransition]
    public static extern void DrawPixels(int width, int height, ImageFormat format, BType type, nint pixels);

    [DllImport(gl, EntryPoint = "glEdgeFlag"), SuppressGCTransition]
    public static extern void EdgeFlag(byte flag);

    [DllImport(gl, EntryPoint = "glEdgeFlagPointer"), SuppressGCTransition]
    public static extern void EdgeFlagPointer(int stride, nint pointer);

    public static void EdgeFlag(byte[] flag)
    {
        fixed (byte* flagPtr = flag)
            EdgeFlag(flagPtr);
    }

    [DllImport(gl, EntryPoint = "glEdgeFlagv"), SuppressGCTransition]
    public static extern void EdgeFlag(byte* flag);

    [DllImport(gl, EntryPoint = "glEnable"), SuppressGCTransition]
    public static extern void Enable(Cap cap);

    [DllImport(gl, EntryPoint = "glEnableClientState"), SuppressGCTransition]
    public static extern void EnableClientState(ArrayState array);

    [DllImport(gl, EntryPoint = "glEnd"), SuppressGCTransition]
    public static extern void End();

    [DllImport(gl, EntryPoint = "glEndList"), SuppressGCTransition]
    public static extern void EndList();

    [DllImport(gl, EntryPoint = "glEvalCoord1d"), SuppressGCTransition]
    public static extern void EvalCoord1(double u);

    [DllImport(gl, EntryPoint = "glEvalCoord1dv"), SuppressGCTransition]
    public static extern void EvalCoord1(double* pointer);

    public static void EvalCoord1(double[] u)
    {
        fixed (double* uPtr = u)
            EvalCoord1(uPtr);
    }

    [DllImport(gl, EntryPoint = "glEvalCoord1f"), SuppressGCTransition]
    public static extern void EvalCoord(float u);

    [DllImport(gl, EntryPoint = "glEvalCoord1fv"), SuppressGCTransition]
    public static extern void EvalCoord1(float* pointer);

    public static void EvalCoord1(float[] u)
    {
        fixed (float* uPtr = u)
            EvalCoord1(uPtr);
    }

    [DllImport(gl, EntryPoint = "glEvalCoord2d"), SuppressGCTransition]
    public static extern void EvalCoord(double u, double v);

    [DllImport(gl, EntryPoint = "glEvalCoord2dv"), SuppressGCTransition]
    public static extern void EvalCoord2(double* pointer);

    public static void EvalCoord2(double[] u)
    {
        fixed (double* uPtr = u)
            EvalCoord2(uPtr);
    }

    [DllImport(gl, EntryPoint = "glEvalCoord2f"), SuppressGCTransition]
    public static extern void EvalCoord(float u, float v);

    [DllImport(gl, EntryPoint = "glEvalCoord2fv"), SuppressGCTransition]
    public static extern void EvalCoord2(float* pointer);

    public static void EvalCoord2(float[] u)
    {
        fixed (float* uPtr = u)
            EvalCoord2(uPtr);
    }

    [DllImport(gl, EntryPoint = "glEvalMesh1"), SuppressGCTransition]
    public static extern void EvalMesh(EMesh mode, int i1, int i2);

    [DllImport(gl, EntryPoint = "glEvalMesh2"), SuppressGCTransition]
    public static extern void EvalMesh(MeshType mode, int i1, int i2, int j1, int j2);

    [DllImport(gl, EntryPoint = "glEvalPoint1"), SuppressGCTransition]
    public static extern void EvalPoint(int i);

    [DllImport(gl, EntryPoint = "glEvalPoint2"), SuppressGCTransition]
    public static extern void EvalPoint(int i, int j);

    public static void FeedbackBuffer(int size, VertexType type, float[] buffer)
    {
        fixed (float* bufferPtr = buffer)
            FeedbackBuffer(size, type, bufferPtr);
    }

    [DllImport(gl, EntryPoint = "glFeedbackBuffer"), SuppressGCTransition]
    public static extern void FeedbackBuffer(int size, VertexType type, float* buffer);

    [DllImport(gl, EntryPoint = "glFinish"), SuppressGCTransition]
    public static extern void Finish();

    [DllImport(gl, EntryPoint = "glFlush"), SuppressGCTransition]
    public static extern void Flush();

    [DllImport(gl, EntryPoint = "glFogf"), SuppressGCTransition]
    public static extern void Fogf(Fog pname, float param);

    [DllImport(gl, EntryPoint = "glFogfv"), SuppressGCTransition]
    public static extern void Fogf(Fog pname, float* pointer);

    public static void Fogf(Fog pname, float[] @params)
    {
        fixed (float* paramsPtr = @params)
            Fogf(pname, paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glFogi"), SuppressGCTransition]
    public static extern void Fogi(Fog pname, int param);

    [DllImport(gl, EntryPoint = "glFogiv"), SuppressGCTransition]
    public static extern void Fogf(Fog pname, int* pointer);

    public static void Fogi(Fog pname, int[] @params)
    {
        fixed (int* paramsPtr = @params)
            Fogf(pname, paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glFrontFace"), SuppressGCTransition]
    public static extern void FrontFace(FaceMode mode);

    [DllImport(gl, EntryPoint = "glFrustum"), SuppressGCTransition]
    public static extern void Frustum(double left, double right, double bottom, double top, double zNear, double zFar);

    [DllImport(gl, EntryPoint = "glGenLists"), SuppressGCTransition]
    public static extern uint GenLists(int range);

    [DllImport(gl, EntryPoint = "glGenTextures"), SuppressGCTransition]
    public static extern void GenTextures(int n, uint* textures);

    public static void GenTextures(int n, uint[] textures)
    {
        fixed (uint* texturesPtr = textures)
            GenTextures(n, texturesPtr);
    }

    [DllImport(gl, EntryPoint = "glGetBooleanv"), SuppressGCTransition]
    public static extern void GetBoolean(PName pname, byte* @params);

    public static void GetBoolean(PName pname, byte[] @params)
    {
        fixed (byte* @paramsPtr = @params)
            GetBoolean(pname, @paramsPtr);
    }

    public static void GetClipPlane(PlaneOrdinal plane, double[] equation)
    {
        fixed (double* equationPtr = equation)
            GetClipPlane(plane, equationPtr);
    }

    [DllImport(gl, EntryPoint = "glGetClipPlane"), SuppressGCTransition]
    public static extern void GetClipPlane(PlaneOrdinal plane, double* equation);

    [DllImport(gl, EntryPoint = "glGetDoublev"), SuppressGCTransition]
    public static extern void GetDouble(PName pname, double* @params);

    public static void GetDouble(PName pname, double[] @params)
    {
        fixed (double* @paramsPtr = @params)
            GetDouble(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetError"), SuppressGCTransition]
    public static extern Status GetError();

    [DllImport(gl, EntryPoint = "glGetFloatv"), SuppressGCTransition]
    public static extern void GetFloat(PName pname, float* @params);

    public static void GetFloat(PName pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            GetFloat(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetIntegerv"), SuppressGCTransition]
    public static extern void GetInteger(PName pname, int* @params);

    public static void GetInteger(PName pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            GetInteger(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetLightfv"), SuppressGCTransition]
    public static extern void GetLight(LightOrdinal light, LightN pname, float* @params);

    public static void GetLight(LightOrdinal light, LightN pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            GetLight(light, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetLightiv"), SuppressGCTransition]
    public static extern void GetLight(LightOrdinal light, LightN pname, int* @params);

    public static void GetLight(LightOrdinal light, LightN pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            GetLight(light, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetMapdv"), SuppressGCTransition]
    public static extern void GetMap(MapTarget target, QueryType query, double* pointer);

    public static void GetMap(MapTarget target, QueryType query, double[] v)
    {
        fixed (double* vPtr = v)
            GetMap(target, query, vPtr);
    }

    [DllImport(gl, EntryPoint = "glGetMapfv"), SuppressGCTransition]
    public static extern void GetMap(MapTarget target, QueryType query, float* pointer);

    public static void GetMap(MapTarget target, QueryType query, float[] v)
    {
        fixed (float* vPtr = v)
            GetMap(target, query, vPtr);
    }

    [DllImport(gl, EntryPoint = "glGetMapiv"), SuppressGCTransition]
    public static extern void GetMap(MapTarget target, QueryType query, int* pointer);

    public static void GetMap(MapTarget target, QueryType query, int[] v)
    {
        fixed (int* vPtr = v)
            GetMap(target, query, vPtr);
    }

    [DllImport(gl, EntryPoint = "glGetMaterialfv"), SuppressGCTransition]
    public static extern void GetMaterial(SideEnum face, MaterialParam pname, float* pointer);

    public static void GetMaterial(SideEnum face, MaterialParam pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            GetMaterial(face, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetMaterialiv"), SuppressGCTransition]
    public static extern void GetMaterial(SideEnum face, MaterialParam pname, int* pointer);

    public static void GetMaterial(SideEnum face, MaterialParam pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            GetMaterial(face, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetPixelMapfv"), SuppressGCTransition]
    public static extern void GetPixelMap(MapType map, float* pointer);

    public static void GetPixelMap(MapType map, float[] values)
    {
        fixed (float* valuesPtr = values)
            GetPixelMap(map, valuesPtr);
    }

    [DllImport(gl, EntryPoint = "glGetPixelMapuiv"), SuppressGCTransition]
    public static extern void GetPixelMap(MapType map, uint* pointer);

    public static void GetPixelMap(MapType map, uint[] values)
    {
        fixed (uint* valuesPtr = values)
            GetPixelMap(map, valuesPtr);
    }

    [DllImport(gl, EntryPoint = "glGetPixelMapusv"), SuppressGCTransition]
    public static extern void GetPixelMap(MapType map, ushort* pointer);

    public static void GetPixelMap(MapType map, ushort[] values)
    {
        fixed (ushort* valuesPtr = values)
            GetPixelMap(map, valuesPtr);
    }

    [DllImport(gl, EntryPoint = "glGetPointerv"), SuppressGCTransition]
    public static extern void GetPointer(PNamePtr pname, nint* @params);

    public static void GetPointer(PNamePtr pname, nint[] @params)
    {
        fixed (nint* @paramsPtr = @params)
            GetPointer(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetPolygonStipple"), SuppressGCTransition]
    public static extern void GetPolygonStipple(byte* mask);

    public static void GetPolygonStipple(byte[] mask)
    {
        fixed (byte* maskPtr = mask)
            GetPolygonStipple(maskPtr);
    }

    [DllImport(gl, EntryPoint = "glGetString"), SuppressGCTransition]
    public static extern byte* GetString(StringName name);

    [DllImport(gl, EntryPoint = "glGetTexEnvfv"), SuppressGCTransition]
    public static extern void GetTexEnv(TexEnvN pname, float* @params);

    public static void GetTexEnv(TexEnvN pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            GetTexEnv(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetTexEnviv"), SuppressGCTransition]
    public static extern void GetTexEnv(TexEnvN pname, int* @params);

    public static void GetTexEnv(TexEnvN pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            GetTexEnv(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetTexGendv"), SuppressGCTransition]
    public static extern void GetTexGen(CoordsEnum coord, TexGenN pname, double* @params);

    public static void GetTexGen(CoordsEnum coord, TexGenN pname, double[] @params)
    {
        fixed (double* @paramsPtr = @params)
            GetTexGen(coord, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetTexGenfv"), SuppressGCTransition]
    public static extern void GetTexGen(CoordsEnum coord, TexGenN pname, float* @params);

    public static void GetTexGen(CoordsEnum coord, TexGenN pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            GetTexGen(coord, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetTexGeniv"), SuppressGCTransition]
    public static extern void GetTexGen(CoordsEnum coord, TexGenN pname, int* @params);

    public static void GetTexGen(CoordsEnum coord, TexGenN pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            GetTexGen(coord, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetTexImage"), SuppressGCTransition]
    public static extern void GetTexImage(TexTarget target, int level, ImagePixelType format, BType type, nint pixels);

    [DllImport(gl, EntryPoint = "glGetTexLevelParameterfv"), SuppressGCTransition]
    public static extern void GetTexLevelParameter(TexPTarget target, int level, TexN pname, float* @params);

    public static void GetTexLevelParameter(TexPTarget target, int level, TexN pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            GetTexLevelParameter(target, level, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetTexLevelParameteriv"), SuppressGCTransition]
    public static extern void GetTexLevelParameter(TexPTarget target, int level, TexN pname, int* @params);

    public static void GetTexLevelParameter(TexPTarget target, int level, TexN pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            GetTexLevelParameter(target, level, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetTexParameterfv"), SuppressGCTransition]
    public static extern void GetTexParameter(TexTarget target, TexNV pname, float* @params);

    public static void GetTexParameter(TexTarget target, TexNV pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            GetTexParameter(target, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glGetTexParameteriv"), SuppressGCTransition]
    public static extern void GetTexParameter(TexTarget target, TexNV pname, int* @params);

    public static void GetTexParameter(TexTarget target, TexNV pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            GetTexParameter(target, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glHint"), SuppressGCTransition]
    public static extern void Hint(Hint target, CalcType mode);

    [DllImport(gl, EntryPoint = "glIndexMask"), SuppressGCTransition]
    public static extern void IndexMask(uint mask);

    [DllImport(gl, EntryPoint = "glIndexPointer"), SuppressGCTransition]
    public static extern void IndexPointer(TexType type, int stride, nint pointer);

    [DllImport(gl, EntryPoint = "glIndexd"), SuppressGCTransition]
    public static extern void Index(double c);

    [DllImport(gl, EntryPoint = "glIndexdv"), SuppressGCTransition]
    public static extern void Index(double* c);

    public static void Index(double[] c)
    {
        fixed (double* cPtr = c)
            Index(cPtr);
    }

    [DllImport(gl, EntryPoint = "glIndexfv"), SuppressGCTransition]
    public static extern void Index(float* c);

    [DllImport(gl, EntryPoint = "glIndexf"), SuppressGCTransition]
    public static extern void Index(float c);

    public static void Index(float[] c)
    {
        fixed (float* cPtr = c)
            Index(cPtr);
    }

    [DllImport(gl, EntryPoint = "glIndexi"), SuppressGCTransition]
    public static extern void Index(int c);

    [DllImport(gl, EntryPoint = "glIndexiv"), SuppressGCTransition]
    public static extern void Index(int* c);

    public static void Index(int[] c)
    {
        fixed (int* cPtr = c)
            Index(cPtr);
    }

    [DllImport(gl, EntryPoint = "glIndexs"), SuppressGCTransition]
    public static extern void Index(short c);

    [DllImport(gl, EntryPoint = "glIndexsv"), SuppressGCTransition]
    public static extern void Index(short* c);

    public static void Index(short[] c)
    {
        fixed (short* cPtr = c)
            Index(cPtr);
    }

    [DllImport(gl, EntryPoint = "glIndexub"), SuppressGCTransition]
    public static extern void Index(byte c);

    [DllImport(gl, EntryPoint = "glIndexubv"), SuppressGCTransition]
    public static extern void Index(byte* c);

    public static void Index(byte[] c)
    {
        fixed (byte* cPtr = c)
            Index(cPtr);
    }

    [DllImport(gl, EntryPoint = "glInitNames"), SuppressGCTransition]
    public static extern void InitNames();

    [DllImport(gl, EntryPoint = "glInterleavedArrays"), SuppressGCTransition]
    public static extern void InterleavedArrays(ArrayFormat format, int stride, nint pointer);

    [DllImport(gl, EntryPoint = "glIsEnabled"), SuppressGCTransition]
    public static extern bool IsEnabled(Cap cap);

    [DllImport(gl, EntryPoint = "glIsList"), SuppressGCTransition]
    public static extern byte IsList(uint list);

    [DllImport(gl, EntryPoint = "glIsTexture"), SuppressGCTransition]
    public static extern byte IsTexture(uint texture);

    [DllImport(gl, EntryPoint = "glLightModelf"), SuppressGCTransition]
    public static extern void LightModel(LightModel pname, float param);

    [DllImport(gl, EntryPoint = "glLightModelfv"), SuppressGCTransition]
    public static extern void LightModel(LightModel pname, float* @params);

    public static void LightModel(LightModel pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            LightModel(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glLightModeli"), SuppressGCTransition]
    public static extern void LightModel(LightModel pname, int param);

    [DllImport(gl, EntryPoint = "glLightModeliv"), SuppressGCTransition]
    public static extern void LightModel(LightModel pname, int* @params);

    public static void LightModel(LightModel pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            LightModel(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glLightf"), SuppressGCTransition]
    public static extern void Light(LightOrdinal light, LightN pname, float param);

    [DllImport(gl, EntryPoint = "glLightfv"), SuppressGCTransition]
    public static extern void Light(LightOrdinal light, LightN pname, float* @params);

    public static void Light(LightOrdinal light, LightN pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            Light(light, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glLighti"), SuppressGCTransition]
    public static extern void Light(LightOrdinal light, LightN pname, int param);

    [DllImport(gl, EntryPoint = "glLightiv"), SuppressGCTransition]
    public static extern void Light(LightOrdinal light, LightN pname, int* @params);

    public static void Light(LightOrdinal light, LightN pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            Light(light, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glLineStipple"), SuppressGCTransition]
    public static extern void LineStipple(int factor, ushort pattern);

    [DllImport(gl, EntryPoint = "glLineWidth"), SuppressGCTransition]
    public static extern void LineWidth(float width);

    [DllImport(gl, EntryPoint = "glListBase"), SuppressGCTransition]
    public static extern void ListBase(uint @base);

    [DllImport(gl, EntryPoint = "glLoadIdentity"), SuppressGCTransition]
    public static extern void LoadIdentity();

    public static void LoadMatrix(double[] m)
    {
        fixed (double* mPtr = m)
            LoadMatrix(mPtr);
    }

    [DllImport(gl, EntryPoint = "glLoadMatrixd"), SuppressGCTransition]
    public static extern void LoadMatrix(double* m);

    public static void LoadMatrix(MatrixType mode, double* m)
    {
        MatrixMode(mode);
        LoadMatrix(m);
    }

    public static void LoadMatrix(MatrixType mode, double[] m)
    {
        MatrixMode(mode);
        fixed (double* mPtr = m)
            LoadMatrix(mPtr);
    }

    public static void LoadModelview(double* m) => LoadMatrix(MatrixType.Modelview, m);

    public static void LoadProjection(double* m) => LoadMatrix(MatrixType.Projection, m);

    public static double[] LoadModelviewd()
    {
        var m = new double[16];
        fixed (double* mPtr = m)
            LoadModelview(mPtr);
        return m;
    }

    public static double[] LoadProjectiond()
    {
        var m = new double[16];
        fixed (double* mPtr = m)
            LoadProjection(mPtr);
        return m;
    }

    public static void LoadMatrix(float[] m)
    {
        fixed (float* mPtr = m)
            LoadMatrix(mPtr);
    }

    [DllImport(gl, EntryPoint = "glLoadMatrixf"), SuppressGCTransition]
    public static extern void LoadMatrix(float* m);

    public static void LoadMatrix(MatrixType mode, float* m)
    {
        MatrixMode(mode);
        LoadMatrix(m);
    }

    public static void LoadMatrix(MatrixType mode, float[] m)
    {
        MatrixMode(mode);
        fixed (float* mPtr = m)
            LoadMatrix(mPtr);
    }

    public static float[] LoadModelviewf()
    {
        var m = new float[16];
        fixed (float* mPtr = m)
            LoadModelview(mPtr);
        return m;
    }

    public static float[] LoadProjectionf()
    {
        var m = new float[16];
        fixed (float* mPtr = m)
            LoadProjection(mPtr);
        return m;
    }

    public static void LoadModelview(float* m) => LoadMatrix(MatrixType.Modelview, m);

    public static void LoadProjection(float* m) => LoadMatrix(MatrixType.Projection, m);

    [DllImport(gl, EntryPoint = "glLoadName"), SuppressGCTransition]
    public static extern void LoadName(uint name);

    [DllImport(gl, EntryPoint = "glLogicOp"), SuppressGCTransition]
    public static extern void LogicOp(OpCode opcode);

    [DllImport(gl, EntryPoint = "glMap1d"), SuppressGCTransition]
    public static extern void Map(Map1Target target, double u1, double u2, int stride, int order, double* points);

    public static void Map(Map1Target target, double u1, double u2, int stride, int order, double[] points)
    {
        fixed (double* pointsPtr = points)
            Map(target, u1, u2, stride, order, pointsPtr);
    }

    [DllImport(gl, EntryPoint = "glMap1f"), SuppressGCTransition]
    public static extern void Map(Map1Target target, float u1, float u2, int stride, int order, float* points);

    public static void Map(Map1Target target, float u1, float u2, int stride, int order, float[] points)
    {
        fixed (float* pointsPtr = points)
            Map(target, u1, u2, stride, order, pointsPtr);
    }

    [DllImport(gl, EntryPoint = "glMap2d"), SuppressGCTransition]
    public static extern void Map(Map1Target target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double* points);

    public static void Map(Map1Target target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double[] points)
    {
        fixed (double* pointsPtr = points)
            Map(target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, pointsPtr);
    }

    [DllImport(gl, EntryPoint = "glMap2f"), SuppressGCTransition]
    public static extern void Map(Map1Target target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float* points);

    public static void Map(Map1Target target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float[] points)
    {
        fixed (float* pointsPtr = points)
            Map(target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, pointsPtr);
    }

    [DllImport(gl, EntryPoint = "glMapGrid1d"), SuppressGCTransition]
    public static extern void MapGrid(int un, double u1, double u2);

    [DllImport(gl, EntryPoint = "glMapGrid1f"), SuppressGCTransition]
    public static extern void MapGrid(int un, float u1, float u2);

    [DllImport(gl, EntryPoint = "glMapGrid2d"), SuppressGCTransition]
    public static extern void MapGrid(int un, double u1, double u2, int vn, double v1, double v2);

    [DllImport(gl, EntryPoint = "glMapGrid2f"), SuppressGCTransition]
    public static extern void MapGrid(int un, float u1, float u2, int vn, float v1, float v2);

    [DllImport(gl, EntryPoint = "glMaterialf"), SuppressGCTransition]
    public static extern void Material(SideEnum face, float param);

    [DllImport(gl, EntryPoint = "glMaterialfv"), SuppressGCTransition]
    public static extern void Material(SideEnum face, MaterialParam pname, float* @params);

    public static void Material(SideEnum face, MaterialParam pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            Material(face, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glMateriali"), SuppressGCTransition]
    public static extern void Material(SideEnum face, MaterialParam pname, int param);

    [DllImport(gl, EntryPoint = "glMaterialiv"), SuppressGCTransition]
    public static extern void Material(SideEnum face, MaterialParam pname, int* @params);

    public static void Material(SideEnum face, MaterialParam pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            Material(face, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glMatrixMode"), SuppressGCTransition]
    public static extern void MatrixMode(MatrixType mode);

    [DllImport(gl, EntryPoint = "glMultMatrixd"), SuppressGCTransition]
    public static extern void MultMatrix(double* m);

    public static void MultMatrix(double[] m)
    {
        fixed (double* mPtr = m)
            MultMatrix(mPtr);
    }

    [DllImport(gl, EntryPoint = "glMultMatrixf"), SuppressGCTransition]
    public static extern void MultMatrix(float* m);

    public static void MultMatrix(float[] m)
    {
        fixed (float* mPtr = m)
            MultMatrix(mPtr);
    }

    [DllImport(gl, EntryPoint = "glNewList"), SuppressGCTransition]
    public static extern void NewList(uint list, CompileState mode);

    [DllImport(gl, EntryPoint = "glNormal3b"), SuppressGCTransition]
    public static extern void Normal(sbyte nx, sbyte ny, sbyte nz);

    [DllImport(gl, EntryPoint = "glNormal3bv"), SuppressGCTransition]
    public static extern void Normal3(sbyte* v);

    public static void Normal3(sbyte[] v)
    {
        fixed (sbyte* vPtr = v)
            Normal3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glNormal3d"), SuppressGCTransition]
    public static extern void Normal(double nx, double ny, double nz);

    [DllImport(gl, EntryPoint = "glNormal3dv"), SuppressGCTransition]
    public static extern void Normal3(double* v);

    public static void Normal3(double[] v)
    {
        fixed (double* vPtr = v)
            Normal3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glNormal3f"), SuppressGCTransition]
    public static extern void Normal(float nx, float ny, float nz);

    [DllImport(gl, EntryPoint = "glNormal3fv"), SuppressGCTransition]
    public static extern void Normal3(float* v);

    public static void Normal3(float[] v)
    {
        fixed (float* vPtr = v)
            Normal3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glNormal3i"), SuppressGCTransition]
    public static extern void Normal(int nx, int ny, int nz);

    [DllImport(gl, EntryPoint = "glNormal3iv"), SuppressGCTransition]
    public static extern void Normal3(int* v);

    public static void Normal3(int[] v)
    {
        fixed (int* vPtr = v)
            Normal3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glNormal3s"), SuppressGCTransition]
    public static extern void Normal(short nx, short ny, short nz);

    [DllImport(gl, EntryPoint = "glNormal3sv"), SuppressGCTransition]
    public static extern void Normal3(short* v);

    public static void Normal3(short[] v)
    {
        fixed (short* vPtr = v)
            Normal3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glNormalPointer"), SuppressGCTransition]
    public static extern void NormalPointer(PtrType type, int stride, nint pointer);

    [DllImport(gl, EntryPoint = "glOrtho"), SuppressGCTransition]
    public static extern void Ortho(double left, double right, double bottom, double top, double zNear, double zFar);

    [DllImport(gl, EntryPoint = "glPassThrough"), SuppressGCTransition]
    public static extern void PassThrough(float token);

    [DllImport(gl, EntryPoint = "glPixelMapfv"), SuppressGCTransition]
    public static extern void PixelMap(MapType map, int mapsize, float* values);

    public static void PixelMap(MapType map, int mapsize, float[] values)
    {
        fixed (float* valuesPtr = values)
            PixelMap(map, mapsize, valuesPtr);
    }

    [DllImport(gl, EntryPoint = "glPixelMapuiv"), SuppressGCTransition]
    public static extern void PixelMap(MapType map, int mapsize, uint* values);

    public static void PixelMap(MapType map, int mapsize, uint[] values)
    {
        fixed (uint* valuesPtr = values)
            PixelMap(map, mapsize, valuesPtr);
    }

    [DllImport(gl, EntryPoint = "glPixelMapusv"), SuppressGCTransition]
    public static extern void PixelMapu(MapType map, int mapsize, ushort* values);

    public static void PixelMapu(MapType map, int mapsize, ushort[] values)
    {
        fixed (ushort* valuesPtr = values)
            PixelMapu(map, mapsize, valuesPtr);
    }

    [DllImport(gl, EntryPoint = "glPixelStoref"), SuppressGCTransition]
    public static extern void PixelStore(StoreN pname, float param);

    [DllImport(gl, EntryPoint = "glPixelStorei"), SuppressGCTransition]
    public static extern void PixelStore(StoreN pname, int param);

    [DllImport(gl, EntryPoint = "glPixelTransferf"), SuppressGCTransition]
    public static extern void PixelTransfer(TransferN pname, float param);

    [DllImport(gl, EntryPoint = "glPixelTransferi"), SuppressGCTransition]
    public static extern void PixelTransfer(TransferN pname, int param);

    [DllImport(gl, EntryPoint = "glPixelZoom"), SuppressGCTransition]
    public static extern void PixelZoom(float xfactor, float yfactor);

    [DllImport(gl, EntryPoint = "glPointSize"), SuppressGCTransition]
    public static extern void PointSize(float size);

    [DllImport(gl, EntryPoint = "glPolygonMode"), SuppressGCTransition]
    public static extern void PolygonMode(MaterialFace face, MeshType mode);

    [DllImport(gl, EntryPoint = "glPolygonOffset"), SuppressGCTransition]
    public static extern void PolygonOffset(float factor, float units);

    [DllImport(gl, EntryPoint = "glPolygonStipple"), SuppressGCTransition]
    public static extern void PolygonStipple(byte* mask);

    public static void PolygonStipple(byte[] mask)
    {
        fixed (byte* maskPtr = mask)
            PolygonStipple(maskPtr);
    }

    [DllImport(gl, EntryPoint = "glPopAttrib"), SuppressGCTransition]
    public static extern void PopAttrib();

    [DllImport(gl, EntryPoint = "glPopClientAttrib"), SuppressGCTransition]
    public static extern void PopClientAttrib();

    [DllImport(gl, EntryPoint = "glPopMatrix"), SuppressGCTransition]
    public static extern void PopMatrix();

    [DllImport(gl, EntryPoint = "glPopName"), SuppressGCTransition]
    public static extern void PopName();

    [DllImport(gl, EntryPoint = "glPrioritizeTextures"), SuppressGCTransition]
    public static extern void PrioritizeTextures(int n, uint* textures, float* priorities);

    public static void PrioritizeTextures(int n, uint[] textures, float[] priorities)
    {
        fixed (uint* texturesPtr = textures)
        fixed (float* prioritiesPtr = priorities)
            PrioritizeTextures(n, texturesPtr, prioritiesPtr);
    }

    [DllImport(gl, EntryPoint = "glPushAttrib"), SuppressGCTransition]
    public static extern void PushAttrib(uint mask);

    [DllImport(gl, EntryPoint = "glPushClientAttrib"), SuppressGCTransition]
    public static extern void PushClientAttrib(uint mask);

    [DllImport(gl, EntryPoint = "glPushMatrix"), SuppressGCTransition]
    public static extern void PushMatrix();

    [DllImport(gl, EntryPoint = "glPushName"), SuppressGCTransition]
    public static extern void PushName(uint name);

    [DllImport(gl, EntryPoint = "glRasterPos2d"), SuppressGCTransition]
    public static extern void RasterPos(double x, double y);

    [DllImport(gl, EntryPoint = "glRasterPos2dv"), SuppressGCTransition]
    public static extern void RasterPos2(double* v);

    public static void RasterPos2(double[] v)
    {
        fixed (double* vPtr = v)
            RasterPos2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos2f"), SuppressGCTransition]
    public static extern void RasterPos(float x, float y);

    [DllImport(gl, EntryPoint = "glRasterPos2fv"), SuppressGCTransition]
    public static extern void RasterPos2(float* v);

    public static void RasterPos2(float[] v)
    {
        fixed (float* vPtr = v)
            RasterPos2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos2i"), SuppressGCTransition]
    public static extern void RasterPos(int x, int y);

    [DllImport(gl, EntryPoint = "glRasterPos2iv"), SuppressGCTransition]
    public static extern void RasterPos2(int* v);

    public static void RasterPos2(int[] v)
    {
        fixed (int* vPtr = v)
            RasterPos2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos2s"), SuppressGCTransition]
    public static extern void RasterPos(short x, short y);

    [DllImport(gl, EntryPoint = "glRasterPos2sv"), SuppressGCTransition]
    public static extern void RasterPos2(short* v);

    public static void RasterPos2(short[] v)
    {
        fixed (short* vPtr = v)
            RasterPos2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos3d"), SuppressGCTransition]
    public static extern void RasterPos(double x, double y, double z);

    [DllImport(gl, EntryPoint = "glRasterPos3dv"), SuppressGCTransition]
    public static extern void RasterPos3(double* v);

    public static void RasterPos3(double[] v)
    {
        fixed (double* vPtr = v)
            RasterPos3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos3f"), SuppressGCTransition]
    public static extern void RasterPos(float x, float y, float z);

    [DllImport(gl, EntryPoint = "glRasterPos3fv"), SuppressGCTransition]
    public static extern void RasterPos3(float* v);

    public static void RasterPos3(float[] v)
    {
        fixed (float* vPtr = v)
            RasterPos3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos3i"), SuppressGCTransition]
    public static extern void RasterPos(int x, int y, int z);

    [DllImport(gl, EntryPoint = "glRasterPos3iv"), SuppressGCTransition]
    public static extern void RasterPos3(int* v);

    public static void RasterPos3(int[] v)
    {
        fixed (int* vPtr = v)
            RasterPos3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos3s"), SuppressGCTransition]
    public static extern void RasterPos(short x, short y, short z);

    [DllImport(gl, EntryPoint = "glRasterPos3sv"), SuppressGCTransition]
    public static extern void RasterPos3(short* v);

    public static void RasterPos3(short[] v)
    {
        fixed (short* vPtr = v)
            RasterPos3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos4d"), SuppressGCTransition]
    public static extern void RasterPos(double x, double y, double z, double w);

    [DllImport(gl, EntryPoint = "glRasterPos4dv"), SuppressGCTransition]
    public static extern void RasterPos4(double* v);

    public static void RasterPos4(double[] v)
    {
        fixed (double* vPtr = v)
            RasterPos4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos4f"), SuppressGCTransition]
    public static extern void RasterPos(float x, float y, float z, float w);

    [DllImport(gl, EntryPoint = "glRasterPos4fv"), SuppressGCTransition]
    public static extern void RasterPos4(float* v);

    public static void RasterPos4(float[] v)
    {
        fixed (float* vPtr = v)
            RasterPos4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos4i"), SuppressGCTransition]
    public static extern void RasterPos(int x, int y, int z, int w);

    [DllImport(gl, EntryPoint = "glRasterPos4iv"), SuppressGCTransition]
    public static extern void RasterPos4(int* v);

    public static void RasterPos4(int[] v)
    {
        fixed (int* vPtr = v)
            RasterPos4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glRasterPos4s"), SuppressGCTransition]
    public static extern void RasterPos(short x, short y, short z, short w);

    [DllImport(gl, EntryPoint = "glRasterPos4sv"), SuppressGCTransition]
    public static extern void RasterPos4(short* v);

    public static void RasterPos4(short[] v)
    {
        fixed (short* vPtr = v)
            RasterPos4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glReadBuffer"), SuppressGCTransition]
    public static extern void ReadBuffer(BufType mode);

    [DllImport(gl, EntryPoint = "glReadPixels"), SuppressGCTransition]
    public static extern void ReadPixels(int x, int y, int width, int height, ImagePixelType format, ReadType type, nint pixels);

    [DllImport(gl, EntryPoint = "glRectd"), SuppressGCTransition]
    public static extern void Rect(double x1, double y1, double x2, double y2);

    [DllImport(gl, EntryPoint = "glRectdv"), SuppressGCTransition]
    public static extern void Rect(double* v1, double* v2);

    public static void Rect(double[] v1, double[] v2)
    {
        fixed (double* v1Ptr = v1)
        fixed (double* v2Ptr = v2)
            Rect(v1Ptr, v2Ptr);
    }

    [DllImport(gl, EntryPoint = "glRectf"), SuppressGCTransition]
    public static extern void Rect(float x1, float y1, float x2, float y2);

    [DllImport(gl, EntryPoint = "glRectfv"), SuppressGCTransition]
    public static extern void Rect(float* v1, float* v2);

    public static void Rect(float[] v1, float[] v2)
    {
        fixed (float* v1Ptr = v1)
        fixed (float* v2Ptr = v2)
            Rect(v1Ptr, v2Ptr);
    }

    [DllImport(gl, EntryPoint = "glRecti"), SuppressGCTransition]
    public static extern void Rect(int x1, int y1, int x2, int y2);

    [DllImport(gl, EntryPoint = "glRectiv"), SuppressGCTransition]
    public static extern void Rect(int* v1, int* v2);

    public static void Rect(int[] v1, int[] v2)
    {
        fixed (int* v1Ptr = v1)
        fixed (int* v2Ptr = v2)
            Rect(v1Ptr, v2Ptr);
    }

    [DllImport(gl, EntryPoint = "glRects"), SuppressGCTransition]
    public static extern void Rect(short x1, short y1, short x2, short y2);

    [DllImport(gl, EntryPoint = "glRectsv"), SuppressGCTransition]
    public static extern void Rect(short* v1, short* v2);

    public static void Rect(short[] v1, short[] v2)
    {
        fixed (short* v1Ptr = v1)
        fixed (short* v2Ptr = v2)
            Rect(v1Ptr, v2Ptr);
    }

    [DllImport(gl, EntryPoint = "glRenderMode"), SuppressGCTransition]
    public static extern int RenderMode(RenderEnum mode);

    [DllImport(gl, EntryPoint = "glRotated"), SuppressGCTransition]
    public static extern void Rotate(double angle, double x, double y, double z);

    [DllImport(gl, EntryPoint = "glRotatef"), SuppressGCTransition]
    public static extern void Rotate(float angle, float x, float y, float z);

    [DllImport(gl, EntryPoint = "glScaled"), SuppressGCTransition]
    public static extern void Scale(double x, double y, double z);

    [DllImport(gl, EntryPoint = "glScalef"), SuppressGCTransition]
    public static extern void Scale(float x, float y, float z);

    [DllImport(gl, EntryPoint = "glScissor"), SuppressGCTransition]
    public static extern void Scissor(int x, int y, int width, int height);

    [DllImport(gl, EntryPoint = "glSelectBuffer"), SuppressGCTransition]
    public static extern void SelectBuffer(int size, uint* buffer);

    public static void SelectBuffer(int size, uint[] buffer)
    {
        fixed (uint* bufferPtr = buffer)
            SelectBuffer(size, bufferPtr);
    }

    [DllImport(gl, EntryPoint = "glShadeModel"), SuppressGCTransition]
    public static extern void ShadeModel(FillType mode);

    [DllImport(gl, EntryPoint = "glStencilFunc"), SuppressGCTransition]
    public static extern void StencilFunc(Func func, int @ref, uint mask);

    [DllImport(gl, EntryPoint = "glStencilMask"), SuppressGCTransition]
    public static extern void StencilMask(uint mask);

    [DllImport(gl, EntryPoint = "glStencilOp"), SuppressGCTransition]
    public static extern void StencilOp(FailType fail, FailType zfail, FailType zpass);

    [DllImport(gl, EntryPoint = "glTexCoord1d"), SuppressGCTransition]
    public static extern void TexCoord(double s);

    [DllImport(gl, EntryPoint = "glTexCoord1dv"), SuppressGCTransition]
    public static extern void TexCoord1(double* v);

    public static void TexCoord1(double[] v)
    {
        fixed (double* vPtr = v)
            TexCoord1(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord1f"), SuppressGCTransition]
    public static extern void TexCoord(float s);

    [DllImport(gl, EntryPoint = "glTexCoord1fv"), SuppressGCTransition]
    public static extern void TexCoord1(float* v);

    public static void TexCoord1(float[] v)
    {
        fixed (float* vPtr = v)
            TexCoord1(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord1i"), SuppressGCTransition]
    public static extern void TexCoord(int s);

    [DllImport(gl, EntryPoint = "glTexCoord1iv"), SuppressGCTransition]
    public static extern void TexCoord1(int* v);

    public static void TexCoord1(int[] v)
    {
        fixed (int* vPtr = v)
            TexCoord1(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord1s"), SuppressGCTransition]
    public static extern void TexCoord(short s);

    [DllImport(gl, EntryPoint = "glTexCoord1sv"), SuppressGCTransition]
    public static extern void TexCoord1(short* v);

    public static void TexCoord1(short[] v)
    {
        fixed (short* vPtr = v)
            TexCoord1(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord2d"), SuppressGCTransition]
    public static extern void TexCoord(double s, double t);

    [DllImport(gl, EntryPoint = "glTexCoord2dv"), SuppressGCTransition]
    public static extern void TexCoord2(double* v);

    public static void TexCoord2(double[] v)
    {
        fixed (double* vPtr = v)
            TexCoord2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord2f"), SuppressGCTransition]
    public static extern void TexCoord(float s, float t);

    [DllImport(gl, EntryPoint = "glTexCoord2fv"), SuppressGCTransition]
    public static extern void TexCoord2(float* v);

    public static void TexCoord2(float[] v)
    {
        fixed (float* vPtr = v)
            TexCoord2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord2i"), SuppressGCTransition]
    public static extern void TexCoord(int s, int t);

    [DllImport(gl, EntryPoint = "glTexCoord2iv"), SuppressGCTransition]
    public static extern void TexCoord2(int* v);

    public static void TexCoord2(int[] v)
    {
        fixed (int* vPtr = v)
            TexCoord2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord2s"), SuppressGCTransition]
    public static extern void TexCoord(short s, short t);

    [DllImport(gl, EntryPoint = "glTexCoord2sv"), SuppressGCTransition]
    public static extern void TexCoord2(short* v);

    public static void TexCoord2(short[] v)
    {
        fixed (short* vPtr = v)
            TexCoord2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord3d"), SuppressGCTransition]
    public static extern void TexCoord(double s, double t, double r);

    [DllImport(gl, EntryPoint = "glTexCoord3dv"), SuppressGCTransition]
    public static extern void TexCoord3(double* v);

    public static void TexCoord3(double[] v)
    {
        fixed (double* vPtr = v)
            TexCoord3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord3f"), SuppressGCTransition]
    public static extern void TexCoord(float s, float t, float r);

    [DllImport(gl, EntryPoint = "glTexCoord3fv"), SuppressGCTransition]
    public static extern void TexCoord3(float* v);

    public static void TexCoord3(float[] v)
    {
        fixed (float* vPtr = v)
            TexCoord3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord3i"), SuppressGCTransition]
    public static extern void TexCoord(int s, int t, int r);

    [DllImport(gl, EntryPoint = "glTexCoord3iv"), SuppressGCTransition]
    public static extern void TexCoord3(int* v);

    public static void TexCoord3(int[] v)
    {
        fixed (int* vPtr = v)
            TexCoord3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord3s"), SuppressGCTransition]
    public static extern void TexCoord(short s, short t, short r);

    [DllImport(gl, EntryPoint = "glTexCoord3sv"), SuppressGCTransition]
    public static extern void TexCoord3(short* v);

    public static void TexCoord3(short[] v)
    {
        fixed (short* vPtr = v)
            TexCoord3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord4d"), SuppressGCTransition]
    public static extern void TexCoord(double s, double t, double r, double q);

    [DllImport(gl, EntryPoint = "glTexCoord4dv"), SuppressGCTransition]
    public static extern void TexCoord4(double* v);

    public static void TexCoord4(double[] v)
    {
        fixed (double* vPtr = v)
            TexCoord4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord4f"), SuppressGCTransition]
    public static extern void TexCoord(float s, float t, float r, float q);

    [DllImport(gl, EntryPoint = "glTexCoord4fv"), SuppressGCTransition]
    public static extern void TexCoord4(float* v);

    public static void TexCoord4(float[] v)
    {
        fixed (float* vPtr = v)
            TexCoord4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord4i"), SuppressGCTransition]
    public static extern void TexCoord(int s, int t, int r, int q);

    [DllImport(gl, EntryPoint = "glTexCoord4iv"), SuppressGCTransition]
    public static extern void TexCoord4(int* v);

    public static void TexCoord4(int[] v)
    {
        fixed (int* vPtr = v)
            TexCoord4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoord4s"), SuppressGCTransition]
    public static extern void TexCoord(short s, short t, short r, short q);

    [DllImport(gl, EntryPoint = "glTexCoord4sv"), SuppressGCTransition]
    public static extern void TexCoord4(short* v);

    public static void TexCoord4(short[] v)
    {
        fixed (short* vPtr = v)
            TexCoord4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glTexCoordPointer"), SuppressGCTransition]
    public static extern void TexCoordPointer(int size, TexType type, int stride, nint pointer);

    [DllImport(gl, EntryPoint = "glTexEnvf"), SuppressGCTransition]
    public static extern void TexEnv(TexEnvN pname, float param);

    [DllImport(gl, EntryPoint = "glTexEnvfv"), SuppressGCTransition]
    public static extern void TexEnv(TexEnvN pname, float* @params);

    public static void TexEnv(TexEnvN pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            TexEnv(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glTexEnvi"), SuppressGCTransition]
    public static extern void TexEnv(TexEnvN pname, int param);

    [DllImport(gl, EntryPoint = "glTexEnviv"), SuppressGCTransition]
    public static extern void TexEnv(TexEnvN pname, int* @params);

    public static void TexEnv(TexEnvN pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            TexEnv(pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glTexGend"), SuppressGCTransition]
    public static extern void TexGen(CoordsEnum coord, TexGen pname, double param);

    [DllImport(gl, EntryPoint = "glTexGendv"), SuppressGCTransition]
    public static extern void TexGen(CoordsEnum coord, TexGen pname, double* @params);

    public static void TexGen(CoordsEnum coord, TexGen pname, double[] @params)
    {
        fixed (double* @paramsPtr = @params)
            TexGen(coord, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glTexGenf"), SuppressGCTransition]
    public static extern void TexGen(CoordsEnum coord, TexGen pname, float param);

    [DllImport(gl, EntryPoint = "glTexGenfv"), SuppressGCTransition]
    public static extern void TexGen(CoordsEnum coord, TexGen pname, float* @params);

    public static void TexGen(CoordsEnum coord, TexGen pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            TexGen(coord, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glTexGeni"), SuppressGCTransition]
    public static extern void TexGen(CoordsEnum coord, TexGen pname, int param);

    [DllImport(gl, EntryPoint = "glTexGeniv"), SuppressGCTransition]
    public static extern void TexGen(CoordsEnum coord, TexGen pname, int* @params);

    public static void TexGen(CoordsEnum coord, TexGen pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            TexGen(coord, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glTexImage1D"), SuppressGCTransition]
    public static extern void TexImage(int level, InternalFormat internalformat, int width, int border, ImageFormat format, ImageType type, nint pixels);

    [DllImport(gl, EntryPoint = "glTexImage2D"), SuppressGCTransition]
    public static extern void TexImage(int level, InternalFormat internalformat, int width, int height, int border, ImageFormat format, ImageType type, nint pixels);

    [DllImport(gl, EntryPoint = "glTexParameterf"), SuppressGCTransition]
    public static extern void TexParameter(TexTarget target, TexNV2 pname, float param);

    [DllImport(gl, EntryPoint = "glTexParameterfv"), SuppressGCTransition]
    public static extern void TexParameter(TexTarget target, TexNV2 pname, float* @params);

    public static void TexParameter(TexTarget target, TexNV2 pname, float[] @params)
    {
        fixed (float* @paramsPtr = @params)
            TexParameter(target, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glTexParameteri"), SuppressGCTransition]
    public static extern void TexParameter(TexTarget target, TexNV2 pname, int param);

    [DllImport(gl, EntryPoint = "glTexParameteriv"), SuppressGCTransition]
    public static extern void TexParameter(TexTarget target, TexNV2 pname, int* @params);

    public static void TexParameter(TexTarget target, TexNV2 pname, int[] @params)
    {
        fixed (int* @paramsPtr = @params)
            TexParameter(target, pname, @paramsPtr);
    }

    [DllImport(gl, EntryPoint = "glTexSubImage1D"), SuppressGCTransition]
    public static extern void TexSubImage(int level, int xoffset, int width, ImageFormat format, ImageType type, nint pixels);

    [DllImport(gl, EntryPoint = "glTexSubImage2D"), SuppressGCTransition]
    public static extern void TexSubImage(int level, int xoffset, int yoffset, int width, int height, ImageFormat format, ImageType type, nint pixels);

    [DllImport(gl, EntryPoint = "glTranslated"), SuppressGCTransition]
    public static extern void Translate(double x, double y, double z);

    [DllImport(gl, EntryPoint = "glTranslatef"), SuppressGCTransition]
    public static extern void Translate(float x, float y, float z);

    [DllImport(gl, EntryPoint = "glVertex2d"), SuppressGCTransition]
    public static extern void Vertex(double x, double y);

    [DllImport(gl, EntryPoint = "glVertex2dv"), SuppressGCTransition]
    public static extern void Vertex2(double* v);

    public static void Vertex2(double[] v)
    {
        fixed (double* vPtr = v)
            Vertex2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex2f"), SuppressGCTransition]
    public static extern void Vertex(float x, float y);

    [DllImport(gl, EntryPoint = "glVertex2fv"), SuppressGCTransition]
    public static extern void Vertex2(float* v);

    public static void Vertex2(float[] v)
    {
        fixed (float* vPtr = v)
            Vertex2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex2i"), SuppressGCTransition]
    public static extern void Vertex(int x, int y);

    [DllImport(gl, EntryPoint = "glVertex2iv"), SuppressGCTransition]
    public static extern void Vertex2(int* v);

    public static void Vertex2(int[] v)
    {
        fixed (int* vPtr = v)
            Vertex2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex2s"), SuppressGCTransition]
    public static extern void Vertex(short x, short y);

    [DllImport(gl, EntryPoint = "glVertex2sv"), SuppressGCTransition]
    public static extern void Vertex2(short* v);

    public static void Vertex2(short[] v)
    {
        fixed (short* vPtr = v)
            Vertex2(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex3d"), SuppressGCTransition]
    public static extern void Vertex(double x, double y, double z);

    [DllImport(gl, EntryPoint = "glVertex3dv"), SuppressGCTransition]
    public static extern void Vertex3(double* v);

    public static void Vertex3(double[] v)
    {
        fixed (double* vPtr = v)
            Vertex3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex3f"), SuppressGCTransition]
    public static extern void Vertex(float x, float y, float z);

    [DllImport(gl, EntryPoint = "glVertex3fv"), SuppressGCTransition]
    public static extern void Vertex3(float* v);

    public static void Vertex3(float[] v)
    {
        fixed (float* vPtr = v)
            Vertex3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex3i"), SuppressGCTransition]
    public static extern void Vertex(int x, int y, int z);

    [DllImport(gl, EntryPoint = "glVertex3iv"), SuppressGCTransition]
    public static extern void Vertex3(int* v);

    public static void Vertex3(int[] v)
    {
        fixed (int* vPtr = v)
            Vertex3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex3s"), SuppressGCTransition]
    public static extern void Vertex(short x, short y, short z);

    [DllImport(gl, EntryPoint = "glVertex3sv"), SuppressGCTransition]
    public static extern void Vertex3(short* v);

    public static void Vertex3(short[] v)
    {
        fixed (short* vPtr = v)
            Vertex3(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex4d"), SuppressGCTransition]
    public static extern void Vertex(double x, double y, double z, double w);

    [DllImport(gl, EntryPoint = "glVertex4dv"), SuppressGCTransition]
    public static extern void Vertex4(double* v);

    public static void Vertex4(double[] v)
    {
        fixed (double* vPtr = v)
            Vertex4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex4f"), SuppressGCTransition]
    public static extern void Vertex(float x, float y, float z, float w);

    [DllImport(gl, EntryPoint = "glVertex4fv"), SuppressGCTransition]
    public static extern void Vertex4(float* v);

    public static void Vertex4(float[] v)
    {
        fixed (float* vPtr = v)
            Vertex4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex4i"), SuppressGCTransition]
    public static extern void Vertex(int x, int y, int z, int w);

    [DllImport(gl, EntryPoint = "glVertex4iv"), SuppressGCTransition]
    public static extern void Vertex4(int* v);

    public static void Vertex4(int[] v)
    {
        fixed (int* vPtr = v)
            Vertex4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertex4s"), SuppressGCTransition]
    public static extern void Vertex(short x, short y, short z, short w);

    [DllImport(gl, EntryPoint = "glVertex4sv"), SuppressGCTransition]
    public static extern void Vertex4(short* v);

    public static void Vertex4(short[] v)
    {
        fixed (short* vPtr = v)
            Vertex4(vPtr);
    }

    [DllImport(gl, EntryPoint = "glVertexPointer"), SuppressGCTransition]
    public static extern void VertexPointer(int size, TexType type, int stride, nint pointer);

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

    [DllImport(gl, EntryPoint = "wglUseFontBitmapsA"), SuppressGCTransition]
    public static extern bool UseFontBitmapsA(nint hdc, uint first, uint count, uint listBase);

    [DllImport(gl, EntryPoint = "wglUseFontBitmapsW"), SuppressGCTransition]
    public static extern bool UseFontBitmapsW(nint hdc, uint first, uint count, uint listBase);

    [DllImport(gl, EntryPoint = "wglUseFontBitmapsW"), SuppressGCTransition]
    public static extern bool UseFontBitmaps(nint hdc, uint first, uint count, uint listBase);

    [DllImport(gl, EntryPoint = "wglSwapBuffers"), SuppressGCTransition]
    public static extern bool SwapBuffers(nint hdc);

    static GL()
    {
        if (Kernel32.LoadLibrary("opengl32"u8) == 0)
            throw new Exception("Unable load opengl32 module");
    }

    const string gl = "opengl32";
}