namespace Pamx.Common;

/// <summary>
/// Represents a marker in the beatmap
/// </summary>
public interface IMarker : IReference<IMarker>
{
    /// <inheritdoc />
    IMarker IReference<IMarker>.Value => this;
    
    /// <summary>
    /// The marker's name
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    /// The marker's description
    /// </summary>
    string Description { get; set; }
    
    /// <summary>
    /// The marker's color index
    /// </summary>
    int Color { get; set; }
    
    /// <summary>
    /// The marker's time
    /// </summary>
    float Time { get; set; }
}