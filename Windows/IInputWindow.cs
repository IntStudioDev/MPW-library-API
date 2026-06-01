#nullable enable
namespace MPW.Windows;

/// <summary> Represents window for some input. </summary>
public interface IInputWindow : IDialogWindow
{
	/// <summary> Close window after value confirmed? </summary>
	bool CloseWindowAfterConfirm { get; set; }
}
/// <summary> Represents window for <typeparamref name="TValue"/> input. </summary>
public interface IInputWindow<TValue> : IInputWindow
{
	/// <summary> Current value. </summary>
	/// <exception cref="InvalidCastException"> Thrown if failed to cast value. </exception>
	TValue? Value { get; set; }

	//// <summary> Invoked when input confirmed by window button. </summary>
	event Action<TValue?>? ValueConfirmed;
}
