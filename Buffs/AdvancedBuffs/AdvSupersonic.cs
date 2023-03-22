using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvSupersonic : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        if (true) //!player.GetModPlayer<AvalonPlayer>().CaesiumBoostActive)
        {
            player.accRunSpeed = 18.29f;
        }
        else
        {
            player.accRunSpeed = 7f;
        }
        if (!player.vortexStealthActive)// && !player.GetModPlayer<AvalonPlayer>().CaesiumBoostActive)
        {
            if (player.controlLeft)
            {
                if (player.velocity.X > -5f)
                {
                    player.velocity.X = player.velocity.X - 0.61f;
                }

                if (player.velocity.X < -5f && player.velocity.X > -18f)
                {
                    player.velocity.X = player.velocity.X - 0.59f;
                }
            }

            if (player.controlRight)
            {
                if (player.velocity.X < 5f)
                {
                    player.velocity.X = player.velocity.X + 0.61f;
                }

                if (player.velocity.X > 5f && player.velocity.X < 18f)
                {
                    player.velocity.X = player.velocity.X + 0.59f;
                }
            }
        }
        if (player.velocity.X > 6f || player.velocity.X < -6f)
        {
            var newColor = default(Color);
            int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height,
                DustID.Cloud, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 100, newColor, 2f);
            Main.dust[num].noGravity = true;
        }
    }
}
