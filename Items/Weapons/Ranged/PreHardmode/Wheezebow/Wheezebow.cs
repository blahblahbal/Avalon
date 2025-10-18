using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.Wheezebow;

public class Wheezebow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToBow(13, 0f, 7.5f, 21, 21, width: 12, height: 28);
		Item.scale = 1.1f;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(silver: 36);
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0, 0);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 9).AddTile(TileID.Anvils).Register();
	}
}
