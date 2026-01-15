using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Spears;

public class DurataniumGlaive : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<DurataniumGlaiveProjectile>(), 44, 5.1f, 26, 5f);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1);

	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}

