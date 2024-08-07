namespace Pamx.Common.Enum;

public struct ParentOffset(float position, float scale, float rotation)
{
    public float Position { get; set; } = position;
    public float Scale { get; set; } = scale;
    public float Rotation { get; set; } = rotation;
}