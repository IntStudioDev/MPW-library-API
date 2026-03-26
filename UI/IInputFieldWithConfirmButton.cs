#nullable enable
using TMPro;

using UnityEngine.UI;

namespace MPW.UIElements;

public interface IInputFieldWithConfirmButton : IUIObject
{
	TMP_InputField Field { get; }
	Button ConfirmButton { get; }
}
