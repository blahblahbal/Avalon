using Avalon.Common.Extensions;
using Terraria;
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
		Item.DefaultToUseable(width: 16, height: 24);
		Item.UseSound = SoundID.Item1;
		Item.damage = 0;
		Item.noMelee = true;
		Item.shootSpeed = 4f;
		Item.shoot = ModContent.ProjectileType<Projectiles.VirulentPowder>();
		Item.value = Item.sellPrice(copper: 20);
	}

	public override void AddRecipes()
	{
		CreateRecipe(5).AddIngredient(ModContent.ItemType<VirulentMushroom>()).AddTile(TileID.Bottles).Register();
	}
}
