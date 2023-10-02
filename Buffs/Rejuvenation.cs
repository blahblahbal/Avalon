using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Rejuvenation : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        if (player.buffTime[buffIndex] % 30 == 0)
        {
            player.Heal(5);
        }
    }
}
public class RejuvinationPlayer : ModPlayer
{
    public override void OnHurt(Player.HurtInfo info)
    {
        if(info.Damage > 10)
        {
            if (Player.HasBuff(ModContent.BuffType<Rejuvenation>()))
            {
                Dust d = Dust.NewDustPerfect(Player.Center, DustID.GemRuby, Main.rand.NextVector2Circular(6, 6));
                d.noGravity = true;
                Player.ClearBuff(ModContent.BuffType<Rejuvenation>());
            }
        }
    }
}
