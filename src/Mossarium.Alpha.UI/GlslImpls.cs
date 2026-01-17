using Mossarium.Alpha.UI.OpenGL;
using static OpenGL.Enums;
using GL = OpenGL.GLEX;

namespace Mossarium.Alpha.UI;

public static class GlslImpls
{
    public static void Compile()
    {
        Shader.Vertex.GradientRgbTriangles = new GlslShader(ShaderType.Vertex, 
@"#version 420 core
layout (std140, binding = 0) uniform WindowData {
    vec2 winSizeT;
};
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec3 aColor;
out vec3 ourColor;
void main()
{
    gl_Position = vec4(aPos.x * winSizeT.x - 1, aPos.y * winSizeT.y - 1, 0.0, 1.0);
    ourColor = aColor;
}"u8);

        Shader.Fragment.RgbToRgba = new GlslShader(ShaderType.Fragment,
@"#version 420 core
in vec3 ourColor;
out vec4 FragColor;
void main()
{
    FragColor = vec4(ourColor, 1.0);
}"u8);

        Program.GradientRgbTriangles = new GlslProgram([
            Shader.Vertex.GradientRgbTriangles,
            Shader.Fragment.RgbToRgba
        ]);
    }

    public static class Shader
    {
        public static class Vertex
        {
            public static GlslShader GradientRgbTriangles;
        }

        public static class Fragment
        {
            public static GlslShader RgbToRgba;
        }
    }

    public static class Program
    {
        public static GlslProgram GradientRgbTriangles;
    }
}