using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Spears;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Spears;

public class DarklightLance : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	int timesUsed = 0;
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<DarklightLanceProjectile>(), 55, 5.5f, 22, 4f, true);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 40);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		timesUsed++;
		int soulType = -1;

		if (timesUsed == 3)
			soulType = 0;
		else if (timesUsed >= 6)
		{
			soulType = 1;
			timesUsed = 0;
		}

		if (soulType != -1)
			Projectile.NewProjectile(source,position, velocity * 3, ModContent.ProjectileType<DarklightLanceBeam>(), (int)(damage * 1.75f), knockback,player.whoAmI, soulType);
		return true;
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddTile(TileID.AdamantiteForge)
			.AddIngredient(ItemID.DarkLance)
			.AddIngredient(ItemID.Gungnir)
			.AddIngredient(ItemID.SoulofFright,20)
			.AddIngredient(ItemID.LightShard)
			.AddIngredient(ItemID.DarkShard)
			.AddIngredient(ItemID.SoulofLight,20)
			.AddIngredient(ItemID.SoulofNight,20)
			.Register();
	}
}
