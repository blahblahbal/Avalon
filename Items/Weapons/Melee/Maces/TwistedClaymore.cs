using Avalon.Common.Extensions;
using Avalon.Data.Sets;
using Avalon.Items.Material.Shards;
using Avalon.Projectiles.Melee.Maces;
using Avalon.WorldGeneration.Structures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Maces;

[LegacyName("HallowedClaymore")]
public class TwistedClaymore : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<TwistedClaymoreProj>(), 90, 12f, 1, 32, width: 32, height: 32);
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
		Item.UseSound = SoundID.Item71;
	}
	public override void SetStaticDefaults()
	{
		ItemSets.Maces[Type] = true;
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
		CreateRecipe().AddIngredient(ItemID.HallowedBar, 15)
			.AddIngredient(ItemID.SoulofFright, 7)
			.AddRecipeGroup("DemoniteBar", 10)
			.AddIngredient(ModContent.ItemType<WickedShard>(),7)
			.AddTile(TileID.DemonAltar)
			.Register();
	}
}
