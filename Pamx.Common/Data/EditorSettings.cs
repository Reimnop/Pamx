namespace Pamx.Common.Data;

public struct EditorSettings()
{
    public float TimelinePosition { get; set; } = 0.0f;
    public BpmSettings Bpm { get; set; } = new();
    public GridSettings Grid { get; set; } = new();
    public GeneralSettings General { get; set; } = new();
    public PreviewSettings Preview { get; set; } = new();
    public AutoSaveSettings AutoSave { get; set; } = new();
}