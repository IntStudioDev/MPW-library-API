#nullable enable
using Microsoft.Extensions.DependencyInjection;

namespace MPW;

/// <summary> <see cref="IServiceProvider"/> extensions. </summary>
public static class ServiceProviderExtensions
{
	/// <summary> Try get service of type <typeparamref name="TService"/>. </summary>
	/// <typeparam name="TService"> Type of service. </typeparam>
	/// <returns> <see langword="true"/> if service found, otherwise <see langword="false"/>. </returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static bool TryGetService<TService>(this IServiceProvider provider,  out TService service)
	{
		if (provider == null)
		{
			throw new ArgumentNullException(nameof(provider));
		}
		service = provider.GetService<TService>()!;
		return service != null;
	}
}
