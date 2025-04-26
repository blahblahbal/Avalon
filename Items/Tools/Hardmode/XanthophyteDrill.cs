using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class XanthophyteDrill : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsDrill[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToDrill(ModContent.ProjectileType<Projectiles.Tools.XanthophyteDrill>(), 202, 35, 4, 1, shootSpeed: 40f, knockback: 1f);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 4, 32);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 18)
			.AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
			.AddTile(TileID.Anvils).Register();
	}
}
