using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class HellsteelPlate : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(18, 24);
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 0, 2);
	}
}
