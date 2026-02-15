using Avalon.Common.Extensions;
using Avalon.Items.Material.Shards;
using Avalon.Projectiles.Magic.Tomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Tomes;

public class AquaImpact : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<AquaBlast>(), 61, 5.5f, 10, 7f, 25, 25);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10);
		Item.UseSound = SoundID.Item21;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, Main.rand.NextFloat(-1.2f, -0.8f));
		return false;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.WaterBolt)
			.AddIngredient(ModContent.ItemType<TorrentShard>(), 6)
			.AddIngredient(ItemID.SoulofMight, 15)
			.AddIngredient(ItemID.Bubble, 25)
			.AddTile(TileID.Bookcases)
			.Register();
	}
}
