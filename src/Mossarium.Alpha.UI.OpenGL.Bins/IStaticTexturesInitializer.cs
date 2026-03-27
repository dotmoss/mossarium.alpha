namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe interface IStaticTexturesInitializer
{
    public static abstract void InitializeStaticTextures(StaticTextures* initializer);
}