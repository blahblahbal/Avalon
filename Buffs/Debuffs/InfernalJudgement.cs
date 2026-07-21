using Avalon.Data.Sets;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class InfernalJudgement : ModBuff
{
	private static Asset<Texture2D> _flame;
	private static Asset<Texture2D> _flameWhite;
	public override void SetStaticDefaults()
	{
		_flame = Mod.Assets.Request<Texture2D>("Assets/Textures/Flame");
		_flameWhite = Mod.Assets.Request<Texture2D>("Assets/Textures/FlameWhite");
		Main.debuff[Type] = true;
	}
	private void Visuals(Entity e)
	{
		for (int i = 0; i < 2; i++)
		{
			var p = VanillaParticles.RequestRandomizedFrameParticle();
			int time = Main.rand.Next(5, 25);
			bool colorFlame = !Main.rand.NextBool(3);
			p.SetBasicInfo(colorFlame ? _flameWhite : _flame, null, e.velocity + new Vector2(Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(-2f, -0.5f)), Main.rand.NextVector2FromRectangle(e.Hitbox) + e.velocity);
			p.SetTypeInfo(3, Main.rand.Next(15), time);
			p.Scale = Vector2.One * Main.rand.NextFloat(2);
			p.ColorTint = colorFlame ? Color.Lerp(Color.Lerp(Color.Red, Color.Purple, Main.rand.NextFloat()), Color.White, 0.2f) with { A = 0 } : Color.White with { A = 0 };
			p.ColorTint *= Main.rand.NextFloat();
			p.FadeInNormalizedTime = 0.1f;
			p.FadeOutNormalizedTime = 0.1f;
			p.AccelerationPerFrame = -p.Velocity / time;
			p.Rotation = p.Velocity.X * -0.2f;
			p.RotationVelocity = -p.Rotation / time;
			Main.ParticleSystem_World_OverPlayers.Add(p);
		}

		if (Main.rand.NextBool(3))
		{
			Dust d = Dust.NewDustDirect(e.position, e.width, e.height, DustID.BoneTorch);
			d.noGravity = true;
			d.velocity += e.velocity * 0.5f;
			d.noLightEmittence = Main.rand.NextBool();
			d.scale *= 2;
		}
		Dust d2 = Dust.NewDustDirect(e.position, e.width, e.height, DustID.DesertTorch);
		d2.noGravity = true;
		d2.scale *= 2;
		d2.velocity += e.velocity * 0.5f;
		d2.noLightEmittence = Main.rand.NextBool();
	}
	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.GetGlobalNPC<InfernalJudgementNPC>().Active = true;
		Visuals(npc);
	}
	public override void Update(Player player, ref int buffIndex)
	{
		Visuals(player);
		player.GetModPlayer<InfernalJudgementPlayer>().Active = false;
		if (player.lifeRegen > 0)
			player.lifeRegen = 0;
		player.lifeRegen -= 40;
	}
}
public class InfernalJudgementPlayer : ModPlayer
{
	public bool Active = false;
	public override void ResetEffects()
	{
		Active = false;
	}
	public override void PostUpdateEquips()
	{
		if (Active)
			for (int i = 0; i < Player.buffImmune.Length; i++)
			{
				if (!BuffSets.ImmunityCannotBeRemovedFromPlayers[i])
					Player.buffImmune[i] = false;
			}
	}
	public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
	{
		if (Active)
		{
			fullBright = true;
			float multi = 0.35f;
			r *= multi;
			g *= multi;
			b *= multi;
		}
	}
}
public class InfernalJudgementNPC : GlobalNPC
{
	public override bool InstancePerEntity => true;
	public bool Active = false;
	public bool NeedsToRestore = false;
	public bool NeedsToClear = false;

