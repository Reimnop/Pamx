using Pamx.Common;

namespace Pamx.Vg;

public class VgReferenceTheme(string id) : IReference<ITheme>, IIdentifiable<string>
{
    public ITheme? Value => null;
    public string Id { get; } = id;
}