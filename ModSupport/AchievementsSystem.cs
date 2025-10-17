using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Melee.Hardmode;
using Avalon.Items.Weapons.Melee.Hardmode.VertexOfExcalibur;
using Avalon.Items.Weapons.Melee.PreHardmode.AeonsEternity;
using Avalon.Tiles;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace Avalon.ModSupport;

internal class AchievementsSystem : ModSystem
{
	public override void PostSetupContent()
	{
		if (ExxoAvalonOrigins.Achievements == null)
		{
			return;
		}

		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "TheFiveSword", AchievementCategory.Collector, "Avalon/Assets/Textures/Achievements/AeonsEternity", null, false, false, 5.5f, new string[] { "Craft_" + ModContent.ItemType<AeonsEternity>() });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "Safe", AchievementCategory.Collector, "Avalon/Assets/Textures/Achievements/Safe", null, false, false, 5.5f, new string[] { "Craft_" + ModContent.ItemType<GuardianBoots>() });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "LuckyStiff", AchievementCategory.Collector, "Avalon/Assets/Textures/Achievements/LuckyStiff", null, false, false, 5.5f, new string[] { "Collect_" + ModContent.ItemType<FourLeafClover>() });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "Boosted", AchievementCategory.Explorer, "Avalon/Assets/Textures/Achievements/StaminaCrystal", null, false, false, 2.5f, new string[] { "Event_UseStaminaCrystal" });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "SolarSystemPluto", AchievementCategory.Collector, "Avalon/Assets/Textures/Achievements/SolarSystemPluto", null, false, false, 40f, new string[] { "Event_SolarSystemPluto" });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "FalseAlarm", AchievementCategory.Slayer, "Avalon/Assets/Textures/Achievements/FalseAlarm", null, false, false, 3.5f, new string[] { "Event_FalseAlarm" });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "Rock", AchievementCategory.Slayer, "Avalon/Assets/Textures/Achievements/Rock", null, false, false, 3.5f, new string[] { "Event_BreakGlassWithRock" });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "Hellcastle", AchievementCategory.Explorer, "Avalon/Assets/Textures/Achievements/WoFsHouse", null, false, false, 37f, new string[] { "Mine_" + ModContent.TileType<UltraResistantWood>() });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "DesertBeak", AchievementCategory.Slayer, "Avalon/Assets/Textures/Achievements/DesertBeak", null, false, false, 7.5f, new string[] { "Kill_" + ModContent.NPCType<NPCs.Bosses.PreHardmode.DesertBeak>() });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "BacteriumPrime", AchievementCategory.Slayer, "Avalon/Assets/Textures/Achievements/Sulfonamided", null, false, false, 4.5f, new string[] { "Kill_" + ModContent.NPCType<NPCs.Bosses.PreHardmode.BacteriumPrime>() });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "Fatality", AchievementCategory.Slayer, "Avalon/Assets/Textures/Achievements/Fatality", null, false, false, 5f, new string[] { "Event_DrinkBottledLava" });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "Gems", AchievementCategory.Collector, "Avalon/Assets/Textures/Achievements/Gems", null, false, false, 10f, new string[] { "Event_HaveAllLargeGems" });
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "Hellevator", AchievementCategory.Explorer, "Avalon/Assets/Textures/Achievements/Hellevator", null, false, false, 12f, new string[] { "Event_Hellevator" });

		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "Unification", AchievementCategory.Collector, "Avalon/Assets/Textures/Achievements/Unification", null, false, false, 41.5f, new string[] { "Craft_" + ModContent.ItemType<VertexOfExcalibur>() });
		
		// change later
		ExxoAvalonOrigins.Achievements.Call("AddAchievement", ExxoAvalonOrigins.Mod, "ItBurnsBurnsBurnsBurns!", AchievementCategory.Slayer, "Avalon/Assets/Textures/Achievements/ItBurnsX4", null, false, false, 36.5f, new string[] { "Event_ItBurnsBurnsBurnsBurns" });
	}
}
