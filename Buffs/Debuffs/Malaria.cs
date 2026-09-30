using Avalon.Common.Players;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Avalon.Common.Extensions;
using System;

namespace Avalon.Buffs.Debuffs;

public class Malaria : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.AddDOT(3, NetworkText.FromKey($"Mods.Avalon.DeathText.Malaria_1", $"{player.name}"));
	}
	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.AddDOT(3, 3);
	}
}
