#nullable enable
using System.Collections.Generic;

using UnityEngine;

namespace MPW.UIElements;

/// <summary> Contains version data for creating changelog. </summary>
/// <see cref="IUIBuilder.Changelog(Transform, IEnumerable{ChangelogVersion})"/>
public readonly struct ChangelogVersion
{
	/// <summary> Version name. </summary>
	public readonly string Name;
	/// <summary> Version color. </summary>
	public readonly Color Color;

	/// <summary> Version changes. </summary>
	public readonly IEnumerable<string> Changes;

	/// <inheritdoc/>
	public ChangelogVersion(string versionName, Color versionColor, IEnumerable<string> changes)
	{
		Name = versionName;
		Color = versionColor;
		Changes = changes;
	}
}
