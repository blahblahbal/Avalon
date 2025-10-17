using Avalon.Data.Sets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.Templates;
public abstract class MaceTemplate : ModProjectile
{
	public Player Owner => Main.player[Projectile.owner];
	/// <summary>
	/// The direction the mace is swinging. If owner is facing right, a value of 1 means it is swinging clockwise. -1 means it is swinging anticlockwise. Inverse if the owner is facing left.
	/// </summary>
	public ref float SwingDirection => ref Projectile.ai[0];
	public ref float AngleToMouse => ref Projectile.ai[1];
	public ref float InitialTimeLeft => ref Projectile.ai[2];
	public virtual float MaxRotation => MathHelper.Pi * 4;
	/// <summary>
	/// Limits the starting rotational offset from <see cref="AngleToMouse"/>.
	/// </summary>
	/// <remarks>
	/// Set to <see cref="MathF.PI"/> to set the limit directly opposite the mouse, or <see cref="MathHelper.PiOver2"/> for a quarter rotation.<br></br>
	/// Defaults to null, meaning no limit.
	/// </remarks>
	public virtual float? StartRotationLimit => null;
	/// <summary>
	/// Defaults to 80f
	/// </summary>
	public virtual float SwingRadius => 80f;
	/// <summary>
	/// Defaults to <see cref="Vector2.Zero"/>
	/// </summary>
	public virtual Vector2 VisualOffset => Vector2.Zero;
	/// <summary>
	/// Defaults to 1f
	/// </summary>
	public virtual float ScaleMult => 1f;
	/// <summary>
	/// Adjusts based on the leftover time from <see cref="EndScaleTime"/>.
	/// </summary>
	/// <remarks>
	/// Defaults to 0f
	/// </remarks>
	public virtual float StartScaleTime => 0f;
	/// <summary>
	/// Defaults to 0.875f
	/// </summary>
	public virtual float StartScaleMult => 0.875f;
	/// <summary>
	/// Defaults to 0.667f
	/// </summary>
	public virtual float EndScaleTime => 0.667f;
	/// <summary>
	/// Defaults to 0.875f
	/// </summary>
	public virtual float EndScaleMult => 0.875f;
	/// <summary>
	/// The easing function the mace should use for its rotation.
	/// </summary>
	/// <remarks>
	/// Defaults to <see cref="Easings.PowOut(float, float)"/> where the second input is 2f.
	/// </remarks>
	public virtual Func<float, float> EasingFunc => static rot => Easings.PowOut(rot, 2f);
	public virtual int TrailLength => 4;
	/// <summary>
	/// Sets the color the trail should draw with.
	/// </summary>
	/// <remarks>
	/// Defaults to null, which uses the lightColor param.
	/// </remarks>
	public virtual Color? TrailColor => null;
	/// <summary>
	/// Makes the trail correctly follow the player so it looks the same regardless of the player's movement.
	/// </summary>
	/// <remarks>
	/// Defaults to true, maybe set to false if you wanted like a ghostly looking afterimage for your mace idk.
	/// </remarks>
	public virtual bool TrailFollowsPlayer => true;
	public virtual Func<(SpriteEffects, float, Vector2), (SpriteEffects, float, Vector2)> SpriteEffectsFunc => spriteEffectsTuple => spriteEffectsTuple;

	private static readonly Dictionary<int, Asset<Texture2D>> TrailTextures = [];
	public override void SetStaticDefaults()
	{
		if (!Main.dedServ && TrailLength > 0)
		{
			TrailTextures.Add(Type, ModContent.Request<Texture2D>(Texture + "_Trail"));

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = TrailLength;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
		}
		ProjectileSets.TrueMeleeProjectiles[Type] = true;
	}
	public override void SetDefaults()
	{
		Projectile.width = 26;
		Projectile.height = 26;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.hide = true;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.ownerHitCheck = true;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = -1;
	}
	public bool FirstFrame = true;
	public override void AI()
	{
		if (!Owner.active || Owner.dead || Owner.noItems || Owner.CCed)
		{
			Projectile.Kill();
			return;
		}
		Owner.heldProj = Projectile.whoAmI;

		if (FirstFrame)
		{
			Projectile.scale = Owner.GetAdjustedItemScale(Owner.HeldItem);
			Projectile.Size *= (float)(Projectile.scale < 1 ? 1 : Projectile.scale);

			Projectile.timeLeft = Owner.itemAnimationMax;
			InitialTimeLeft = Projectile.timeLeft;

			FirstFrame = false;
		}

		#region Scaling
		float swingRadius = SwingRadius;
		float scaleWhileBelow = (int)(InitialTimeLeft * EndScaleTime);
		float totalStartScaleTime = (int)((InitialTimeLeft - scaleWhileBelow) * (1f - StartScaleTime));
		float scaleWhileAbove = scaleWhileBelow + totalStartScaleTime;
		totalStartScaleTime = InitialTimeLeft - scaleWhileBelow - totalStartScaleTime;
		if (Projectile.timeLeft < scaleWhileBelow)
		{
			float scaleOld = Utils.Remap((Projectile.timeLeft + 1) / scaleWhileBelow, 0, 1, EndScaleMult, 1);
			float scaleNew = Utils.Remap(Projectile.timeLeft / scaleWhileBelow, 0, 1, EndScaleMult, 1);
			Projectile.scale = Projectile.scale / scaleOld * scaleNew;
			swingRadius *= scaleNew;
		}
		else if (StartScaleTime != 0 && Projectile.timeLeft >= scaleWhileAbove)
		{
			float scaleOld = Utils.Remap(1f - ((Projectile.timeLeft + 1 - scaleWhileAbove) / totalStartScaleTime), 0, 1, StartScaleMult, 1);
			float scaleNew = Utils.Remap(1f - ((Projectile.timeLeft - scaleWhileAbove) / totalStartScaleTime), 0, 1, StartScaleMult, 1);
			if (Projectile.timeLeft == InitialTimeLeft)
			{
				Projectile.scale = Projectile.scale * scaleNew;
			}
			else
			{
				Projectile.scale = Projectile.scale / scaleOld * scaleNew;
			}
			swingRadius *= scaleNew;
		}
		#endregion

		float curRotProgress = 1f - Projectile.timeLeft / InitialTimeLeft;
		float easedRotProgress = EasingFunc.Invoke(curRotProgress);

		Vector2 HandPosition = Owner.RotatedRelativePoint(Owner.MountedCenter) + new Vector2(Owner.direction * -4f, 0);
		float initialRotOffset = StartRotationLimit.HasValue ? Math.Clamp(MaxRotation * 0.5f, 0, StartRotationLimit.Value) : MaxRotation * 0.5f;
		Projectile.Center = HandPosition + (AngleToMouse.ToRotationVector2() * swingRadius * Projectile.scale).RotatedBy((MaxRotation * easedRotProgress - initialRotOffset) * Owner.direction * SwingDirection * Owner.gravDir);

		Projectile.rotation = Projectile.AngleFrom(HandPosition) + MathHelper.PiOver4;
		Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * Owner.gravDir + (Owner.gravDir == -1 ? MathHelper.Pi : 0) - Owner.fullRotation);

