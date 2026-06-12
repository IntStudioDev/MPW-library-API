using MPW.Windows;

using Unity.AdvancedUI;

using UnityEngine;

namespace MPW.Extensions;

/// <summary> Windows extensions. </summary>
/// <see cref="IWindow"/>
public static class WindowExtensions
{
	/// <param name="window"> Window to move. </param>
	/// <param name="anchor"> Target anchor. </param>
	public static void MoveTo(this IWindow window, TextAnchor anchor)
	{
		if (window == null || !window.IsAlive)
		{
			throw new ArgumentNullException(nameof(window), "Must be not null and alive.");
		}
		window.MoveTo(Anchor.From(anchor));
	}
	/// <param name="window"> Window to move. </param>
	/// <param name="normalizedPos"> Starts in lower left corner (0; 0), ends in upper right corner (1; 1). </param>
	public static void MoveTo(this IWindow window, Vector2 normalizedPos)
	{
		if (window == null || !window.IsAlive)
		{
			throw new ArgumentNullException(nameof(window), "Must be not null and alive.");
		}

		RectTransform windowT = window.RectTransform;
		if (windowT.parent is not RectTransform parentT)
		{
			throw new InvalidOperationException($"Window parent must have {typeof(RectTransform)} component.");
		}

		normalizedPos.x = Mathf.Clamp01(normalizedPos.x);
		normalizedPos.y = Mathf.Clamp01(normalizedPos.y);

		Vector2 parentSize = parentT.rect.size;
		Vector2 mySize = windowT.rect.size;

		float freeWidth = Mathf.Max(0, parentSize.x - mySize.x);
		float freeHeight = Mathf.Max(0, parentSize.y - mySize.y);

		float targetLeft = freeWidth * normalizedPos.x;
		float targetBottom = freeHeight * normalizedPos.y;

		float pivotXOffset = windowT.pivot.x * mySize.x;
		float pivotYOffset = windowT.pivot.y * mySize.y;

		float anchorXPos = windowT.anchorMin.x * parentSize.x;
		float anchorYPos = windowT.anchorMin.y * parentSize.y;

		float finalX = targetLeft + pivotXOffset - anchorXPos;
		float finalY = targetBottom + pivotYOffset - anchorYPos;

		windowT.anchoredPosition = new Vector2(finalX, finalY);
	}
}
