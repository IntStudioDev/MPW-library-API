namespace MPW.Canvases;

/// <summary> Canvas type flags. </summary>
[Flags]
public enum CanvasType : sbyte
{
	/// <summary> None. </summary>
	None = 0,
	/// <summary> Windows canvas. </summary>
	Windows = 1,
	/// <summary> Context menu canvas. </summary>
	ContextMenu = 2,
	/// <summary> World space canvas. </summary>
	World = 4,
	/// <summary> All canvases. </summary>
	All = Windows | ContextMenu | World,
}
