namespace Pamx.Common.Enum;

[Flags]
public enum ParentType
{
    Position = 0b001,
    Scale = 0b010,
    Rotation = 0b100,
    None = 0,
}