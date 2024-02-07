using Avalon.Common;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks;

public class PrefixChanges : ModHook
{

    public static bool OnIsAPrefixableAccessory(On_Item.orig_IsAPrefixableAccessory orig, Item self)
    {
        return self.IsArmor() || orig(self);
    }

    protected override void Apply()
    {
        On_Item.IsAPrefixableAccessory += OnIsAPrefixableAccessory;
        On_Item.Prefix += On_Item_Prefix;
    }

    private bool On_Item_Prefix(On_Item.orig_Prefix orig, Item self, int prefixWeWant)
    {
        bool flag = orig.Invoke(self, prefixWeWant);
        if (prefixWeWant == ModContent.PrefixType<Prefixes.Expanded>())
        {
            self.rare += 2;
        }
        return flag;
    }
}
