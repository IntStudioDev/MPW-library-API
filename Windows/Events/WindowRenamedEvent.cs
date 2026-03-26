#nullable enable
namespace MPW.Windows;

/// <summary> <see cref="IWindow"/> renamed event handler. </summary>
public delegate void WindowRenamedEventHandler(in RenamedEventArgs e);

/// <summary> <see cref="WindowRenamedEventHandler"/> args.</summary>
public readonly struct RenamedEventArgs
{
	/// <summary> Renamed window. </summary>
	public readonly IWindow Window { get; }
	/// <summary> New window name. </summary>
	public readonly string NewName { get; }
	
	/// <inheritdoc/>
	public RenamedEventArgs(IWindow window, string name)
	{
		Window = window;
		NewName = name;
	}
}
