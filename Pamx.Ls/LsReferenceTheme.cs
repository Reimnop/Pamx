using Pamx.Common;

namespace Pamx.Ls;

public class LsReferenceTheme(int id) : IReference<ITheme>, IIdentifiable<int>
{
    public ITheme? Value => null;
    public int Id { get; } = id;
}