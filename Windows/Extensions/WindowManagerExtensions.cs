#nullable enable
using System.Globalization;

using UnityEngine;

namespace MPW.Windows.Extensions;

/// <summary> <see cref="IWindowManager"/> extensions. </summary>
public static class WindowManagerExtensions
{
	#region Decimal input windows
	/// <summary> Create <see cref="float"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<float> CreateFloatInputWindow(this IWindowManager manager, string name = "Float input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<float> window = manager.CreateInputWindow<float>(name, icon, text);
		window.ToValueConverter = static s => float.Parse(s);
		window.ToStringConverter = static f => f.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.DecimalNumber;

		return window;
	}
	/// <summary> Create <see cref="float"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<double> CreateDoubleInputWindow(this IWindowManager manager, string name = "Double input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<double> window = manager.CreateInputWindow<double>(name, icon, text);
		window.ToValueConverter = static s => double.Parse(s);
		window.ToStringConverter = static f => f.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.DecimalNumber;

		return window;
	}
	#endregion

	#region Integer input windows
	/// <summary> Create <see cref="byte"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<byte> CreateByteInputWindow(this IWindowManager manager, string name = "Byte input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<byte> window = manager.CreateInputWindow<byte>(name, icon, text);
		window.ToValueConverter = static s => byte.Parse(s);
		window.ToStringConverter = static b => b.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

		return window;
	}
	/// <summary> Create <see cref="sbyte"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<sbyte> CreateSbyteInputWindow(this IWindowManager manager, string name = "Sbyte input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<sbyte> window = manager.CreateInputWindow<sbyte>(name, icon, text);
		window.ToValueConverter = static s => sbyte.Parse(s);
		window.ToStringConverter = static b => b.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

		return window;
	}

	/// <summary> Create <see cref="int"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<int> CreateIntInputWindow(this IWindowManager manager, string name = "Int input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<int> window = manager.CreateInputWindow<int>(name, icon, text);
		window.ToValueConverter = static s => int.Parse(s);
		window.ToStringConverter = static i => i.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

		return window;
	}
	/// <summary> Create <see cref="uint"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<uint> CreateUintInputWindow(this IWindowManager manager, string name = "Uint input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<uint> window = manager.CreateInputWindow<uint>(name, icon, text);
		window.ToValueConverter = static s => uint.Parse(s);
		window.ToStringConverter = static i => i.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

		return window;
	}

	/// <summary> Create <see cref="short"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<short> CreateShortInputWindow(this IWindowManager manager, string name = "Short input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<short> window = manager.CreateInputWindow<short>(name, icon, text);
		window.ToValueConverter = static s => short.Parse(s);
		window.ToStringConverter = static i => i.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

		return window;
	}
	/// <summary> Create <see cref="ushort"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<ushort> CreateUshortInputWindow(this IWindowManager manager, string name = "Ushort input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<ushort> window = manager.CreateInputWindow<ushort>(name, icon, text);
		window.ToValueConverter = static s => ushort.Parse(s);
		window.ToStringConverter = static i => i.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

		return window;
	}

	/// <summary> Create <see cref="long"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<long> CreateLongInputWindow(this IWindowManager manager, string name = "Long input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<long> window = manager.CreateInputWindow<long>(name, icon, text);
		window.ToValueConverter = static s => long.Parse(s);
		window.ToStringConverter = static i => i.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

		return window;
	}
	/// <summary> Create <see cref="ulong"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<ulong> CreateUlongInputWindow(this IWindowManager manager, string name = "Ulong input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<ulong> window = manager.CreateInputWindow<ulong>(name, icon, text);
		window.ToValueConverter = static s => ulong.Parse(s);
		window.ToStringConverter = static i => i.ToString(CultureInfo.InvariantCulture);
		window.InputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;

		return window;
	}
	#endregion

	/// <summary> Create <see cref="string"/> input window. </summary>
	/// <inheritdoc cref="IWindowManager.CreateInputWindow{TValue}(string, Sprite?, string)"/>
	public static IFieldInputWindow<string> CreateStringInputWindow(this IWindowManager manager, string name = "String input", Sprite? icon = null, string text = "")
	{
		IFieldInputWindow<string> window = manager.CreateInputWindow<string>(name, icon, text);
		window.ToValueConverter = static s => s;
		window.ToStringConverter = static s => s ?? string.Empty;

		return window;
	}
}
