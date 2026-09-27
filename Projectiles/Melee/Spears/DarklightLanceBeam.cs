using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Spears;

public class DarklightLanceBeam : ModProjectile
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}
	public override void SetDefaults()
	{
		Projectile.friendly = true;
		Projectile.extraUpdates = 5;
		Projectile.aiStyle = -1;
		Projectile.width = Projectile.height = 6;
		Projectile.timeLeft = 150;
		Projectile.Opacity = 0;
		Projectile.hide = true;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.velocity != Vector2.Zero)
		{
			Projectile.position += Projectile.oldVelocity;
			Projectile.timeLeft = 600;
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			Projectile.velocity = Vector2.Zero;
		}
		return false;
	}
	public override void AI()
	{
		if (Projectile.velocity != Vector2.Zero)
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
		Projectile.ai[1]++;
		float pingPong = Utils.PingPongFrom01To010(Projectile.ai[1] % 160 / 160f);
		Projectile.scale = 0.75f + pingPong * 0.2f;
		Projectile.Opacity = (1f + pingPong * 0.3f) * Utils.Remap(Projectile.ai[1],0,30,0,1);
		if (Projectile.ai[1] > 10)
		{
			Projectile.velocity *= 0.98f;
		}

		if (Projectile.velocity != Vector2.Zero)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<GlowySoulDust>());
			d.noGravity = true;
			d.velocity *= 0.2f;
			d.velocity += Projectile.velocity * 2;

			if (Main.rand.NextBool(6))
				d.fadeIn = Main.rand.NextFloat(-6);

			d.customData = Projectile.ai[0] == 0 ? GlowySoulDust.SoulType.Light : GlowySoulDust.SoulType.Night;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		int lightBuff = ModContent.BuffType<DarklightLanceLight>();
		int nightBuff = ModContent.BuffType<DarklightLanceNight>();
		int buffTime = 60 * 3;
		if (Projectile.ai[0] == 0)
		{
			int nightIndex = target.FindBuffIndex(nightBuff);
			target.AddBuff(lightBuff, buffTime);
			if(nightIndex != -1)
			{
				target.AddBuff(nightBuff, buffTime);
			}
		}
		else
		{
			int lightIndex = target.FindBuffIndex(lightBuff);
			target.AddBuff(nightBuff, buffTime);
			if (lightIndex != -1)
			{
				target.AddBuff(lightBuff, buffTime);
			}
		}
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item10,Projectile.position);
		int dustType = ModContent.DustType<GlowySoulDust>();
		Vector2 normalizedVelocity = (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2();
		var soul = Projectile.ai[0] == 0 ? GlowySoulDust.SoulType.Light : GlowySoulDust.SoulType.Night;
		for (int i = 0; i < 45; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position + normalizedVelocity * Main.rand.NextFloat(-70,30), Projectile.width, Projectile.height, dustType);
			d.noGravity = true;
			d.velocity *= 4f;
			d.velocity += Projectile.oldVelocity * 2;

			if (Main.rand.NextBool(6))
				d.fadeIn = Main.rand.NextFloat(-6);
			else
				d.fadeIn = Main.rand.NextFloat(1.75f);

			d.customData = soul;
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var tex = TextureAssets.Projectile[Type].Value;
		var frame = tex.Frame(1, 2, 0, (int)Projectile.ai[0]);
		DrawData d = new(tex, Projectile.Center - Main.screenPosition, frame, Color.White with { A = 128 } * Projectile.Opacity, Projectile.rotation, new Vector2(38,24), Projectile.scale, SpriteEffects.None);
		Main.EntitySpriteDraw(d with { color = d.color with { A = 0 } * 0.1f, scale = d.scale * 2f });
		Main.EntitySpriteDraw(d with { color = d.color with { A = 0 } * 0.25f, scale = d.scale * 1.5f});
		Main.EntitySpriteDraw(d);
		return false;
	}
}
