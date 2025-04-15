using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class VirulentPowder : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Contagion;
	}
	public override void SetDefaults()
	{
		Item.damage = 0;
		Item.useStyle = 1;
		Item.shootSpeed = 4f;
		Item.shoot = ModContent.ProjectileType<Projectiles.VirulentPowder>();
		Item.width = 16;
		Item.height = 24;
		Item.maxStack = 9999;
		Item.consumable = true;
		Item.UseSound = SoundID.Item1;
		Item.useAnimation = 15;
		Item.useTime = 15;
		Item.noMelee = true;
		Item.value = 100;
	}

	public override void AddRecipes()
	{
		CreateRecipe(5).AddIngredient(ModContent.ItemType<VirulentMushroom>()).AddTile(TileID.Bottles).Register();
	}
}
