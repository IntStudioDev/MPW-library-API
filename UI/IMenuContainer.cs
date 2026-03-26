#nullable enable
using UnityEngine;

namespace MPW;

/// <summary> Represents service with container in MPW menu button. </summary>
/// <see cref="MPWAPI.Services"/>
public interface IMenuContainer
{
	/// <summary> Menu children container. </summary>
	RectTransform? MenuChildContainer { get; }
}
