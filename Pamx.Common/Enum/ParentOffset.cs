namespace Pamx.Common.Enum;

public struct ParentOffset()
{
    public float Position { get; set; } = 0.0f;
    public float Scale { get; set; } = 0.0f;
    public float Rotation { get; set; } = 0.0f;

    public ParentOffset(float position, float scale, float rotation) : this()
    {
        Position = position;
        Scale = scale;
        Rotation = rotation;
    }
}