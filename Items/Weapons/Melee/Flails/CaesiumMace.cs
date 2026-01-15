using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Avalon.Projectiles.Melee.Flails;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Flails;

public class CaesiumMace : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
	}
	public override void SetDefaults()
	{
		Item.DefaultToFlail(ModContent.ProjectileType<CaesiumMaceProj>(), 49, 9f, 40, 25f);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 1, 8);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<CaesiumBar>(), 30)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
