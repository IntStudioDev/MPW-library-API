#nullable enable
using System.Collections.Generic;

using UnityEngine;

namespace MPW.Settings;

/// <summary> Manages settings. </summary>
public interface ISettingsManager
{
	/// <summary> All registered reflection mods settings.</summary>
	IReadOnlyCollection<ModReflectionSettingsData> RegisteredReflectionSettings { get; }

	/// <summary> Create reflection settings at specified parent. </summary>
	CreatedSettingsD CreateReflectionSettingsNoReturn(ModReflectionSettingsData data, Transform? parent = null);

	/// <summary> Register reflection settings in mods settings menu. </summary>
	void RegisterReflectionSettings(ModReflectionSettingsData data);
} 
