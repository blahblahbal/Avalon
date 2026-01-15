using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Avalon.Projectiles.Melee.Yoyos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Yoyos;

public class Noxious : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Yoyo[Item.type] = true;
		ItemID.Sets.GamepadExtraRange[Item.type] = 10;
		ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToYoyo(ModContent.ProjectileType<NoxiousProj>(), 15, 5.25f, 16f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<BacciliteBar>(), 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
