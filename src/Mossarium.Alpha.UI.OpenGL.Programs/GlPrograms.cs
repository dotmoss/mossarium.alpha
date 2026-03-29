#if DEBUG
#define Define_Disable_OpenGL_Shaders_Cache
#endif

using DebugProfiler;
using Mossarium.Alpha.UI.OpenGL.Programs.Internal;
using OpenGL;
using System.Runtime.CompilerServices;

namespace Mossarium.Alpha.UI.OpenGL.Programs;

public unsafe class GlPrograms
{
    public static void Initialize(ulong cacheVersion)
    {
        var cacheFilePath = Path.Combine(Path.GetTempPath(), $"da43ff68-6d81-4389-9fb1-1fedb101b8eb-{cacheVersion:x}");

#if !Define_Disable_OpenGL_Shaders_Cache
        if (File.Exists(cacheFilePath))
        {
            LoadFromCache(cacheFilePath);
            return;
        }
#endif
        Compile(cacheFilePath);        
    }

    static void LoadFromCache(string cacheFile)
    {
        Profiler.Push(ProfileStage.GlProgramsCacheLoading);

        Unsafe.SkipInit(out GlProgramsCacheReader cache);
        cache.SetFilePath(cacheFile);

        GradientRgbTriangles = cache.Read();
        RoundedWindowRectangle = cache.Read();
        TransparentWindowCorners = cache.Read();
        cache.Dispose();

        Profiler.Pop();
    }

    static void Compile(string cacheFile)
    {
        Profiler.Push(ProfileStage.GlProgramsCompilation);

        Unsafe.SkipInit(out GlProgramsCacheWriter cache);
        cache.SetFilePath(cacheFile);

        {
            using var vertex = new GlShader(ShaderType.Vertex,
@"#version 430 core

layout (std140, binding = 0) uniform WindowData
{
    vec2 winSize;
    vec2 winSizeT;
};

layout (location = 0) in vec2 inPos;
layout (location = 1) in vec3 inColor;

out vec3 fragColor;

void main()
{
    gl_Position = vec4(
        inPos * winSizeT - 1.0,
        0.0,
        1.0
    );

    fragColor = inColor;
}"u8);


            using var fragment = new GlShader(ShaderType.Fragment,
@"#version 430 core

in vec3 fragColor;

out vec4 outColor;

void main()
{
    outColor = vec4(fragColor, 1.0);
}"u8);
            GradientRgbTriangles = new GlProgram(vertex, fragment);
            cache.Write(GradientRgbTriangles);
        }

        {
            using var vertex = new GlShader(ShaderType.Vertex,
@"#version 430 core

layout (std140, binding = 0) uniform WindowData 
{
    vec2 winSize;
    vec2 winSizeT;
};

layout (std140, binding = 1) uniform RoundedRectangleData 
{
    vec3 inColor;
    float inCornerRadius;
    vec2 inPosition;
    vec2 inSize;
};

void main()
{
    vec2 localPos = vec2(gl_VertexID % 2, gl_VertexID / 2) * inSize;
    vec2 globalPos = inPosition + localPos;

    gl_Position = vec4(
        globalPos / winSize * 2.0 - 1.0,
        0.0,
        1.0
    );
}"u8);

            using var fragment = new GlShader(ShaderType.Fragment,
    @"#version 430 core

layout (std140, binding = 0) uniform WindowData 
{
    vec2 winSize;
    vec2 winSizeT;
};

layout (std140, binding = 1) uniform RoundedRectangleData 
{
    vec3 inColor;
    float inCornerRadius;
    vec2 inPosition;
    vec2 inSize;
};

out vec4 outColor;
float squircleSDF(vec2 point, vec2 size, float radius) 
{
    vec2 a = max(abs(point) - size + vec2(radius), 0.0);
    vec2 a2 = a * a;
    vec2 a4 = a2 * a2;
    
    float b = a4.x + a4.y;
    float b05 = sqrt(b);
    float b025 = sqrt(b05);
    return b025 - radius;
}

void main()
{
    const float edgeSoftness = 1.0;

    vec2 halfSize = inSize / 2.0;
    vec2 center = inPosition + halfSize;

    float distance = squircleSDF(gl_FragCoord.xy - center, halfSize, inCornerRadius);

    float smoothedAlpha = 1.0 - smoothstep(-edgeSoftness, edgeSoftness, distance);

    outColor = vec4(inColor, smoothedAlpha);
}"u8);

            cache.Write(RoundedWindowRectangle = new GlProgram(
                vertex,
                fragment
            ));
        }

        {
            using var vertex = new GlShader(ShaderType.Vertex,
@"#version 430 core

layout (std140, binding = 0) uniform WindowData 
{
    vec2 winSize;
    vec2 winSizeT;
};

void main()
{
    float radius = 21;
    float radius2 = radius * 2.0;
    vec2 cornerSize = vec2(radius2);
    
    int localVertexID = gl_VertexID % 3;
    vec2 globalDirection = vec2(gl_VertexID / 6, gl_VertexID % 6 / 3);
    vec2 localDirectionMask = vec2(localVertexID / 2, localVertexID % 3 % 2);
    vec2 localDirection = -(globalDirection * 2 - 1);
    vec2 position = globalDirection * winSize + localDirectionMask * localDirection * cornerSize;

    gl_Position = vec4(
        position / winSize * 2.0 - 1.0,
        0.0,
        1.0
    );
}"u8);

            using var fragment = new GlShader(ShaderType.Fragment,
    @"#version 430 core

layout (std140, binding = 0) uniform WindowData 
{
    vec2 winSize;
    vec2 winSizeT;
};

// TRANSPARENT_COLOR_REFERENCE
const vec3 inColor = vec3(1.0 / 256.0 * 56.0, 1.0 / 256.0 * 30.0, 1.0 / 256.0 * 12.0);

out vec4 outColor;
void main()
{
    vec2 center = winSize / 2.0;
    vec2 coord = center - abs(center - gl_FragCoord.xy);
    
    float distance = (coord.x + coord.y) * coord.x * coord.y;
	distance = 1.0 - step(1450.0, distance);

    outColor = vec4(inColor, distance);
}"u8);

            cache.Write(TransparentWindowCorners = new GlProgram(
                vertex,
                fragment
            ));
        }

        cache.Dispose();

        Profiler.Pop();
    }

    public static GlProgram GradientRgbTriangles, RoundedWindowRectangle, TransparentWindowCorners;
}