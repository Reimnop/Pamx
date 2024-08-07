namespace Pamx.Common.Enum;

[Flags]
public enum ObjectTimelineColor
{
    Red = 0b001,
    Green = 0b010,
    Blue = 0b100,
    
    Yellow = Red | Green,
    Cyan = Green | Blue,
    Magenta = Red | Blue,
    White = Red | Green | Blue,
    None = 0,
}