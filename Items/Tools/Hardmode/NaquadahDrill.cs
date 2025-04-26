using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class NaquadahDrill : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsDrill[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToDrill(ModContent.ProjectileType<Projectiles.Tools.NaquadahDrill>(), 150, 15, 6);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 5);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 15)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
