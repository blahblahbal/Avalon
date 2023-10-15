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

namespace Avalon.Systems.Recipes;
public class RecipeSystem : ModSystem
{
    public override void AddRecipeGroups()
    {
        if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
        {
            int index = RecipeGroup.recipeGroupIDs["Wood"];
            RecipeGroup group0 = RecipeGroup.recipeGroups[index];
            group0.ValidItems.Add(ModContent.ItemType<Items.Placeable.Tile.ApocalyptusWood>());
            group0.ValidItems.Add(ModContent.ItemType<Items.Placeable.Tile.Coughwood>());
            group0.ValidItems.Add(ModContent.ItemType<Items.Placeable.Tile.BleachedEbony>());
            group0.ValidItems.Add(ModContent.ItemType<Items.Placeable.Tile.ResistantWood>());
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

        var groupMusicBoxes = new RecipeGroup(() => "Any Music Box", boxes);
        RecipeGroup.RegisterGroup("Avalon:MusicBoxes", groupMusicBoxes);

        var groupGemStaves = new RecipeGroup(() => "Any Gem Staff", new int[]
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

        var groupSilverBarMagicStorage = new RecipeGroup(() => "Any Silver Bar", new int[]
        {
            ItemID.SilverBar,
            ItemID.TungstenBar,
            ModContent.ItemType<ZincBar>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnySilverBar", groupSilverBarMagicStorage);

        var groupMythrilBarMagicStorage = new RecipeGroup(() => "Any Mythril Bar", new int[]
        {
            ItemID.MythrilBar,
            ItemID.OrichalcumBar,
            ModContent.ItemType<NaquadahBar>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyMythrilBar", groupMythrilBarMagicStorage);

        var groupHMAnvilMagicStorage = new RecipeGroup(() => "Any Mythril Anvil", new int[]
        {
            ItemID.MythrilAnvil,
            ItemID.OrichalcumAnvil,
            ModContent.ItemType<NaquadahAnvil>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyHmAnvil", groupHMAnvilMagicStorage);

        var groupHMFurnaceMagicStorage = new RecipeGroup(() => "Any Adamantite Forge", new int[]
        {
            ItemID.AdamantiteForge,
            ItemID.TitaniumForge,
            ModContent.ItemType<TroxiniumForge>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyHmFurnace", groupHMFurnaceMagicStorage);

        var groupDemoniteBarMagicStorage = new RecipeGroup(() => "Any Demonite Bar", new int[]
        {
            ItemID.DemoniteBar,
            ItemID.CrimtaneBar,
            ModContent.ItemType<BacciliteBar>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyDemoniteBar", groupDemoniteBarMagicStorage);

        var groupDemonAltarMagicStorage = new RecipeGroup(() => "Any Demon Altar", new int[]
        {
            ModContent.ItemType<DemonAltar>(),
            ModContent.ItemType<CrimsonAltar>(),
            ModContent.ItemType<IckyAltar>()
        });
        RecipeGroup.RegisterGroup("MagicStorage:AnyDemonAltar", groupDemonAltarMagicStorage);

        var groupTombstones = new RecipeGroup(() => "Any Tombstone", new int[]
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
        RecipeGroup.RegisterGroup("Avalon:Tombstones", groupTombstones);

        //RecipeGroup.RegisterGroup("MagicStorage:AnyTombstone", groupTombstones);

        var groupWings = new RecipeGroup(() => "Any Wings", new int[]
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
        RecipeGroup.RegisterGroup("Avalon:Wings", groupWings);
        var groupWorkBenches = new RecipeGroup(() => "Any Work Bench", new int[]
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
            ModContent.ItemType<CoughwoodWorkBench>(),
            ModContent.ItemType<BleachedEbonyWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.DarkSlimeWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.HeartstoneWorkBench>(),
            ModContent.ItemType<OrangeDungeonWorkBench>(),
            ModContent.ItemType<PurpleDungeonWorkbench>(),
            ModContent.ItemType<YellowDungeonWorkBench>(),
            ModContent.ItemType<ResistantWoodWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.VertebraeWorkBench>()
        });
        RecipeGroup.RegisterGroup("Avalon:WorkBenches", groupWorkBenches);

        var groupHerbs = new RecipeGroup(() => "Any Herb", new int[]
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
        RecipeGroup.RegisterGroup("Avalon:Herbs", groupHerbs);

        var groupTier1Watch = new RecipeGroup(() => "Any Copper Watch", new int[]
        {
            ItemID.CopperWatch,
            ItemID.TinWatch,
            ModContent.ItemType<BronzeWatch>()
        });
        RecipeGroup.RegisterGroup("Avalon:Tier1Watch", groupTier1Watch);

        var groupTier2Watch = new RecipeGroup(() => "Any Silver Watch", new int[]
        {
            ItemID.SilverWatch,
            ItemID.TungstenWatch,
            ModContent.ItemType<ZincWatch>()
        });
        RecipeGroup.RegisterGroup("Avalon:Tier2Watch", groupTier2Watch);

        var groupTier3Watch = new RecipeGroup(() => "Any Gold Watch", new int[]
        {
            ItemID.GoldWatch,
            ItemID.PlatinumWatch,
            ModContent.ItemType<BismuthWatch>()
        });
        RecipeGroup.RegisterGroup("Avalon:Tier3Watch", groupTier3Watch);

        var groupGoldBar = new RecipeGroup(() => "Any Gold Bar", new int[]
        {
            ItemID.GoldBar,
            ItemID.PlatinumBar,
            ModContent.ItemType<BismuthBar>()
        });
        RecipeGroup.RegisterGroup("Avalon:GoldBar", groupGoldBar);

        var groupEvilBar = new RecipeGroup(() => "Any Demonite Bar", new int[]
        {
            ItemID.DemoniteBar,
            ItemID.CrimtaneBar,
            ModContent.ItemType<BacciliteBar>()
        });
        RecipeGroup.RegisterGroup("Avalon:EvilBar", groupEvilBar);

        if (RecipeGroup.recipeGroupIDs.ContainsKey("IronBar"))
        {
            int index = RecipeGroup.recipeGroupIDs["IronBar"];
            RecipeGroup groupWood = RecipeGroup.recipeGroups[index];
            groupWood.ValidItems.Add(ModContent.ItemType<NickelBar>());
        }

        var groupCopperBar = new RecipeGroup(() => "Any Copper Bar", new int[]
        {
            ItemID.CopperBar,
            ItemID.TinBar,
            ModContent.ItemType<BronzeBar>()
        });
        RecipeGroup.RegisterGroup("Avalon:CopperBar", groupCopperBar);

        var groupSilverBar = new RecipeGroup(() => "Any Silver Bar", new int[]
        {
            ItemID.SilverBar,
            ItemID.TungstenBar,
            ModContent.ItemType<ZincBar>()
        });
        RecipeGroup.RegisterGroup("Avalon:SilverBar", groupSilverBar);
    }
}
