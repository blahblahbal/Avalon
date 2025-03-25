using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvLeaping : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        if (player.controlJump && player.wingsLogic != 0)
        {
            player.velocity.Y = player.velocity.Y - (0.7f * player.gravDir);
            if (player.gravDir == 1f)
            {
                if (player.velocity.Y > 0f)
                {
                    player.velocity.Y = player.velocity.Y - 1f;
                }
                else if (player.velocity.Y > -Player.jumpSpeed)
                {
                    player.velocity.Y = player.velocity.Y - 0.5f;
                }

                if (player.velocity.Y < -Player.jumpSpeed * 4.5f)
                {
                    player.velocity.Y = -Player.jumpSpeed * 4.5f;
                }
            }
            else
            {
                if (player.velocity.Y < 0f)
                {
                    player.velocity.Y = player.velocity.Y + 1f;
                }
                else if (player.velocity.Y < Player.jumpSpeed)
                {
                    player.velocity.Y = player.velocity.Y + 0.5f;
                }

                if (player.velocity.Y > Player.jumpSpeed * 4.5f)
                {
                    player.velocity.Y = Player.jumpSpeed * 4.5f;
                }
            }
        }
	}
	public override bool ReApply(Player player, int time, int buffIndex)
	{
		player.GetModPlayer<AvalonPlayer>().IcarusTimer = 0;
		return false;
	}
}
