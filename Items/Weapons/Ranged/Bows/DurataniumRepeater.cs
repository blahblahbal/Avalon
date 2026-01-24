using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Bows;

public class DurataniumRepeater : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToRepeater(39, 1.5f, 10.5f, 21, 21);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 36);
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(3, -3);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
