#nullable enable
using ObservableCollections;

using TMPro;

using Unity.AdvancedUI;

namespace MPW.Windows;

/// <summary> Represents small window. </summary>
public interface IDialogWindow : IWindow
{
	/// <summary> Text in window. </summary>
	string Text { get; set; }
	/// <summary> Window text component. </summary>
	TextMeshProUGUI TextTmp { get; }

	/// <summary> Buttons in this dialog window. </summary>
	ObservableList<ButtonData> ButtonsData { get; }
}
