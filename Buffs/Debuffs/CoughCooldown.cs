using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Avalon.Common.Players;

namespace Avalon.Buffs.Debuffs;

public class CoughCooldown : ModBuff
{
    private int timer;
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<AvalonPlayer>().CoughCooldown = true;
    }
}
