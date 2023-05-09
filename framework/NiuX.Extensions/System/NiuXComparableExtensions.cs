namespace System;

/// <summary>
/// Extension methods for <see cref="IComparable{T}"/>.
/// </summary>
public static partial class NiuXComparableExtensions
{
    /// <summary>
    /// Checks a value is between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to be checked</param>
    /// <param name="minInclusiveValue">Minimum (inclusive) value</param>
    /// <param name="maxInclusiveValue">Maximum (inclusive) value</param>
    public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
    {
        return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
    }

    /// <summary>
    /// Checks a value is between a minimum and maximum value.
    /// </summary>
    /// <returns></returns>
    public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool includeMinValue, bool includeMaxValue)
        where T : IComparable<T>
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        var lowerCompareResult = value.CompareTo(minValue);
        var upperCompareResult = value.CompareTo(maxValue);

        return (includeMinValue && lowerCompareResult == 0) || (includeMaxValue && upperCompareResult == 0) ||
               (lowerCompareResult > 0 && upperCompareResult < 0);
    }
}
