using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Avalon.Biomes;

namespace Avalon.Buffs;

public class BasaltObeliskBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
	{
		if (player.InModBiome(ModContent.GetInstance<BasaltObeliskBiome>()))
		{
			Main.buffNoTimeDisplay[Type] = true;
		}
		else
		{
			Main.buffNoTimeDisplay[Type] = false;
		}
		player.lavaMax += 420;
		if (player.lavaTime < player.lavaMax && !player.lavaWet)
		{
			player.lavaTime++;
		}
		player.buffImmune[BuffID.OnFire] = true;
	}
}
