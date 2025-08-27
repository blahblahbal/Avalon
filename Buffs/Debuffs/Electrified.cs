using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

public class Electrified : ModBuff
{
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
		player.lifeRegenTime = 0;
		player.GetModPlayer<AvalonPlayer>().Electrified = true;
	}
}
