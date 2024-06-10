using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class Sandstormer : ModProjectile
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
            if (!Terraria.GameContent.Events.Sandstorm.Happening)
			{
				Main.windSpeedCurrent = 2f * (Main.rand.NextBool() ? -1 : 1);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Terraria.GameContent.Events.Sandstorm.StartSandstorm();
					Terraria.GameContent.Events.Sandstorm.TimeLeft = 86400;
				}
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.Sandstormer.Started"), 212, 192, 100);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Tools.Sandstormer.Started")), new Color(212, 192, 100));
                }
            }
            else
            {
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Terraria.GameContent.Events.Sandstorm.StopSandstorm();
				}
				if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Language.GetTextValue("Mods.Avalon.Tools.Sandstormer.Stopped"), 212, 192, 100);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.Avalon.Tools.Sandstormer.Stopped")), new Color(212, 192, 100));
                }
            }
            Projectile.active = false;
            return;
        }
    }
}
