using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Ores;
using Avalon.Items.Other;
using Avalon.Items.Pets;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Tokens;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Items.Weapons.Magic.PreHardmode;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Systems.Recipes;

public class TokensRecipesSystem : ModSystem
{
    public override void AddRecipes()
    {
        if (ExxoAvalonOrigins.Tokens != null)
        {
            SushiRecipes.CreateRecipes(ExxoAvalonOrigins.Tokens);
        }
    }
}

public static class SushiRecipes
{
    public static void CreateRecipes(Mod imk)
    {
        #region Evil Biome
        Recipe.Create(imk.Find<ModItem>("CorruptionToken").Type).AddIngredient(imk.Find<ModItem>("CrimsonToken").Type).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(imk.Find<ModItem>("CorruptionToken").Type).AddIngredient(ModContent.ItemType<ContagionToken>()).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(imk.Find<ModItem>("CrimsonToken").Type).AddIngredient(imk.Find<ModItem>("CorruptionToken").Type).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(imk.Find<ModItem>("CrimsonToken").Type).AddIngredient(ModContent.ItemType<ContagionToken>()).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(ModContent.ItemType<ContagionToken>()).AddIngredient(imk.Find<ModItem>("CorruptionToken").Type).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(ModContent.ItemType<ContagionToken>()).AddIngredient(imk.Find<ModItem>("CrimsonToken").Type).AddTile(TileID.MythrilAnvil).Register();

        Recipe.Create(ModContent.ItemType<Blunderblight>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<SepticCell>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<Smogscreen>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<TetanusChakram>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<NerveNumbNecklace>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        #endregion Evil Biome

        #region Jungle
        Recipe.Create(ModContent.ItemType<BandofStamina>()).AddIngredient(imk.Find<ModItem>("JungleToken").Type, 25).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<FlowerofTheJungle>()).AddIngredient(imk.Find<ModItem>("JungleToken").Type, 25).AddTile(TileID.TinkerersWorkbench).Register();
        #endregion Jungle

        #region Snow
        Recipe.Create(ModContent.ItemType<FrozenLyre>()).AddIngredient(imk.Find<ModItem>("SnowToken").Type, 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<GlacierStaff>()).AddIngredient(imk.Find<ModItem>("SnowToken").Type, 20).AddTile(TileID.TinkerersWorkbench).Register();
        #endregion Snow

        #region Space
        Recipe.Create(ModContent.ItemType<MoonplateBlock>(), 40).AddIngredient(imk.Find<ModItem>("PreHardmodeSpaceToken").Type).AddIngredient(ItemID.PlatinumOre).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<DuskplateBlock>(), 40).AddIngredient(imk.Find<ModItem>("PreHardmodeSpaceToken").Type).AddIngredient(ModContent.ItemType<BismuthOre>()).AddTile(TileID.TinkerersWorkbench).Register();
        #endregion Space

