namespace Pamx.Common;

/// <summary>
/// Represents a reference to a beatmap object, without having any actual object data
/// </summary>
public class BeatmapReferenceObject : IReference<IObject>, IIdentifiable<string>
{
    public IObject? Value => null;
    public string Id { get; set; }

    /// <summary>
    /// Creates a new <see cref="BeatmapReferenceObject"/>
    /// </summary>
    /// <param name="id">The reference object's id</param>
    public BeatmapReferenceObject(string id)
    {
        Id = id;
    }
}