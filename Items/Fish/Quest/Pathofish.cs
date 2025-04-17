using Avalon.Common;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Fish.Quest;

public class Pathofish : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToQuestFish();
	}
	public override bool IsQuestFish()
	{
		return true;
	}
	public override bool IsAnglerQuestAvailable()
	{
		return ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion && Main.hardMode;
	}

	public override void AnglerQuestChat(ref string description, ref string catchLocation)
	{
		description = Language.GetTextValue("Mods.Avalon.QuestFish.Pathofish.Description");
		catchLocation = Language.GetTextValue("Mods.Avalon.QuestFish.Pathofish.CatchLocation");
	}
}
