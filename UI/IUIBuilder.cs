#nullable enable
using MPW.UIElements;

using System.Collections.Generic;

using Unity.AdvancedUI;

using UnityEngine;
using UnityEngine.Events;

namespace MPW;

/// <summary> Represents ui builder service. </summary>
/// <see cref="MPWAPI.Services"/>
public interface IUIBuilder
{
	#region Input
	#region Buttons
	/// <summary> Create simple 2 x 1 button. </summary>
	ButtonD SimpleButton2x1(Transform? parent = null, UnityAction? onClick = null);
	/// <summary> Create simple 1 x 1 button. </summary>
	ButtonD SimpleButton1x1(Transform? parent = null, UnityAction? onClick = null);
	#endregion
	/// <summary> Create input field with confirm button. </summary>
	IInputFieldWithConfirmButton InputFieldWithConfirmButton(Vector2 size, Transform? parent = null);

	#region Color picking
	/// <summary> Create button,that opens a color picker window when clicked. </summary>
	IColorPickerButton ColorPickerButton(Transform? parent = null, Action<Color>? colorPicked = null);
	#endregion
	#endregion

	#region Complex
	/// <summary> Create changelog. </summary>
	/// <returns> Created changelog root. </returns>
	RectTransform Changelog(Transform? parent, IEnumerable<ChangelogVersion> versions);
	#endregion


	/// <summary> Play blip sound if settings allows it. </summary>
	void Blip();
}
