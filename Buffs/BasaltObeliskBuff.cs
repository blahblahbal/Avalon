using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Avalon.Biomes;
using Avalon.Common;

namespace Avalon.Buffs;

public class BasaltObeliskBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
	{
		player.lavaMax += 420;
		// code not needed, breaks behaviour when in shallow lava
		// leaving in case we want to make the lava immunity remain after the buff disappears instead of being immediately depleted
		//if (player.lavaTime < player.lavaMax && !player.lavaWet)
		//{
		//	player.lavaTime++;
		//}
		player.buffImmune[BuffID.OnFire] = true;
	}
}

public class DisableBuffTimerDrawing : ModHook
{
	protected override void Apply()
	{
		On_Main.TryGetBuffTime += OnTryGetBuffTime;
	}
	private bool OnTryGetBuffTime(On_Main.orig_TryGetBuffTime orig, int buffSlotOnPlayer, out int buffTimeValue)
	{
		Player player = Main.LocalPlayer;
		if (!player.InModBiome(ModContent.GetInstance<BasaltObeliskBiome>()) || player.buffType[buffSlotOnPlayer] != ModContent.BuffType<BasaltObeliskBuff>())
		{
			return orig.Invoke(buffSlotOnPlayer, out buffTimeValue);
		}
		else
		{
			return !orig.Invoke(buffSlotOnPlayer, out buffTimeValue);
		}
	}
}
