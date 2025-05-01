using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class XanthophyteClub : ModItem
{
	// TODO: ADD PROJECTILE
	public override void SetDefaults()
	{
		Item.DefaultToSword(97, 6.1f, 24, scale: 1.2f);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 5, 52);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 12)
			.AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
