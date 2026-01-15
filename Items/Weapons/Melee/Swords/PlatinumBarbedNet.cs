using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;

public class PlatinumBarbedNet : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.CatchingTool[Item.type] = true;
		ItemID.Sets.LavaproofCatchingTool[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.PlatinumBroadsword);
		Item.autoReuse = true;
		Item.useTurn = true;
		Item.useTime = 23;
		Item.useAnimation = 23;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.PlatinumBar, 10)
			.AddIngredient(ItemID.BugNet)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
