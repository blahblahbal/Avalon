using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class BleachedEbonyBreastplate : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToArmor(1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Placeable.Tile.BleachedEbony>(), 30)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
