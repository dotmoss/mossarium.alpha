using DebugProfiler;
using System.Drawing;
using static OpenGL.Enums;

namespace Mossarium.Alpha.UI.OpenGL;

public static class GlPrograms
{
    public static GlProgram GradientRgbTriangles, RoundedRectangle, TransparentWindowCorners;

    public static class Shader
    {
        public static class Vertex
        {
            public static GlShader GradientRgbTriangles, GeneratedRectangle, WindowRectangle;
        }

        public static class Fragment
        {
            public static GlShader RgbToRgba, RoundedCorners, TransparentWindowCorners;
        }
    }

    public static void Initialize()
    {
        Profiler.Push(ProfileStage.GlProgramsCompilation);

        Shader.Vertex.GradientRgbTriangles = new GlShader(ShaderType.Vertex,
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

        Shader.Fragment.RgbToRgba = new GlShader(ShaderType.Fragment,
@"#version 430 core

in vec3 fragColor;

out vec4 FragColor;

void main()
{
    FragColor = vec4(fragColor, 1.0);
}"u8);

        GradientRgbTriangles = new GlProgram(
            Shader.Vertex.GradientRgbTriangles,
            Shader.Fragment.RgbToRgba
        );

        Shader.Vertex.GeneratedRectangle = new GlShader(ShaderType.Vertex,
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
        (globalPos / winSize) * 2.0 - 1.0,
        0.0,
        1.0
    );
}"u8);

        Shader.Fragment.RoundedCorners = new GlShader(ShaderType.Fragment,
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

out vec4 color;
float squircleSDF(vec2 point, vec2 size, float radius) 
{
    vec2 a = abs(point) - size + vec2(radius);
    a = max(a, 0.0);    
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

    color = vec4(inColor, smoothedAlpha);
}"u8);

        RoundedRectangle = new GlProgram(
            Shader.Vertex.GeneratedRectangle,
            Shader.Fragment.RoundedCorners
        );

        Profiler.Pop();
    }
}