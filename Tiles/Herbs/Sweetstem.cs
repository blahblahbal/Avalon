using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Avalon.Common.Templates;

namespace Avalon.Tiles.Herbs;

public class Sweetstem : ModHerb
{
    public override int HerbDrop => ModContent.ItemType<Items.Material.Herbs.Sweetstem>();
    public override int SeedDrop => ModContent.ItemType<SweetstemSeeds>();
    public override int[] ValidAnchorTiles => new int[]
    {
        //ModContent.TileType<Nest>(),
        TileID.Hive
    };
    public override LocalizedText MapName => this.GetLocalization("MapEntry");
    public override Color MapColor => new Color(216, 161, 50);
    public override int Dust => DustID.Hive;
    public override void SetStaticDefaults()
    {
        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;
        base.SetStaticDefaults();
    }
    public override void RandomUpdate(int i, int j)
    {
        Tile tile = Framing.GetTileSafely(i, j); //Safe way of getting a tile instance
        PlantStage stage = GetStage(i, j); //The current stage of the herb

        //Only grow to the next stage if there is a next stage. We dont want our tile turning pink!
        if (stage != PlantStage.Blooming && Main.rand.NextBool(13))
        {
            //Increase the x frame to change the stage
            tile.TileFrameX += 18;

            //If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
    }
}
