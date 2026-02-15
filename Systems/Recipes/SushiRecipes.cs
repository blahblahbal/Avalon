using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Items.Pets;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Tools.PreHardmode;
using Avalon.Items.Weapons.Magic.Other;
using Avalon.Items.Weapons.Magic.Wands;
using Avalon.Items.Weapons.Magic.Tomes;
using Avalon.Items.Weapons.Melee.Boomerangs;
using Avalon.Items.Weapons.Melee.Maces;
using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Items.Weapons.Ranged.Bows;
using Avalon.Items.Weapons.Ranged.Guns;
using Avalon.Items.Weapons.Ranged.Longbows;
using Avalon.ModSupport.Tokens;
using Avalon.Tiles.Furniture.Crafting;
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
        Recipe.Create(imk.Find<ModItem>("CorruptionToken").Type).AddIngredient(imk.Find<ModItem>("CrimsonToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
        Recipe.Create(imk.Find<ModItem>("CorruptionToken").Type).AddIngredient(ModContent.ItemType<ContagionToken>()).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
        Recipe.Create(imk.Find<ModItem>("CrimsonToken").Type).AddIngredient(imk.Find<ModItem>("CorruptionToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
        Recipe.Create(imk.Find<ModItem>("CrimsonToken").Type).AddIngredient(ModContent.ItemType<ContagionToken>()).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<ContagionToken>()).AddIngredient(imk.Find<ModItem>("CorruptionToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<ContagionToken>()).AddIngredient(imk.Find<ModItem>("CrimsonToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();

        Recipe.Create(ModContent.ItemType<Blunderblight>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<SepticCell>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<Smogscreen>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<TetanusChakram>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<NerveNumbNecklace>()).AddIngredient(ModContent.ItemType<ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        #endregion Evil Biome

        #region Jungle
        Recipe.Create(ModContent.ItemType<BandofStamina>()).AddIngredient(imk.Find<ModItem>("JungleToken").Type, 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<FlowerofTheJungle>()).AddIngredient(imk.Find<ModItem>("JungleToken").Type, 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		#endregion Jungle

		#region Tropics
		Recipe.Create(ModContent.ItemType<BandofStamina>()).AddIngredient(ModContent.ItemType<TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<Thompson>()).AddIngredient(ModContent.ItemType<TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<RubberGloves>()).AddIngredient(ModContent.ItemType<TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		Recipe.Create(ItemID.FlowerBoots).AddIngredient(ModContent.ItemType<TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<AnkletofAcceleration>()).AddIngredient(ModContent.ItemType<TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		#endregion

		#region Snow
		Recipe.Create(ModContent.ItemType<FrozenLyre>()).AddIngredient(imk.Find<ModItem>("SnowToken").Type, 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<GlacierStaff>()).AddIngredient(imk.Find<ModItem>("SnowToken").Type, 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        #endregion Snow

        #region Space
        Recipe.Create(ModContent.ItemType<MoonplateBlock>(), 40).AddIngredient(imk.Find<ModItem>("PreHardmodeSpaceToken").Type).AddIngredient(ItemID.PlatinumOre).AddTile(TileID.TinkerersWorkbench).DisableDecraft().DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<DuskplateBlock>(), 40).AddIngredient(imk.Find<ModItem>("PreHardmodeSpaceToken").Type).AddIngredient(ModContent.ItemType<BismuthOre>()).AddTile(TileID.TinkerersWorkbench).DisableDecraft().DisableDecraft().Register();
        #endregion Space

        #region Goblins
        Recipe.Create(ModContent.ItemType<ChaosTome>()).AddIngredient(imk.Find<ModItem>("PostGoblinsLootToken").Type, 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<Longbow>()).AddIngredient(imk.Find<ModItem>("PostGoblinsLootToken").Type, 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<GoblinDagger>()).AddIngredient(imk.Find<ModItem>("PostGoblinsLootToken").Type, 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        #endregion Goblins

        #region Dungeon
        Recipe.Create(ModContent.ItemType<Blueshift>()).AddIngredient(imk.Find<ModItem>("DungeonToken").Type, 30).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<SapphirePickaxe>()).AddIngredient(imk.Find<ModItem>("DungeonToken").Type, 30).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<MarrowMasher>()).AddIngredient(imk.Find<ModItem>("DungeonToken").Type, 25).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        Recipe.Create(ModContent.ItemType<RodofCoalescence>()).AddIngredient(imk.Find<ModItem>("DungeonToken").Type, 75).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
        #endregion Dungeon

		#region immunity accessories
		Recipe.Create(ModContent.ItemType<GoldenShield>()).AddIngredient(imk.Find<ModItem>("PostMechanicalBossLootToken").Type, 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<GreekExtinguisher>()).AddIngredient(imk.Find<ModItem>("PostMechanicalBossLootToken").Type, 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<SixHundredWattLightbulb>()).AddIngredient(imk.Find<ModItem>("PostPlanteraLootToken").Type, 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<Vortex>()).AddIngredient(imk.Find<ModItem>("PostMechanicalBossLootToken").Type, 20).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<Windshield>()).AddIngredient(imk.Find<ModItem>("DesertToken").Type, 60).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		#endregion

		//Recipe.Create(ModContent.ItemType<Items.Accessories.StingerPack>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.Mechazapinator>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.StingerPack>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.HeatSeeker>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.Mechazapinator>()).AddIngredient(ModContent.ItemType<Items.Accessories.StingerPack>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.Mechazapinator>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.HeatSeeker>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.HeatSeeker>()).AddIngredient(ModContent.ItemType<Items.Accessories.StingerPack>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.HeatSeeker>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.Mechazapinator>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		/*Recipe.Create(ModContent.ItemType<VampireTeeth>()).AddIngredient(ModContent.ItemType<PhantomKnives>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<VampireTeeth>()).AddIngredient(ModContent.ItemType<EtherealHeart>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<EtherealHeart>()).AddIngredient(ModContent.ItemType<PhantomKnives>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<EtherealHeart>()).AddIngredient(ModContent.ItemType<VampireTeeth>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<PhantomKnives>()).AddIngredient(ModContent.ItemType<VampireTeeth>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<PhantomKnives>()).AddIngredient(ModContent.ItemType<EtherealHeart>()).AddIngredient(imk.Find<ModItem>("BossLootSwapToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		*/
		// reenable when this releases
		//Recipe.Create(imk.Find<ModItem>("PostMartiansLootToken").Type).AddIngredient(ModContent.ItemType<HellcastleToken>()).AddTile(TileID.TinkerersWorkbench).DisableDecraft().Register();
		//Recipe.Create(imk.Find<ModItem>("JungleToken").Type).AddIngredient(ModContent.ItemType<TropicsToken>()).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<TropicsToken>()).AddIngredient(imk.Find<ModItem>("JungleToken").Type).AddTile(TileID.MythrilAnvil).DisableDecraft().Register();
		
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagicCleaver>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.SuperhardmodeToken>(), 25).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.FleshBoiler>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.SuperhardmodeToken>(), 25).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.BubbleBoost>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.SuperhardmodeToken>(), 25).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		
		//Recipe.Create(ModContent.ItemType<Items.Material.SpikedBlastShell>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.SuperhardmodeToken>(), 10).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Material.PointingLaser>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.SuperhardmodeToken>(), 50).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Material.AlienDevice>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.SuperhardmodeToken>(), 60).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Material.Rock>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.SuperhardmodeToken>(), 40).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<GhostintheMachine>()).AddIngredient(ModContent.ItemType<HellcastleToken>(), 10).AddTile(ModContent.TileType<CaesiumForge>()).DisableDecraft().Register();
		Recipe.Create(ModContent.ItemType<Boomlash>()).AddIngredient(ModContent.ItemType<HellcastleToken>(), 30).AddTile(ModContent.TileType<CaesiumForge>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.Terraspin>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.MechastingToken>(), 60).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.QuadroCannon>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Summon.ReflectorStaff>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.DragonStone>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.Infernasword>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Weapons.Magic.MagmafrostBolt>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.MechastingToken>(), 45).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<HellcastleToken>()).AddIngredient(ModContent.ItemType<SuperhardmodeToken>()).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<SuperhardmodeToken>()).AddIngredient(ModContent.ItemType<DarkMatterToken>()).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<Items.Accessories.VampireHarpyWings>()).AddIngredient(ModContent.ItemType<ModSupport.Tokens.DarkMatterToken>(), 75).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();
		//Recipe.Create(ModContent.ItemType<DarkMatterToken>()).AddIngredient(ModContent.ItemType<MechastingToken>()).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).DisableDecraft().Register();

	}
}
