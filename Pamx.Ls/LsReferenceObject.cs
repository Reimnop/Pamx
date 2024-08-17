using Pamx.Common;

namespace Pamx.Ls;

public class LsReferenceObject(string id) : LsObject, IIdentifiable<string>
{
    public string Id { get; } = id;
}