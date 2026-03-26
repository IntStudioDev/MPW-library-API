namespace MPW.Windows;

/// <summary> <see cref="IWindow"/> close protection flags. </summary>
[Flags]
public enum WindowCloseProtection : byte
{
	/// <summary> Window will be closed after map changed or after exiting/entering the menu.  </summary>
	None = 0,
	/// <summary> Window will not be closed after map changed. </summary>
	AfterMapChange = 1,
	/// <summary> Window will not be closed after map changed and after exiting/entering the menu. </summary>
	Always = 2
}
