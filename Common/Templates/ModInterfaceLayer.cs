using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExxoAvalonOrigins.Common.Templates;

public abstract class ModInterfaceLayer : ModType
{
    public static readonly List<ModInterfaceLayer> RegisteredInterfaceLayers = new();

    [NotNull] protected GameInterfaceLayer? GameInterfaceLayer { get; private set; }

    /// <inheritdoc />
    public override void Load() => GameInterfaceLayer =
        new LegacyGameInterfaceLayer($"{Mod.DisplayName}: {Name}", Draw, InterfaceScaleType.UI);

    public abstract void Update();
    public abstract void ModifyInterfaceLayers(List<GameInterfaceLayer> layers);

    protected sealed override void Register() => RegisteredInterfaceLayers.Add(this);
    protected abstract bool Draw();
}
