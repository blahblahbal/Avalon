using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Items.Material.Shards;
using Avalon.Items.Material.Ores;
using Avalon.Items.Weapons.Ranged.PreHardmode;

namespace Avalon.Systems.Recipes;

public class VanillaItemRecipeCreator : ModSystem
{
    public override void AddRecipes()
    {
        //Recipe.Create(ItemID.SlimeCrown)
        //    .AddIngredient(ModContent.ItemType<Items.Vanity.BismuthCrown>())
        //    .AddIngredient(ItemID.Gel, 20)
        //    .AddTile(TileID.DemonAltar)
        //    .Register();

        Recipe.Create(ItemID.Aglet).AddRecipeGroup("Avalon:CopperBar").AddRecipeGroup("Wood", 6).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.IronskinPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Daybloom).AddIngredient(ModContent.ItemType<NickelOre>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.SpelunkerPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.Moonglow).AddIngredient(ModContent.ItemType<BismuthOre>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.PeaceCandle).AddIngredient(ModContent.ItemType<Items.Material.Bars.BismuthBar>(), 2).AddIngredient(ItemID.PinkTorch).AddTile(TileID.WorkBenches).Register();
        //Recipe.Create(ItemID.NightsEdge).AddIngredient(ModContent.ItemType<Snotsabre>()).AddIngredient(ItemID.Muramasa).AddIngredient(ItemID.BladeofGrass).AddIngredient(ItemID.FieryGreatsword).AddTile(TileID.DemonAltar).Register();
        Recipe.Create(ItemID.MagicMirror).AddIngredient(ItemID.RecallPotion, 3).AddRecipeGroup("IronBar", 5).AddIngredient(ItemID.Glass, 20).AddTile(TileID.Furnaces).Register();
        //Recipe.Create(ItemID.GuideVoodooDoll).AddIngredient(ItemID.Silk, 5).AddIngredient(ModContent.ItemType<FleshyTendril>(), 5).AddIngredient(ItemID.SoulofNight, 5).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.MagicPowerPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Moonglow).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.FallenStar).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.MagicPowerPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Moonglow).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.FallenStar).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.BattlePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Vertebrae).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.BattlePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ModContent.ItemType<YuckyBit>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Deathweed).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ModContent.ItemType<MosquitoProboscis>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ItemID.Stinger).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ItemID.Stinger).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ModContent.ItemType<MosquitoProboscis>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.ThornsPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Cactus).AddIngredient(ItemID.WormTooth).AddIngredient(ModContent.ItemType<MosquitoProboscis>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.GravitationPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Fireblossom).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.Feather).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.GravitationPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Fireblossom).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Blinkroot).AddIngredient(ItemID.Feather).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.CratePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Amber).AddIngredient(ItemID.Moonglow).AddIngredient(ItemID.Blinkroot).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.CratePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Amber).AddIngredient(ItemID.Moonglow).AddIngredient(ItemID.Blinkroot).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.TitanPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Bone).AddIngredient(ModContent.ItemType<Bloodberry>()).AddIngredient(ItemID.Shiverthorn).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.TitanPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Bone).AddIngredient(ModContent.ItemType<Barfbush>()).AddIngredient(ItemID.Shiverthorn).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.RagePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Hemopiranha).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.RagePotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Hemopiranha).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.WrathPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Ebonkoi).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.WrathPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Ebonkoi).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.RecallPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.SpecularFish).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.RecallPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.SpecularFish).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ModContent.ItemType<Items.Potions.ForceFieldPotion>()).AddIngredient(ModContent.ItemType<BottledLava>()).AddIngredient(ItemID.SoulofNight, 3).AddIngredient(ModContent.ItemType<Sweetstem>(), 2).AddIngredient(ItemID.Hellstone).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.StinkPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Stinkfish).AddIngredient(ModContent.ItemType<Bloodberry>()).AddTile(TileID.Bottles).Register();
        //Recipe.Create(ItemID.StinkPotion).AddIngredient(ItemID.BottledWater).AddIngredient(ItemID.Stinkfish).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
        Recipe.Create(ItemID.IceSkates).AddIngredient(ItemID.Leather, 6).AddRecipeGroup("IronBar", 4).AddIngredient(ModContent.ItemType<FrostShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.WaterWalkingBoots).AddIngredient(ItemID.Leather, 7).AddIngredient(ItemID.WaterWalkingPotion, 10).AddIngredient(ModContent.ItemType<WaterShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.LavaCharm).AddIngredient(ItemID.ObsidianSkull).AddIngredient(ItemID.ObsidianSkinPotion, 10).AddIngredient(ModContent.ItemType<FireShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.Starfury).AddIngredient(ItemID.GoldBroadsword).AddIngredient(ItemID.MeteoriteBar, 10).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.Starfury).AddIngredient(ItemID.PlatinumBroadsword).AddIngredient(ItemID.MeteoriteBar, 10).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.Starfury).AddIngredient(ModContent.ItemType<BismuthBroadsword>()).AddIngredient(ItemID.MeteoriteBar, 10).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.Anvils).Register();
        Recipe.Create(ItemID.AnkletoftheWind).AddIngredient(ItemID.Cloud, 25).AddIngredient(ModContent.ItemType<BreezeShard>(), 3).AddIngredient(ItemID.JungleSpores, 20).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.AnkletoftheWind).AddIngredient(ItemID.Cloud, 25).AddIngredient(ModContent.ItemType<BreezeShard>(), 3).AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.CloudinaBottle).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.Cloud, 30).AddIngredient(ItemID.Feather, 2).AddIngredient(ModContent.ItemType<BreezeShard>()).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ModContent.ItemType<LightninginaBottle>()).AddIngredient(ItemID.Bottle).AddIngredient(ModContent.ItemType<BlastShard>(), 3).AddIngredient(ModContent.ItemType<SacredShard>(), 2).AddIngredient(ItemID.SoulofFright, 15).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.SandstorminaBottle).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.SandBlock, 50).AddIngredient(ModContent.ItemType<EarthShard>(), 5).AddIngredient(ModContent.ItemType<BreezeShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BlizzardinaBottle).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.IceBlock, 50).AddIngredient(ModContent.ItemType<FrostShard>(), 5).AddIngredient(ModContent.ItemType<BreezeShard>(), 5).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.FlyingCarpet).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.Silk, 20).AddIngredient(ItemID.Cloud, 25).AddIngredient(ItemID.SoulofFlight, 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BandofStarpower).AddIngredient(ItemID.ManaCrystal, 3).AddIngredient(ItemID.Shackle, 2).AddIngredient(ModContent.ItemType<CorruptShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.BandofRegeneration).AddIngredient(ItemID.LifeCrystal, 3).AddIngredient(ItemID.Shackle, 2).AddIngredient(ItemID.HealingPotion, 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.IceBlade).AddIngredient(ItemID.GoldBroadsword).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 8).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.IceBlade).AddIngredient(ItemID.PlatinumBroadsword).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 8).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.IceBlade).AddIngredient(ModContent.ItemType<BismuthBroadsword>()).AddIngredient(ModContent.ItemType<Icicle>(), 50).AddIngredient(ItemID.FallenStar, 8).AddIngredient(ModContent.ItemType<FrostShard>(), 4).AddTile(TileID.IceMachine).Register();
        Recipe.Create(ItemID.Extractinator).AddRecipeGroup("IronBar", 30).AddIngredient(ItemID.Glass, 5).AddIngredient(ItemID.Wire, 20).AddIngredient(ItemID.Timer1Second).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.HermesBoots).AddIngredient(ItemID.OldShoe).AddIngredient(ItemID.SwiftnessPotion, 2).AddIngredient(ItemID.Cloud, 60).AddIngredient(ModContent.ItemType<BreezeShard>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ItemID.RodofDiscord).AddIngredient(ModContent.ItemType<ChaosDust>(), 45).AddIngredient(ItemID.SoulofLight, 25).AddIngredient(ItemID.Diamond, 10).AddIngredient(ItemID.SoulofMight).AddIngredient(ItemID.SoulofFright).AddIngredient(ItemID.SoulofSight).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.LihzahrdPowerCell).AddIngredient(ModContent.ItemType<SolariumStar>(), 5).AddIngredient(ItemID.LihzahrdBrick, 10).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(ItemID.Leather).AddIngredient(ModContent.ItemType<RottenFlesh>(), 4).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ItemID.Leather).AddIngredient(ModContent.ItemType<YuckyBit>(), 6).AddTile(TileID.WorkBenches).Register();
        //Recipe.Create(ItemID.Picksaw).AddIngredient(ModContent.ItemType<SolariumStar>(), 50).AddIngredient(ModContent.ItemType<Items.Placeable.Bar.BeetleBar>(), 3).AddIngredient(ItemID.SoulofMight, 15).AddTile(TileID.MythrilAnvil).Register();



        Recipe.Create(ItemID.VoidLens)
            .AddIngredient(ItemID.Bone, 30)
            .AddIngredient(ItemID.JungleSpores, 15)
            .AddIngredient(ModContent.ItemType<Booger>(), 30)
            .AddTile(TileID.DemonAltar)
            .Register();

        Recipe.Create(ItemID.VoidVault)
            .AddIngredient(ItemID.Bone, 15)
            .AddIngredient(ItemID.JungleSpores, 8)
            .AddIngredient(ModContent.ItemType<Booger>(), 15)
            .AddTile(TileID.DemonAltar)
            .Register();

        Recipe.Create(ItemID.ObsidianShirt)
            .AddIngredient(ItemID.Silk, 10)
            .AddIngredient(ItemID.Obsidian, 20)
            .AddIngredient(ModContent.ItemType<Booger>(), 5)
            .AddTile(TileID.Hellforge)
            .Register();

        Recipe.Create(ItemID.ObsidianHelm)
            .AddIngredient(ItemID.Silk, 10)
            .AddIngredient(ItemID.Obsidian, 20)
            .AddIngredient(ModContent.ItemType<Booger>(), 5)
            .AddTile(TileID.Hellforge)
            .Register();

        Recipe.Create(ItemID.ObsidianPants)
            .AddIngredient(ItemID.Silk, 10)
            .AddIngredient(ItemID.Obsidian, 20)
            .AddIngredient(ModContent.ItemType<Booger>(), 5)
            .AddTile(TileID.Hellforge)
            .Register();
    }
}
