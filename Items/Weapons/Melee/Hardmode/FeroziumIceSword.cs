using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Shards;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class FeroziumIceSword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToProjectileSword(ModContent.ProjectileType<Projectiles.Melee.IcicleFerozium>(), 50, 6f, 15f, 20, 20, crit: 2, scale: 1.5f, width: 54, height: 54);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 7);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.AdamantiteBar, 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ItemID.TitaniumBar, 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();

		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<TroxiniumBar>(), 18)
			.AddIngredient(ItemID.FrostCore)
			.AddIngredient(ModContent.ItemType<FrigidShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
