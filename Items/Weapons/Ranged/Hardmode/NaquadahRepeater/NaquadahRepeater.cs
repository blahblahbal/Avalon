using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode.NaquadahRepeater;

public class NaquadahRepeater : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToRepeater(41, 2.05f, 10.5f, 20, 20);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 30);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6, 0);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
