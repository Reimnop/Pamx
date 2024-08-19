namespace Pamx.Common.Data;

/// <summary>
/// The editor settings for the preview
/// </summary>
public struct PreviewSettings()
{
    /// <summary>
    /// How much should the preview camera be zoomed outside of the gameplay bounds
    /// </summary>
    public float CameraZoomOffset { get; set; } = 9.0f; 
    
    /// <summary>
    /// The tint color index of the zoomed out area
    /// </summary>
    public int CameraZoomOffsetColor { get; set; } = 0;
    
    public override bool Equals(object? obj)
    {
        return obj is PreviewSettings settings &&
               CameraZoomOffset == settings.CameraZoomOffset &&
               CameraZoomOffsetColor == settings.CameraZoomOffsetColor;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(CameraZoomOffset, CameraZoomOffsetColor);
    }
    
    public static bool operator ==(PreviewSettings left, PreviewSettings right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(PreviewSettings left, PreviewSettings right)
    {
        return !(left == right);
    }
}