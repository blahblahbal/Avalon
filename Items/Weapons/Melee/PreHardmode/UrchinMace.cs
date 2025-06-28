using Avalon.Common.Extensions;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class UrchinMace : ModItem
{
	public const float ScaleMult = 1.1f;
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<Projectiles.Melee.UrchinMaceProj>(), 23, 6.5f, ScaleMult, 40, width: 54, height: 54);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(silver: 40);
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
		Recipe.Create(Type).AddTile(TileID.WorkBenches)
			.AddIngredient(ModContent.ItemType<WaterShard>(), 2)
			.AddIngredient(ItemID.SandBlock, 25)
			.AddIngredient(ItemID.Coral, 15)
			.AddIngredient(ItemID.ShellPileBlock, 10)
			.AddIngredient(ItemID.SharkFin, 2)
			.Register();
	}
}
