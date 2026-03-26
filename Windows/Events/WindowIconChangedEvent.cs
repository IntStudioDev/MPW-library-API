#nullable enable
using UnityEngine;

namespace MPW.Windows;

/// <summary> <see cref="IWindow"/> icon changed event handler. </summary>
public delegate void WindowIconChangedEventHandler(in IconChangedEventArgs e);

/// <summary> <see cref="WindowIconChangedEventHandler"/> args. </summary>
public readonly struct IconChangedEventArgs
{
	/// <summary> Window whose icon has been changed. </summary>
	public readonly IWindow Window { get; }
	/// <summary> New window icon. </summary>
	public readonly Sprite NewIcon { get; }
	
	/// <inheritdoc/>
	public IconChangedEventArgs(IWindow window, Sprite newIcon)
	{
		Window = window;
		NewIcon = newIcon;
	}
}
