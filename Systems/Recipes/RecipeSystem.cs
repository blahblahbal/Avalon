using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Placeable.Crafting;
using Avalon.Items.Placeable.Furniture.BleachedEbony;
using Avalon.Items.Placeable.Furniture.Coughwood;
using Avalon.Items.Placeable.Furniture.OrangeDungeon;
using Avalon.Items.Placeable.Furniture.PurpleDungeon;
using Avalon.Items.Placeable.Furniture.ResistantWood;
using Avalon.Items.Placeable.Furniture.YellowDungeon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Common.Players;
using System.Collections.Generic;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Placeable.Furniture;
using Avalon.Items.Placeable.Furniture.Heartstone;
using Terraria.Localization;
using Avalon.Items.Placeable.Furniture.Gem;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Items.Food;
using Avalon.ModSupport.Thorium.Items.Placeable.Furniture.Gem;
using SDL2;
using System;

namespace Avalon.Systems.Recipes;
public class RecipeSystem : ModSystem
{
	//public override void PostAddRecipes()
	//{
	//	for (int i = 0; i < Recipe.numRecipes; i++)
	//	{
	//		Recipe recipe = Main.recipe[i];
	//		if (recipe.TryGetIngredient(ItemID.RottenChunk, out var chunk) && !Data.Sets.Recipe.RottenChunkOnlyItem[recipe.createItem.type])
	//		{
	//			recipe.AddRecipeGroup("RottenChunk", chunk.stack);
	//			recipe.RemoveIngredient(chunk);
	//		}
	//	}
	//}
	public override void AddRecipeGroups()
    {
        string any = Language.GetTextValue("LegacyMisc.37");

        if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
        {
            int index = RecipeGroup.recipeGroupIDs["Wood"];
            RecipeGroup group0 = RecipeGroup.recipeGroups[index];
            group0.ValidItems.Add(ModContent.ItemType<ApocalyptusWood>());
            group0.ValidItems.Add(ModContent.ItemType<Coughwood>());
            group0.ValidItems.Add(ModContent.ItemType<BleachedEbony>());
            group0.ValidItems.Add(ModContent.ItemType<ResistantWood>());
        }

        if (RecipeGroup.recipeGroups.TryGetValue(RecipeGroupID.Fruit, out var group1))
        {
            group1.ValidItems.Add(ModContent.ItemType<Blackberry>());
            group1.ValidItems.Add(ModContent.ItemType<Durian>());
            group1.ValidItems.Add(ModContent.ItemType<Mangosteen>());
            group1.ValidItems.Add(ModContent.ItemType<Medlar>());
            group1.ValidItems.Add(ModContent.ItemType<Raspberry>());
        }

        List<int> JukeboxTracks = new List<int>
        {
            ItemID.MusicBoxOverworldDay,
            ItemID.MusicBoxEerie,
            ItemID.MusicBoxNight,
            ItemID.MusicBoxTitle,
            ItemID.MusicBoxUnderground,
            ItemID.MusicBoxBoss1,
            ItemID.MusicBoxJungle,
            ItemID.MusicBoxCorruption,
            ItemID.MusicBoxTheHallow,
            ItemID.MusicBoxUndergroundCorruption,
            ItemID.MusicBoxBoss2,
            ItemID.MusicBoxUndergroundHallow,
            ItemID.MusicBoxBoss3,
            ItemID.MusicBoxSnow,
            ItemID.MusicBoxSpace,
            ItemID.MusicBoxCrimson,
            ItemID.MusicBoxBoss4,
            ItemID.MusicBoxAltOverworldDay,
            ItemID.MusicBoxRain,
            ItemID.MusicBoxIce,
            ItemID.MusicBoxDesert,
            ItemID.MusicBoxOcean,
            ItemID.MusicBoxDungeon,
            ItemID.MusicBoxPlantera,
            ItemID.MusicBoxBoss5,
            ItemID.MusicBoxTemple,
            ItemID.MusicBoxEclipse,
            ItemID.MusicBoxMushrooms,
            ItemID.MusicBoxPumpkinMoon,
            ItemID.MusicBoxAltUnderground,
            ItemID.MusicBoxFrostMoon,
            ItemID.MusicBoxUndergroundCrimson,
            ItemID.MusicBoxLunarBoss,
            ItemID.MusicBoxMartians,
            ItemID.MusicBoxPirates,
            ItemID.MusicBoxHell,
            ItemID.MusicBoxTowers,
            ItemID.MusicBoxGoblins,
            ItemID.MusicBoxSandstorm,
            ItemID.MusicBoxDD2,
            ItemID.MusicBoxSpaceAlt,
            ItemID.MusicBoxOceanAlt,
            ItemID.MusicBoxWindyDay,
            ItemID.MusicBoxTownDay,
            ItemID.MusicBoxTownNight,
            ItemID.MusicBoxSlimeRain,
            ItemID.MusicBoxDayRemix,
            ItemID.MusicBoxTitleAlt,
            ItemID.MusicBoxStorm,
            ItemID.MusicBoxGraveyard,
            ItemID.MusicBoxUndergroundJungle,
            ItemID.MusicBoxJungleNight,
            ItemID.MusicBoxQueenSlime,
            ItemID.MusicBoxEmpressOfLight,
            ItemID.MusicBoxDukeFishron,
            ItemID.MusicBoxMorningRain,
            ItemID.MusicBoxConsoleTitle,
            ItemID.MusicBoxUndergroundDesert,
            ItemID.MusicBoxOWRain,
            ItemID.MusicBoxOWDay,
            ItemID.MusicBoxOWNight,
            ItemID.MusicBoxOWUnderground,
            ItemID.MusicBoxOWDesert,
            ItemID.MusicBoxOWOcean,
            ItemID.MusicBoxOWMushroom,
            ItemID.MusicBoxOWDungeon,
            ItemID.MusicBoxOWSpace,
            ItemID.MusicBoxOWUnderworld,
            ItemID.MusicBoxOWSnow,
            ItemID.MusicBoxOWCorruption,
            ItemID.MusicBoxOWUndergroundCorruption,
            ItemID.MusicBoxOWCrimson,
            ItemID.MusicBoxOWUndergroundCrimson,
            ItemID.MusicBoxOWUndergroundSnow,
            ItemID.MusicBoxOWUndergroundHallow,
            ItemID.MusicBoxOWBloodMoon,
            ItemID.MusicBoxOWBoss2,
            ItemID.MusicBoxOWBoss1,
            ItemID.MusicBoxOWInvasion,
            ItemID.MusicBoxOWTowers,
            ItemID.MusicBoxOWMoonLord,
            ItemID.MusicBoxOWPlantera,
            ItemID.MusicBoxOWJungle,
            ItemID.MusicBoxOWWallOfFlesh,
            ItemID.MusicBoxOWHallow,
            ItemID.MusicBoxCredits,
            ItemID.MusicBoxDeerclops,
            ItemID.MusicBoxShimmer,
        };
        List<int> boxesList = JukeboxTracks;
        boxesList.AddRange(AvalonJukeboxPlayer.AvalonTracks);
        int[] boxes = boxesList.ToArray();

        var groupMusicBoxes = new RecipeGroup(() => $"{any} Music Box", boxes);
        RecipeGroup.RegisterGroup("MusicBoxes", groupMusicBoxes);

        #region Balloons
        var groupFartBalloons = new RecipeGroup(() => $"{any} Fart Balloon", new int[]
        {
            ItemID.FartInABalloon,
            ItemID.BalloonHorseshoeFart
        });
        RecipeGroup.RegisterGroup("Avalon:FartBalloons", groupFartBalloons);
        var groupRocketBalloons = new RecipeGroup(() => $"{any} Rocket Balloon", new int[]
        {
            ModContent.ItemType<Items.Accessories.Hardmode.RocketinaBalloon>(),
            ModContent.ItemType<Items.Accessories.Hardmode.RocketHorseshoeBalloon>()
        });
        RecipeGroup.RegisterGroup("Avalon:RocketBalloons", groupRocketBalloons);
        var groupSharkronBalloons = new RecipeGroup(() => $"{any} Sharkron Balloon", new int[]
        {
            ItemID.SharkronBalloon,
            ItemID.BalloonHorseshoeSharkron
        });
        RecipeGroup.RegisterGroup("Avalon:SharkronBalloons", groupSharkronBalloons);
		#endregion Balloons

		#region evil groups
		//var groupRottenChunks = new RecipeGroup(() => $"{any} Rotten Chunk",
		//[
		//	ItemID.RottenChunk,
		//	ItemID.Vertebrae,
		//	ModContent.ItemType<Items.Material.YuckyBit>()
		//]);
		//RecipeGroup.RegisterGroup("RottenChunk", groupRottenChunks);

		#endregion

		var groupGemStaves = new RecipeGroup(() => $"{any} Gem Staff", new int[]
        {
            ItemID.RubyStaff,
            ItemID.AmberStaff,
            ItemID.TopazStaff,
            ItemID.EmeraldStaff,
            ItemID.SapphireStaff,
            ItemID.AmethystStaff,
            ItemID.DiamondStaff,
            ModContent.ItemType<Items.Weapons.Magic.PreHardmode.PeridotStaff>(),
            ModContent.ItemType<Items.Weapons.Magic.PreHardmode.TourmalineStaff>(),
            ModContent.ItemType<Items.Weapons.Magic.PreHardmode.ZirconStaff>()
        });
        RecipeGroup.RegisterGroup("Avalon:GemStaves", groupGemStaves);

        List<int> banners = new();
        for (int i = 0; i < NPCLoader.NPCCount; i++)
        {
            NPC npc = new NPC();
            npc.SetDefaults(i);
            if (NPCLoader.GetNPC(i) is ModNPC modnpc)
            {
				if (modnpc.BannerItem != ItemID.None)
				{
					banners.Add(modnpc.BannerItem);
				}
            }
            else
            {
                if (Item.NPCtoBanner(npc.BannerID()) > 0)
                {
                    int bannerID = ClassExtensions.BannerPlaceStyleToItemID(Item.NPCtoBanner(npc.BannerID()));
                    if (bannerID > 0)
                    {
                        banners.Add(bannerID);
                    }
                }
            }
        }
        int[] bannerArray = banners.ToArray();
        var groupBanners = new RecipeGroup(() => $"{any} Monster Banner", bannerArray);
        RecipeGroup.RegisterGroup("Avalon:Banners", groupBanners);
        //{
        //    ItemID.RedBanner,
        //    ItemID.YellowBanner,
        //    ItemID.GreenBanner,
        //    ItemID.BlueBanner,
        //    ItemID.MarchingBonesBanner,
        //    ItemID.NecromanticSign,
        //    ItemID.RustedCompanyStandard,
        //    ItemID.RaggedBrotherhoodSigil,
        //    ItemID.MoltenLegionFlag,
        //    ItemID.DiabolicSigil,
        //    ItemID.WorldBanner,
        //    ItemID.SunBanner,
        //    ItemID.GravityBanner,
        //    ItemID.HellboundBanner,
        //    ItemID.HellHammerBanner,
        //    ItemID.HelltowerBanner,
        //    ItemID.LostHopesofManBanner,
        //    ItemID.ObsidianWatcherBanner,
        //    ItemID.LavaEruptsBanner,
        //    ItemID.AnkhBanner,
        //    ItemID.SnakeBanner,
        //    ItemID.OmegaBanner
        //});

        var groupGoldPickaxe = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.GoldPickaxe)}", new int[]
        {
            ItemID.GoldPickaxe,
            ItemID.PlatinumPickaxe,
            ModContent.ItemType<BismuthPickaxe>()
        });
        RecipeGroup.RegisterGroup("GoldPickaxe", groupGoldPickaxe);

        #region magic storage stuff
        var groupSilverBarMagicStorage = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.SilverBar)}", new int[]
        {
            ItemID.SilverBar,
            ItemID.TungstenBar,
            ModContent.ItemType<ZincBar>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnySilverBar", groupSilverBarMagicStorage);

        

        var groupChestMS = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.Chest)}", new int[]
        {
            ModContent.ItemType<BleachedEbonyChest>(),
            ModContent.ItemType<CoughwoodChest>(),
            ModContent.ItemType<OrangeDungeonChest>(),
            ModContent.ItemType<PurpleDungeonChest>(),
            ModContent.ItemType<ResistantWoodChest>(),
            ModContent.ItemType<YellowDungeonChest>(),
            ModContent.ItemType<HellfireChest>(),
            ModContent.ItemType<HeartstoneChest>(),
            ModContent.ItemType<AmberChest>(),
            ModContent.ItemType<AmethystChest>(),
            ModContent.ItemType<DiamondChest>(),
            ModContent.ItemType<EmeraldChest>(),
            ModContent.ItemType<PeridotChest>(),
            ModContent.ItemType<RubyChest>(),
            ModContent.ItemType<SapphireChest>(),
            ModContent.ItemType<TopazChest>(),
            ModContent.ItemType<TourmalineChest>(),
            ModContent.ItemType<ZirconChest>(),
            #region thorium
            ModContent.ItemType<AquamarineChest>(),
            ModContent.ItemType<ChrysoberylChest>(),
            ModContent.ItemType<OpalChest>()
            #endregion thorium
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyChest", groupChestMS);

        var groupMythrilBarMagicStorage = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.MythrilBar)}", new int[]
        {
            ItemID.MythrilBar,
            ItemID.OrichalcumBar,
            ModContent.ItemType<NaquadahBar>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyMythrilBar", groupMythrilBarMagicStorage);

        var groupHMAnvilMagicStorage = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.MythrilAnvil)}", new int[]
        {
            ItemID.MythrilAnvil,
            ItemID.OrichalcumAnvil,
            ModContent.ItemType<NaquadahAnvil>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyHmAnvil", groupHMAnvilMagicStorage);

        var groupHMFurnaceMagicStorage = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.AdamantiteForge)}", new int[]
        {
            ItemID.AdamantiteForge,
            ItemID.TitaniumForge,
            ModContent.ItemType<TroxiniumForge>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyHmFurnace", groupHMFurnaceMagicStorage);

        var groupDemoniteBarMagicStorage = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.DemoniteBar)}", new int[]
        {
            ItemID.DemoniteBar,
            ItemID.CrimtaneBar,
            ModContent.ItemType<BacciliteBar>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyDemoniteBar", groupDemoniteBarMagicStorage);

        var groupDemonAltarMagicStorage = new RecipeGroup(() => $"{any} {Language.GetTextValue("MapObject.DemonAltar")}", new int[]
        {
            ModContent.ItemType<DemonAltar>(),
            ModContent.ItemType<CrimsonAltar>(),
            ModContent.ItemType<IckyAltar>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyDemonAltar", groupDemonAltarMagicStorage);
        #endregion

        var groupTombstones = new RecipeGroup(() => $"{any} Tombstone", new int[]
        {
            ItemID.Gravestone,
            ItemID.Tombstone,
            ItemID.CrossGraveMarker,
            ItemID.Obelisk,
            ItemID.Headstone,
            ItemID.GraveMarker,
            ItemID.RichGravestone1,
            ItemID.RichGravestone2,
            ItemID.RichGravestone3,
            ItemID.RichGravestone4,
            ItemID.RichGravestone5
        });
        RecipeGroup.RegisterGroup("Tombstones", groupTombstones);

        var groupDungeonBricks = new RecipeGroup(() => $"{any} Dungeon Brick", new int[]
        {
            ItemID.PinkBrick,
            ModContent.ItemType<OrangeBrick>(),
            ModContent.ItemType<YellowBrick>(),
            ItemID.GreenBrick,
            ItemID.BlueBrick,
            ModContent.ItemType<PurpleBrick>()
        });
        RecipeGroup.RegisterGroup("DungeonBrick", groupDungeonBricks);

        //RecipeGroup.RegisterGroup("MagicStorage:AnyTombstone", groupTombstones);

        var groupWings = new RecipeGroup(() => $"{any} Wings", new int[]
        {
            ItemID.DemonWings,
            ItemID.AngelWings,
            ItemID.ButterflyWings,
            ItemID.FairyWings,
            ItemID.HarpyWings,
            ItemID.BoneWings,
            ItemID.FlameWings,
            ItemID.FrozenWings,
            ItemID.GhostWings,
            ItemID.LeafWings,
            ItemID.BatWings,
            ItemID.BeeWings,
            ItemID.TatteredFairyWings,
            ItemID.SpookyWings,
            ItemID.FestiveWings,
            ItemID.BeetleWings,
            ItemID.FinWings,
            ItemID.FishronWings,
            ItemID.WingsNebula,
            ItemID.WingsSolar,
            ItemID.WingsStardust,
            ItemID.WingsVortex,
            ItemID.FinWings,
            ItemID.MothronWings,
            ItemID.BetsyWings,
            ItemID.SteampunkWings,
            ItemID.RainbowWings,
            //ModContent.ItemType<ContagionWings>(),
            //ModContent.ItemType<CrimsonWings>(),
            //ModContent.ItemType<CorruptionWings>(),
            //ModContent.ItemType<HolyWings>(),
            //ModContent.ItemType<EtherealWings>()
        });
        RecipeGroup.RegisterGroup("Wings", groupWings);
        var groupWorkBenches = new RecipeGroup(() => $"{any} Work Bench", new int[]
        {
            ItemID.WorkBench,
            ItemID.EbonwoodWorkBench,
            ItemID.BlueDungeonWorkBench,
            ItemID.SteampunkWorkBench,
            ItemID.SpookyWorkBench,
            ItemID.SlimeWorkBench,
            ItemID.SkywareWorkbench,
            ItemID.ShadewoodWorkBench,
            ItemID.RichMahoganyWorkBench,
            ItemID.PumpkinWorkBench,
            ItemID.PinkDungeonWorkBench,
            ItemID.PearlwoodWorkBench,
            ItemID.PalmWoodWorkBench,
            ItemID.ObsidianWorkBench,
            ItemID.MushroomWorkBench,
            ItemID.MeteoriteWorkBench,
            ItemID.MartianWorkBench,
            ItemID.MarbleWorkBench,
            ItemID.LivingWoodWorkBench,
            ItemID.LihzahrdWorkBench,
            ItemID.HoneyWorkBench,
            ItemID.GreenDungeonWorkBench,
            ItemID.GraniteWorkBench,
            ItemID.GoldenWorkbench,
            ItemID.GlassWorkBench,
            ItemID.FrozenWorkBench,
            ItemID.FleshWorkBench,
            ItemID.DynastyWorkBench,
            ItemID.CrystalWorkbench,
            ItemID.CactusWorkBench,
            ItemID.BorealWoodWorkBench,
            ItemID.BoneWorkBench,
            ItemID.GothicWorkBench,
            ItemID.AshWoodWorkbench,
            ItemID.BalloonWorkbench,
            ItemID.CoralWorkbench,
            ItemID.SandstoneWorkbench,
            ItemID.BambooWorkbench,
            ItemID.SpiderWorkbench,
            ItemID.LesionWorkbench,
            ItemID.SolarWorkbench,
            ItemID.NebulaWorkbench,
            ItemID.StardustWorkbench,
            ItemID.VortexWorkbench,
            ModContent.ItemType<CoughwoodWorkBench>(),
            ModContent.ItemType<BleachedEbonyWorkBench>(),
            ModContent.ItemType<Items.Placeable.Furniture.WildMushroom.WildMushroomWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.DarkSlimeWorkBench>(),
            ModContent.ItemType<HeartstoneWorkBench>(),
            ModContent.ItemType<OrangeDungeonWorkBench>(),
            ModContent.ItemType<PurpleDungeonWorkbench>(),
            ModContent.ItemType<YellowDungeonWorkBench>(),
            ModContent.ItemType<ResistantWoodWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.VertebraeWorkBench>()
        });
        RecipeGroup.RegisterGroup("WorkBenches", groupWorkBenches);

        var groupHerbs = new RecipeGroup(() => $"{any} Herb", new int[]
        {
            ItemID.Blinkroot,
            ItemID.Fireblossom,
            ItemID.Deathweed,
            ItemID.Shiverthorn,
            ItemID.Waterleaf,
            ItemID.Moonglow,
            ItemID.Daybloom,
            ModContent.ItemType<Bloodberry>(),
            ModContent.ItemType<Sweetstem>(),
            ModContent.ItemType<Barfbush>(),
            ModContent.ItemType<Holybird>(),
            //ModContent.ItemType<Items.TwilightPlume>(),
        });
        RecipeGroup.RegisterGroup("Herbs", groupHerbs);

        var groupTier1Watch = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.CopperWatch)}", new int[]
        {
            ItemID.CopperWatch,
            ItemID.TinWatch,
            ModContent.ItemType<BronzeWatch>()
        });
        RecipeGroup.RegisterGroup("CopperWatch", groupTier1Watch);

        var groupTier2Watch = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.SilverWatch)}", new int[]
        {
            ItemID.SilverWatch,
            ItemID.TungstenWatch,
            ModContent.ItemType<ZincWatch>()
        });
        RecipeGroup.RegisterGroup("SilverWatch", groupTier2Watch);

        var groupTier3Watch = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.GoldWatch)}", new int[]
        {
            ItemID.GoldWatch,
            ItemID.PlatinumWatch,
            ModContent.ItemType<BismuthWatch>()
        });
        RecipeGroup.RegisterGroup("GoldWatch", groupTier3Watch);

        var groupGoldBar = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.GoldBar)}", new int[]
        {
            ItemID.GoldBar,
            ItemID.PlatinumBar,
            ModContent.ItemType<BismuthBar>()
        });
        RecipeGroup.RegisterGroup("GoldBar", groupGoldBar);

        var groupEvilBar = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.DemoniteBar)}", new int[]
        {
            ItemID.DemoniteBar,
            ItemID.CrimtaneBar,
            ModContent.ItemType<BacciliteBar>()
        });
        RecipeGroup.RegisterGroup("DemoniteBar", groupEvilBar);

        if (RecipeGroup.recipeGroupIDs.ContainsKey("IronBar"))
        {
            int index = RecipeGroup.recipeGroupIDs["IronBar"];
            RecipeGroup groupWood = RecipeGroup.recipeGroups[index];
            groupWood.ValidItems.Add(ModContent.ItemType<NickelBar>());
        }

        var groupCopperBar = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.CopperBar)}", new int[]
        {
            ItemID.CopperBar,
            ItemID.TinBar,
            ModContent.ItemType<BronzeBar>()
        });
        RecipeGroup.RegisterGroup("CopperBar", groupCopperBar);

        var groupSilverBar = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.SilverBar)}", new int[]
        {
            ItemID.SilverBar,
            ItemID.TungstenBar,
            ModContent.ItemType<ZincBar>()
        });
        RecipeGroup.RegisterGroup("SilverBar", groupSilverBar);

        #region thorium stuff
        var groupThoriumCobalt = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.CobaltBar)}", new int[]
        {
            ItemID.CobaltBar,
            ItemID.PalladiumBar,
            ModContent.ItemType<DurataniumBar>()
        });
        RecipeGroup.RegisterGroup("CobaltBar", groupThoriumCobalt);

        var groupThoriumMythril = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.MythrilBar)}", new int[]
        {
            ItemID.MythrilBar,
            ItemID.OrichalcumBar,
            ModContent.ItemType<NaquadahBar>()
        });
        RecipeGroup.RegisterGroup("MythrilBar", groupThoriumMythril);

        var groupThoriumAdamantite = new RecipeGroup(() => $"{any} {Lang.GetItemNameValue(ItemID.AdamantiteBar)}", new int[]
        {
            ItemID.AdamantiteBar,
            ItemID.TitaniumBar,
            ModContent.ItemType<TroxiniumBar>()
        });
        RecipeGroup.RegisterGroup("AdamantiteBar", groupThoriumAdamantite);
        #endregion
    }
}
