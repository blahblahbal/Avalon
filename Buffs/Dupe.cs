﻿using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Dupe : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
		player.GetModPlayer<AvalonPlayer>().DupeLoot = true;
    }
}
