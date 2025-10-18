using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode.CaesiumCrossbow;

public class CaesiumCrossbow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToRepeater(53, 2.75f, 16f, 16, 16);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 1, 50);
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-3, 0);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (type == ProjectileID.WoodenArrowFriendly)
		{
			type = ProjectileID.HellfireArrow;
		}
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<CaesiumBar>(), 28)
			.AddTile(TileID.MythrilAnvil).Register();
	}
}
