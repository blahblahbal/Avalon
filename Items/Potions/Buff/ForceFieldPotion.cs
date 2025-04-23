using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class ForceFieldPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(137, 93, 13),
			new Color(198, 137, 23),
			new Color(246, 172, 34)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.ForceField>(), TimeUtils.MinutesToTicks(5), ClassExtensions.PotionCorkType.Obsidian);
	}

	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddIngredient(ModContent.ItemType<BottledLava>())
	//        .AddIngredient(ItemID.SoulofNight, 3)
	//        .AddIngredient(ModContent.ItemType<Sweetstem>(), 2)
	//        .AddIngredient(ItemID.Hellstone)
	//        .AddTile(TileID.Bottles)
	//        .Register();
	//}
}
