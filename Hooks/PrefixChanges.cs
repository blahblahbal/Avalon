using ExxoAvalonOrigins.Common;
using Terraria;

namespace ExxoAvalonOrigins.Hooks;

public class PrefixChanges : ModHook
{

    public static bool OnIsAPrefixableAccessory(On_Item.orig_IsAPrefixableAccessory orig, Terraria.Item self)
    {
        return self.IsArmor() || orig(self);
    }

    protected override void Apply()
    {
        On_Item.IsAPrefixableAccessory += OnIsAPrefixableAccessory;
    }
}
