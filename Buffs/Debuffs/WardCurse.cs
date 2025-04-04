using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Avalon.Common.Players;
using Terraria.Localization;

namespace Avalon.Buffs.Debuffs;

public class WardCurse : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.buffTime[buffIndex] % 120 == 0)
		{
			float defEffectiveness = 0.5f;
			if (Main.expertMode) defEffectiveness = 0.75f;
			if (Main.masterMode) defEffectiveness = 1f;
			player.Hurt(PlayerDeathReason.ByCustomReason(NetworkText.FromKey($"Mods.Avalon.DeathText.{Name}_1", $"{player.name}")), (player.statDefense * defEffectiveness) + (player.GetModPlayer<AvalonPlayer>().WardCurseDOT / 10 / 5), 0);
		}
		if (player.buffTime[buffIndex] == 0)
		{
			player.GetModPlayer<AvalonPlayer>().WardCurseDOT = 0;
			player.GetModPlayer<AvalonPlayer>().WardCD = 60 * 45;
		}
	}
}
