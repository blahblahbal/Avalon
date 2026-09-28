using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common;

public class BackportBaBDebuffs : GlobalNPC // remove when tmod updates
{
	public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
	{
		if (npc.HasBuff(BuffID.BrokenArmor))
		{
			modifiers.ArmorPenetration += 20;
		}
	}
	public override void DrawEffects(NPC npc, ref Color drawColor)
	{
		if (npc.HasBuff(BuffID.BrokenArmor))
		{
			drawColor = drawColor.MultiplyRGBByFloat(0.75f);
		}
		if (npc.HasBuff(BuffID.Bleeding))
		{
			drawColor = drawColor.MultiplyRGB(new Color(1, 0.85f, 0.85f));
			if (Main.rand.Next(2) == 0)
			{
				Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Blood);
				d.velocity.Y += 0.5f;
				d.velocity *= 0.35f;
			}
		}
	}
	public override void UpdateLifeRegen(NPC npc, ref int damage)
	{
		if (npc.HasBuff(BuffID.Bleeding))
		{
			npc.lifeRegen -= 24;
			if (damage < 4)
				damage = 4;
		}
	}
}
