using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Data.Sets;
using Avalon.Items.Weapons.Melee.PreHardmode.WoodenClub;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.HallowedClaymore;

public class HallowedClaymore : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return true;
	}
	public const float scaleMult = 1.35f; 
	public override void SetDefaults()
	{
		Item.DefaultToMace(ModContent.ProjectileType<HallowedClaymoreProj>(), 160, 12f, scaleMult, 42, width: 56, height: 62);
		Item.ArmorPenetration = 15;
		Item.rare = ItemRarityID.Pink;
		Item.value = Item.sellPrice(0, 5);
		Item.UseSound = null;
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
}
public class HallowedClaymoreProj : MaceTemplate
{
	public override string Texture => ModContent.GetInstance<HallowedClaymore>().Texture;
	public override string TrailTexture => ModContent.GetInstance<HallowedClaymore>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<HallowedClaymore>().DisplayName;
	public override float MaxRotation => 4.5f;
	public override float SwingRadius => 100;
	public override float ScaleMult => HallowedClaymore.scaleMult;
	public override float StartScaleTime => 0.5f;
	public override float StartScaleMult => 1f;
	public override float EndScaleTime => 1f / 3f;
	public override float EndScaleMult => 1f;

	public override Color? TrailColor => new Color(1f,1f,0.4f,0f);
	public override Func<float, float> EasingFunc => rot => Easings.PowInOut(rot, 5f);
	public override int TrailLength => 8;

	public override void EmitDust(Vector2 handPosition, float swingRadius, float rotationProgress, float easedRotationProgress)
	{
		if (Projectile.localAI[2] != 1 && easedRotationProgress > 0.1f)
		{
			Projectile.localAI[2] = 1;
			SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
		}
		Vector2 offsetFromHand = Projectile.Center - handPosition;
		float dirMod = SwingDirection * Owner.gravDir;
		float speedMultiplier = Math.Clamp(Math.Abs(Projectile.oldRot[0] - Projectile.rotation), 0, 1f);
		if (speedMultiplier > 0.1f)
		{
			for (int i = 0; i < 3; i++)
			{
				Dust d = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Center, handPosition, Main.rand.NextFloat(-0.3f,0.7f)), DustID.HallowedWeapons);
				d.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * speedMultiplier * 3;
			}
		}
	}
}
