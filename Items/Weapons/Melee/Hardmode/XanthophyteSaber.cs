using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class XanthophyteSaber : ModItem
{
	// TODO: ADD PROJECTILE
	public override void SetDefaults()
	{
		Item.DefaultToSword(6, 4.2f, 15, width: 32, height: 36);
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
