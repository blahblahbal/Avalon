using Avalon.Common.Players;
using Avalon.Projectiles.Summon;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Buffs.Minions;

// TODO: IMPLEMENT
public class PrimeArms : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[Type] = true;
		Main.buffNoSave[Type] = false;
	}
	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
	{
		tip += "\n" + Language.GetTextValue("Mods.Avalon.TooltipEdits.UpgradeStage") + Main.LocalPlayer.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()];
		base.ModifyBuffText(ref buffName, ref tip, ref rare);
	}
	public override void Update(Player player, ref int buffIndex)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()] > 0)
		{
			player.GetModPlayer<AvalonPlayer>().PrimeMinion = true;
		}
		if (!player.GetModPlayer<AvalonPlayer>().PrimeMinion)
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
		else
		{
			player.buffTime[buffIndex] = 18000;
		}
		if (player.whoAmI == Main.myPlayer)
		{
			player.GetModPlayer<AvalonPlayer>().UpdatePrimeMinionStatus();
		}
	}
}
