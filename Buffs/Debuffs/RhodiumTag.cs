using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class RhodiumTag : ModBuff
{
    public override void SetStaticDefaults()
    {
		BuffID.Sets.IsATagBuff[Type] = true;
		BuffID.Sets.CanBeRemovedByNetMessage[Type] = true;
	}
	public override void Update(NPC npc, ref int buffIndex)
	{
		Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<SimpleColorableGlowyDust>());
		d.color = new Color(1f, 0.3f, Main.rand.NextFloat(0.3f, 0.6f), 0f);
		d.velocity *= 0.3f;
		d.velocity += npc.velocity;
		d.noGravity = true;
		d.scale = 1.2f;
	}
}
