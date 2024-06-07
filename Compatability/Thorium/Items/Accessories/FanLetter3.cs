using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;

namespace Avalon.Compatability.Thorium.Items.Accessories;

[ExtendsFromMod("ThoriumMod")]
public class FanLetter3 : FanLetter
{
    public override bool IsLoadingEnabled(Mod mod)
    {
		return false; //ModLoader.HasMod("ThoriumMod");
    }
}
