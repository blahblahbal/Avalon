using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class NaquadahLance : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<Projectiles.Melee.NaquadahLance>(), 47, 5.5f, 26, 5f, scale: 1.1f);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 72);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
