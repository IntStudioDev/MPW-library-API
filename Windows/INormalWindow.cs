#nullable enable
namespace MPW.Windows;

/// <summary> </summary>
public interface INormalWindow : IWindow
{
	/// <summary> </summary>
	bool IsMinimized { get; set; }

	/// <summary> </summary>
	event WindowChangeMinimizedEventHandler? MinimizedChanged;

	/// <summary> Minimized button. </summary>
	IMinimizedWindowButton MinimizedButton { get; set; }
}
