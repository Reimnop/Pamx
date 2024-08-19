namespace Pamx.Common;

/// <summary>
/// A reference to an object
/// </summary>
/// <typeparam name="T">The object type</typeparam>
public interface IReference<out T>
{
    /// <summary>
    /// The value of the object reference
    /// </summary>
    T? Value { get; }
}