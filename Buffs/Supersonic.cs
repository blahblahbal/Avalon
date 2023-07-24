using Avalon.Items.Armor.Hardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Supersonic : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        if (!player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
        {
            player.accRunSpeed = 14.29f;
        }
        else
        {
            player.accRunSpeed = 6f;
        }
        if (!player.vortexStealthActive && !player.GetModPlayer<CaesiumBoostingStancePlayer>().CaesiumBoostActive)
        {
            if (player.controlLeft)
            {
                if (player.velocity.X > -5f)
                {
                    player.velocity.X = player.velocity.X - 0.41f;
                }

                if (player.velocity.X < -5f && player.velocity.X > -14f)
                {
                    player.velocity.X = player.velocity.X - 0.39f;
                }
            }

            if (player.controlRight)
            {
                if (player.velocity.X < 5f)
                {
                    player.velocity.X = player.velocity.X + 0.41f;
                }

                if (player.velocity.X > 5f && player.velocity.X < 14f)
                {
                    player.velocity.X = player.velocity.X + 0.39f;
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