        #region Goblins
        Recipe.Create(ModContent.ItemType<ChaosTome>()).AddIngredient(imk.Find<ModItem>("PostGoblinsLootToken").Type, 25).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<Longbow>()).AddIngredient(imk.Find<ModItem>("PostGoblinsLootToken").Type, 25).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<GoblinDagger>()).AddIngredient(imk.Find<ModItem>("PostGoblinsLootToken").Type, 25).AddTile(TileID.TinkerersWorkbench).Register();
        #endregion Goblins

        #region Dungeon
        Recipe.Create(ModContent.ItemType<Blueshift>()).AddIngredient(imk.Find<ModItem>("DungeonToken").Type, 30).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<SapphirePickaxe>()).AddIngredient(imk.Find<ModItem>("DungeonToken").Type, 30).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<MarrowMasher>()).AddIngredient(imk.Find<ModItem>("DungeonToken").Type, 25).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<RodofCoalescence>()).AddIngredient(imk.Find<ModItem>("DungeonToken").Type, 75).AddTile(TileID.TinkerersWorkbench).Register();
        #endregion Dungeon

        #region Biome Keys
        Recipe.Create(ModContent.ItemType<ContagionKey>()).AddIngredient(imk.Find<ModItem>("Biome Key").Type).AddIngredient(ModContent.ItemType<BacciliteBar>(), 50).AddTile(TileID.TinkerersWorkbench).Register();
        #endregion Biome Keys

        //Recipe.Create(ModContent.ItemType<Items.Accessories.StingerPack>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.Mechazapinator>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.StingerPack>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.HeatSeeker>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.Mechazapinator>()).AddIngredient(ModContent.ItemType<Items.Accessories.StingerPack>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.Mechazapinator>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.HeatSeeker>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.HeatSeeker>()).AddIngredient(ModContent.ItemType<Items.Accessories.StingerPack>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.HeatSeeker>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.Mechazapinator>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.VampireTeeth>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.PhantomKnives>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.VampireTeeth>()).AddIngredient(ModContent.ItemType<Items.Accessories.EtherealHeart>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.EtherealHeart>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.PhantomKnives>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.EtherealHeart>()).AddIngredient(ModContent.ItemType<Items.Accessories.VampireTeeth>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.PhantomKnives>()).AddIngredient(ModContent.ItemType<Items.Accessories.VampireTeeth>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.PhantomKnives>()).AddIngredient(ModContent.ItemType<Items.Accessories.EtherealHeart>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(imk.Find<ModItem>("PostMartiansLootToken").Type).AddIngredient(ModContent.ItemType<Items.Tokens.HellcastleToken>()).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(imk.Find<ModItem>("JungleToken").Type).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>()).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Tokens.TropicsToken>()).AddIngredient(imk.Find<ModItem>("JungleToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.Thompson>()).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.FeralClaws).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.FlowerBoots).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.AnkletoftheWind).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.VirulentPike>()).AddIngredient(ModContent.ItemType<Items.Tokens.ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(ModContent.ItemType<Items.Tokens.SuperhardmodeToken>(), 25).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(ModContent.ItemType<Items.Tokens.SuperhardmodeToken>(), 25).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(ModContent.ItemType<Items.Tokens.SuperhardmodeToken>(), 25).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.GoldenShield>()).AddIngredient(imk.Find<ModItem>("PostMechanicalBossLootToken").Type, 20).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.GreekExtinguisher>()).AddIngredient(imk.Find<ModItem>("PostMechanicalBossLootToken").Type, 20).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.SixHundredWattLightbulb>()).AddIngredient(imk.Find<ModItem>("PostPlanteraLootToken").Type, 20).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.Vortex>()).AddIngredient(imk.Find<ModItem>("PostMechanicalBossLootToken").Type, 20).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ModContent.ItemType<Items.Material.SpikedBlastShell>()).AddIngredient(ModContent.ItemType<Items.Tokens.SuperhardmodeToken>(), 10).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Material.PointingLaser>()).AddIngredient(ModContent.ItemType<Items.Tokens.SuperhardmodeToken>(), 50).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Material.AlienDevice>()).AddIngredient(ModContent.ItemType<Items.Tokens.SuperhardmodeToken>(), 60).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Material.Rock>()).AddIngredient(ModContent.ItemType<Items.Tokens.SuperhardmodeToken>(), 40).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Material.GhostintheMachine>()).AddIngredient(ModContent.ItemType<Items.Tokens.HellcastleToken>(), 10).AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.Boomlash>()).AddIngredient(ModContent.ItemType<Items.Tokens.HellcastleToken>(), 30).AddTile(ModContent.TileType<Tiles.CaesiumForge>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.Terraspin>()).AddIngredient(ModContent.ItemType<Items.Tokens.MechastingToken>(), 60).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<Items.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<Items.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<Items.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<Items.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<Items.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Tokens.HellcastleToken>()).AddIngredient(ModContent.ItemType<Items.Tokens.SuperhardmodeToken>()).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Tokens.SuperhardmodeToken>()).AddIngredient(ModContent.ItemType<Items.Tokens.DarkMatterToken>()).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Accessories.VampireHarpyWings>()).AddIngredient(ModContent.ItemType<Items.Tokens.DarkMatterToken>(), 75).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        //Recipe.Create(ModContent.ItemType<Items.Tokens.DarkMatterToken>()).AddIngredient(ModContent.ItemType<Items.Tokens.MechastingToken>()).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();

    }
}
