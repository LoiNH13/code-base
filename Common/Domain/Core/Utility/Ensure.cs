﻿namespace Domain.Core.Utility;

/// <summary>
/// Contains assertions for the most common application checks.
/// </summary>
public static class Ensure
{
    /// <summary>
    /// Ensures that the specified <see cref="string"/> value is not empty.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="message">The message to show if the check fails.</param>
    /// <param name="argumentName">The name of the argument being checked.</param>
    /// <exception cref="ArgumentException"> if the specified value is empty.</exception>
    public static void NotEmpty(string value, string message, string argumentName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    /// <summary>
    /// Ensures that the specified <see cref="Guid"/> value is not empty.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="message">The message to show if the check fails.</param>
    /// <param name="argumentName">The name of the argument being checked.</param>
    /// <exception cref="ArgumentException"> if the specified value is empty.</exception>
    public static void NotEmpty(Guid value, string message, string argumentName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    /// <summary>
    /// Ensures that the specified <see cref="DateTime"/> value is not empty.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="message">The message to show if the check fails.</param>
    /// <param name="argumentName">The name of the argument being checked.</param>
    /// <exception cref="ArgumentException"> if the specified value is the default value for the type.</exception>
    public static void NotEmpty(DateTime value, string message, string argumentName)
    {
        if (value == default)
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    /// <summary>
    /// Ensures that the specified value is not null.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="message">The message to show if the check fails.</param>
    /// <param name="argumentName">The name of the argument being checked.</param>
    /// <exception cref="ArgumentNullException"> if the specified value is null.</exception>
    public static void NotNull<T>(T value, string message, string argumentName)
        where T : class
    {
        if (value is null)
        {
            throw new ArgumentNullException(argumentName, message);
        }
    }

    /// <summary>
    /// Throws an ArgumentException if the given function returns false.
    /// </summary>
    /// <param name="func">The function to evaluate.</param>
    /// <param name="message">The error message to include in the exception.</param>
    /// <param name="argumentName">The name of the argument being checked.</param>
    /// <exception cref="ArgumentException">Thrown if the function returns false.</exception>
    public static void NotFalse(Func<bool> func, string message, string argumentName)
    {
        if (!func.Invoke())
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void GreaterThan<T>(T value, T minValue, string message, string argumentName)
        where T : IComparable<T>
    {
        if (value.CompareTo(minValue) <= 0)
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void LessThanOrEqual<T>(T value, T maxValue, string message, string argumentName)
        where T : struct, IComparable<T>
    {
        if (value.CompareTo(maxValue) > 0)
        {
            throw new ArgumentException(message, argumentName);
        }
    }
}