	public override void SetDefaults(NPC entity)
	{
		entity.buffImmune[ModContent.BuffType<InfernalJudgement>()] = false;
	}
	public override void DrawEffects(NPC npc, ref Color drawColor)
	{
		if (Active)
		{
			float multi = 0.35f;
			drawColor = new Color(multi,multi,multi,drawColor.A);
		}
	}
	private void RestoreBuffImmunities(NPC npc)
	{
		if (NPCID.Sets.ImmuneToAllBuffs[npc.type])
		{
			Array.Fill(npc.buffImmune, value: true);
		}
		if (NPCID.Sets.ImmuneToRegularBuffs[npc.type] || NPCID.Sets.ImmuneToAllBuffs[npc.type])
		{
			if (!NPCID.Sets.ImmuneToAllBuffs[npc.type])
			{
				for (int num2 = 0; num2 < npc.buffImmune.Length; num2++)
				{
					npc.buffImmune[num2] = !BuffID.Sets.IsATagBuff[num2];
				}
			}
			for (int num3 = 0; num3 < NPCID.Sets.SpecificDebuffImmunity[npc.type].Length; num3++)
			{
				if ((!NPCID.Sets.SpecificDebuffImmunity[npc.type][num3]) ?? false)
				{
					npc.buffImmune[num3] = false;
				}
			}
			int num4;
			for (num4 = 0; num4 < BuffID.Sets.GrantImmunityWith.Length; num4++)
			{
				List<int> buffsToInherit = BuffID.Sets.GrantImmunityWith[num4];
				if (buffsToInherit.Count > 0 && buffsToInherit.All((int x) => !npc.buffImmune[num4]))
				{
					npc.buffImmune[num4] = false;
				}
			}
			for (int i2 = 0; i2 < NPCID.Sets.SpecificDebuffImmunity[npc.type].Length; i2++)
			{
				if (NPCID.Sets.SpecificDebuffImmunity[npc.type][i2] ?? false)
				{
					npc.buffImmune[i2] = true;
				}
			}
		}
		else
		{
			for (int i3 = 0; i3 < NPCID.Sets.SpecificDebuffImmunity[npc.type].Length; i3++)
			{
				if (NPCID.Sets.SpecificDebuffImmunity[npc.type][i3] ?? false)
				{
					npc.buffImmune[i3] = true;
				}
			}
			for (int i4 = 0; i4 < BuffID.Sets.GrantImmunityWith.Length; i4++)
			{
				foreach (int inheritableBuff in BuffID.Sets.GrantImmunityWith[i4])
				{
					if (npc.buffImmune[inheritableBuff])
					{
						npc.buffImmune[i4] = true;
						break;
					}
				}
			}
			for (int i5 = 0; i5 < NPCID.Sets.SpecificDebuffImmunity[npc.type].Length; i5++)
			{
				if ((!NPCID.Sets.SpecificDebuffImmunity[npc.type][i5]) ?? false)
				{
					npc.buffImmune[i5] = false;
				}
			}
		}
		npc.buffImmune[ModContent.BuffType<InfernalJudgement>()] = false;
	}
	public override void ResetEffects(NPC npc)
	{
		if (!Active)
		{
			NeedsToClear = true;
			if (NeedsToRestore)
			{
				NeedsToRestore = false;
				//Main.NewText("resto");
				NeedsToRestore = false;
				RestoreBuffImmunities(npc);
				npc.ClearImmuneToBuffs(out _);
			}
			return;
		}
		else
		{
			NeedsToRestore = true;
		}
		if(NeedsToClear)
		{
			//Main.NewText("clear");
			NeedsToClear = false;
			for(int i = 0; i < npc.buffImmune.Length; i++)
			{
				if (!BuffSets.ImmunityCannotBeRemovedFromEnemies[i] && !BuffSets.CCOrSlowDebuffThatCannotGoOnBossesOrNPCsThatWouldCauseSignificantJank[i])
					npc.buffImmune[i] = false;
			}
		}
		Active = false;
	}
	public override void UpdateLifeRegen(NPC npc, ref int damage)
	{
		if (Active)
		{
			damage = Math.Max(damage, 50);
			npc.lifeRegen -= 200;
		}
	}
}
