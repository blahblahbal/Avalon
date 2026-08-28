using Avalon.Common.Extensions;
using Avalon.Common.Interfaces;
using Avalon.Core;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Swords;
public class MinersSword : ModItem, ISyncedOnHitEffect
{
	public override void SetDefaults()
	{
		Item.DefaultToSword(25, 5.5f, 20, useTurn: false, width: 32, height: 32);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1);
		Item.UseSound = SoundID.Item7 with { pitch = 0.3f, volume = 2 };
	}
	public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
	{
		float power = player.GetModPlayer<MinersSwordPlayer>().Power;
		modifiers.FinalDamage *= Utils.Remap(power * power, 0, 1, 0.4f, 4);
		modifiers.Knockback *= Utils.Remap(power, 0, 1, 0.25f, 3);
		if(player.velocity.Y > 0)
		{
			modifiers.SetCrit();
			modifiers.CritDamage *= 0.8f;
		}
	}
	public override bool ModifyItemDraw(ref PlayerDrawSet drawInfo, ref DrawData drawData, ref DrawData? coloredDrawData, ref DrawData? glowMaskDrawData)
	{
		var msp = drawInfo.drawPlayer.GetModPlayer<MinersSwordPlayer>();
		drawInfo.DrawDataCache.Add(drawData with { color = Color.Lerp(drawData.color,Color.White,msp.Power)});
		var data2 = drawData with { sourceRect = new Rectangle(0, (drawData.texture.Height + 2) * ((int)(Main.timeForVisualEffects) % 23), drawData.texture.Width, drawData.texture.Height), texture = AssetReferences.Items.Weapons.Melee.Swords.MinersSwordGlow.Asset.Value, color = Color.White with { A = 0 } * msp.Power * msp.Power };
		drawInfo.DrawDataCache.Add(data2);
		Vector2[] directions = [new Vector2(2, 0), new Vector2(0, 2), new Vector2(-2, 0), new Vector2(0, -2), new Vector2(2, 2), new Vector2(-2, 2), new Vector2(2, -2), new Vector2(-2, -2)];
		for (int i = 0; i < 8; i++)
		{
			drawInfo.DrawDataCache.Add(data2 with { position = drawData.position + directions[i].RotatedBy(drawData.rotation), color = data2.color * 0.25f});
		}
		return false;
		//Color glowColor = Color.SkyBlue with { A = 0 } * 0.25f * msp.Power * msp.Power;
		//Vector2[] directions = [new Vector2(2,0), new Vector2(0, 2), new Vector2(-2, 0), new Vector2(0, -2), new Vector2(2,2), new Vector2(-2, 2), new Vector2(2, -2), new Vector2(-2, -2)];
		//for (int i = 0; i < 8; i++)
		//{
		//	drawInfo.DrawDataCache.Add(drawData with { position = drawData.position + directions[i].RotatedBy(drawData.rotation), color = glowColor, shader = ContentSamples.CommonlyUsedContentSamples.ColorOnlyShaderIndex});
		//}
		//return base.ModifyItemDraw(ref drawInfo, ref drawData, ref coloredDrawData, ref glowMaskDrawData);
	}
	public bool SyncedOnHitNPC(Player player, NPC target, Rectangle attackHitbox, int damage, float knockback, bool crit, int hitDirection, Projectile? projectile)
	{
		Vector2 pos = target.Hitbox.ClosestPointInRect(player.Center);
		var tex = TextureAssets.Extra[ExtrasID.ThePerfectGlow];
		tex.Wait();

		Color slashColor = Color.Lerp(Color.SkyBlue, new Color(0.7f,0.4f,1), player.GetModPlayer<MinersSwordPlayer>().Power) with { A = 0};
		if (player.velocity.Y > 0)
		{
			for(int i = 0; i < 5; i++)
			{
				var p = VanillaParticles.RequestPrettySparkleParticle();
				p.ColorTint = Color.OrangeRed * 0.75f;
				p.LocalPosition = pos;
				p.TimeToLive = Main.rand.Next(20, 40);
				p.AccelerationPerFrame = new Vector2(0, 0.2f);
				p.Scale = new Vector2(2,0.5f) * Main.rand.NextFloat(0.3f,1);
				p.Rotation = MathHelper.PiOver2 + Main.rand.NextFloat(-0.3f,0.3f);
				p.Velocity = new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-5, -1));
				Main.ParticleSystem_World_OverPlayers.Add(p);
			}
			for (int i = 0; i < 3; i++)
			{
				var p = VanillaParticles.RequestFadingParticle();
				p.SetBasicInfo(tex, null, Vector2.Zero, pos);
				int time = Main.rand.Next(15, 20);
				p.SetTypeInfo(time);
				p.FadeInNormalizedTime = p.FadeOutNormalizedTime = Main.rand.NextFloat(0.5f);
				p.Scale = Vector2.One * 0.2f;
				p.ScaleVelocity = new Vector2(2, 1) / time * Main.rand.NextFloat(0.2f, 1.5f);
				p.ScaleAcceleration = p.ScaleVelocity * new Vector2(-2, 2) / time;
				p.ColorTint = slashColor;
				p.Rotation = Main.rand.NextFloat(-0.2f, 0.2f);
				Main.ParticleSystem_World_OverPlayers.Add(p);
			}
		}
		else
		{
			for (int i = 0; i < 2; i++)
			{
				var p = VanillaParticles.RequestFadingParticle();
				p.SetBasicInfo(tex, null, Vector2.Zero, pos);
				int time = Main.rand.Next(10, 15);
				p.SetTypeInfo(time);
				p.FadeInNormalizedTime = p.FadeOutNormalizedTime = 0.1f;
				p.Scale = Vector2.One * 0.24f;
				p.ScaleVelocity = new Vector2(2, 1) / time * Main.rand.NextFloat(0.8f, 1.5f);
				p.ScaleAcceleration = p.ScaleVelocity * new Vector2(-2, 1) / time;
				p.ColorTint = slashColor * 0.5f;
				p.Rotation = p.LocalPosition.DirectionTo(player.Center).ToRotation() + Main.rand.NextFloat(-0.4f, 0.4f) + MathHelper.PiOver4 * player.direction;
				Main.ParticleSystem_World_OverPlayers.Add(p);
			}
		}
		return true;
	}
}
public class MinersSwordPlayer : ModPlayer
{
	public float Power = 0;
	private bool _notified = false;
	public override bool PreItemCheck()
	{
		if (Player.CCed || Player.HeldItem.type != ModContent.ItemType<MinersSword>() || Player.itemAnimation == 1)
		{
			Power = 0;
		}
		else if(!Player.ItemAnimationActive)
		{
			Power += 1 / 60f;
			if (Power > 1)
			{
				if (!_notified)
				{
					Player.EmitMaxManaEffect();
					_notified = true;
				}
				Power = 1;
				//if (Main.rand.NextBool(8))
				//{
				//	Dust d = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(Player.Hitbox), DustID.RainbowMk2, Player.velocity);
				//	d.color = Color.DodgerBlue;
				//	d.noGravity = true;
				//}
			}
			else
			{
				_notified = false;
				//for (int i = 0; i < 3; i++)
				//{
				//	if (Main.rand.NextBool(5 - (int)(Power * 5)))
				//	{
				//		Vector2 vect = Main.rand.NextVector2Circular(64, 64);
				//		Dust d = Dust.NewDustPerfect(Player.Center + vect, DustID.RainbowMk2, -vect * 0.1f);
				//		d.color = Color.DodgerBlue;
				//		d.velocity += Player.velocity;
				//		d.noGravity = true;
				//		d.scale = Power;
				//	}
				//}
			}
		}
		return true;
	}
}