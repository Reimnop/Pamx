namespace Pamx.Neo.Objects;

public struct ObjectColorValue()
{
    public static readonly ObjectColorValue Default = new();
    
    /// <summary>
    /// The index of the theme color.
    /// </summary>
    public int Index { get; set; } = 0;
    
    /// <summary>
    /// The end index of the theme color, used for gradient.
    /// </summary>
    public int EndIndex { get; set; } = 0;
    
    /// <summary>
    /// The opacity of the theme color, ranging from 0 to 1.
    /// </summary>
    public float Opacity { get; set; } = 1.0f;
}