using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Avalon.Common.Templates;
using Avalon.Tiles.Contagion;

namespace Avalon.Tiles.Herbs;

public class Barfbush : ModHerb
{
    public override int HerbDrop => ModContent.ItemType<Items.Material.Herbs.Barfbush>();
    public override int SeedDrop => ModContent.ItemType<BarfbushSeeds>();
    public override int[] ValidAnchorTiles => new int[]
    {
        ModContent.TileType<Ickgrass>(),
        ModContent.TileType<Chunkstone>()
    };
    public override LocalizedText MapName => this.GetLocalization("MapEntry");
    public override Color MapColor => new Color(0, 200, 50);
    public override int Dust => DustID.Grass;
    public override void SetStaticDefaults()
    {
        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;
        base.SetStaticDefaults();
    }
    public override void RandomUpdate(int i, int j)
    {
        Tile tile = Framing.GetTileSafely(i, j); //Safe way of getting a tile instance
        PlantStage stage = GetStage(i, j); //The current stage of the herb

        if (stage == PlantStage.Planted && Main.rand.NextBool(12))
        {
            tile.TileFrameX += 18;
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
        if (stage == PlantStage.Mature)
        {
            if (Main.bloodMoon || Main.moonPhase == 0)
            {
                tile.TileFrameX = 36;
            }
            else tile.TileFrameX = 18;
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
        if (stage == PlantStage.Blooming && (!Main.bloodMoon || Main.moonPhase != 0))
        {
            tile.TileFrameX = 18;
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }

        ////Only grow to the next stage if there is a next stage. We dont want our tile turning pink!
        //if (stage != PlantStage.Grown)
        //{
        //    //Increase the x frame to change the stage
        //    tile.frameX += FrameWidth;

        //    //If in multiplayer, sync the frame change
        //    if (Main.netMode != NetmodeID.SinglePlayer)
        //        NetMessage.SendTileSquare(-1, i, j, 1);
        //}
    }
}
