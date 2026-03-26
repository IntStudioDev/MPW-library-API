#nullable enable
using Unity.AdvancedUI;

namespace MPW.Windows;

/// <summary> Represents button of minimized <see cref="INormalWindow"/>. </summary>
/// <see cref="IMinimizedWindowsManager"/>
public interface IMinimizedWindowButton : IUIObject
{
	/// <summary> Window to which this button belongs. </summary>
	INormalWindow Window { get; }
	/// <summary> Button tooltip component. </summary>
	HasTooltip Tooltip { get; }
	/// <summary> The text of the tooltip will not change automatically. You can change it yourself. </summary>
	bool OverrideTooltip { get; set; }
}
