#nullable enable
using TMPro;

using Unity.AdvancedUI;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MPW.Windows;

/// <summary> Represent window. </summary>
/// <see cref="IWindowManager"/>
public interface IWindow : IUIObject, IDisposable
{
	/// <summary> Window name. </summary>
	string Name { get; set; }
	/// <summary> Window icon. </summary>
	Sprite? Icon { get; set; }
	/// <summary> Window size. </summary>
	Vector2 Size { get; set; }

	/// <summary> Window name text component. </summary>
	TextMeshProUGUI NameTmp { get; }
	/// <summary> Window icon component. </summary>
	Image IconImage { get; }
	
	/// <summary> Window buttons container in draggable panel. </summary>
	RectTransform DraggablePanelButtonsContainer { get; }

	/// <summary> Window viewport. </summary>
	RectTransform Viewport { get; }

	/// <summary> </summary>
	WindowCloseProtection CloseProtection { get; set; }


	#region Events
	/// <summary> Invoked when window <see cref="Name"/> changed. </summary>
	event WindowRenamedEventHandler? Renamed;
	/// <summary> Invoked when <see cref="Icon"/> changed. </summary>
	event WindowIconChangedEventHandler? IconChanged;
	/// <summary> Invoked when <see cref="Size"/> changed. </summary>
	event WindowResizedEventHandler? Resized;

	/// <summary> Invoked when window closing. </summary>
	event WindowCloseEventHandler? Closing;
	/// <summary> Invoked when window closed. </summary>
	event WindowCloseEventHandler? Closed;
	#endregion

	/// <summary> Close window. </summary>
	void Close();

	/// <summary> Add button in draggable panel. </summary>
	/// <returns> Created button data. </returns>
	ButtonD AddWindowButton(Sprite sprite, Sprite highlightedSprite, Sprite? disabledSprite = null, UnityAction? onClick = null);
}