		EmitDust(HandPosition, swingRadius, curRotProgress, easedRotProgress);
	}
	/// <summary>
	/// Empty method for you to put whatever into
	/// </summary>
	public virtual void EmitDust(Vector2 handPosition, float swingRadius, float rotationProgress, float easedRotationProgress)
	{
	}
	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (targetHitbox.Intersects(projHitbox) || targetHitbox.IntersectsConeSlowMoreAccurate(Owner.MountedCenter, Projectile.Center.Distance(Owner.Center), Projectile.rotation - (45 * (MathHelper.Pi / 180)), MathHelper.Pi / 16))
		{
			return true;
		}
		return false;
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float diff = target.Center.X - Owner.Center.X;
		if (diff > 0)
		{
			modifiers.HitDirectionOverride = 1;
		}
		else
		{
			modifiers.HitDirectionOverride = -1;
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		Rectangle frame = TextureAssets.Projectile[Type].Frame();
		Vector2 drawPos = Projectile.Center - Main.screenPosition;
		Vector2 offset = new((float)(TextureAssets.Projectile[Type].Width() * ScaleMult * 0.25f) - VisualOffset.X, -(float)(TextureAssets.Projectile[Type].Height() * ScaleMult * 0.25f) - VisualOffset.Y);

		(SpriteEffects spriteDirection, float rotationFlip, Vector2 offset) spriteEffectsTuple = SpriteEffectsFunc.Invoke((SpriteEffects.None, 0f, offset));

		if (TrailLength > 0)
		{
			Vector2 HandPosition = Owner.RotatedRelativePoint(Owner.MountedCenter) + new Vector2(Owner.direction * -4f, 0);
			for (int i = 0; i < Projectile.oldPos.Length; i++)
			{
				Vector2 drawPosOld;
				if (!TrailFollowsPlayer || (Projectile.oldPos[i] == Vector2.Zero && Projectile.oldRot[i] == 0)) // janky workaround to prevent it drawing at incorrect positions (rly wish vanilla nulled these fields now that I'm thinking about it)
				{
					drawPosOld = Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2f;
				}
				else
				{
					drawPosOld = (HandPosition - Main.screenPosition) + ((Projectile.oldRot[i] - MathHelper.PiOver4).ToRotationVector2() * (Projectile.Center - HandPosition).Length());
				}
				Main.EntitySpriteDraw(TrailTextures[Type].Value, drawPosOld, frame, (TrailColor ?? lightColor) * (1 - ((float)i / (Projectile.oldPos.Length - 1))) * 0.25f, Projectile.oldRot[i] + spriteEffectsTuple.rotationFlip, frame.Size() / 2f + spriteEffectsTuple.offset, Projectile.scale, spriteEffectsTuple.spriteDirection);
			}
		}
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, frame, lightColor, Projectile.rotation + spriteEffectsTuple.rotationFlip, frame.Size() / 2f + spriteEffectsTuple.offset, Projectile.scale, spriteEffectsTuple.spriteDirection);

		return false;
	}
	public override void CutTiles()
	{
		// tilecut_0 is an unnamed decompiled variable which tells CutTiles how the tiles are being cut (in this case, via a Projectile).
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Utils.TileActionAttempt cut = new(DelegateMethods.CutTiles);
		Vector2 beamStartPos = Projectile.Center;
		Vector2 beamEndPos = Owner.MountedCenter;

		// PlotTileLine is a function which performs the specified action to all tiles along a drawn line, with a specified width.
		// In this case, it is cutting all tiles which can be destroyed by Projectiles, for example grass or pots.
		Utils.PlotTileLine(beamStartPos, beamEndPos, Projectile.width, cut);
	}
}
