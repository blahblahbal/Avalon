using Avalon;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Projectiles.Melee.Spears;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Spears;

public class DarklightLance : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<DarklightLanceProjectile>(), 99, 5.5f, 26, 4f, true);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 40);

	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.DarkLance)
			.AddIngredient(ItemID.Gungnir)
			.AddIngredient(ItemID.SoulofFright)
			.AddIngredient(ItemID.DarkShard)
			.AddIngredient(ItemID.LightShard);
	}
}
