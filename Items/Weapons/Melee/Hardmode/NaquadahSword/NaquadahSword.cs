using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.NaquadahSword;

public class NaquadahSword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(55, 4f, 24);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 64);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 8)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
