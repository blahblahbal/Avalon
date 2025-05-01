using Avalon.Items.Material.Shards;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class AquaImpact : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpellBook(ModContent.ProjectileType<Projectiles.Magic.AquaBlast>(), 61, 5.5f, 10, 7f, 25, 25);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10);
		Item.UseSound = SoundID.Item21;
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
