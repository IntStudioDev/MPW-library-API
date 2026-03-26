#nullable enable
using UnityEngine;

namespace MPW.UIElements;

/// <summary> Represents button that opens color picker window. </summary>
public interface IColorPickerButton : IUIObject
{
	/// <summary> Current color picker color. </summary>
	Color Color { get; set; }
	/// <summary> Invoked when color confirmed in picker window. </summary>
	/// <see cref="IColorPickerWindow.ColorConfirmed"/>
	event Action<Color>? ColorConfirmed;

	/// <summary> Color picker window name. </summary>
	string WindowName { get; set; }
	/// <summary> Color picker window icon. </summary>
	Sprite WindowIcon { get; set; }

	/// <summary> Opened color picker window. </summary>
	IColorPickerWindow? OpenedColorPickerWindow { get; }
	/// <summary> Invoked when color picker window opened by this button. </summary>
	event Action<IColorPickerWindow>? ColorPickerWindowOpened;
}
