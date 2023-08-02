using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class Timechanger : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = -1;
        Projectile.width = dims.Width * 10 / 32;
        Projectile.height = dims.Height * 10 / 32 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.damage = 0;
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        if (Main.dayTime) Main.time = 53999;
        else Main.time = 32399;

        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText(string.Format("It is now {0}.", Main.dayTime ? "Night" : "Day"), 50, 255, 130);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(string.Format("It is now {0}.", Main.dayTime ? "Night" : "Day")), new Color(50, 255, 130));
        }
        Projectile.active = false;
    }
}
