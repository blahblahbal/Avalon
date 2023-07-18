using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class LotusCurse : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.maxRunSpeed /= 2f;
        player.runAcceleration /= 2;
        player.accRunSpeed /= 2;
        player.endurance -= 0.3f;
        player.GetDamage(DamageClass.Magic) /= 2;

        Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.ViciousPowder);
        d.noGravity = true;
        d.velocity = Vector2.Zero;
    }
}
