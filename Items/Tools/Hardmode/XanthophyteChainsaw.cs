using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class XanthophyteChainsaw : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsChainsaw[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToChainsaw(ModContent.ProjectileType<Projectiles.Tools.XanthophyteChainsaw>(), 115, 50, 4.6f, 4, 1, shootSpeed: 46f);
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
