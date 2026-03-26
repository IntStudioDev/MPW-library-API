#nullable enable
namespace MPW.Windows;

/// <summary> <see cref="IWindow"/> close event handler. </summary>
public delegate void WindowCloseEventHandler(in CloseEventArgs e);

/// <summary> <see cref="WindowCloseEventHandler"/> args. </summary>
public readonly struct CloseEventArgs
{
	/// <summary> Closing/closed window. </summary>
	public readonly IWindow Window { get; }

	/// <inheritdoc/>
	public CloseEventArgs(IWindow window)
	{
		Window = window;
	}
}
