using Avalon.Common;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Fish.Quest;

public class Snotpiranha : ModItem
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
		return ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion;
	}

	public override void AnglerQuestChat(ref string description, ref string catchLocation)
	{
		description = Language.GetTextValue("Mods.Avalon.QuestFish.Snotpiranha.Description");
		catchLocation = Language.GetTextValue("Mods.Avalon.QuestFish.Snotpiranha.CatchLocation");
	}
}
