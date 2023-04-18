using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Ninja : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.blackBelt = true;
    }
}
