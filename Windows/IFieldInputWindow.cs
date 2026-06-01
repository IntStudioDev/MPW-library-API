#nullable enable
using TMPro;

namespace MPW.Windows;

/// <summary> Represents window with input field. </summary>
public interface IFieldInputWindow : IInputWindow
{
	/// <summary> Input window field. </summary>
	TMP_InputField InputField { get; }
}

/// <summary> Represents window with <typeparamref name="TValue"/> input field. </summary>
public interface IFieldInputWindow<TValue> : IFieldInputWindow, IInputWindow<TValue>
{
	/// <summary> <see cref="string"/> to <typeparamref name="TValue"/> converter. </summary>
	/// <exception cref="ArgumentNullException"></exception>
	Converter<string, TValue?> ToValueConverter { get; set; }

	/// <summary> <typeparamref name="TValue"/> to <see cref="string"/>  converter. </summary>
	/// <exception cref="ArgumentNullException"></exception>
	Converter<TValue?, string> ToStringConverter { get; set; }

	/// <summary>
	/// Optional validator called after successful conversion.
	/// Return <see langword="null"/> to accept the value, or a non-<see langword="null"/> error message to reject it.
	/// </summary>
	Func<TValue?, string?>? Validator { get; set; }
}
