using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class PlasmaLamp : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.PlasmaLamp>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 3);
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type)
			.AddIngredient(ItemID.CrystalBall)
			.AddIngredient(ModContent.ItemType<Material.LivingLightningBlock>(), 50)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
