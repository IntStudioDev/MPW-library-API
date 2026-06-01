namespace MPW.Windows.Extensions;

/// <summary> <see cref="IFieldInputWindow"/> extensions. </summary>
public static class FieldInputWindowExtensions
{
	/// <summary>
	/// Sets a validator for the window that ensures the input value is non-negative.
	/// </summary>
	/// <exception cref="ArgumentNullException"> Thrown when the window is <see langword="null"/>. </exception>
	public static void WithNonNegativeValidation<TValue>(this IFieldInputWindow<TValue> window, string errorMessage = "Value must be non-negative.")
		where TValue : IComparable<TValue>
	{
		if (window == null)
		{
			throw new ArgumentNullException(nameof(window));
		}
		window.Validator = v =>
		{
			if (v != null && v.CompareTo(default) < 0)
			{
				return errorMessage;
			}
			return null;
		};
	}
	/// <summary>
	/// Sets a validator for the window that ensures the input value is non-negative or zero.
	/// </summary>
	/// <exception cref="ArgumentNullException"> Thrown when the window is <see langword="null"/>. </exception>
	public static void WithNonNegativeOrZeroValidation<TValue>(this IFieldInputWindow<TValue> window, string errorMessage = "Value must be non-negative or zero.")
		where TValue : IComparable<TValue>
	{
		if (window == null)
		{
			throw new ArgumentNullException(nameof(window));
		}
		window.Validator = v =>
		{
			if (v != null && v.CompareTo(default) <= 0)
			{
				return errorMessage;
			}
			return null;
		};
	}
	/// <summary>
	/// Sets a validator for the window that ensures the input value is not empty.
	/// </summary>
	/// <exception cref="ArgumentNullException"> Thrown when the window is <see langword="null"/>. </exception>
	public static void WithNonNegativeOrZeroValidation(this IFieldInputWindow<string> window, string errorMessage = "Value must be not empty.")
	{
		if (window == null)
		{
			throw new ArgumentNullException(nameof(window));
		}
		window.Validator = v =>
		{
			if (string.IsNullOrEmpty(v))
			{
				return errorMessage;
			}
			return null;
		};
	}
}
