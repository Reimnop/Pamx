namespace Pamx.Common.Implementation;

/// <inheritdoc cref="Prefab" />
public class BeatmapPrefab : Prefab, IIdentifiable<string>
{
    public string Id { get; set; }

    /// <summary>
    /// Creates a new <see cref="BeatmapPrefab"/>
    /// </summary>
    /// <param name="id">The prefab's id</param>
    public BeatmapPrefab(string id)
    {
        Id = id;
    }
}