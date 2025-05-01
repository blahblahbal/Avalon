using Avalon.Common.Extensions;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class MartianPeaceTreaty : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 3;
	}

	public override void SetDefaults()
	{
		Item.DefaultToSpawner();
	}

	public override bool CanUseItem(Player player)
	{
		return Main.invasionSize > 0 && Main.invasionType == InvasionID.MartianMadness;
	}

	public override bool? UseItem(Player player)
	{
		Main.invasionSize = 0;
		if (ExxoAvalonOrigins.Achievements != null)
		{
			ExxoAvalonOrigins.Achievements.Call("Event", "FalseAlarm");
		}
		SoundEngine.PlaySound(SoundID.Roar, player.position);
		return true;
	}
}
