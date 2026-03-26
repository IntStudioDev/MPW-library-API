#nullable enable
using UnityEngine;

namespace MPW;

/// <summary> Represents color picker element.</summary>
public interface IColorPicker : IUIObject
{
	/// <summary> Current color picker color. </summary>
	Color Color { get; set; }
	/// <summary> Invoked when color picker color changed. </summary>
	event Action<Color>? ColorChanged;
}
