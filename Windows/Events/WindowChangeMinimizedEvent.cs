#nullable enable
namespace MPW.Windows;

/// <summary> Window change minimized changed event handler. </summary>
public delegate void WindowChangeMinimizedEventHandler(in ChangeMinimizedEventArgs e);

/// <summary> <see cref="WindowChangeMinimizedEventHandler"/> args. </summary>
public readonly struct ChangeMinimizedEventArgs
{
	/// <summary> Minimized/Unminimized window. </summary>
	public readonly INormalWindow Window { get; }
	/// <summary> New window minimized value. </summary>
	public readonly bool NewMinimized { get; }

	/// <inheritdoc/>
	public ChangeMinimizedEventArgs(INormalWindow window, bool minimized)
	{
		Window = window;
		NewMinimized = minimized;
	}
}
