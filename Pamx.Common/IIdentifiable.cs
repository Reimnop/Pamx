namespace Pamx.Common;

/// <summary>
/// An identifiable object
/// </summary>
/// <typeparam name="T">The id type</typeparam>
public interface IIdentifiable<T>
{
    /// <summary>
    /// The unique id of the object
    /// </summary>
    T Id { get; set; }
}