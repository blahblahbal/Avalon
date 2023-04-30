using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class BacterialEndurance : ModBuff
{
    public override void SetStaticDefaults()
    {
        //Description.SetDefault("Thorns effect and increased damage and jump speed");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.jumpSpeedBoost += 0.2f;
        //player.thorns = 0.6f;
        player.GetModPlayer<AvalonPlayer>().BacterialEndurance = true;
    }
}
