using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Avalon.Common;

public abstract class ModHook : ModType
{
    public sealed override void SetupContent() => SetStaticDefaults();
	protected sealed override void Register() { }
}
