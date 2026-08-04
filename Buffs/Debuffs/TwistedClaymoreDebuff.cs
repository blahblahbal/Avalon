using Avalon.Common;
using Avalon.Core;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class TwistedClaymoreDebuff : ModBuff
{
    public override void SetStaticDefaults()
    {
		Main.debuff[Type] = true;
    }
	public override void Update(NPC npc, ref int buffIndex)
	{
		var tcdn = npc.GetGlobalNPC<TwistedClaymoreDebuffNPC>();
		tcdn.Active = true;
		npc.GetGlobalNPC<AvalonGlobalNPCInstance>().Speed *= 1f - (Utils.Remap(tcdn.Tier, 0, 4, 0.2f, 0.8f) * Utils.Remap(npc.buffTime[buffIndex], 0, 60, 0, 1));
	}
	public override bool ReApply(NPC npc, int time, int buffIndex)
	{
		var tcdn = npc.GetGlobalNPC<TwistedClaymoreDebuffNPC>();
		tcdn.Tier++;
		if (tcdn.Tier > 4)
		{
			int DPS = npc.SimpleStrikeNPC(250, 0, true, 0, DamageClass.Melee,true);

			SoundEngine.PlaySound(Sounds.Item.TwistedClaymoreBoom.Asset with { pitchVariance = 0.2f });
			
			var sparkle = AssetReferences.Assets.Textures.SparklyDarkOutside.Asset;
			sparkle.Wait();
			var dot = AssetReferences.Assets.Textures.TriangleThing.Asset;
			dot.Wait();

			for (int i = 0; i < 15; i++)
			{
				int time2 = Main.rand.Next(10, 40);
				var p = VanillaParticles.RequestFadingParticle();
				p.SetBasicInfo(dot, null, Vector2.Zero, npc.Center);
				p.ColorTint = Color.Black;
				p.SetTypeInfo(time2);
				p.FadeInNormalizedTime = 0.1f;
				p.FadeOutNormalizedTime = 0.5f;
				p.Velocity = Main.rand.NextVector2Circular(15, 15);
				p.AccelerationPerFrame = -p.Velocity / time2 * 2;
				p.Scale = Vector2.One.RotatedByRandom(0.6f) * Main.rand.NextFloat(2, 4);
				p.Rotation = Main.rand.NextFloatDirection();
				p.RotationVelocity = Main.rand.NextFloat(-0.1f, 0.1f);
				p.ScaleVelocity = p.Scale / -time2;
				Main.ParticleSystem_World_OverPlayers.Add(p);
			}
			for (int i = 0; i < 10; i++)
			{
				int time2 = Main.rand.Next(20, 40);
				var p = VanillaParticles.RequestFadingParticle();
				p.SetBasicInfo(sparkle, null, Main.rand.NextVector2CircularEdge(1,1) * Main.rand.NextFloat(5,15), npc.Center);
				p.ColorTint = new Color(1, 0, Main.rand.NextFloat(), 0.25f);
				p.AccelerationPerFrame = -p.Velocity / time2;
				p.SetTypeInfo(time2);
				p.FadeInNormalizedTime = 0.1f;
				p.FadeOutNormalizedTime = 0.5f;
				p.Scale = new Vector2(2.5f, 4);
				p.ScaleVelocity = -p.Scale / time2;
				p.Rotation = p.Velocity.ToRotation() + MathHelper.PiOver2;
				Main.ParticleSystem_World_OverPlayers.Add(p);
			}
			for (int i = 0; i < 5; i++)
			{
				int time2 = Main.rand.Next(5, 10);
				var p = VanillaParticles.RequestFadingParticle();
				p.SetBasicInfo(dot, null, Vector2.Zero, npc.Center);
				p.ColorTint = Color.Lerp(Color.Magenta,Color.White,Main.rand.NextFloat()) with { A = 0};
				p.SetTypeInfo(time2);
				p.FadeInNormalizedTime = 0.1f;
				p.FadeOutNormalizedTime = 0.1f;
				p.Velocity = Main.rand.NextVector2Circular(25, 25);
				p.AccelerationPerFrame = -p.Velocity / time2 * 2;
				p.Scale = Vector2.One.RotatedByRandom(0.3f) * Main.rand.NextFloat(4, 7);
				p.Rotation = Main.rand.NextFloatDirection();
				p.RotationVelocity = Main.rand.NextFloat(-0.1f, 0.1f);
				p.ScaleVelocity = p.Scale / -time2;
				Main.ParticleSystem_World_OverPlayers.Add(p);
			}
			npc.DelBuff(buffIndex);
			return true;
		}
		return false;
	}
}
public class TwistedClaymoreDebuffNPC : GlobalNPC
{
	public override bool InstancePerEntity => true;
	public bool Active = false;
	public byte Tier = 0;

	public override void ResetEffects(NPC npc)
	{
		if (!Active) Tier = 0;
		Active = false;
	}
	public float getMultiplier()
	{
		return 1f + ((Tier + 1) * 0.5f);
	}
	public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (Active)
		{
			var Tex = AssetReferences.Assets.Textures.TwistedClaymoreStack.Asset;
			var d = new DrawData(Tex.Value, npc.Center - screenPos, Tex.Frame(1, 5, 0, Tier), Color.White with { A = 170 }, (float)Math.Sin(Main.timeForVisualEffects * 0.02) * 0.1f, new Vector2(19, 25), 1f + (float)Math.Sin(Main.timeForVisualEffects * (0.1f + Tier * 0.075f)) * 0.1f, SpriteEffects.None);
			float amt = (float)(Main.timeForVisualEffects % 50) / 50f;
			Main.EntitySpriteDraw(d with { position = d.position + Main.rand.NextVector2Circular(Tier, Tier) * 3f, color = Color.White * 0.25f });
			Main.EntitySpriteDraw(d with { position = d.position + Main.rand.NextVector2Circular(Tier, Tier) * 1.5f, color = Color.White * 0.5f });
			Main.EntitySpriteDraw(d);
		}
	}
}
