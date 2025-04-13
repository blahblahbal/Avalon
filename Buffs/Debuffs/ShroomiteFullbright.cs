using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

internal class ShroomiteFullbright : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}
}
public class ShroomiteFullbrightNPCDrawing : GlobalNPC
{
	public override void DrawEffects(NPC npc, ref Color drawColor)
	{
		if (npc.HasBuff(ModContent.BuffType<ShroomiteFullbright>()))
			drawColor = Color.White;
	}
}
