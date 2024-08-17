namespace Pamx.Common.Data;

public struct ThemeColor()
{
    public int Index { get; set; } = 0;
    public int EndIndex { get; set; } = 0;
    public float Opacity { get; set; } = 1.0f;
    
    public static implicit operator ThemeColor(int index) 
        => new()
        {
            Index = index,
            EndIndex = index,
        };
}