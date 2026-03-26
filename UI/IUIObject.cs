#nullable enable
using UnityEngine;

namespace MPW;

/// <summary> Represents ui object. </summary>
/// <see cref="IUIBuilder"/>
public interface IUIObject
{
	/// <summary> Is ui object alive? </summary>
	bool IsAlive { get; }

	/// <summary> UI game object. </summary>
	GameObject Obj { get; }
	/// <summary> <see cref="UnityEngine.RectTransform"/> of object. </summary>
	RectTransform RectTransform { get; }
}
