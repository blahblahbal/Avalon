using Avalon.Common;
using Avalon.Data.Sets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class CryoStasis : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
		BuffSets.CCOrSlowDebuffThatCannotGoOnBossesOrNPCsThatWouldCauseSignificantJank[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
		npc.GetGlobalNPC<AvalonGlobalNPCInstance>().Speed *= 0.3f;
		if (Main.rand.NextBool())
		{
			Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Snow);
			d.noGravity = true;
			d.velocity *= 0.2f;
		}
		npc.lifeRegen -= 20;
	}
}
