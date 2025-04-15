using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
public class BleachedEbonyGreaves : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Placeable.Tile.BleachedEbony>(), 25)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
