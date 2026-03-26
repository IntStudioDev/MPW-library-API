#nullable enable
namespace MPW.Windows;

/// <summary> Represents minimized windows manager service. </summary>
/// <see cref="IMinimizedWindowButton"/>
/// <see cref="MPWAPI.Services"/>
public interface IMinimizedWindowsManager
{
	/// <summary> Create minimized window button <paramref name="window"/>. </summary>
	void CreateMinimizedWindowButton(INormalWindow window);
}
