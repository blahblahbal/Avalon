﻿using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvInvisibility : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.invis = true;
    }
}
