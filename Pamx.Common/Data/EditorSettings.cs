namespace Pamx.Common.Data;

/// <summary>
/// The editor settings for the beatmap
/// </summary>
public struct EditorSettings()
{
    /// <summary>
    /// The editor settings for beatmap bpm
    /// </summary>
    public BpmSettings Bpm { get; set; } = new();
    
    /// <summary>
    /// The editor settings for the grid
    /// </summary>
    public GridSettings Grid { get; set; } = new();
    
    /// <summary>
    /// The general editor settings
    /// </summary>
    public GeneralSettings General { get; set; } = new();
    
    /// <summary>
    /// The editor settings for preview
    /// </summary>
    public PreviewSettings Preview { get; set; } = new();
    
    /// <summary>
    /// The editor settings for auto-saving
    /// </summary>
    public AutoSaveSettings AutoSave { get; set; } = new();

    public override bool Equals(object? obj)
    {
        return obj is EditorSettings settings &&
               Bpm == settings.Bpm &&
               Grid == settings.Grid &&
               General == settings.General &&
               Preview == settings.Preview &&
               AutoSave == settings.AutoSave;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Bpm, Grid, General, Preview, AutoSave);
    }
    
    public static bool operator ==(EditorSettings left, EditorSettings right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(EditorSettings left, EditorSettings right)
    {
        return !(left == right);
    }
}