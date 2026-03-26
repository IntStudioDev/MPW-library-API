#nullable enable
using UnityEngine;

namespace MPW.Settings;

/// <summary> Created settings data. </summary>
/// <remarks> Created by <see cref="ISettingsManager.CreateReflectionSettingsNoReturn(MPW.Settings.ModReflectionSettingsData, UnityEngine.Transform?)"/></remarks>
public readonly struct CreatedSettingsD
{
	/// <summary> Created settings root. </summary>
	public readonly RectTransform SettingsRoot;
	/// <summary> Layout with settings. This is usually <see cref="UnityEngine.UI.ScrollRect.content"/>. </summary>
	public readonly RectTransform SettingsLayout;
	/// <summary> Layout with settings buttons (Save, Reset). This is usually <see cref="UnityEngine.UI.ScrollRect.content"/>. </summary>
	public readonly RectTransform ButtonsLayout;

	/// <summary> Settings layout footer. </summary>
	public readonly RectTransform SettingsLayoutFooter;

	/// <inheritdoc/>
	public CreatedSettingsD(RectTransform root, RectTransform settingsLayout, RectTransform buttonsLayout, RectTransform layoutFooter)
	{
		SettingsRoot = root;
		SettingsLayout = settingsLayout;
		ButtonsLayout = buttonsLayout;
		SettingsLayoutFooter = layoutFooter;
	}
}
