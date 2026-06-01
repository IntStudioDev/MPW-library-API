#nullable enable
using MPW.Windows;

using Unity.AdvancedUI;

using UnityEngine;

namespace MPW;

/// <summary> Represents color picker window. </summary>
public interface IColorPickerWindow : IInputWindow<Color>
{
	/// <summary> Color picker in window. </summary>
	ColorPicker ColorPicker { get; }

	/// <summary> Invoked when color confirmed by window button. </summary>
	event Action<Color>? ColorConfirmed;
}
