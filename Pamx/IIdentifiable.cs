namespace Pamx;

/// <summary>
/// Represents an identifiable object.
/// </summary>
public interface IIdentifiable<T>
{
    T Id { get; set; }
}