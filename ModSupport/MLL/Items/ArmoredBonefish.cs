using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class ArmoredBonefish : ModItem
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
		return Main.hardMode && NPC.downedPlantBoss;
	}

	public override void AnglerQuestChat(ref string description, ref string catchLocation)
	{
		description = Language.GetTextValue("Mods.Avalon.QuestFish.ArmoredBonefish.Description");
		catchLocation = Language.GetTextValue("Mods.Avalon.QuestFish.ArmoredBonefish.CatchLocation");
	}
}
