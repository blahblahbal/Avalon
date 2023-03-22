using Avalon.Common;
using Avalon.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvShadow : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        if (KeybindSystem.ShadowHotkey.JustPressed &&
            player.GetModPlayer<AvalonPlayer>().ShadowCooldown >= 300 && !Main.editChest && !Main.editSign &&
            !Main.drawingPlayerChat)
        {
            player.GetModPlayer<AvalonPlayer>().ResetShadowCooldown();

            for (int num10 = 0; num10 < 70; num10++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f,
                    player.velocity.Y * 0.5f, 150, default, 1.1f);
            }

            player.position.X = Main.mouseX + Main.screenPosition.X;
            player.position.Y = Main.mouseY + Main.screenPosition.Y;
            for (int num11 = 0; num11 < 70; num11++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default,
                    1.1f);
            }
        }
    }
}
