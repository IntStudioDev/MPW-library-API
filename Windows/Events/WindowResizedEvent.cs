#nullable enable
using UnityEngine;

namespace MPW.Windows;

/// <summary> Window resized event handler. </summary>
/// <param name="e"></param>
public delegate void WindowResizedEventHandler(in ResizedEventArgs e);

/// <summary> <see cref="WindowResizedEventHandler"/> args. </summary>
public readonly struct ResizedEventArgs
{
	/// <summary> Resized window. </summary>
	public readonly IWindow Window { get; }
	/// <summary> Old window size. </summary>
	public readonly Vector2 OldSize { get; }
	/// <summary> New window size. </summary>
	public readonly Vector2 NewSize { get; }
	
	/// <inheritdoc/> 
	public ResizedEventArgs(IWindow window, Vector2 oldSize, Vector2 newSize)
	{
		Window = window;
		OldSize = oldSize;
		NewSize = newSize;
	}
}