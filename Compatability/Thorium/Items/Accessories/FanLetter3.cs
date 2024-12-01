using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;

namespace Avalon.Compatability.Thorium.Items.Accessories;

[ExtendsFromMod("ThoriumMod")]
public class FanLetter3 : FanLetter
{
    public override bool IsLoadingEnabled(Mod mod)
    {
		return ModLoader.HasMod("ThoriumMod");
    }
	public override void SetBardDefaults()
	{
		base.SetBardDefaults();
		Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
	}
}
