using System.Text.Json.Serialization;

namespace Pamx.Editor;

/// <summary>
/// Represents the beatmap's editor settings.
/// </summary>
public sealed class EditorSettings
{
    /// <summary>
    /// The beatmap's BPM snap settings.
    /// </summary>
    public BpmSettings Bpm { get; set; } = new();

    /// <summary>
    /// The beatmap's grid overlay settings.
    /// </summary>
    public GridSettings Grid { get; set; } = new();

    /// <summary>
    /// The beatmap's general settings.
    /// </summary>
    public GeneralSettings General { get; set; } = new();

    /// <summary>
    /// The beatmap's preview settings.
    /// </summary>
    public PreviewSettings PreviewSettings { get; set; } = new();

    /// <summary>
    /// The beatmap's auto-save settings.
    /// </summary>
    [JsonPropertyName("autosave")]
    public AutoSaveSettings AutoSave { get; set; } = new();
}