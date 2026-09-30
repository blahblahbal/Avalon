using Avalon.Common;
using Avalon.Common.DebuffPotency;
using Avalon.Common.Interfaces;
using Avalon.Common.Players;
using Avalon.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class Pathogen : ModBuff, IBuffThatNeedsToUpdatePlayerEarly, IBuffThatNeedsToUpdateNPCEarly
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
		BuffID.Sets.LongerExpertDebuff[Type] = true;
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.timeForVisualEffects % 10 == 0)
        {
            Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1);
            d.noGravity = true;
            d.noLightEmittence= true;
            d.velocity += npc.velocity;
            d.fadeIn = 1.3f;
        }
		var aGNPC = npc.GetGlobalNPC<AvalonGlobalNPCInstance>();
		aGNPC.Pathogen = true;
		aGNPC.DebuffDuration += 1;
	}
    public override void Update(Player player, ref int buffIndex)
    {
        if (Main.rand.NextBool(3))
        {
            Dust d = Dust.NewDustDirect(player.position, player.width, player.height, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1);
            d.noGravity = true;
            d.noLightEmittence = true;
            d.velocity += player.velocity;
            d.fadeIn = 1.3f;
        }
		var aP = player.GetModPlayer<AvalonPlayer>();
		aP.Pathogen = true;
		aP.DebuffDuration += 1;
	}

	public void UpdateEarly(Player player, ref int buffIndex)
	{
		player.GetModPlayer<DebuffPotencyPlayer>().DebuffPotency += 0.5f;
	}

	public void UpdateEarly(NPC npc, ref int buffIndex)
	{
		npc.GetGlobalNPC<DebuffPotencyNPC>().DebuffPotency += 0.5f;
	}
}
