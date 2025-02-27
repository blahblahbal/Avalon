using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;

namespace Avalon.ModSupport.Thorium.Items.Accessories;

[ExtendsFromMod("ThoriumMod")]
public class FanLetter3 : FanLetter
{
    public override bool IsLoadingEnabled(Mod mod)
    {
		return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
	public override void SetBardDefaults()
	{
		base.SetBardDefaults();
		Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
	}
}
