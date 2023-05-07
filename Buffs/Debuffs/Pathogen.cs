using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class Pathogen : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
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
        npc.GetGlobalNPC<AvalonGlobalNPCInstance>().Pathogen = true;
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
        player.GetModPlayer<AvalonPlayer>().Pathogen = true;
    }
}
