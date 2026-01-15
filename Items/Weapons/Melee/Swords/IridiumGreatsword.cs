using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class IridiumGreatsword : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(30, 5.4f, 18, crit: 6);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 14)
			.AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 3)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
