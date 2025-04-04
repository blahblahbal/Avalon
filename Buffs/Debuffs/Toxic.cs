using Avalon.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class Toxic : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.GetModPlayer<AvalonPlayer>().FrameCount % 60 == 0)
		{
			if (player.GetModPlayer<AvalonPlayer>().InfectDamage < 16)
			{
				player.GetModPlayer<AvalonPlayer>().InfectDamage *= 2;
			}
			else
			{
				player.GetModPlayer<AvalonPlayer>().InfectDamage = 16;
			}

			player.Hurt(PlayerDeathReason.ByCustomReason(NetworkText.FromKey($"Mods.Avalon.DeathText.{Name}_1", $"{player.name}")), player.GetModPlayer<AvalonPlayer>().InfectDamage + player.statDefense / 2, 0);
		}
	}
}
