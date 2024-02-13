using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class Rainbringer : ModProjectile
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
            if (!Main.raining)
            {
                Main.StartRain();
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.Rainbringer.Started"), 0, 148, 255);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Tools.Rainbringer.Started")), new Color(0, 148, 255));
                }
            }
            else
            {
                Main.StopRain();
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.Rainbringer.Stopped"), 0, 148, 255);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Tools.Rainbringer.Stopped")), new Color(0, 148, 255));
                }
            }
            Projectile.active = false;
            return;
        }
    }
}
