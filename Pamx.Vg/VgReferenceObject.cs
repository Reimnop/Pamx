using Pamx.Common;

namespace Pamx.Vg;

public class VgReferenceObject(string id) : IReference<IObject>, IIdentifiable<string>
{
    public static VgReferenceObject Camera { get; } = new("camera");

    public IObject? Value => null;
    public string Id { get; } = id;
}