using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class Moonphaser : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.aiStyle = -1;
        Projectile.width = dims.Width;
        Projectile.height = dims.Height / Main.projFrames[Projectile.type];
        Projectile.damage = 0;
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        if (Projectile.active)
        {
            Main.moonPhase++;
            if (Main.moonPhase >= 8)
            {
                Main.moonPhase = 0;
            }
            string[] phase = new string[8]
            {
                "Full", "Last Gibbous", "Last Quarter", "Last Crescent",
                "New", "First Crescent", "First Quarter", "First Gibbous"

            };
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(string.Format("Moon Phase is now {0}.", phase[Main.moonPhase]), 50, 255, 130);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(string.Format("Moon Phase is now {0}.", phase[Main.moonPhase])), new Color(50, 255, 130));

            }
            if (Main.rand.NextBool(14) && !Main.dayTime)
            {
                Main.bloodMoon = true;
                Main.NewText("The Blood Moon has risen...", 50, 255, 130);
            }
            Projectile.active = false;
        }
    }
}
