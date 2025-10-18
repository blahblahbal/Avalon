using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode.WoodBows;

public class BleachedEbonyBow : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.RichMahoganyBow);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Placeable.Tile.BleachedEbony>(), 10)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
