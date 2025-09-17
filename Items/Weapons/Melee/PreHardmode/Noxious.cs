using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class Noxious : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Yoyo[Item.type] = true;
		ItemID.Sets.GamepadExtraRange[Item.type] = 15;
		ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToYoyo(ModContent.ProjectileType<Projectiles.Melee.Noxious>(), 15, 5f, 16f);
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
