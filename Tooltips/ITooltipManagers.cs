#nullable enable
using Unity.AdvancedUI;

namespace MPW.Tooltips;

/// <summary> Represents tooltip manager service. </summary>
/// <see cref="MPWAPI.Services"/>
public interface ITooltipManager
{
	/// <summary> Get tooltip.  </summary>
	ITooltip GetTooltip();
}
