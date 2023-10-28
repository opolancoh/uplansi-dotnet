namespace Uplansi.Core.Exceptions;

public sealed class RecordNotFoundException<T> : Exception
{
    public RecordNotFoundException(T entityId)
        : base($"The record with id '{entityId}' doesn't exist.")
    {
    }
}