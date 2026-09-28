using Avalon.Dusts;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class DarklightLanceDebuffNPC : GlobalNPC
{
	public override bool InstancePerEntity => true;
	public bool Light;
	public bool Night;
	public static int LifeRegenLoss = 20;
	public static int BonusLifeRegenLoss = 75;

	public override void ResetEffects(NPC npc)
	{
		Light = false;
		Night = false;

		//if (Main.rand.NextBool(2))
		//{
		//	Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<GlowySoulDust>());
		//	d.noGravity = true;
		//	d.velocity = Main.rand.NextVector2Circular(8, 8);
		//	d.customData = (GlowySoulDust.SoulType)((Main.timeForVisualEffects / 60) % 11);
		//	d.customData = GlowySoulDust.SoulType.Ice;
		//	d.scale += Main.rand.NextFloat();
		//}
	}
	public override void UpdateLifeRegen(NPC npc, ref int damage)
	{
		if (Light)
		{
			damage = Math.Max(damage, 5);
			npc.lifeRegen -= LifeRegenLoss;
		}
		if (Night)
		{
			damage = Math.Max(damage, 5);
			npc.lifeRegen -= LifeRegenLoss;
			if (Light)
			{
				damage = Math.Max(damage, 15);
				npc.lifeRegen -= BonusLifeRegenLoss;
			}
		}
	}
}
public class DarklightLanceLight : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
		BuffID.Sets.CanBeRemovedByNetMessage[Type] = true;
	}
	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.GetGlobalNPC<DarklightLanceDebuffNPC>().Light = true;

		if (Main.rand.NextBool(5))
		{
			Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<GlowySoulDust>());
			d.noGravity = true;
			d.velocity += npc.velocity;
			d.velocity.Y -= 2;
			d.customData = GlowySoulDust.SoulType.Light;
			d.fadeIn = Main.rand.NextFloat(-3, 1.5f);
		}
	}
}
public class DarklightLanceNight : DarklightLanceLight
{
	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.GetGlobalNPC<DarklightLanceDebuffNPC>().Night = true;
		if (Main.rand.NextBool(5))
		{
			Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<GlowySoulDust>());
			d.noGravity = true;
			d.velocity += npc.velocity;
			d.velocity.Y -= 2;
			d.customData = GlowySoulDust.SoulType.Night;
			d.fadeIn = Main.rand.NextFloat(-3, 1.5f);
		}
	}
}