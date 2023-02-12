using ExxoAvalonOrigins.Items.Material;
using ExxoAvalonOrigins.Items.Material.Bars;
using ExxoAvalonOrigins.Items.Material.Ores;
using ExxoAvalonOrigins.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Systems.Recipes;

internal class CatalyzerRecipes : ModSystem
{
    public override void AddRecipes()
    {
        #region catalyzer

        //start stone types
        Recipe.Create(ItemID.EbonstoneBlock, 50)
            .AddIngredient(ItemID.PearlstoneBlock, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.CrimstoneBlock, 50)
            .AddIngredient(ItemID.EbonstoneBlock, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<ChunkstoneBlock>(), 50)
            .AddIngredient(ItemID.CrimstoneBlock, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PearlstoneBlock, 50)
            .AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end stone types
        //start wood
        Recipe.Create(ItemID.Wood, 50)
            .AddIngredient(ModContent.ItemType<ApocalyptusWood>(), 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Ebonwood, 50)
            .AddIngredient(ItemID.Wood, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Shadewood, 50)
            .AddIngredient(ItemID.Ebonwood, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<Coughwood>(), 50)
            .AddIngredient(ItemID.Shadewood, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Pearlwood, 50)
            .AddIngredient(ModContent.ItemType<Coughwood>(), 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.BorealWood, 50)
            .AddIngredient(ItemID.Pearlwood, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PalmWood, 50)
            .AddIngredient(ItemID.BorealWood, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.RichMahogany, 50)
            .AddIngredient(ItemID.PalmWood, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BleachedEbony>(), 50)
            .AddIngredient(ItemID.RichMahogany, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<ApocalyptusWood>(), 50)
            .AddIngredient(ModContent.ItemType<BleachedEbony>(), 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end wood
        //evil ores
        Recipe.Create(ItemID.DemoniteOre, 40)
            .AddIngredient(ModContent.ItemType<PandemiteOre>(), 40)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.CrimtaneOre, 40)
            .AddIngredient(ItemID.DemoniteOre, 40)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<PandemiteOre>(), 40)
            .AddIngredient(ItemID.CrimtaneOre, 40)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end evil ores
        //hardmode ores
        /*Recipe.Create(ItemID.CobaltOre, 20)
            .AddIngredient(ModContent.ItemType<DurataniumOre>(), 20)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PalladiumOre, 20)
            .AddIngredient(ItemID.CobaltOre, 20)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<DurataniumOre>(), 20)
            .AddIngredient(ItemID.PalladiumOre, 20)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.MythrilOre, 20)
            .AddIngredient(ModContent.ItemType<NaquadahOre>(), 20)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.OrichalcumOre, 20)
            .AddIngredient(ItemID.MythrilOre, 20)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<NaquadahOre>(), 20)
            .AddIngredient(ItemID.OrichalcumOre, 20)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.AdamantiteOre, 10)
            .AddIngredient(ModContent.ItemType<TroxiniumOre>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TitaniumOre, 10)
            .AddIngredient(ItemID.AdamantiteOre, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<TroxiniumOre>(), 10)
            .AddIngredient(ItemID.TitaniumOre, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();*/
        //end hardmode ores
        //evil bars
        Recipe.Create(ItemID.DemoniteBar, 10)
            .AddIngredient(ModContent.ItemType<PandemiteBar>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.CrimtaneBar, 10)
            .AddIngredient(ItemID.DemoniteBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<PandemiteBar>(), 10)
            .AddIngredient(ItemID.CrimtaneBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end evil bars

        //phm ores
        Recipe.Create(ItemID.CopperOre, 30)
            .AddIngredient(ModContent.ItemType<BronzeOre>(), 30)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TinOre, 30)
            .AddIngredient(ItemID.CopperOre, 30)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BronzeOre>(), 30)
            .AddIngredient(ItemID.TinOre, 30)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.IronOre, 30)
            .AddIngredient(ModContent.ItemType<NickelOre>(), 30)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.LeadOre, 30)
            .AddIngredient(ItemID.IronOre, 30)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<NickelOre>(), 30)
            .AddIngredient(ItemID.LeadOre, 30)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.SilverOre, 40)
            .AddIngredient(ModContent.ItemType<ZincOre>(), 40)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TungstenOre, 40)
            .AddIngredient(ItemID.SilverOre, 40)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<ZincOre>(), 40)
            .AddIngredient(ItemID.TungstenOre, 40)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.GoldOre, 40)
            .AddIngredient(ModContent.ItemType<BismuthOre>(), 40)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PlatinumOre, 40)
            .AddIngredient(ItemID.GoldOre, 40)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BismuthOre>(), 40)
            .AddIngredient(ItemID.PlatinumOre, 40)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end phm ores

        //phm bars
        Recipe.Create(ItemID.CopperBar, 10)
            .AddIngredient(ModContent.ItemType<BronzeBar>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TinBar, 10)
            .AddIngredient(ItemID.CopperBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BronzeBar>(), 10)
            .AddIngredient(ItemID.TinBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.IronBar, 10)
            .AddIngredient(ModContent.ItemType<NickelBar>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.LeadBar, 10)
            .AddIngredient(ItemID.IronBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<NickelBar>(), 10)
            .AddIngredient(ItemID.LeadBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.SilverBar, 10)
            .AddIngredient(ModContent.ItemType<ZincBar>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TungstenBar, 10)
            .AddIngredient(ItemID.SilverBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<ZincBar>(), 10)
            .AddIngredient(ItemID.TungstenBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.GoldBar, 10)
            .AddIngredient(ModContent.ItemType<BismuthBar>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PlatinumBar, 10)
            .AddIngredient(ItemID.GoldBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<BismuthBar>(), 10)
            .AddIngredient(ItemID.PlatinumBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end phm bars

        //hardmode ore bars
        /*Recipe.Create(ItemID.CobaltBar, 10)
            .AddIngredient(ModContent.ItemType<DurataniumBar>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PalladiumBar, 10)
            .AddIngredient(ItemID.CobaltBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<DurataniumBar>(), 10)
            .AddIngredient(ItemID.PalladiumBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.MythrilBar, 10)
            .AddIngredient(ModContent.ItemType<NaquadahBar>(), 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.OrichalcumBar, 10)
            .AddIngredient(ItemID.MythrilBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<NaquadahBar>(), 10)
            .AddIngredient(ItemID.OrichalcumBar, 10)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.AdamantiteBar, 5)
            .AddIngredient(ModContent.ItemType<TroxiniumBar>(), 5)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TitaniumBar, 5)
            .AddIngredient(ItemID.AdamantiteBar, 5)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<TroxiniumBar>(), 5)
            .AddIngredient(ItemID.TitaniumBar, 5)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();*/
        //end hardmode ore bars
        //evil boss materials
        Recipe.Create(ItemID.ShadowScale, 3)
            .AddIngredient(ModContent.ItemType<Booger>(), 3)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.TissueSample, 3)
            .AddIngredient(ItemID.ShadowScale, 3)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<Booger>(), 3)
            .AddIngredient(ItemID.TissueSample, 3)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();
        //end evil boss materials

        /*Recipe.Create(ItemID.CursedFlame, 33)
            .AddIngredient(ModContent.ItemType<Pathogen>(), 33)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Ichor, 33)
            .AddIngredient(ItemID.CursedFlame, 33)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<Pathogen>(), 33)
            .AddIngredient(ItemID.Ichor, 33)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();*/

        Recipe.Create(ItemID.RottenChunk, 50)
            .AddIngredient(ModContent.ItemType<YuckyBit>(), 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.Vertebrae, 50)
            .AddIngredient(ItemID.RottenChunk, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<YuckyBit>(), 50)
            .AddIngredient(ItemID.Vertebrae, 50)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        /*Recipe.Create(ItemID.GreenSolution, 100)
            .AddIngredient(ModContent.ItemType<LimeGreenSolution>(), 100)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.PurpleSolution, 100)
            .AddIngredient(ItemID.GreenSolution, 100)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.RedSolution, 100)
            .AddIngredient(ItemID.PurpleSolution, 100)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<YellowSolution>(), 100)
            .AddIngredient(ItemID.RedSolution, 100)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.BlueSolution, 100)
            .AddIngredient(ModContent.ItemType<YellowSolution>(), 100)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ItemID.DarkBlueSolution, 100)
            .AddIngredient(ItemID.BlueSolution, 100)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();

        Recipe.Create(ModContent.ItemType<LimeGreenSolution>(), 100)
            .AddIngredient(ItemID.DarkBlueSolution, 100)
            .AddTile(ModContent.TileType<Tiles.Catalyzer>()).Register();*/

        #endregion catalyzer
    }
}
