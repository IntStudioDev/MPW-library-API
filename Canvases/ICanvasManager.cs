#nullable enable
using System.Collections.Generic;

using UnityEngine;

namespace MPW.Canvases;

/// <summary> Represents canvas manager service. </summary>
/// <see cref="Canvas"/>
/// <see cref="MPWAPI.Services"/>
public interface ICanvasManager
{
	/// <summary> Canvas with windows. </summary>
	RectTransform WindowsCanvas { get; }
	/// <summary> Canvas with tooltip and context menu. </summary>
	RectTransform ContextMenuCanvas { get; }
	/// <summary> World space canvas. </summary>
	RectTransform WorldCanvas { get; }

	/// <summary> </summary>
	IEnumerable<RectTransform> GetCanvasesByType(CanvasType canvasTypes);
	/// <summary> </summary>
	void SetCanvases(CanvasType canvasTypes, bool active);
}
