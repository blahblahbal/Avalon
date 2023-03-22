using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvShine : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        Lighting.AddLight((int)(player.position.X + (player.width / 2)) / 16,
            (int)(player.position.Y + (player.height / 2)) / 16, 2f, 2f, 2f);
    }
}
