using System.Collections.Immutable;

namespace Helldivers2.Core;

public sealed class StratagemDefinition
{
    public StratagemDefinition(
        string id,
        string displayName,
        ImmutableArray<StratagemDirection> sequence
    )
    {
        Id = id;
        DisplayName = displayName;
        Sequence = sequence;
    }

    public string Id { get; }

    public string DisplayName { get; }

    public ImmutableArray<StratagemDirection> Sequence { get; }

    public override string ToString() => DisplayName;
}
