namespace OpenGL;

public enum ProgramStatusName : ushort
{
    LinkStatus = GL_LINK_STATUS,
    ValidateStatus = GL_VALIDATE_STATUS,
    AttachedShaders = GL_ATTACHED_SHADERS,
    AcriveAttributes = GL_ACTIVE_ATTRIBUTES,
    ActiveAttributeMaxLength = GL_ACTIVE_ATTRIBUTE_MAX_LENGTH,
    ActiveUniforms = GL_ACTIVE_UNIFORMS,
    ActiveUniformMaxLength = GL_ACTIVE_UNIFORM_MAX_LENGTH,
    BinaryLength = GL_PROGRAM_BINARY_LENGTH,
    BinaryRetrievableHint = GL_PROGRAM_BINARY_RETRIEVABLE_HINT
}