using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Data.Sets;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.UrchinMace;

public class UrchinMace : ModItem
{
	public const float ScaleMult = 1.1f;
	public override void SetStaticDefaults()
	{
		ItemSets.Maces[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<UrchinMaceProj>(), 23, 6.5f, ScaleMult, 40, width: 54, height: 54);
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
public class UrchinMaceProj : MaceTemplate
{
	public override string Texture => ModContent.GetInstance<UrchinMace>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<UrchinMace>().DisplayName;
	public override float MaxRotation => MathF.PI + MathF.PI / 8f;
	public override float? StartRotationLimit => MathHelper.PiOver2;
	public override float SwingRadius => 83f;
	public override float ScaleMult => UrchinMace.ScaleMult;
	public override float EndScaleTime => 0.5f;
	public override Func<float, float> EasingFunc => rot => Easings.PowOut(rot, 2f);
	public override int TrailLength => 4;
	public override Color? TrailColor => Color.Black;
	public override void EmitDust(Vector2 handPosition, float swingRadius, float rotationProgress, float easedRotationProgress)
	{
		Vector2 offsetFromHand = Projectile.Center - handPosition;
		float dirMod = SwingDirection * Owner.gravDir;
		float progressMult = 2f - (rotationProgress * 2f);

		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water_Cavern);
		d.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;

		Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water);
		d2.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;

		Dust d3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Venom);
		d3.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
		d3.alpha = 128;
		d3.noGravity = true;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.Poisoned, TimeUtils.SecondsToTicks(Main.rand.NextBool(3) ? 3 : 1));
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Poisoned, TimeUtils.SecondsToTicks(Main.rand.NextBool(3) ? 3 : 1));
	}
}
