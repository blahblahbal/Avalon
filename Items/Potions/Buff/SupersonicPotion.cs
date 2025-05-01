using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class SupersonicPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(82, 83, 97),
			new Color(121, 123, 142),
			new Color(186, 189, 218)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Supersonic>(), TimeUtils.MinutesToTicks(6), PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Material.Herbs.Holybird>())
			.AddIngredient(ItemID.Cobweb, 5)
			.AddIngredient(ItemID.Cloud)
			.AddIngredient(ModContent.ItemType<Material.LifeDew>())
			.AddTile(TileID.Bottles)
			.Register();
	}
}
