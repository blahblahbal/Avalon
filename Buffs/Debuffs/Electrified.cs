using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Avalon.Buffs.Debuffs;

public class Electrified : ModBuff
{
	private int timer;
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.lifeRegen > 0)
		{
			player.lifeRegen = 0;
		}
		timer++;
		if (timer % 4 == 0)
		{
			int amt = 3;
			if (player.GetModPlayer<AvalonPlayer>().DuraShield)
			{
				amt = 2;
			}
			else if (player.GetModPlayer<AvalonPlayer>().DuraOmegaShield)
			{
				amt = 1;
			}
			if (player.velocity.Length() != 0)
			{
				amt += 3;
			}
			player.statLife -= amt;
			CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.LifeRegen, amt, dramatic: false, dot: true);
			if (player.statLife <= 0)
			{
				int type = Main.rand.Next(10) + 1;
				player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromKey(type > 4 ? $"Mods.Avalon.DeathText.Electrocuted_{type - 4}" : $"DeathText.Electrocuted_{type}", $"{player.name}")), 10, 0);
			}
		}
		player.lifeRegenTime = 0;
		if (player.buffTime[buffIndex] == 0)
		{
			timer = 0;
		}
		player.GetModPlayer<AvalonPlayer>().Electrified = true;
	}
}
