using static OpenGL.Enums;
using GL = OpenGL.GLEX;

namespace Mossarium.Alpha.UI.OpenGL;

public static class GlslImpl
{
    public static void Initialize()
    {
        Shader.Vertex.GradientRgbTriangles = new GlslShader(ShaderType.Vertex,
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

        Shader.Fragment.RgbToRgba = new GlslShader(ShaderType.Fragment,
@"#version 430 core

in vec3 fragColor;

out vec4 FragColor;

void main()
{
    FragColor = vec4(fragColor, 1.0);
}"u8);

        Program.GradientRgbTriangles = new GlslProgram(
            Shader.Vertex.GradientRgbTriangles,
            Shader.Fragment.RgbToRgba
        );

        Shader.Vertex.AV = new GlslShader(ShaderType.Vertex,
@"#version 430 core

layout (std140, binding = 0) uniform WindowData 
{
    vec2 winSize;
    vec2 winSizeT;
};

layout (location = 0) uniform vec2 inPosition;
layout (location = 1) uniform vec2 inSize;

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

        Shader.Fragment.AF = new GlslShader(ShaderType.Fragment,
@"#version 430 core

layout (location = 0) uniform vec2 inPosition;
layout (location = 1) uniform vec2 inSize;
layout (location = 2) uniform float inCornerRadius;
layout (location = 3) uniform vec3 inColor;

out vec4 color;

float roundedBoxSDF(vec2 center, vec2 size, float radius) 
{
    return length(max(abs(center) - size + radius, 0.0)) - radius;
}

void main() 
{
    float edgeSoftness = 0.6;

    vec2 lowerLeft = vec2(inPosition.x, inPosition.y);
    vec2 halfSize = inSize / 2.0;

    float distance = roundedBoxSDF(gl_FragCoord.xy - lowerLeft - halfSize, halfSize, inCornerRadius);
    float smoothedAlpha = 1.0 - smoothstep(0.0, edgeSoftness * 2.0, distance);
    color = vec4(inColor, smoothedAlpha);
}"u8);

        Program.AP = new GlslProgram(
            Shader.Vertex.AV,
            Shader.Fragment.AF
        );
    }

    public static class Shader
    {
        public static class Vertex
        {
            public static GlslShader GradientRgbTriangles, AV;
        }

        public static class Fragment
        {
            public static GlslShader RgbToRgba, AF;
        }
    }

    public static class Program
    {
        public static GlslProgram GradientRgbTriangles, AP;
    }
}