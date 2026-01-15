using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class DurataniumSword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(45, 5f, 24);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 62);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 8)
			.AddTile(TileID.Anvils).Register();
	}
}
