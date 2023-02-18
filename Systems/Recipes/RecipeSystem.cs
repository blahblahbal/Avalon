//using Avalon.Items.Accessories;
//using Avalon.Items.Material;
//using Avalon.Items.Placeable.Bar;
//using Avalon.Items.Placeable.Crafting;
//using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Systems.Recipes;
public class RecipeSystem : ModSystem
{
    public override void AddRecipeGroups()
    {
        //if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
        //{
        //    int index = RecipeGroup.recipeGroupIDs["Wood"];
        //    RecipeGroup group0 = RecipeGroup.recipeGroups[index];
        //    group0.ValidItems.Add(ModContent.ItemType<ApocalyptusWood>());
        //    group0.ValidItems.Add(ModContent.ItemType<Coughwood>());
        //    group0.ValidItems.Add(ModContent.ItemType<BleachedEbony>());
        //    group0.ValidItems.Add(ModContent.ItemType<ResistantWood>());
        //}

        //var groupGemStaves = new RecipeGroup(() => "Any Gem Staff", new int[]
        //{
        //    ItemID.RubyStaff,
        //    ItemID.AmberStaff,
        //    ItemID.TopazStaff,
        //    ItemID.EmeraldStaff,
        //    ItemID.SapphireStaff,
        //    ItemID.AmethystStaff,
        //    ItemID.DiamondStaff,
        //    ModContent.ItemType<Items.Weapons.Magic.PeridotStaff>(),
        //    ModContent.ItemType<Items.Weapons.Magic.TourmalineStaff>(),
        //    ModContent.ItemType<Items.Weapons.Magic.ZirconStaff>()
        //});
        //RecipeGroup.RegisterGroup("ExxoAvalonOrigins:GemStaves", groupGemStaves);

        //var groupSilverBarMagicStorage = new RecipeGroup(() => "Any Silver Bar", new int[]
        //{
        //    ItemID.SilverBar,
        //    ItemID.TungstenBar,
        //    ModContent.ItemType<ZincBar>()
        //});
        //RecipeGroup.RegisterGroup("MagicStorage:AnySilverBar", groupSilverBarMagicStorage);

        //var groupMythrilBarMagicStorage = new RecipeGroup(() => "Any Mythril Bar", new int[]
        //{
        //    ItemID.MythrilBar,
        //    ItemID.OrichalcumBar,
        //    ModContent.ItemType<NaquadahBar>()
        //});
        //RecipeGroup.RegisterGroup("MagicStorage:AnyMythrilBar", groupMythrilBarMagicStorage);

        //var groupHMAnvilMagicStorage = new RecipeGroup(() => "Any Mythril Anvil", new int[]
        //{
        //    ItemID.MythrilAnvil,
        //    ItemID.OrichalcumAnvil,
        //    ModContent.ItemType<NaquadahAnvil>()
        //});
        //RecipeGroup.RegisterGroup("MagicStorage:AnyHmAnvil", groupHMAnvilMagicStorage);

        //var groupHMFurnaceMagicStorage = new RecipeGroup(() => "Any Adamantite Forge", new int[]
        //{
        //    ItemID.AdamantiteForge,
        //    ItemID.TitaniumForge,
        //    ModContent.ItemType<TroxiniumForge>()
        //});
        //RecipeGroup.RegisterGroup("MagicStorage:AnyHmFurnace", groupHMFurnaceMagicStorage);

        //var groupDemoniteBarMagicStorage = new RecipeGroup(() => "Any Demonite Bar", new int[]
        //{
        //    ItemID.DemoniteBar,
        //    ItemID.CrimtaneBar,
        //    ModContent.ItemType<PandemiteBar>()
        //});
        //RecipeGroup.RegisterGroup("MagicStorage:AnyDemoniteBar", groupDemoniteBarMagicStorage);

        //var groupDemonAltarMagicStorage = new RecipeGroup(() => "Any Demon Altar", new int[]
        //{
        //    ModContent.ItemType<DemonAltar>(),
        //    ModContent.ItemType<CrimsonAltar>(),
        //    ModContent.ItemType<IckyAltar>()
        //});
        //RecipeGroup.RegisterGroup("MagicStorage:AnyDemonAltar", groupDemonAltarMagicStorage);

        //var groupTombstones = new RecipeGroup(() => "Any Tombstone", new int[]
        //{
        //    ItemID.Gravestone,
        //    ItemID.Tombstone,
        //    ItemID.CrossGraveMarker,
        //    ItemID.Obelisk,
        //    ItemID.Headstone,
        //    ItemID.GraveMarker,
        //    ItemID.RichGravestone1,
        //    ItemID.RichGravestone2,
        //    ItemID.RichGravestone3,
        //    ItemID.RichGravestone4,
        //    ItemID.RichGravestone5
        //});
        //RecipeGroup.RegisterGroup("ExxoAvalonOrigins:Tombstones", groupTombstones);

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
        RecipeGroup.RegisterGroup("ExxoAvalonOrigins:Wings", groupWings);
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
            //ModContent.ItemType<Items.Placeable.Crafting.CoughwoodWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.DarkSlimeWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.HeartstoneWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.OrangeDungeonWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.ResistantWoodWorkBench>(),
            //ModContent.ItemType<Items.Placeable.Crafting.VertebraeWorkBench>()
        });
        RecipeGroup.RegisterGroup("ExxoAvalonOrigins:WorkBenches", groupWorkBenches);

        //var groupHerbs = new RecipeGroup(() => "Any Herb", new int[]
        //{
        //    ItemID.Blinkroot,
        //    ItemID.Fireblossom,
        //    ItemID.Deathweed,
        //    ItemID.Shiverthorn,
        //    ItemID.Waterleaf,
        //    ItemID.Moonglow,
        //    ItemID.Daybloom,
        //    ModContent.ItemType<Bloodberry>(),
        //    ModContent.ItemType<Sweetstem>(),
        //    ModContent.ItemType<Barfbush>(),
        //    ModContent.ItemType<Holybird>(),
        //    //ModContent.ItemType<Items.TwilightPlume>(),
        //});
        //RecipeGroup.RegisterGroup("ExxoAvalonOrigins:Herbs", groupHerbs);

        //var groupTier1Watch = new RecipeGroup(() => "Any Copper Watch", new int[]
        //{
        //    ItemID.CopperWatch,
        //    ItemID.TinWatch,
        //    ModContent.ItemType<BronzeWatch>()
        //});
        //RecipeGroup.RegisterGroup("ExxoAvalonOrigins:Tier1Watch", groupTier1Watch);

        //var groupTier2Watch = new RecipeGroup(() => "Any Silver Watch", new int[]
        //{
        //    ItemID.SilverWatch,
        //    ItemID.TungstenWatch,
        //    ModContent.ItemType<ZincWatch>()
        //});
        //RecipeGroup.RegisterGroup("Avalon:Tier2Watch", groupTier2Watch);

        var groupTier3Watch = new RecipeGroup(() => "Any Gold Watch", new int[]
        {
            ItemID.GoldWatch,
            ItemID.PlatinumWatch,
            //ModContent.ItemType<BismuthWatch>()
        });
        RecipeGroup.RegisterGroup("ExxoAvalonOrigins:Tier3Watch", groupTier3Watch);

        var groupGoldBar = new RecipeGroup(() => "Any Gold Bar", new int[]
        {
            ItemID.GoldBar,
            ItemID.PlatinumBar,
            //ModContent.ItemType<BismuthBar>()
        });
        RecipeGroup.RegisterGroup("ExxoAvalonOrigins:GoldBar", groupGoldBar);

        var groupEvilBar = new RecipeGroup(() => "Any Demonite Bar", new int[]
        {
            ItemID.DemoniteBar,
            ItemID.CrimtaneBar,
            //ModContent.ItemType<PandemiteBar>()
        });
        RecipeGroup.RegisterGroup("ExxoAvalonOrigins:EvilBar", groupEvilBar);

        //if (RecipeGroup.recipeGroupIDs.ContainsKey("IronBar"))
        //{
        //    int index = RecipeGroup.recipeGroupIDs["IronBar"];
        //    RecipeGroup groupWood = RecipeGroup.recipeGroups[index];
        //    groupWood.ValidItems.Add(ModContent.ItemType<NickelBar>());
        //}

        //var groupCopperBar = new RecipeGroup(() => "Any Copper Bar", new int[]
        //{
        //    ItemID.CopperBar,
        //    ItemID.TinBar,
        //    ModContent.ItemType<BronzeBar>()
        //});
        //RecipeGroup.RegisterGroup("ExxoAvalonOrigins:CopperBar", groupCopperBar);

        //var groupSilverBar = new RecipeGroup(() => "Any Silver Bar", new int[]
        //{
        //    ItemID.SilverBar,
        //    ItemID.TungstenBar,
        //    ModContent.ItemType<ZincBar>()
        //});
        //RecipeGroup.RegisterGroup("ExxoAvalonOrigins:SilverBar", groupSilverBar);
    }
}
