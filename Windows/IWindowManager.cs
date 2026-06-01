#nullable enable
using System.Collections.Generic;

using Unity.AdvancedUI;

using UnityEngine;

namespace MPW.Windows;

/// <summary> Represents window manager service. </summary>
/// <see cref="MPWAPI.Services"/>
public interface IWindowManager
{
	/// <summary> Selected window. </summary>
	IWindow? SelectedWindow { get; set; }

	/// <summary> Enumerate all windows. </summary>
	IEnumerable<IWindow> Windows { get; }
	/// <summary> Enumerate all windows in reversed order. </summary>
	IEnumerable<IWindow> WindowsReversed { get; }

	/// <summary> Create simple window. </summary>
	/// <param name="name"> Window name. </param>
	/// <param name="icon"> Window icon. </param>
	/// <returns> Created window. </returns>
	IWindow CreateWindow(string name = "New window", Sprite? icon = null);
	/// <summary> Create window, that can be minimized. </summary>
	/// <inheritdoc cref="CreateWindow(string, Sprite?)"/>
	INormalWindow CreateNormalWindow(string name = "New window", Sprite? icon = null);

	/// <summary> Create warning window with specified text. </summary>
	/// <inheritdoc cref="CreateWindow(string, Sprite?)"/>
	IDialogWindow CreateDialogWindow(string name = "New window", Sprite? icon = null, string text = "");

	/// <summary> Create notification window with specified text and "Ok" button. </summary>
	/// <inheritdoc cref="CreateWindow(string, Sprite?)"/>
	IDialogWindow CreateNotifyWindow(string name = "Notification", Sprite? icon = null, string text = "", ButtonData ok = default);
	/// <summary> Create confirm window with specified text and "Ok", "Cancel" buttons. </summary>
	/// <inheritdoc cref="CreateWindow(string, Sprite?)"/>
	IDialogWindow CreateConfirmWindow(string name = "Confirm", Sprite? icon = null, string text = "", ButtonData ok = default, ButtonData cancel = default);

	/// <summary> Create <typeparamref name="TValue"/> input field. </summary>
	/// <typeparam name="TValue"> Type of input value. </typeparam>
	/// <inheritdoc cref="CreateDialogWindow(string, Sprite?, string)"/>
	/// <returns></returns>
	IFieldInputWindow<TValue> CreateInputWindow<TValue>(string name = "Input", Sprite? icon = null, string text = "");

	/// <summary> Create color picker window. </summary>
	/// <inheritdoc cref="CreateWindow(string, Sprite?)"/>
	IColorPickerWindow CreateColorPickerWindow(string name = "Color picker", Sprite? icon = null, Action<Color>? colorConfirmed = null);
}
