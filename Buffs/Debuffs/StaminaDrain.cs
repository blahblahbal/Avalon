using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs.Debuffs;

// TODO: IMPLEMENT
public class StaminaDrain : ModBuff
{
	private int stacks = 1;

	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
	}

	/// <inheritdoc />
	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare) {
		tip += $" {stacks * 20}%";
	}

	public override void Update(Player player, ref int buffIndex)
	{
		var sp = player.GetModPlayer<AvalonStaminaPlayer>();
		sp.StaminaDrain = true;
		stacks = sp.StaminaDrainStacks;
		if (player.buffTime[buffIndex] == 0)
		{
			sp.StaminaDrainStacks = 1;
		}
	}

	public override bool ReApply(Player player, int time, int buffIndex)
	{
		var sp = player.GetModPlayer<AvalonStaminaPlayer>();
		player.buffTime[buffIndex] += time;
		if (sp.StaminaDrainStacks < 5)
		{
			sp.StaminaDrainStacks++;
		}
		if (player.buffTime[buffIndex] > AvalonStaminaPlayer.StaminaDrainTime)
		{
			player.buffTime[buffIndex] = AvalonStaminaPlayer.StaminaDrainTime;
		}
		return false;
	}
}
