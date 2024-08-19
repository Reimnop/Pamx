namespace Pamx.Common.Data;

public struct EditorSettings()
{
    public BpmSettings Bpm { get; set; } = new();
    public GridSettings Grid { get; set; } = new();
    public GeneralSettings General { get; set; } = new();
    public PreviewSettings Preview { get; set; } = new();
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