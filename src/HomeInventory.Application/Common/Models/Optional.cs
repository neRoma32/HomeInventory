namespace HomeInventory.Application.Common.Models;

public struct Optional<T>
{
    private readonly T? _value;
    public bool HasValue { get; }

    private Optional(T? value, bool hasValue)
    {
        _value = value;
        HasValue = hasValue;
    }

    public static Optional<T> Some(T value) => new(value, true);
    public static Optional<T> None() => new(default, false);

    public TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> onNone)
    {
        return HasValue ? onSome(_value!) : onNone();
    }
}