namespace Pamx.Common.Implementation;

public class BeatmapPrefab(string id) : Prefab, IIdentifiable<string>
{
    public string Id { get; set; } = id;
}