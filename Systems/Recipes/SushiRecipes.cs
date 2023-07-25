using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Systems;

public static class SushiRecipes
{
    public static void CreateRecipes(Mod imk)
    {
    // Recipe.Create doesn't work as a replacement for self.CreateRecipe
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
        Recipe.Create(imk.Find<ModItem>("CrimsonToken").Type).AddIngredient(imk.Find<ModItem>("CorruptionToken").Type).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(ModContent.ItemType<Items.Tokens.ContagionToken>()).AddIngredient(imk.Find<ModItem>("CrimsonToken").Type).AddTile(TileID.MythrilAnvil).Register();
        Recipe.Create(imk.Find<ModItem>("CorruptionToken").Type).AddIngredient(ModContent.ItemType<Items.Tokens.ContagionToken>()).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(imk.Find<ModItem>("JungleToken").Type).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>()).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Tokens.TropicsToken>()).AddIngredient(imk.Find<ModItem>("JungleToken").Type).AddTile(TileID.MythrilAnvil).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.Thompson>()).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.FeralClaws).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.FlowerBoots).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ItemID.AnkletoftheWind).AddIngredient(ModContent.ItemType<Items.Tokens.TropicsToken>(), 25).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<Items.Accessories.PreHardmode.BandofStamina>()).AddIngredient(ModContent.ItemType<Items.Tokens.ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        //Recipe.Create(ModContent.ItemType<Items.Weapons.Melee.VirulentPike>()).AddIngredient(ModContent.ItemType<Items.Tokens.ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<Items.Pets.SepticCell>()).AddIngredient(ModContent.ItemType<Items.Tokens.ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
        Recipe.Create(ModContent.ItemType<Items.Weapons.Ranged.PreHardmode.Blunderblight>()).AddIngredient(ModContent.ItemType<Items.Tokens.ContagionToken>(), 20).AddTile(TileID.TinkerersWorkbench).Register();
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
