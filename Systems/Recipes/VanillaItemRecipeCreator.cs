using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Items.Material.Shards;
using Avalon.Items.Material.Ores;
using Avalon.Items.Material.Bars;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Items.Accessories.PreHardmode;

namespace Avalon.Systems.Recipes;

public class VanillaItemRecipeCreator : ModSystem
{
    public override void AddRecipes()
    {
        #region By hand
        Recipe.Create(ItemID.PoisonedKnife, 50).AddIngredient(ItemID.ThrowingKnife, 50).AddIngredient(ModContent.ItemType<VirulentPowder>()).Register();
        #endregion

        #region Demon Altar
        Recipe.Create(ItemID.SlimeCrown).AddIngredient(ItemID.Gel, 20).AddIngredient(ModContent.ItemType<Items.Vanity.BismuthCrown>()).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.NightsEdge).AddIngredient(ModContent.ItemType<Snotsabre>()).AddIngredient(ItemID.Muramasa).AddIngredient(ItemID.BladeofGrass).AddIngredient(ItemID.FieryGreatsword).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.VoidLens).AddIngredient(ItemID.Bone, 30).AddIngredient(ItemID.JungleSpores, 15).AddIngredient(ModContent.ItemType<Booger>(), 30).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.VoidVault).AddIngredient(ItemID.Bone, 15).AddIngredient(ItemID.JungleSpores, 8).AddIngredient(ModContent.ItemType<Booger>(), 15).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.DeerThing).AddIngredient(ItemID.FlinxFur, 3).AddIngredient(ModContent.ItemType<BacciliteOre>(), 5).AddIngredient(ItemID.Lens).AddTile(TileID.DemonAltar).Register();
        #endregion

        #region Anvils
        Recipe.Create(ItemID.Aglet).AddRecipeGroup("Avalon:CopperBar").AddRecipeGroup("Wood", 6).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.Throne).AddIngredient(ItemID.Silk, 20).AddIngredient(ModContent.ItemType<BismuthBar>(), 30).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.Starfury).AddIngredient(ItemID.GoldBroadsword).AddIngredient(ItemID.MeteoriteBar, 10).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.Starfury).AddIngredient(ItemID.PlatinumBroadsword).AddIngredient(ItemID.MeteoriteBar, 10).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.Starfury).AddIngredient(ModContent.ItemType<BismuthBroadsword>()).AddIngredient(ItemID.MeteoriteBar, 10).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.CoffinMinecart).AddRecipeGroup("IronBar", 5).AddRecipeGroup("Wood", 10).AddIngredient(ModContent.ItemType<YuckyBit>(), 10).AddTile(TileID.Anvils).AddCondition(condition: Condition.InGraveyard).Register();
        #endregion

        #region Hardmode Anvil
        Recipe.Create(ItemID.MechanicalWorm)
            .AddIngredient(ModContent.ItemType<YuckyBit>(), 6)
            .AddRecipeGroup("IronBar", 5)
            .AddIngredient(ItemID.SoulofNight, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();

        Recipe.Create(ItemID.RodofDiscord)
            .AddIngredient(ModContent.ItemType<RodofCoalescence>())
            .AddIngredient(ModContent.ItemType<ChaosDust>(), 40)
            .AddIngredient(ItemID.SoulofLight, 25)
            .AddIngredient(ItemID.Diamond, 10)
            .AddIngredient(ItemID.SoulofFright)
            .AddIngredient(ItemID.SoulofMight)
            .AddIngredient(ItemID.SoulofSight)
            .AddTile(TileID.MythrilAnvil)
            .Register();
            //Recipe.Create(ItemID.LihzahrdPowerCell).AddIngredient(ModContent.ItemType<SolariumStar>(), 5).AddIngredient(ItemID.LihzahrdBrick, 10).AddTile(TileID.MythrilAnvil).Register();
        #endregion

        #region Furnace
        Recipe.Create(ItemID.MagicMirror)
            .AddIngredient(ItemID.Glass, 20)
            .AddRecipeGroup(RecipeGroupID.IronBar, 5)
            .AddIngredient(ItemID.RecallPotion, 3)
            .AddTile(TileID.Furnaces)
            .Register();
        #endregion

        #region Hellforge
        Recipe.Create(ItemID.ObsidianHelm).AddIngredient(ItemID.Silk, 10).AddIngredient(ItemID.Obsidian, 20).AddIngredient(ModContent.ItemType<Booger>(), 5).AddTile(TileID.Hellforge).Register();
        Recipe.Create(ItemID.ObsidianShirt).AddIngredient(ItemID.Silk, 10).AddIngredient(ItemID.Obsidian, 20).AddIngredient(ModContent.ItemType<Booger>(), 10).AddTile(TileID.Hellforge).Register();
        Recipe.Create(ItemID.ObsidianPants).AddIngredient(ItemID.Silk, 10).AddIngredient(ItemID.Obsidian, 20).AddIngredient(ModContent.ItemType<Booger>(), 5).AddTile(TileID.Hellforge).Register();
        #endregion

        #region Hardmode Furnace
        Recipe.Create(ItemID.HallowedBar)
            .AddIngredient(ModContent.ItemType<HallowedOre>(), 5)
            .AddTile(TileID.AdamantiteForge)
            .Register();
        #endregion

        #region Workbenches
        Recipe.Create(ItemID.FlinxStaff).AddIngredient(ItemID.FlinxFur, 6).AddIngredient(ModContent.ItemType<BismuthBar>(), 10).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ItemID.PeaceCandle).AddIngredient(ModContent.ItemType<BismuthBar>(), 2).AddIngredient(ItemID.PinkTorch).AddTile(TileID.WorkBenches).Register();
        //Recipe.Create(ItemID.Picksaw).AddIngredient(ModContent.ItemType<SolariumStar>(), 50).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.BeetleBar>(), 3).AddIngredient(ItemID.SoulofMight, 15).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(ItemID.Leather).AddIngredient(ModContent.ItemType<RottenFlesh>(), 4).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ItemID.Leather).AddIngredient(ModContent.ItemType<YuckyBit>(), 6).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ItemID.AcornAxe).AddIngredient(ItemID.StaffofRegrowth).AddIngredient(ModContent.ItemType<BronzeAxe>()).AddIngredient(ItemID.JungleSpores, 12).AddIngredient(ItemID.Vine, 3).AddTile(TileID.WorkBenches).Register();
        #endregion

        #region Tinkerers Workshop
        //Recipe.Create(ItemID.GuideVoodooDoll).AddIngredient(ItemID.Silk, 5).AddIngredient(ModContent.ItemType<FleshyTendril>(), 5).AddIngredient(ItemID.SoulofNight, 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.IceSkates).AddIngredient(ItemID.Leather, 6).AddRecipeGroup("IronBar", 4).AddIngredient(ModContent.ItemType<FrostShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.WaterWalkingBoots).AddIngredient(ItemID.Leather, 7).AddIngredient(ItemID.WaterWalkingPotion, 10).AddIngredient(ModContent.ItemType<WaterShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.LavaCharm).AddIngredient(ItemID.ObsidianSkull).AddIngredient(ItemID.ObsidianSkinPotion, 10).AddIngredient(ModContent.ItemType<FireShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.AnkletoftheWind).AddIngredient(ItemID.Cloud, 25).AddIngredient(ModContent.ItemType<BreezeShard>(), 3).AddIngredient(ItemID.JungleSpores, 20).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.AnkletoftheWind).AddIngredient(ItemID.Cloud, 25).AddIngredient(ModContent.ItemType<BreezeShard>(), 3).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.CloudinaBottle).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.Cloud, 30).AddIngredient(ItemID.Feather, 2).AddIngredient(ModContent.ItemType<BreezeShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ModContent.ItemType<LightninginaBottle>()).AddIngredient(ItemID.Bottle).AddIngredient(ModContent.ItemType<BlastShard>(), 3).AddIngredient(ModContent.ItemType<SacredShard>(), 2).AddIngredient(ItemID.SoulofFright, 15).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.SandstorminaBottle).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.SandBlock, 50).AddIngredient(ModContent.ItemType<EarthShard>(), 5).AddIngredient(ModContent.ItemType<BreezeShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BlizzardinaBottle).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.IceBlock, 50).AddIngredient(ModContent.ItemType<FrostShard>(), 5).AddIngredient(ModContent.ItemType<BreezeShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.FlyingCarpet).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.Silk, 20).AddIngredient(ItemID.Cloud, 25).AddIngredient(ItemID.SoulofFlight, 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BandofStarpower).AddIngredient(ItemID.ManaCrystal, 3).AddIngredient(ItemID.DemoniteBar, 4).AddIngredient(ModContent.ItemType<CorruptShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BandofRegeneration).AddIngredient(ItemID.LifeCrystal, 3).AddRecipeGroup("Avalon:GoldBar", 4).AddIngredient(ItemID.HealingPotion, 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.PanicNecklace).AddIngredient(ItemID.LifeCrystal, 3).AddIngredient(ItemID.CrimtaneBar, 4).AddIngredient(ModContent.ItemType<CorruptShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.Extractinator).AddRecipeGroup("IronBar", 30).AddIngredient(ItemID.Glass, 5).AddIngredient(ItemID.Wire, 20).AddIngredient(ItemID.Timer1Second).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.HermesBoots).AddIngredient(ItemID.OldShoe).AddIngredient(ItemID.SwiftnessPotion, 2).AddIngredient(ItemID.Cloud, 60).AddIngredient(ModContent.ItemType<BreezeShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.FlurryBoots).AddIngredient(ItemID.OldShoe).AddIngredient(ItemID.SwiftnessPotion, 2).AddIngredient(ItemID.Cloud, 30).AddIngredient(ItemID.IceBlock, 30).AddIngredient(ModContent.ItemType<BreezeShard>(), 1).AddIngredient(ModContent.ItemType<FrostShard>(), 1).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.RodofDiscord).AddIngredient(ModContent.ItemType<ChaosDust>(), 45).AddIngredient(ItemID.SoulofLight, 25).AddIngredient(ItemID.Diamond, 10).AddIngredient(ItemID.SoulofMight).AddIngredient(ItemID.SoulofFright).AddIngredient(ItemID.SoulofSight).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.WarriorEmblem).AddIngredient(ItemID.RangerEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.WarriorEmblem).AddIngredient(ItemID.SorcererEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.WarriorEmblem).AddIngredient(ItemID.SummonerEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.RangerEmblem).AddIngredient(ItemID.WarriorEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.RangerEmblem).AddIngredient(ItemID.SorcererEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.RangerEmblem).AddIngredient(ItemID.SummonerEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.SorcererEmblem).AddIngredient(ItemID.WarriorEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.SorcererEmblem).AddIngredient(ItemID.RangerEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.SorcererEmblem).AddIngredient(ItemID.SummonerEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.SummonerEmblem).AddIngredient(ItemID.WarriorEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.SummonerEmblem).AddIngredient(ItemID.RangerEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.SummonerEmblem).AddIngredient(ItemID.SorcererEmblem).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BundleofBalloons).AddIngredient(ModContent.ItemType<SandyStormcloudinaBottle>()).AddIngredient(ItemID.ShinyRedBalloon, 3).AddTile(TileID.TinkerersWorkbench).Register();
        #endregion

        #region Bottles
        Recipe.Create(ItemID.IronskinPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Daybloom).AddIngredient(ModContent.ItemType<NickelOre>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.SpelunkerPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.Moonglow).AddIngredient(ModContent.ItemType<BismuthOre>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.MagicPowerPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Moonglow).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.FallenStar).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.MagicPowerPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Moonglow).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.FallenStar).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.BattlePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Vertebrae).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.BattlePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ModContent.ItemType<YuckyBit>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Deathweed).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ModContent.ItemType<MosquitoProboscis>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ItemID.Stinger).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ItemID.Stinger).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ModContent.ItemType<MosquitoProboscis>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ModContent.ItemType<MosquitoProboscis>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.GravitationPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Fireblossom).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.Feather).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.GravitationPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Fireblossom).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.Feather).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.CratePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Amber).AddIngredient(ItemID.Moonglow).AddIngredient(ItemID.Blinkroot).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.CratePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Amber).AddIngredient(ItemID.Moonglow).AddIngredient(ItemID.Blinkroot).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.TitanPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Bone).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Shiverthorn).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.TitanPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Bone).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Shiverthorn).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.RagePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Hemopiranha).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.RagePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Hemopiranha).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.WrathPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Ebonkoi).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.WrathPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Ebonkoi).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.RecallPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.SpecularFish).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.RecallPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.SpecularFish).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ModContent.ItemType<Items.Potions.ForceFieldPotion>()).AddIngredient(ModContent.ItemType<BottledLava>()).AddIngredient(ItemID.SoulofNight, 3).AddIngredient(ModContent.ItemType<Sweetstem>(), 2).AddIngredient(ItemID.Hellstone).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.StinkPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Stinkfish).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.StinkPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Stinkfish).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        #endregion

        #region Cooking Pot
        Recipe.Create(ItemID.MonsterLasagna).AddIngredient(ModContent.ItemType<YuckyBit>(), 8).AddTile(TileID.CookingPots).Register();
        #endregion

        #region Loom
        Recipe.Create(ItemID.FlinxFurCoat)
            .AddIngredient(ItemID.Silk, 10)
            .AddIngredient(ItemID.FlinxFur, 8)
            .AddIngredient(ModContent.ItemType<BismuthBar>(), 8)
            .AddTile(TileID.Loom)
            .Register();
        #endregion

        #region Ice Machine
        Recipe.Create(ItemID.IceBlade).AddIngredient(ItemID.GoldBroadsword).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 8).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.IceBlade).AddIngredient(ItemID.PlatinumBroadsword).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 8).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.IceBlade).AddIngredient(ModContent.ItemType<BismuthBroadsword>()).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 8).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.IceBoomerang).AddIngredient(ItemID.WoodenBoomerang).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 2).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.SnowballCannon).AddRecipeGroup("IronBar", 10).AddIngredient(ModContent.ItemType<Icicle>(), 20).AddIngredient(ItemID.SnowBlock, 20).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        #endregion

        #region Dungeon Furniture
        #region blue
        Recipe.Create(ItemID.BlueDungeonBathtub)
            .AddIngredient(ItemID.BlueBrick, 15)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonBed)
            .AddIngredient(ItemID.BlueBrick, 15)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonBookcase)
            .AddIngredient(ItemID.BlueBrick, 20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonCandelabra)
            .AddIngredient(ItemID.BlueBrick, 5)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonCandle)
            .AddIngredient(ItemID.BlueBrick, 4)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonChair)
            .AddIngredient(ItemID.BlueBrick, 4)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonChandelier)
            .AddIngredient(ItemID.BlueBrick, 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.DungeonClockBlue)
            .AddIngredient(ItemID.BlueBrick, 10)
            .AddRecipeGroup(RecipeGroupID.IronBar, 3)
            .AddIngredient(ItemID.Glass, 6)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonDoor)
            .AddIngredient(ItemID.BlueBrick, 6)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonDresser)
            .AddIngredient(ItemID.BlueBrick, 16)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonLamp)
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ItemID.BlueBrick, 3)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonPiano)
            .AddIngredient(ItemID.BlueBrick, 15)
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ItemID.Book)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonSofa)
            .AddIngredient(ItemID.BlueBrick, 5)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonTable)
            .AddIngredient(ItemID.BlueBrick, 8)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueDungeonWorkBench)
            .AddIngredient(ItemID.BlueBrick, 10)
            .AddTile(TileID.BoneWelder)
            .Register();
        #endregion

        #region green
        Recipe.Create(ItemID.GreenDungeonBathtub)
            .AddIngredient(ItemID.GreenBrick, 15)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonBed)
            .AddIngredient(ItemID.GreenBrick, 15)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonBookcase)
            .AddIngredient(ItemID.GreenBrick, 20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonCandelabra)
            .AddIngredient(ItemID.GreenBrick, 5)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonCandle)
            .AddIngredient(ItemID.GreenBrick, 4)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonChair)
            .AddIngredient(ItemID.GreenBrick, 4)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonChandelier)
            .AddIngredient(ItemID.GreenBrick, 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.DungeonClockGreen)
            .AddIngredient(ItemID.GreenBrick, 10)
            .AddRecipeGroup(RecipeGroupID.IronBar, 3)
            .AddIngredient(ItemID.Glass, 6)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonDoor)
            .AddIngredient(ItemID.GreenBrick, 6)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonDresser)
            .AddIngredient(ItemID.GreenBrick, 16)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonLamp)
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ItemID.GreenBrick, 3)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonPiano)
            .AddIngredient(ItemID.GreenBrick, 15)
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ItemID.Book)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonSofa)
            .AddIngredient(ItemID.GreenBrick, 5)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonTable)
            .AddIngredient(ItemID.GreenBrick, 8)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenDungeonWorkBench)
            .AddIngredient(ItemID.GreenBrick, 10)
            .AddTile(TileID.BoneWelder)
            .Register();
        #endregion

        #region pink
        Recipe.Create(ItemID.PinkDungeonBathtub)
            .AddIngredient(ItemID.PinkBrick, 15)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonBed)
            .AddIngredient(ItemID.PinkBrick, 15)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonBookcase)
            .AddIngredient(ItemID.PinkBrick, 20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonCandelabra)
            .AddIngredient(ItemID.PinkBrick, 5)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonCandle)
            .AddIngredient(ItemID.PinkBrick, 4)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonChair)
            .AddIngredient(ItemID.PinkBrick, 4)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonChandelier)
            .AddIngredient(ItemID.PinkBrick, 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.DungeonClockPink)
            .AddIngredient(ItemID.PinkBrick, 10)
            .AddRecipeGroup(RecipeGroupID.IronBar, 3)
            .AddIngredient(ItemID.Glass, 6)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonDoor)
            .AddIngredient(ItemID.PinkBrick, 6)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonDresser)
            .AddIngredient(ItemID.PinkBrick, 16)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonLamp)
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ItemID.PinkBrick, 3)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonPiano)
            .AddIngredient(ItemID.PinkBrick, 15)
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ItemID.Book)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonSofa)
            .AddIngredient(ItemID.PinkBrick, 5)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonTable)
            .AddIngredient(ItemID.PinkBrick, 8)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.PinkDungeonWorkBench)
            .AddIngredient(ItemID.PinkBrick, 10)
            .AddTile(TileID.BoneWelder)
            .Register();
        #endregion
        #endregion

        #region Obsidian Furniture
        Recipe.Create(ItemID.ObsidianBathtub)
            .AddIngredient(ItemID.Obsidian, 15)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddTile(TileID.Sawmill)
            .Register();

        Recipe.Create(ItemID.ObsidianBed)
            .AddIngredient(ItemID.Obsidian, 15)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.Sawmill)
            .Register();

        Recipe.Create(ItemID.ObsidianBookcase)
            .AddIngredient(ItemID.Obsidian, 20)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.Sawmill)
            .Register();

        Recipe.Create(ItemID.ObsidianCandelabra)
            .AddIngredient(ItemID.Obsidian, 5)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile(TileID.WorkBenches)
            .Register();

        Recipe.Create(ItemID.ObsidianCandle)
            .AddIngredient(ItemID.Obsidian, 4)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches)
            .Register();

        Recipe.Create(ItemID.ObsidianChair)
            .AddIngredient(ItemID.Obsidian, 4)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddTile(TileID.WorkBenches)
            .Register();

        Recipe.Create(ItemID.ObsidianChandelier)
            .AddIngredient(ItemID.Obsidian, 4)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(ItemID.ObsidianClock)
            .AddIngredient(ItemID.Obsidian, 10)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddRecipeGroup(RecipeGroupID.IronBar, 3)
            .AddIngredient(ItemID.Glass, 6)
            .AddTile(TileID.Sawmill)
            .Register();

        Recipe.Create(ItemID.ObsidianDoor)
            .AddIngredient(ItemID.Obsidian, 6)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddTile(TileID.WorkBenches)
            .Register();

        Recipe.Create(ItemID.ObsidianDresser)
            .AddIngredient(ItemID.Obsidian, 16)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddTile(TileID.Sawmill)
            .Register();

        Recipe.Create(ItemID.ObsidianLamp)
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ItemID.Obsidian, 3)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddTile(TileID.WorkBenches)
            .Register();

        Recipe.Create(ItemID.ObsidianLantern)
            .AddIngredient(ItemID.Obsidian, 6)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddIngredient(ItemID.Torch)
            .AddTile(TileID.WorkBenches)
            .Register();

        Recipe.Create(ItemID.ObsidianPiano)
            .AddIngredient(ItemID.Obsidian, 15)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ItemID.Book)
            .AddTile(TileID.Sawmill)
            .Register();

        Recipe.Create(ItemID.ObsidianSofa)
            .AddIngredient(ItemID.Obsidian, 5)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.Sawmill)
            .Register();

        Recipe.Create(ItemID.ObsidianTable)
            .AddIngredient(ItemID.Obsidian, 8)
            .AddIngredient(ItemID.Hellstone, 2)
            .AddTile(TileID.WorkBenches)
            .Register();

        Recipe.Create(ItemID.ObsidianWorkBench)
            .AddIngredient(ItemID.Obsidian, 10)
            .AddIngredient(ItemID.Hellstone, 2)
            .Register();
        #endregion

        #region Bone Welder
        Recipe.Create(ItemID.PinkBrick)
            .AddIngredient(ItemID.StoneBlock)
            .AddIngredient(ItemID.Bone, 2)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.GreenBrick)
            .AddIngredient(ItemID.StoneBlock)
            .AddIngredient(ItemID.Bone, 2)
            .AddTile(TileID.BoneWelder)
            .Register();

        Recipe.Create(ItemID.BlueBrick)
            .AddIngredient(ItemID.StoneBlock)
            .AddIngredient(ItemID.Bone, 2)
            .AddTile(TileID.BoneWelder)
            .Register();
        #endregion
    }
}
