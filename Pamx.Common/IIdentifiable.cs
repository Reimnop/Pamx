namespace Pamx.Common;

public interface IIdentifiable<out T>
{
    T Id { get; }
}