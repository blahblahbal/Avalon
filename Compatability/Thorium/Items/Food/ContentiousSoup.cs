using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.Consumable;

namespace Avalon.Compatability.Thorium.Items.Food;

[ExtendsFromMod("ThoriumMod")]
public class ContentiousSoup : CookFoodItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("ThoriumMod");
	}
	//[JITWhenModsEnabled("ThoriumMod")]
	public override void CookSetStaticDefaults()
	{
		SetParticleColors((Color[])(object)new Color[3]
		{
			new Color(174, 66, 66),
			new Color(190, 90, 90),
			new Color(98, 23, 23)
		});
	}

	//[JITWhenModsEnabled("ThoriumMod")]
	public override void CookSetDefaults()
	{
		foodHealLife = 75;
		EatStyle = FoodEatStyle.Food;
		wellFedType = 26;
		Item.width = 32;
		Item.height = 30;
		Item.value = Item.sellPrice(0, 0, 4, 0);
		Item.rare = 3;
	}

	//[JITWhenModsEnabled("ThoriumMod")]
	public override void EatFood(Player player, bool quickEat)
	{
		player.AddBuff(13, 3600, true, false);
	}
}
