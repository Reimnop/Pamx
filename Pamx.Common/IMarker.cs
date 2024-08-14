namespace Pamx.Common;

public interface IMarker : IReference<IMarker>
{
    IMarker IReference<IMarker>.Value => this;
    string Name { get; set; }
    string Description { get; set; }
    int Color { get; set; }
    float Time { get; set; }
}