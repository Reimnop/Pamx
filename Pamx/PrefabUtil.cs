using System.Diagnostics.CodeAnalysis;
using Pamx.Common;
using Pamx.Common.Implementation;

namespace Pamx;

public static class PrefabUtil
{
    public static IPrefab CloneWithId(this IPrefab prefab, string? id = null)
    {
        var newId = string.IsNullOrEmpty(id)
            ? prefab.TryGetId(out var prefabId)
                ? prefabId
                : RandomUtil.GenerateId()
            : id;
        var newPrefab = new BeatmapPrefab(newId)
        {
            Name = prefab.Name,
            Description = prefab.Description,
            Preview = prefab.Preview,
            Offset = prefab.Offset,
        };
        newPrefab.BeatmapObjects.AddRange(prefab.BeatmapObjects);
        return newPrefab;
    }

    private static bool TryGetId(this IPrefab prefab, [NotNullWhen(true)] out string? id)
    {
        if (prefab is IIdentifiable<string> identifiable)
        {
            id = identifiable.Id;
            return true;
        }
        
        id = null;
        return false;
    }
}