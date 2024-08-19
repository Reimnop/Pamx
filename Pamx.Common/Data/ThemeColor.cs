namespace Pamx.Common.Data;

/// <summary>
/// The settings for theme color keyframes
/// </summary>
public struct ThemeColor()
{
    /// <summary>
    /// The index of the theme color
    /// </summary>
    public int Index { get; set; } = 0;
    
    /// <summary>
    /// The end index of the theme color, used for gradient
    /// </summary>
    public int EndIndex { get; set; } = 0;
    
    /// <summary>
    /// The opacity of the theme color
    /// </summary>
    public float Opacity { get; set; } = 1.0f;
    
    public static implicit operator ThemeColor(int index) 
        => new()
        {
            Index = index,
            EndIndex = index,
        };
}