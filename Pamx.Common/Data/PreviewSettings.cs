namespace Pamx.Common.Data;

public struct PreviewSettings()
{
    public float CameraZoomOffset { get; set; } = 9.0f; 
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