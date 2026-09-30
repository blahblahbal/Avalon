using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.ModSupport.MLL.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Buffs;

public class Dissolving : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}
	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.AddDOT(6, 3);
		npc.AvalonGlobalNPCInstance.EffectColor *= new Color(0.8f,1,0.6f);
		if (Main.rand.NextBool(15))
		{
			Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<SimpleColorableGlowyDust>());
			d.color = new Color(0.5f, 1, 0.2f);
			d.noGravity = true;
			d.velocity *= 0.2f;
			d.velocity += npc.velocity;
			d.fadeIn = Main.rand.NextFloat(-1, 1);
		}
	}
	public override void Update(Player player, ref int buffIndex)
	{
		player.AddDOT(player.AvalonPlayer.AcidDmgReduction? 14 : 24, NetworkText.FromKey($"Mods.Avalon.DeathText.Acid_{Main.rand.Next(5)}", $"{player.name}"));
		player.AvalonPlayer.EffectColor *= new Color(0.8f, 1, 0.6f);
		if (Main.rand.NextBool(15))
		{
			Dust d = Dust.NewDustDirect(player.position, player.width, player.height, ModContent.DustType<SimpleColorableGlowyDust>());
			d.color = new Color(0, 1, 0, 0.8f);
			d.noGravity = true;
			d.velocity *= 0.2f;
			d.velocity += player.velocity;
			d.fadeIn = Main.rand.NextFloat(-1,1);
		}
	}
}
