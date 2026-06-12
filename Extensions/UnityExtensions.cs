using System.Collections;

using UnityEngine;

using UnityEngine.UI;

namespace MPW.Extensions;

internal static class UnityExtensions
{
	public static void SetVPosition(this ScrollRect scrollRect, float verticalPosition) => scrollRect.StartCoroutine(ApplyScrollPositionRoutine(scrollRect, verticalPosition));
	private static IEnumerator ApplyScrollPositionRoutine(ScrollRect scrollRect, float verticalPosition)
	{
		yield return null;
		scrollRect.verticalNormalizedPosition = verticalPosition;
		LayoutRebuilder.MarkLayoutForRebuild((RectTransform)scrollRect.transform);
	}
}
