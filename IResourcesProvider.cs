#nullable enable
namespace MPW;

/// <summary> Provides resources. </summary>
public interface IResourcesProvider
{
	/// <summary> Get <see cref="Object"/> by name. </summary>
	/// <returns> Object or <see langword="null"/> if not found. </returns>
	T? GetObject<T>(string name) where T : Object;
}
