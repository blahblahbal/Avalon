using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using ThoriumMod.Items.Consumable;

namespace Avalon.Compatability.Thorium.Items.Food;

[ExtendsFromMod("ThoriumMod")]
public class ContentiousSoup : ObjectionableStock
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("ThoriumMod");
	}
	public override void CookSetStaticDefaults()
	{
		base.CookSetStaticDefaults();
		SetParticleColors(new Color[3]
		{
			new Color(129, 161, 45),
			new Color(143, 75, 75),
			new Color(83, 70, 60)
		});
	}
	public override void CookSetDefaults()
	{
		base.CookSetDefaults();
		Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
	}
}
