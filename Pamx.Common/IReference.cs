namespace Pamx.Common;

public interface IReference<out T>
{
    T? Value { get; }
}