using Avalon.Common.Extensions;
using Avalon.Data.Sets;
using Avalon.Projectiles.Melee.Maces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces.WoodenClubs;

public class WoodenClub : ModItem
{
	public const float ScaleMult = 1.1f;
	public override void SetStaticDefaults()
	{
		ItemSets.Maces[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<WoodenClubProj>(), 12, 6.4f, ScaleMult, 55, false, 0, 32, 32);
		Item.value = Item.sellPrice(copper: 30);
		Item.UseSound = null;
	}
	public override bool MeleePrefix()
	{
		return true;
	}

	public int swing;
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity = Vector2.Zero;
		if (swing == 1)
		{
			swing = -1;
		}
		else
		{
			swing = 1;
		}
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, swing, Main.LocalPlayer.MountedCenter.AngleTo(Main.MouseWorld));
		return false;
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddTile(TileID.WorkBenches)
			.AddIngredient(ItemID.Wood, 20)
			.SortAfterFirstRecipesOf(ItemID.WoodYoyo)
			.Register();
	}
}
