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
		if (stacks == 1)
		{
			tip += " 20%";
		}
		else if (stacks == 2)
		{
			tip += " 40%";
		}
		else if (stacks == 3)
		{
			tip += " 60%";
		}
		else if (stacks == 4)
		{
			tip += " 80%";
		}
		else if (stacks == 5)
		{
			tip += " 100%";
		}
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrain = true;
		stacks = player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks;
		if (player.buffTime[buffIndex] == 0)
		{
			player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks = 1;
		}
	}

	public override bool ReApply(Player player, int time, int buffIndex)
	{
		player.buffTime[buffIndex] += time;
		if (player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks < 5)
		{
			player.GetModPlayer<AvalonStaminaPlayer>().StaminaDrainStacks++;
		}
		if (player.buffTime[buffIndex] > AvalonStaminaPlayer.StaminaDrainTime)
		{
			player.buffTime[buffIndex] = AvalonStaminaPlayer.StaminaDrainTime;
		}
		return false;
	}
}
