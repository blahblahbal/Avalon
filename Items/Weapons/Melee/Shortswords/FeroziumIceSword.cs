using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Items.Material.Shards;
using Avalon.Projectiles.Melee.Shortsword;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Shortswords;

public class FeroziumIceSword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToShortsword(ModContent.ProjectileType<FeroziumIceswordProj>(),40, 5, 10, 4, true);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 7);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity = velocity.RotatedByRandom(0.15f) * Main.rand.NextFloat(0.8f, 1.3f);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddRecipeGroup("AdamantiteBar", 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}