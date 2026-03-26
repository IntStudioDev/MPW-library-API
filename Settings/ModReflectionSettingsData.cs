#nullable enable
using UnityEngine;

namespace MPW.Settings;

/// <summary> Mod reflection settings data. </summary>
public readonly struct ModReflectionSettingsData : IEquatable<ModReflectionSettingsData>
{
	/// <summary> Settings name. </summary>
	public readonly string Name;
	/// <summary> Settings icon (optional). </summary>
	public readonly Sprite? Icon;

	/// <summary> Settings mod. </summary>
	public readonly ModMetaData? Mod;

	/// <summary> Current settings getter. </summary>
	public readonly Func<object> SettingsGetter;
	/// <summary> Default settings getter. </summary>
	public readonly Func<object> DefaultSettingsGetter;

	/// <summary> Save settings action. </summary>
	public readonly Action? Save;
	/// <summary> Reset settings action. </summary>
	public readonly Action? Reset;

	/// <inheritdoc/>
	public ModReflectionSettingsData(string name, ModMetaData? mod, Func<object> settingsGetter, Func<object> defaultSettingsGetter, Action? save = null, Action? reset = null)
		: this(name, null, mod, settingsGetter, defaultSettingsGetter, save, reset) { }
	/// <inheritdoc/>
	public ModReflectionSettingsData(string name, Sprite? icon, ModMetaData? mod, Func<object> settingsGetter, Func<object> defaultSettingsGetter, Action? save = null, Action? reset = null)
	{
		if (string.IsNullOrEmpty(name))
		{
			throw new ArgumentException("Must be not null or empty", nameof(name));
		}
		if (settingsGetter == null)
		{
			throw new ArgumentNullException(nameof(settingsGetter));
		}
		if (defaultSettingsGetter == null)
		{
			throw new ArgumentNullException(nameof(defaultSettingsGetter));
		}

		Name = name;
		Icon = icon;
		Mod = mod;

		SettingsGetter = settingsGetter;
		DefaultSettingsGetter = defaultSettingsGetter;

		Save = save;
		Reset = reset;
	}

	/// <summary> Get current settings or throw. </summary>
	/// <exception cref="NullReferenceException"></exception>
	/// <exception cref="Exception"></exception>
	public readonly object GetSettingsOrThrow()
	{
		try
		{
			if (SettingsGetter == null)
			{
				throw new NullReferenceException($"{nameof(SettingsGetter)} is null");
			}

			object settings = SettingsGetter.Invoke();
			if (settings == null)
			{
				throw new NullReferenceException($"{nameof(SettingsGetter)} returned null");
			}
			return settings;
		}
		catch (Exception ex)
		{
			throw new Exception($"Failed to get current settings: {ex}");
		}
	}

	/// <summary> Get default settings or throw. </summary>
	/// <exception cref="NullReferenceException"></exception>
	/// <exception cref="Exception"></exception>
	public readonly object GetDefaultSettingsOrThrow()
	{
		try
		{
			if (DefaultSettingsGetter == null)
			{
				throw new NullReferenceException($"{nameof(DefaultSettingsGetter)} is null");
			}

			object defaultSettings = DefaultSettingsGetter.Invoke();
			if (defaultSettings == null)
			{
				throw new NullReferenceException($"{nameof(DefaultSettingsGetter)} returned null");
			}
			return defaultSettings;
		}	
		catch (Exception ex)
		{
			throw new Exception($"Failed to get default settings: {ex}");
		}
	}

	/// <summary> Try get current settings. </summary>
	public readonly bool TryGetSettings(out object? settings)
	{
		if (SettingsGetter == null)
		{
			settings = null;
			Debug.LogError($"Failed to get {ToString()}: {nameof(SettingsGetter)} is null.");
			return false;
		}

		try
		{
			settings = SettingsGetter.Invoke();
			return settings != null;
		}
		catch (Exception ex)
		{
			Debug.LogError($"Failed to get {ToString()}: {ex}");
			settings = null;
			return false;
		}
	}
	/// <summary> Try get current settings. </summary>
	public readonly bool TryGetDefaultSettings(out object? defaultSettings)
	{
		if (DefaultSettingsGetter == null)
		{
			defaultSettings = null;
			Debug.LogError($"Failed to get default {ToString()}: {nameof(DefaultSettingsGetter)} is null.");
			return false;
		}

		try
		{
			defaultSettings = DefaultSettingsGetter.Invoke();
			return defaultSettings != null;
		}
		catch (Exception ex)
		{
			Debug.LogError($"Failed to get default {ToString()}: {ex}");
			defaultSettings = null;
			return false;
		}
	}


	/// <summary> Try save settings. </summary>
	public readonly bool TrySave()
	{
		if (Save == null)
		{
			Debug.LogError($"{nameof(Save)} action is null.");
			return false;
		}

		try
		{
			Save.Invoke();
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogError($"Failed to save {this}: {ex}");
			return false;
		}
	}
	/// <summary> Try reset settings. </summary>
	public readonly bool TryReset()
	{
		if (Reset == null)
		{
			Debug.LogError($"{nameof(Reset)} action is null.");
			return false;
		}

		try
		{
			Reset.Invoke();
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogError($"Failed to reset {this}: {ex}");
			return false;
		}
	}

	/// <inheritdoc/>
	public readonly override string ToString() => $"settings \"{Name}\" from {GetModName()}";

	/// <inheritdoc/>
	public readonly bool Equals(ModReflectionSettingsData other) => string.Equals(Name, other.Name, StringComparison.Ordinal) && Mod == other.Mod;
	/// <inheritdoc/>
	public readonly override bool Equals(object? obj) => obj is ModReflectionSettingsData other && Equals(other);
	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(Name, Mod);

	private readonly string GetModName() => $"\"{(Mod == null ? "null" : Mod.Name)}\"";

}
