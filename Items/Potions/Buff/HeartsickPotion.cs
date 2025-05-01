using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class HeartsickPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(63, 116, 34),
			new Color(92, 175, 46),
			new Color(159, 224, 124)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Heartsick>(), TimeUtils.MinutesToTicks(6), PotionCorkType.Obsidian);
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>(), 2)
			.AddIngredient(ItemID.LifeCrystal)
			.AddIngredient(ItemID.NeonTetra, 2)
			.AddTile(TileID.Bottles)
			.Register();
		//tropics recipe later
	}
}
